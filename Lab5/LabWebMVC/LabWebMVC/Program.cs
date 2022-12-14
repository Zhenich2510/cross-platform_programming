using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

// Add services to the container.
builder.Services.AddAuthentication(configureOptions =>
{
    configureOptions.DefaultScheme = "Cookies";
    configureOptions.DefaultChallengeScheme = "oidc";
}).AddCookie("Cookies")
.AddOpenIdConnect("oidc", options =>
{
    options.Authority = "http://192.168.88.212:5001";
    options.ClientId = "mvc";
    options.ClientSecret = "secret";
    options.ResponseType = "code";
    options.SaveTokens = true;
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("phone"); 
    options.RequireHttpsMetadata= false;
    options.GetClaimsFromUserInfoEndpoint = true;
});

 

var app = builder.Build();
app.Urls.Add("http://0.0.0.0:80");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
   // app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//.RequireAuthorization();

app.Run();
