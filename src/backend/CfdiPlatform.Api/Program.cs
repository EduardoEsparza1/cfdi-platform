var builder = WebApplication.CreateBuilder(args);

// Register application services.
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Health check endpoint.
app.MapGet("/api/health", () =>
{
    return Results.Ok(new
    {
        Status = "Healthy",
        Service = "CfdiPlatform.Api",
        Timestamp = DateTimeOffset.UtcNow
    });
})
.WithName("GetHealth")
.WithTags("Health");

app.Run();