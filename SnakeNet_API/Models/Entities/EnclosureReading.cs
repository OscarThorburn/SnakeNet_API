using SnakeNet_API.Models.Enums;

namespace SnakeNet_API.Models.Entities
{
	public class EnclosureReading
	{
        public string Id { get; set; }
        public int Temperature { get; set; }
        public int Humidity { get; set; }
        public DateTime Date { get; set; }
        public EnclosureSide EnclosureSide { get; set; }
        public string Comment { get; set; }
		public Enclosure Enclosure { get; set; }
	}
}
