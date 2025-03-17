using Microsoft.EntityFrameworkCore;
using Parduotuve.Components;
using Parduotuve.Data;
using Parduotuve.Data.Repositories;
using Parduotuve.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("EmployeeDB");
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient<SkinSeeder>();
builder.Services.AddDbContext<StoreDataContext>();
builder.Services.AddScoped<ISkinRepository, SkinRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IChromaRepository, ChromaRepository>();
//builder.Services.AddScoped<Shopping_Cart_Service>();
builder.Services.AddSingleton<Shopping_Cart_Service>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
var app = builder.Build();
app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<SkinSeeder>();
    await seeder.SeedSkinsAsync();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
