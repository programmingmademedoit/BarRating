namespace BarRating.Data.Entities
{
	public class Bar : MetadataBaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Image {  get; set; }
		public List<Review> Reviews { get; set; } = new List<Review>();
	}
}
