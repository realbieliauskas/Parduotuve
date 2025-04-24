using MudBlazor.Services;
using Parduotuve.Components;
using Parduotuve.Data;
using Parduotuve.Data.Repositories;
using Parduotuve.Services;
using Stripe;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

string? connectionString = builder.Configuration.GetConnectionString("EmployeeDB");
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient<SkinSeeder>();
builder.Services.AddDbContext<StoreDataContext>();
builder.Services.AddScoped<ISkinRepository, SkinRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IChromaRepository, ChromaRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
//builder.Services.AddScoped<Shopping_Cart_Service>();
builder.Services.AddSingleton<ShoppingCartService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(30); });
builder.Services.AddMudServices();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
WebApplication? app = builder.Build();
app.UseSession();

app.MapControllers();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (IServiceScope? scope = app.Services.CreateScope())
{
    SkinSeeder? seeder = scope.ServiceProvider.GetRequiredService<SkinSeeder>();
    await seeder.SeedSkinsAsync();
}

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API"); });

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
StripeConfiguration.ApiKey = app.Configuration.GetValue<string>("StripeAPIKey");

app.Run();