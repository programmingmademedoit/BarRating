
namespace BarRating.Data.Entities
{
    public class Review : MetadataBaseEntity
    {
        public int BarId { get; set; }
        public Bar Bar { get; set; }
        public string Text {  get; set; }
        
    }
}
