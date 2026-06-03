using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ColorTransform.Tests;

public class IndexPageTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public IndexPageTests(WebApplicationFactory<Program> factory)
    {
        // Tests will run with the ASPNETCORE_ENVIRONMENT set to "Development"
        // by default, so we override it in the constructor so that we don't
        // pollute the test environment with any sample test data.
        _client = factory
            .WithWebHostBuilder(builder => builder.UseEnvironment("Testing"))
            .CreateClient();
    }


    /*
    This test creates a server, runs the ColorTransform.Web application, and
    then sends a POST request to the / endpoint with the "invert" transform
    type. This mimics what happens when the user submits the form via a button click,
    but there is no browser involved in this test. It is not simulating the entire environment,
    as with an E2E test.
    */
    [Fact]
    public async Task post_with_invert_returns_transformed_color_string_in_returned_html()
    {
        // The "_client" object is an HttpClient that can be used to send HTTP requests to the test server.
        // Here we get the initial "index" page from the in-memory test server.
        var getResponse = await _client.GetAsync("/");

        // Get the HTML response body as a string.
        var html = await getResponse.Content.ReadAsStringAsync();

        // The form submission needs a token to prevent CSRF attacks, so we parse it from the HTML.
        var token = ParseAntiforgeryToken(html);

        // Here we assemble the form variables for submission.
        var form = new Dictionary<string, string>
        {
            ["TransformType"] = "invert",
            ["InputColor"] = "#336699",
            ["__RequestVerificationToken"] = token,
        };

        // The "PostAsync" method sends the form variables to the server.
        // This is what the browser would do, but we're doing it programmatically.
        var postResponse = await _client.PostAsync("/", new FormUrlEncodedContent(form));

        // Get the HTML response body as a string.
        var resultHtml = await postResponse.Content.ReadAsStringAsync();

        // Finally, assert that the result HTML string contains the transformed color.
        Assert.Contains("#CC9966", resultHtml, StringComparison.OrdinalIgnoreCase);
    }

    /*
    This is simply a helper method to search for and return the needed token's
    key-value pair in the HTML.
    */
    private static string ParseAntiforgeryToken(string html)
    {
        const string name = "name=\"__RequestVerificationToken\"";
        var nameIndex = html.IndexOf(name, StringComparison.Ordinal);
        Assert.True(nameIndex >= 0, "Antiforgery token field not found in page HTML.");

        const string valuePrefix = "value=\"";
        var valueIndex = html.IndexOf(valuePrefix, nameIndex, StringComparison.Ordinal);
        Assert.True(valueIndex >= 0, "Antiforgery token value not found.");

        valueIndex += valuePrefix.Length;
        var valueEnd = html.IndexOf('"', valueIndex);
        Assert.True(valueEnd > valueIndex, "Antiforgery token value was not terminated.");

        return html[valueIndex..valueEnd];
    }
}