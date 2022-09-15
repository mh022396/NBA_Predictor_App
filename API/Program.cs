using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options => {
    options.AddPolicy("CorsPolicy", policy => policy.AllowAnyOrigin().AllowAnyHeader().WithMethods("GET"));
});

var app = builder.Build();

if(args.Length == 1 && args[0].ToLower() == "seeddata"){
    using (var scope = app.Services.CreateScope()){
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<DataContext>();
        await Seed.SeedData(context);
    }
}
if(args.Length == 1 && args[0].ToLower() == "migrations"){
    using (var scope = app.Services.CreateScope()){
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<DataContext>();
        try{
            context.Database.Migrate();
        }catch{

        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
