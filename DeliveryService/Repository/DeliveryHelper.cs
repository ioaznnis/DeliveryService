using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryService.Models;

namespace DeliveryService.Repository
{
    /// <summary>
    /// Начало слоя репозитория, содержащего одинаковые методы работы с данными
    /// </summary>
    public static class DeliveryHelper
    {
        /// <summary>
        /// Получение поставок за период
        /// </summary>
        /// <remarks>Будем сравнивать даты, а не даты с секундами, так как в БД храним - дату без времени</remarks>
        /// <param name="deliveries"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static IQueryable<Delivery> GetDelivery(this IQueryable<Delivery> deliveries, DateTime from, DateTime to)
        {
            return deliveries.Where(x => from.Date <= x.DateOf && x.DateOf <= to.Date);
        }
    }
}