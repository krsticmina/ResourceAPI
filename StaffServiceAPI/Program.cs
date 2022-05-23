using Microsoft.EntityFrameworkCore;
using StaffServiceAPI.DbContexts;
using StaffServiceDAL.Services;
using StaffServiceBLL;
using StaffServiceAPI.CustomExceptionMiddleware;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using StaffServiceAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtAuthentication:Issuer"],
            ValidAudience = builder.Configuration["JwtAuthentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["JwtAuthentication:SecretForKey"]))
        };
    }
);

builder.Services.AddDbContext<StaffDatabaseContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("StaffDatabaseConnectionString"));
});


// This is for calling CreatedAtAction in my controller because it trims the 'Async' part from action names
builder.Services.AddControllersWithViews(options => { options.SuppressAsyncSuffixInActionNames = false; });

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionMiddleware();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(
    endpoints => endpoints.MapControllers()
);

app.Run();
