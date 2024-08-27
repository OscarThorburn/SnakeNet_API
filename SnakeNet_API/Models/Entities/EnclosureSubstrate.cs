using SnakeNet_API.Models.Enums;

namespace SnakeNet_API.Models.Entities
{
	public class EnclosureSubstrate
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public SubstrateType SubstrateType { get; set; }
        public string Manufacturer { get; set; }
        public int Volume { get; set; }
        public bool InUse { get; set; }
        public DateTime AddedDate { get; set; }
        public Enclosure Enclosure { get; set; }
    }
}
