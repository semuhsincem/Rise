using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rise.BusinessLayer.Abstract;
using Rise.BusinessLayer.Concrete;
using Rise.DAL;
using Rise.DAL.Abstract;
using Rise.DAL.Concrete;
using Rise.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rise.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MongoDbSettings>(options =>
            {
                options.ConnectionString = "mongodb://localhost:27017";
                options.Database = "RiseDbTest";
            });
            services.AddScoped<IPersonService, PersonManager>();
            services.AddScoped<IPersonDal, PersonMongoDbDal>();
            services.AddScoped<IPersonDetailsDal, PersonDetailsMongoDbDal>();
            services.AddScoped<IReportService, ReportManager>();
            services.AddScoped<IReportDal, ReportMongoDbDal>();
        }
    }
}