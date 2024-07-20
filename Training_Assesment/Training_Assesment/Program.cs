using Microsoft.EntityFrameworkCore;
using Training_Assesment.DBContext;
using Training_Assesment.Model;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<TrainingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Training_Assesment")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<TrainerService>();
builder.Services.AddScoped<TraineeService>();
var app = builder.Build();
//builder.Services.AddScoped<TrainerService>();
// Subscribe to the event


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
