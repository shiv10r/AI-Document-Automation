using AI_Document_Automation_Backend.Business;
using AI_Document_Automation_Backend.Repository;
using AI_Document_Automation_Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS (allow frontend React app)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// ====== Dependency Injection for MVC layers ======
builder.Services.AddSingleton<PythonProcessorService>();                 // Python AI script service
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();    // Repository layer
builder.Services.AddScoped<IDocumentService, DocumentService>();          // Business layer

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
