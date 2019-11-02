using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryService.Models;
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
    }
}