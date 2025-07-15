using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using NZWalkAPICore8.Data;
using NZWalkAPICore8.Mappings;
using NZWalkAPICore8.Repositaries;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using Serilog;
using NZWalkAPICore8.Middlewares;
using NZWalkAPICore8;
//using Asp.Versioning;
//using Asp.Versioning.ApiExplorer;
//using Microsoft.AspNetCore.Mvc.ApiExplorer;
//using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
//using Asp.Versioning.Routing;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Asp.Versioning;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Implemneting the SeriLog 
#region ImplementingSeriLog
var logger = new LoggerConfiguration()
    .WriteTo.Console() //Write the Log in Console
    .WriteTo.File("Logs/NZWalks_Log.txt", rollingInterval: RollingInterval.Minute)
    .MinimumLevel.Information()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
#endregion

builder.Services.AddControllers();
builder.Services.AddApiVersioning();

// Add API versioning services to DI
//builder.Services.AddApiVersioning(options =>
//{
//    // This allows the API to use a default version if the client does not specify one.
//    options.AssumeDefaultVersionWhenUnspecified = true;
//    // This sets the default version (here, 1.0).
//    options.DefaultApiVersion = new ApiVersion(1, 0);
//    // This adds headers in the response informing clients about all the supported API versions.
//    ////options.ReportApiVersions = true;
//    // This tells the framework to use the query string parameter api-version for versioning
//    options.ApiVersionReader = new QueryStringApiVersionReader("api-version");
//});
//builder.Services.AddApiVersioning();
// Add API versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));
})
.AddMvc() // This is needed for controllers
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});



// Add versioned API explorer (for Swagger)
//object value = builder.Services.AddVersionedApiExplorer(options =>
//{
//    options.GroupNameFormat = "'v'VVV";
//    options.SubstituteApiVersionInUrl = true;
//});
// Register constraint type in routing
//builder.Services.Configure<RouteOptions>(options =>
//{
//    options.ConstraintMap.Add("apiVersion", typeof(Asp.Versioning.Routing.ApiVersionRouteConstraint));
//});

//builder.Services.AddVersionedApiExplorer();

//builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
// Register Swagger after versioning services
builder.Services.AddSwaggerGen();


builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();



#region AddDbcontext

builder.Services.AddDbContext<NZWalkDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalkConnection"))
);

builder.Services.AddDbContext<NZWalkAuthDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalkAuthConnection"))
);
#endregion

//Inject the Repositry

builder.Services.AddScoped<IRegionRepositry, SQLRegionRepositary>();
builder.Services.AddScoped<IWalkRepositry, SQLWalkRepositry>();
builder.Services.AddScoped<ITokenRepositary, TokenRepositary>();
builder.Services.AddScoped<IImageUploadRepositry, ImageUploadRepositry>();


//Inject the Mapping
builder.Services.AddAutoMapper(typeof(AutoMappingProfile));

//Setting the Identity
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
    .AddEntityFrameworkStores<NZWalkAuthDBContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
}
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option => option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issurer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"].ToString()))
    });




var app = builder.Build();
//var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

//Used For Static File Handeler
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
});
app.MapControllers();

app.Run();
