using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using StudentsConsultations;
using StudentsConsultations.Data.EF;
using StudentsConsultations.Data.EF.Repositories;
using StudentsConsultations.Data.Interface;
using StudentsConsultations.Data.Interface.Repositories;
using StudentsConsultations.Service;
using StudentsConsultations.Service.Interfaces;
using System.Text;

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
            services.AddScoped<AppSettings>();

            services.AddScoped<IDatumService, DatumService>();
            services.AddScoped<IIspitService, IspitService>();
            services.AddScoped<IKonsultacijeService, KonsultacijeService>();
            services.AddScoped<INastavnikService, NastavnikService>();
            services.AddScoped<IProjekatService, ProjekatService>();
            services.AddScoped<IRazlogService, RazlogService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IVrstaZadatkaService, VrstaZadatkaService>();
            services.AddScoped<IZadatakService, ZadatakService>();
            services.AddScoped<IZavrsniRadService, ZavrsniRadService>();

            services.AddScoped<IPDFGenerator, PDFGenerator>();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
            services.AddAutoMapper();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStatusCodePages();

            app.UseDeveloperExceptionPage();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
