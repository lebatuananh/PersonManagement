using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace PersonManagement.WebAPI
{
    using System.IO;
    using System.Reflection;

    using AutoMapper;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using PesonManagement.Application.Implementation;
    using PesonManagement.Application.Interface;
    using PesonManagement.Data;
    using PesonManagement.Data.Entity;
    using PesonManagement.Data.Implementation;
    using PesonManagement.Data.Interface;

    using Swashbuckle.AspNetCore.Swagger;

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
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    o => o.MigrationsAssembly("PersonManagement")));
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
                {
                    // Password settings
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;

                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                    options.Lockout.MaxFailedAccessAttempts = 10;

                    // User settings
                    options.User.RequireUniqueEmail = true;
                });
            services.AddAutoMapper();
            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(
                sp => new Mapper
                    (sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService)
            );


            // Register For DI
            services.AddScoped<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddScoped<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>();

            services.AddTransient(typeof(IUnitOfWork), typeof(EFUnitOfWork));
            services.AddTransient(typeof(IRepository<,>), typeof(EFRepository<,>));

            //services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IFunctionService, FunctionService>();
            services.AddTransient<IRoleService, RoleService>();


            //Config Swagger Test
            services.AddSwaggerGen(s =>
                {
                    s.SwaggerDoc("v1", new Info
                    {
                        Version = "v1",
                        Title = "Authentication Project",
                        Description = "Authentication API Swagger surface",
                        Contact = new Contact { Name = "LeBaTuanAnh", Email = "tuananh300496@gmail.com" },
                        License = new License { Name = "MIT", Url = "https://github.com/mramra3004/Example-CSharp" }
                    });
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    s.IncludeXmlComments(xmlPath);
                });

            services.AddCors(o => o.AddPolicy("MRACorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                }));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseCors("MRACorsPolicy");
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwaggerUI(s =>
                {
                    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Project API v1.1");
                });
        }
    }
}