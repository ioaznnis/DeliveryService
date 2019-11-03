using System;
using System.Linq;
using System.Threading.Tasks;
using DeliveryService.Models;
using DeliveryService.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Controllers
{
    /// <summary>
    /// Controller для получения аналитических данных
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public AnalyticsController(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение информации о количестве данных в БД
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetDatabaseInfo()
        {
            var deliveries = await _context.Deliveries.LongCountAsync();
            var providers = await _context.Providers.LongCountAsync();
            var typeEquipments = await _context.TypeEquipments.LongCountAsync();

            return new JsonResult(new {deliveries, providers, typeEquipments});
        }

        /// <summary>
        /// Вернуть:
        /// <para>общее количество оборудования в Поставках за календарный период</para>
        /// <para>список Поставщиков в этих Поставках, ранжированный от большего к меньшему по количеству единиц поставленного оборудования (в %)</para>
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet("QuantityByProviders")]
        public async Task<IActionResult> GetQuantityByProviders(
            [FromQuery] int year,
            [FromQuery] int month)
        {
            if (year == default)
                return BadRequest("Необходимо указать год!");
            if (month == default)
                return BadRequest("Необходимо указать месяц!");
            if (month < 1 || month > 12)
                return BadRequest("К сожалению в году 12 месяцев!");

            var from = new DateTime(year, month, 1);
            var to = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            var providers = await _context.Deliveries
                .GetDelivery(from, to)
                .GroupBy(x => x.Provider)
                .Select(x => new {x.Key.Name, Quantity = x.Sum(delivery => delivery.Quantity)})
                .OrderByDescending(x => x.Quantity)
                .ToListAsync();

            var equipmentAmount = providers.Sum(x => x.Quantity);

            return new JsonResult(
                new
                {
                    equipmentAmount,
                    proviers = providers.Select(
                        x => new
                        {
                            x.Name,
                            percent = Math.Round(x.Quantity / (decimal) equipmentAmount * 100M, 2)
                        })
                });
        }

        /// <summary>
        /// Вернуть:
        /// <para>общее количество оборудования в Поставках определенного Поставщика за календарный период</para>
        /// <para>Вернуть список Типов оборудования в этих Поставках, ранжированный от большего к меньшему по количеству поставленного оборудования</para>
        /// </summary>
        /// <param name="providerOf">Id поставщика услуг</param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet("QuantityByEquipment/{providerOf}")]
        public async Task<IActionResult> GetQuantityByEquipment(
            [FromRoute] long providerOf,
            [FromQuery] int year,
            [FromQuery] int month)
        {
            if (year == default) return BadRequest("Необходимо указать год!");
            if (month == default) return BadRequest("Необходимо указать месяц!");
            if (month < 1 || month > 12) return BadRequest("К сожалению в году 12 месяцев!");
            if (providerOf == default) return BadRequest("Необходимо указать поставщика!");

            var from = new DateTime(year, month, 1);
            var to = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            var typeEquipments = await _context.Deliveries
                .GetDelivery(from, to)
                .Where(x => x.ProviderId == providerOf)
                .GroupBy(x => x.TypeEquipment)
                .Select(x => new {x.Key.Name, Quantity = x.Sum(delivery => delivery.Quantity)})
                .OrderByDescending(x => x.Quantity)
                .ToListAsync();

            var equipmentAmount = typeEquipments.Sum(x => x.Quantity);

            return new JsonResult(
                new
                {
                    equipmentAmount,
                    typeEquipments
                });
        }
    }
}