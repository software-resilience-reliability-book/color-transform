using System.Diagnostics;
using Microsoft.Playwright;

namespace ColorTransform.Tests.E2E;

public sealed class ColorTransformWebApplication : IAsyncLifetime
{
    private Process? _process;

    public string BaseUrl { get; } = "http://127.0.0.1:5157";

    public async Task InitializeAsync()
    {
        var projectPath = Path.GetFullPath(Path.Combine(
            AppContext.BaseDirectory,
            "..", "..", "..", "..", "..",
            "src", "ColorTransform.Web", "ColorTransform.Web.csproj"));

        var startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"run --project \"{projectPath}\" --urls {BaseUrl} --no-launch-profile",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
        };
        startInfo.Environment["ASPNETCORE_ENVIRONMENT"] = "Testing";

        _process = Process.Start(startInfo)
            ?? throw new InvalidOperationException("Failed to start ColorTransform.Web.");

        _process.BeginOutputReadLine();
        _process.BeginErrorReadLine();

        using var http = new HttpClient { Timeout = TimeSpan.FromSeconds(2) };
        for (var attempt = 0; attempt < 60; attempt++)
        {
            try
            {
                using var response = await http.GetAsync($"{BaseUrl}/");
                if (response.IsSuccessStatusCode)
                    return;
            }
            catch (HttpRequestException)
            {
            }
            catch (TaskCanceledException)
            {
            }

            if (_process.HasExited)
                throw new InvalidOperationException(
                    $"ColorTransform.Web exited before becoming ready (exit code {_process.ExitCode}).");

            await Task.Delay(500);
        }

        throw new InvalidOperationException($"ColorTransform.Web did not respond at {BaseUrl}.");
    }

    public Task DisposeAsync()
    {
        if (_process is { HasExited: false })
        {
            _process.Kill(entireProcessTree: true);
        }

        _process?.Dispose();
        return Task.CompletedTask;
    }
}

[Trait("Category", "E2E")]
public class IndexPageTests : IClassFixture<ColorTransformWebApplication>, IAsyncLifetime
{
    private readonly ColorTransformWebApplication _webApp;
    private IPlaywright _playwright = null!;
    private IBrowser _browser = null!;
    private IBrowserContext _context = null!;
    private IPage _page = null!;

    public IndexPageTests(ColorTransformWebApplication webApp) => _webApp = webApp;

    public async Task InitializeAsync()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            ExecutablePath = "/usr/bin/chromium-browser",
            Headless = true
        });
        _context = await _browser.NewContextAsync();
        _page = await _context.NewPageAsync();
    }

    public async Task DisposeAsync()
    {
        await _page.CloseAsync();
        await _context.CloseAsync();
        await _browser.CloseAsync();
        _playwright.Dispose();
    }

    /*
    This test runs the real web application in a separate process, drives the
    page through a headless browser, and checks what the user sees in the DOM.
    Unlike the integration test, it uses an actual browser to simulate the
    user's actions. Even though you don't see this browser, it is running on the
    machine.
    */
    [Fact]
    public async Task user_applies_invert_and_sees_transformed_color_in_output_preview()
    {
        await _page.GotoAsync($"{_webApp.BaseUrl}/");

        await _page.SelectOptionAsync("select[name='TransformType']", "invert");
        await _page.Locator("input[name='InputColor']").FillAsync("#336699");
        await _page.GetByRole(AriaRole.Button, new() { Name = "Apply transform" }).ClickAsync();

        var outputPreview = _page.Locator(".output-color-preview");
        var outputValue = await outputPreview.GetAttributeAsync("value");
        Assert.Equal("#cc9966", outputValue, StringComparer.OrdinalIgnoreCase);
    }
}
