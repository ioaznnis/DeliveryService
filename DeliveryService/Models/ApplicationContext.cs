using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Models
{
    /// <summary>
    /// Работа с БД
    /// </summary>
    public class ApplicationContext : DbContext
    {
        // ReSharper disable once SuggestBaseTypeForParameter
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Использовать базовый тип <see cref="DbContextOptions"/> не рекомендуется:
        /// https://docs.microsoft.com/ru-ru/ef/core/miscellaneous/configuring-dbcontext
        /// </remarks>
        /// <param name="options"></param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Provider> Providers { get; set; }
        public DbSet<TypeEquipment> TypeEquipments { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
    }
}