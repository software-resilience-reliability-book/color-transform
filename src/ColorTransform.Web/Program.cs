using ColorTransform.Web.ErrorLogging;

var builder = WebApplication.CreateBuilder(args);

var errorLogRelativePath = builder.Configuration["ErrorLogging:FilePath"]
    ?? throw new InvalidOperationException("ErrorLogging:FilePath is not configured.");
var errorLogPath = Path.IsPathRooted(errorLogRelativePath)
    ? errorLogRelativePath
    : Path.Combine(builder.Environment.ContentRootPath, errorLogRelativePath);

builder.Services.AddSingleton<IErrorLog>(_ => new FileErrorLog(errorLogPath));

builder.Services.AddRazorPages();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();

// Make Program visible to the integration tests so that they can instantiate it.
// Otherwise the Program class will be scoped to "internal".
public partial class Program { }