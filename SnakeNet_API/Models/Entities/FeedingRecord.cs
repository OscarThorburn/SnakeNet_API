using SnakeNet_API.Models.Enums;

namespace SnakeNet_API.Models.Entities
{
	public class FeedingRecord
	{
        public string Id { get; set; }
        public int FeederWeight { get; set; }
        public Feeder Feeder { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public Snake Snake { get; set; }

    }
}
