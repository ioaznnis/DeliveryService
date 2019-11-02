using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace DeliveryService.Models
{
    /// <summary>
    /// Поставка
    /// </summary>
    public class Delivery
    {
        public long Id { get; set; }

        /// <summary>
        /// Ссылка на тип оборудования
        /// </summary>
        public long TypeEquipmentId { get; set; }

        /// <summary>
        /// Ссылка на поставщика
        /// </summary>
        public long ProviderId { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public long Quantity { get; set; }

        /// <summary>
        /// Дата поставки
        /// </summary>
        [Column(TypeName = "Date")]
        public DateTime DateOf { get; set; }

        [JsonIgnore]
        public TypeEquipment TypeEquipment { get; set; }

        [JsonIgnore]
        public Provider Provider { get; set; }
    }
}