using Hackathon.Api;
using Hackathon.AppService;
using Hackathon.Infra.Adapters;
using Hackathon.Infra.ChatGPT;
using Hackathon.Infra.Repository;
using Hackathon.Infra.Jobs;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddFactories();
builder.Services.AddAdapters();
builder.Services.AddServices();
builder.Services.AddUnitOfWork();
builder.Services.AddRepositories();
builder.Services.AddJobs(builder.Configuration);
builder.Services.AddAdaptersDetrans();
builder.Services.AddAdapterIAChatGPT();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureJobs();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
