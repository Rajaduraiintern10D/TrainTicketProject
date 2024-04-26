using TicketBookingProjectUI.ConstantFile;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//session service
builder.Services.AddSession();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ApiService>();

// Register IHttpClientFactory
builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();//Enable session middleware
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Sign}/{action=Index}/{id?}");

app.Run();
