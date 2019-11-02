using System.Collections.Generic;
using Newtonsoft.Json;

namespace DeliveryService.Models
{
    /// <summary>
    /// Тип оборудования
    /// </summary>
    public class TypeEquipment
    {
        public long Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Delivery> Deliveries { get; set; }
    }
}