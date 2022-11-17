using Microsoft.EntityFrameworkCore;
using BackEnd.Endpoints;
using BackEnd.Data.Context;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=ConferencePlanner.db";
builder.Services.AddDbContext<ConferencePlannerContext>(options => options.UseSqlite(connectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    setup.CustomSchemaIds(type => type.ToString());
    setup.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Conference Planner API", 
        Version = "v1" 
    });
});

builder.Services.AddHealthChecks()
    .AddDbContextCheck<ConferencePlannerContext>();

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(x => x.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setup =>
    {
        setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Conference Planner API v1");
    });

    app.UseDeveloperExceptionPage();
    app.UseDatabaseErrorPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseRouting();

app.MapSpeakerEndpoints();
app.MapAttendeeEndpoints();
app.MapSessionEndpoints();
app.MapSearchEndpoints();

app.MapHealthChecks("/health");

app.Run();
