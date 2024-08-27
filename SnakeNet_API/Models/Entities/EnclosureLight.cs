using SnakeNet_API.Models.Enums;

namespace SnakeNet_API.Models.Entities
{
	public class EnclosureLight
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public LightingType LightingType { get; set; }
        public string Manufacturer { get; set; }
        public EnclosureSide Side { get; set; }
        public int Wattage { get; set; }
        public DateTime AddedDate { get; set; }
        public bool InUse { get; set; }
        public Enclosure Enclosure { get; set; }
    }
}