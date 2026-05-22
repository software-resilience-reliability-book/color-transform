using ColorTransform.Web.ErrorLogging;

var builder = WebApplication.CreateBuilder(args);

// TODO: get this from configuration
// Show that dev path is different from prod path
var errorLogPath = Path.Combine(builder.Environment.ContentRootPath, "logs", "errors.log");
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
