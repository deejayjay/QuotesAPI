using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Okta.AspNetCore;
using QuotesAPI.Data;
using QuotesAPI.Repository;

var builder = WebApplication.CreateBuilder(args);
IdentityModelEventSource.ShowPII = true;

// Add services to the container.
builder.Services.AddDbContext<QuotesDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("QuotesAPIDb")));
builder.Services.AddScoped<IQuotesService, QuotesService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", builder => builder.AllowAnyOrigin()
                                                        .AllowAnyMethod()
                                                        .AllowAnyHeader());
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = OktaDefaults.ApiAuthenticationScheme;
    options.DefaultChallengeScheme = OktaDefaults.ApiAuthenticationScheme;
    options.DefaultSignInScheme = OktaDefaults.ApiAuthenticationScheme;
})
    .AddOktaWebApi(new OktaWebApiOptions()
    {
        OktaDomain = builder.Configuration["Okta:OktaDomain"],
        AuthorizationServerId = builder.Configuration["Okta:AuthorizationServerId"],
        Audience = builder.Configuration["Okta:Audience"],
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyCorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
