using Microsoft.EntityFrameworkCore;
using Vidyano.Service;
using Vidyano.Service.PostgreSQL;
using RheaWebsiteLatest.Service;

namespace RheaWebsiteLatest;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; set; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddVidyanoPostgreSQL(Configuration);
        services.AddDbContext<RheaWebsiteLatestContext>(options =>
        {
            options.UseLazyLoadingProxies();
            options.UseNpgsql(Configuration.GetConnectionString("RheaWebsiteLatestContext"));
        });
        services.AddTransient<RequestScopeProvider<RheaWebsiteLatestContext>>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseVidyano(env, Configuration);
        UpdateDatabase(app);
    }

    private static void UpdateDatabase(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope())
        {
            using (var context = serviceScope.ServiceProvider.GetService<RheaWebsiteLatestContext>())
            {
                context.Database.Migrate();
            }
        }
    }
}