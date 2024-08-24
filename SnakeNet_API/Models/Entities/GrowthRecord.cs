namespace SnakeNet_API.Models.Entities
{
	public class GrowthRecord
	{
        public string Id { get; set; }
        public int Lenght { get; set; }
        public int Weight { get; set; }
        public DateTime Date { get; set; }
		public Snake Snake { get; set; }
	}
}
