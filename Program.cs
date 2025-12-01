using Microsoft.EntityFrameworkCore;
using ProjetoMidasAPI.Data;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

internal partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Conexão com SQL Server usando ConexaoCasa
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoCasa"));
        });

        // Configuração de Controllers e JSON
        builder.Services.AddControllers()
            .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        // Configuração de Autenticação JWT
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(builder.Configuration.GetSection("ConfiguracaoToken:Chave").Value)
                    ),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        builder.Services.AddAuthorization();

        var app = builder.Build();

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
    }
}
