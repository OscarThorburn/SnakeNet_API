using SnakeNet_API.Models.Enums;

namespace SnakeNet_API.Models.Entities
{
    public class Snake
	{
		public string Id { get; set; }
        public string Name { get; set; }
        public Sex Sex { get; set; }
        public Enclosure Enclosure { get; set; }
    }
}
