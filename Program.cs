using StudentManagementApp.Models;
using StudentManagementApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuration validation
var mongoDBSettings = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>()
    ?? throw new ApplicationException("MongoDBSettings configuration is missing");

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));

// Services
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<StudentInfoService>();
builder.Services.AddSingleton<EmailService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();