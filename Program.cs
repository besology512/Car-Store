using Car_Store.models;
using Car_Store.Services.EmailService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<DB>();//
builder.Services.AddSingleton<customer>();//
builder.Services.AddSingleton<Client>();//
builder.Services.AddSingleton<vehicle>();//
builder.Services.AddSingleton<new_vehicle>();//
builder.Services.AddSingleton<used_vehicle>();//
builder.Services.AddSingleton<ShowRoom>();//
builder.Services.AddSingleton<product>();//
builder.Services.AddSingleton<WishCart>();//
builder.Services.AddSession();
builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
