namespace DeliveryService.Models
{
    /// <summary>
    /// Сущность с первичным ключом
    /// </summary>
    public interface IId
    {
        long Id { get; set; }
    }
}