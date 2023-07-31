using DataSrv.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataSrv
{
    public static class DataDI
    {
        public static void DataDependencyinjection(this IServiceCollection services)
        {
            services.AddDbContext<UserDbContext>(opt => opt.UseInMemoryDatabase("Products"));
        }

    }
}
