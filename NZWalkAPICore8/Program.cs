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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("V1",new OpenApiInfo() { Title="NZ Walk API", Version="v1"});
//    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Scheme = JwtBearerDefaults.AuthenticationScheme,
//        Type = SecuritySchemeType.ApiKey

//    });
//    options.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//        new OpenApiSecurityScheme
//        {
//            Reference = new OpenApiReference
//            {
//                Type = ReferenceType.SecurityScheme,
//                Id = JwtBearerDefaults.AuthenticationScheme
//            },
//            Scheme = "Oauth2",
//            Name= JwtBearerDefaults.AuthenticationScheme,
//            In = ParameterLocation.Header,
//        },
//        new List<string>()
//        }
//    });
//});

builder.Services.AddDbContext<NZWalkDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalkConnection"))
);

builder.Services.AddDbContext<NZWalkAuthDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalkAuthConnection"))
);


//Inject the Repositry
builder.Services.AddScoped<IRegionRepositry, SQLRegionRepositary>();
builder.Services.AddScoped<IWalkRepositry, SQLWalkRepositry>();
builder.Services.AddScoped<ITokenRepositary, TokenRepositary>();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
