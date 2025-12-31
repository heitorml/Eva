using Eva.Application;
using Eva.Worker.Configurations;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddLogisticServiceBus(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

var host = builder.Build();
host.Run();
