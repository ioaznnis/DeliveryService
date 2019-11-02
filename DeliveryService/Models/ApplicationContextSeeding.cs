using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Models
{
    /// <summary>
    /// Класс первоначального заполнения данных в БД
    /// </summary>
    public class ApplicationContextSeeding
    {
        private readonly ApplicationContext _context;

        public ApplicationContextSeeding(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Создает произвольный номер в формате +79xx-xxx-xxxx
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        private static string GeneratePhoneNumber(Random random)
        {
            string GenSequence(int length)
            {
                var s = "";
                for (var i = 0; i < length; i++)
                {
                    s += random.Next(0, 9);
                }

                return s;
            }

            return $"+79{GenSequence(2)}-{GenSequence(2)}-{GenSequence(4)}";
        }

        /// <summary>
        /// Создает и инициализирует данными БД
        /// </summary>
        /// <returns></returns>
        public async Task Seed()
        {
//            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();

            var random = new Random();

            if (!await _context.Providers.AnyAsync())
            {
                _context.Providers.AddRange(Enumerable.Range(1, 10).Select(x => new Provider()
                {
                    Name = new Guid().ToString(),
                    Address = new Guid().ToString(),
                    Phone = GeneratePhoneNumber(random)
                }));

                await _context.SaveChangesAsync();
            }

            if (!await _context.TypeEquipments.AnyAsync())
            {
                _context.TypeEquipments.AddRange(Enumerable.Range(1, 10).Select(x => new TypeEquipment()
                {
                    Name = new Guid().ToString(),
                }));

                await _context.SaveChangesAsync();
            }
        }
    }
}