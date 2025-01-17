using NSE.WebMvc.APP.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvcConfiguration();
builder.Services.AddIdentityConfig();

var app = builder.Build();

app.UseMvcConfiguration(app.Environment);

app.Run();;