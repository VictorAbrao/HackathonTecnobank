using Hackathon.Api;
using Hackathon.AppService;
using Hackathon.Infra.Adapters;
using Hackathon.Infra.ChatGPT;
using Hackathon.Infra.Repository;
using Hackathon.Infra.Jobs;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediator();
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddJobs();
builder.Services.AddAdaptersDetrans();
builder.Services.AddAdapterIAChatGPT();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
