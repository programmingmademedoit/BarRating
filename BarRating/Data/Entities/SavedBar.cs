namespace BarRating.Data.Entities
{
    public class SavedBar : MetadataBaseEntity
    { 
        public Bar Bar { get; set; }
        public int BarId { get; set; }


    }
}