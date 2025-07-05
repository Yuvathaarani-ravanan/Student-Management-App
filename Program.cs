var builder = WebApplication.CreateBuilder(args);

// ✅ Add services BEFORE .Build()
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDBSettings")
);

// Register your services
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<StudentInfoService>();
builder.Services.AddSingleton<EmailService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware and pipeline setup
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
