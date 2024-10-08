using furniro_server.Contracts;
using furniro_server.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Newtonsoft.Json;
using Supabase;
using Supabase.Interfaces;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<Supabase.Client>(provider => 
    new Supabase.Client(
        "https://eeaanrtmijertemmpium.supabase.co",
        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImVlYWFucnRtaWplcnRlbW1waXVtIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MjgyOTM4MTIsImV4cCI6MjA0Mzg2OTgxMn0.f5S2v2zXh8oG3--hM7pVaXTPFLGzIv5250wfLgCeyjE",
        new SupabaseOptions
        {
            AutoRefreshToken = true,
            AutoConnectRealtime = true
        }
    )
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    // Add more configuration as needed
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();


