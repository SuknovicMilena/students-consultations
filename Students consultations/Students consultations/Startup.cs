using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using StudentsConsultations.Data.EF;
using StudentsConsultations.Data.EF.Repositories;
using StudentsConsultations.Data.Interface;
using StudentsConsultations.Data.Interface.Repositories;
using StudentsConsultations.Service;
using StudentsConsultations.Service.Interfaces;

namespace Students_consultations
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(env.ContentRootPath)
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                 .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddDbContext<StudentskeKonsultacijeDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("studentskeKonsultacijeDb")));

            services.AddScoped<IDatabaseManager, DatabaseManager>();

            services.AddScoped<DatumRepository>();
            services.AddScoped<IspitRepository>();
            services.AddScoped<KonsultacijeRepository>();
            services.AddScoped<NastavnikRepository>();
            services.AddScoped<ProjekatRepository>();
            services.AddScoped<RazlogRepository>();
            services.AddScoped<StudentRepository>();
            services.AddScoped<VrstaZadatkaRepository>();
            services.AddScoped<ZadatakRepository>();
            services.AddScoped<ZavrsniRadRepository>();

            services.AddScoped<DatumService>();
            services.AddScoped<IspitService>();
            services.AddScoped<KonsultacijeService>();
            services.AddScoped<INastavnikService, NastavnikService>();
            services.AddScoped<ProjekatService>();
            services.AddScoped<RazlogService>();
            services.AddScoped<StudentService>();
            services.AddScoped<VrstaZadatkaService>();
            services.AddScoped<ZadatakService>();
            services.AddScoped<ZavrsniRadService>();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
            services.AddAutoMapper();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStatusCodePages();

            app.UseDeveloperExceptionPage();

            app.UseCors("CorsPolicy");

            app.UseMvc();
        }
    }
}
