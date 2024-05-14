using libaryAPI.Data;
using libaryAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var _logger = new LoggerConfiguration()
.WriteTo.Console()// ghi ra console
.WriteTo.File("Logs/Book_log.txt", rollingInterval: RollingInterval.Minute) //ghi ra file lưu trong thư mục Logs
.MinimumLevel.Information()
.CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(_logger);

// Thêm các dịch vụ vào container.
builder.Services.AddControllers();

// Đăng ký các repository
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
/*builder.Services.AddScoped<IImageRepository, ImageRepository>();*/
// config identity user
builder.Services.AddIdentityCore<IdentityUser>()
.AddRoles<IdentityRole>()
.AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("Book")
.AddEntityFrameworkStores<BookAuthDbContext>()
.AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(option =>
{
	option.Password.RequireDigit = false;// Yêu cầu về password chứa ký số không?
	option.Password.RequireLowercase = false;
	option.Password.RequireNonAlphanumeric = false;
	option.Password.RequireUppercase = false;
	option.Password.RequiredLength = 6;
	option.Password.RequiredUniqueChars = 1;
});

// Cấu hình Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Kết nối đến cơ sở dữ liệu
builder.Services.AddDbContext<dbcontext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("library")));

// Kết nối đến cơ sở dữ liệu xác thực
builder.Services.AddDbContext<BookAuthDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("BookAuthenConnect")));

// Cấu hình Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
	.AddEntityFrameworkStores<BookAuthDbContext>()
	.AddDefaultTokenProviders();

// Cấu hình xác thực với JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Audience"],
		ClockSkew = TimeSpan.Zero,
		IssuerSigningKey = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
	};
});
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
	{
		Title = "Book API",
		Version = "v1"
	});
	options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new
	OpenApiSecurityScheme
	{
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = JwtBearerDefaults.AuthenticationScheme
	});
	options.AddSecurityRequirement(new OpenApiSecurityRequirement
{
{
new OpenApiSecurityScheme
{
Reference= new OpenApiReference
{
Type= ReferenceType.SecurityScheme,
Id= JwtBearerDefaults.AuthenticationScheme
},
Scheme = "Oauth2",
Name =JwtBearerDefaults.AuthenticationScheme,
In = ParameterLocation.Header
},
new List<string>()
}
});
});
var app = builder.Build();

// Cấu hình pipeline của HTTP request.
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
