using ThamCoClient.Services.Products;
﻿using Auth0.AspNetCore.Authentication;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuth0WebAppAuthentication(options => {
    options.Domain = builder.Configuration["Auth:Domain"];
    options.ClientId = builder.Configuration["Auth:ClientId"];
});




// Add services to the container.
if(builder.Environment.IsDevelopment())
{
    builder.Services.AddSingleton<IProductService, ProductServiceFake>();
}
else
{
    builder.Services.AddHttpClient<IProductService, ProductService>();
}
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
