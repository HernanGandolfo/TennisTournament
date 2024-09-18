using Supabase;
using Tennis.Dependencies;

var builder = WebApplication.CreateBuilder(args);

TennisDependencies core = new(builder.Configuration);
core.ConfigureService(builder.Services);
var app = builder.Build();

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

