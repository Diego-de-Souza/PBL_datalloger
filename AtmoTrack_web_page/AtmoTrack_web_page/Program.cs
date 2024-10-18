using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Session; // Adicione esta linha
using Microsoft.Extensions.FileProviders;

namespace AtmoTrack_web_page
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Adiciona o serviço de autenticação por cookies
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/Index";  // Caminho para a página de login
                    options.LogoutPath = "/Login/Logout"; // Caminho para logout
                    options.AccessDeniedPath = "/Home/Index"; // Página para acesso negado (opcional)
                });

            // Adiciona suporte a sessões
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Duração da sessão
                options.Cookie.HttpOnly = true; // O cookie da sessão não pode ser acessado por scripts
                options.Cookie.IsEssential = true; // O cookie é essencial para o funcionamento da aplicação
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            // Habilitar a autenticação e autorização
            app.UseAuthentication(); // Deve ser adicionado antes do Authorization
            app.UseAuthorization();

            // Habilitar sessões
            app.UseSession(); // Adicione esta linha

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
