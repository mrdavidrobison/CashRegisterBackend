using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // Add CORS services
        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", builder =>
            {
                builder.WithOrigins("http://localhost:3000") // Replace with your frontend URL
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            // Production error handler middleware configuration
            // app.UseExceptionHandler("/Home/Error");
            // app.UseHsts();
        }

        // Enable CORS
        app.UseCors("AllowFrontend");

        // Other middleware configurations...

        // Default routing configuration
        app.UseRouting();

        // Authorization middleware configuration
        // app.UseAuthorization();

        // Endpoint configuration
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // Map controller endpoints
            // endpoints.MapRazorPages(); // Optionally, map Razor Pages endpoints
        });
    }
}
