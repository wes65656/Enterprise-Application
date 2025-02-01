using NSE.WebMvc.APP.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvcConfiguration();
builder.Services.AddIdentityConfig();
builder.Services.RegisterServices();

var app = builder.Build();

app.UseMvcConfiguration(app.Environment);

app.Run();;