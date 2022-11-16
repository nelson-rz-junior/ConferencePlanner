using FrontEnd.Services;
using Microsoft.EntityFrameworkCore;
using FrontEnd.Data;
using FrontEnd.Areas.Identity;
using FrontEnd.Middleware;
using QRCoder;
using FrontEnd.HealthChecks;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("IdentityContextConnection") ?? "Identity.db";

builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<User>()
    .AddEntityFrameworkStores<IdentityContext>()
    .AddClaimsPrincipalFactory<ClaimsPrincipalFactory>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
    {
        policy.RequireAuthenticatedUser().RequireIsAdminClaim();
    });
});

builder.Services.AddAuthentication()
    .AddMicrosoftAccount(microsoftOptions =>
    {
        microsoftOptions.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"];
        microsoftOptions.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"];
        microsoftOptions.AccessDeniedPath = "/Identity/Account/AccessDenied";
    });


builder.Services.AddSingleton<IAdminService, AdminService>();

builder.Services.AddSingleton(new QRCodeService(new QRCodeGenerator()));

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Admin", "Admin");
})
.AddRazorRuntimeCompilation();

builder.Services.AddHttpClient<IApiClient, ApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrl"]);
});

builder.Services.AddHealthChecks()
    .AddCheck<BackendHealthCheck>("backend")
    .AddDbContextCheck<IdentityContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseDatabaseErrorPage();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<RequireLoginMiddleware>();

app.MapHealthChecks("/health");

app.MapRazorPages();

app.Run();
