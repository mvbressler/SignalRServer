using System.Text;
using GSIMSignalRServerProject.Application.Hub;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "127.0.0.1",
            ValidAudience = "127.0.0.1",
            
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SvNhHG3PMx_ql8g_mwwX4QXa_dQlqJjjpGgSjXKrB80\n"))
        };
        
        // If you want to receive the token via query string or route parameter
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/g-connect"))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<GConnectHub>("/g-connect").RequireAuthorization();
app.MapControllers();

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (SecurityTokenExpiredException)
    {
        // Handle the expired token. For example:
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Token has expired.");
    }
});
app.Run();
