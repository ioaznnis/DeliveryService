using System;
using System.Collections.Generic;
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
                    Name = Guid.NewGuid().ToString(),
                    Address = Guid.NewGuid().ToString(),
                    Phone = GeneratePhoneNumber(random)
                }));

                await _context.SaveChangesAsync();
            }

            if (!await _context.TypeEquipments.AnyAsync())
            {
                _context.TypeEquipments.AddRange(Enumerable.Range(1, 10).Select(x => new TypeEquipment()
                {
                    Name = Guid.NewGuid().ToString(),
                }));

                await _context.SaveChangesAsync();
            }

            if (!await _context.Deliveries.AnyAsync())
            {
                var providers = await _context.Providers.ToListAsync();
                var typeEquipments = await _context.TypeEquipments.ToListAsync();

                //Будем считать
                _context.Deliveries.AddRange(Enumerable.Range(1, 1000).Select(x => new Delivery()
                {
                    TypeEquipmentId = GetRandomEntityId(providers, random),
                    ProviderId = GetRandomEntityId(typeEquipments, random),
                    Quantity = random.Next(1000),
                    DateOf = DateTime.Today.AddDays(-random.Next(DateTime.Today.DayOfYear)),
                }));

                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Выбирает случайный Id в коллекции данных
        /// </summary>
        /// <param name="providers"></param>
        /// <param name="random"></param>
        /// <returns></returns>
        private static long GetRandomEntityId<T>(IReadOnlyList<T> providers, Random random) where T : IId
        {
            return providers[random.Next(providers.Count-1)].Id;
        }
    }
}