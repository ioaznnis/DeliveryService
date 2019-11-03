using System.Collections.Generic;
using Newtonsoft.Json;

namespace DeliveryService.Models
{
    /// <summary>
    /// Поставщик
    /// </summary>
    public class Provider : IId
    {
        public long Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; }

        [JsonIgnore]
        public ICollection<Delivery> Deliveries { get; set; }
    }
}