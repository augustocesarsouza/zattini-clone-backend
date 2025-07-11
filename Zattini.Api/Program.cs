
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using Zattini.Infra.IoC;

ValidatorOptions.Global.LanguageManager.Culture = System.Globalization.CultureInfo.InvariantCulture;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<ICurrentUser, CurrentUser>();
//builder.Services.AddScoped<IBaseController, BaseController>();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

builder.Services.AddMvc().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

});

var frontEndUrl = Environment.GetEnvironmentVariable("FRONTEND_URL") ?? builder.Configuration["FRONTEND:URL"];

if (frontEndUrl != null)
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolity", builder =>
        {
            builder.WithOrigins(frontEndUrl)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
    });
}

var keyJwtBearerSecret = Environment.GetEnvironmentVariable("KEY_JWT") ?? builder.Configuration["Key:Jwt"];
if (keyJwtBearerSecret != null)
{
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyJwtBearerSecret));

    builder.Services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            IssuerSigningKey = key,
            ValidateAudience = false,
            ValidateIssuer = false
        };
    });
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddServices(builder.Configuration);
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors("CorsPolity");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DataHelper.ManageDataAsync(services);
}

app.Run();

