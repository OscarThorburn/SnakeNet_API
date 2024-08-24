namespace SnakeNet_API.Models.Entities
{
	public class Enclosure
	{
        public string Id { get; set; }
        public int Lenght { get; set; }
        public int Height { get; set; }
        public int Depth { get; set; }
		public Snake Snake { get; set; }
	}
}
