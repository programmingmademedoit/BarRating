
namespace BarRating.Data.Entities
{
    public class Review : MetadataBaseEntity
    {
        public int RestaurantId { get; set; }
        public Bar Restaurant { get; set; }
        public string Text {  get; set; }
        
    }
}
