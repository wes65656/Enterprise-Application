using NSE.WebMvc.APP.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddIdentityConfig();

var app = builder.Build();

app.Run();