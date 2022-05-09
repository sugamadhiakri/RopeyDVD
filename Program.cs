using Microsoft.EntityFrameworkCore;
using RopeyDVD.Data;
using Microsoft.AspNetCore.Identity;
using RopeyDVD.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("dbConnection");;

builder.Services.AddDbContext<RopeyDVDContext>(options =>
    options.UseSqlServer(connectionString));;

builder.Services.AddDefaultIdentity<RopeyDVDUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<RopeyDVDContext>();;

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Authorization

AddAuthorizationPolicies(builder.Services);

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

void AddAuthorizationPolicies(IServiceCollection services)
{
    services.AddAuthorization(options =>
    {

        options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Administrator"));
        options.AddPolicy("AssistantOrAdmin", policy => policy.RequireAuthenticatedUser());
        
    });
}