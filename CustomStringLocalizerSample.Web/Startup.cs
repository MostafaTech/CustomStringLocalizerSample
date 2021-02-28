using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace CustomStringLocalizerSample.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //CultureInfo[] supportedCultures = new CultureInfo[] { new CultureInfo("en-US"), new CultureInfo("fa-IR") };

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //services.AddLocalization();
            //services.Configure<RequestLocalizationOptions>(options =>
            //{
            //    options.DefaultRequestCulture = new RequestCulture("en-US", "en-US");
            //    options.SupportedCultures = supportedCultures;
            //    options.SupportedUICultures = supportedCultures;
            //});

            services.Configure<LocalizationOptions>(Configuration.GetSection("Localization"));
            services.AddTransient<ILocalizationService, LocalizationService>();
            services.AddTransient<IStringLocalizer, Localization.CustomStringLocalizer>();
            services.AddTransient<IStringLocalizerFactory, Localization.CustomLocalizerFactory>();
            services.AddTransient<IValidationMetadataProvider, Localization.LocalizedValidationMetadataProvider>();
            services.AddOptions<MvcOptions>()
                .Configure<IValidationMetadataProvider>((options, provider) =>
                {
                    options.ModelMetadataDetailsProviders.Add(provider);
                });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddDataAnnotationsLocalization(options =>
                    options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(type))
                .AddViewLocalization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            //app.UseRequestLocalization(new RequestLocalizationOptions()
            //{
            //    DefaultRequestCulture = new RequestCulture(new CultureInfo("en-US")),
            //    SupportedCultures = supportedCultures,
            //    SupportedUICultures = supportedCultures,
            //});

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
