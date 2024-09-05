using SnakeNet_API.Models.Enums;

namespace SnakeNet_API.Models.Entities
{
	public class Elimination
	{
        public string Id { get; set; }
        public EliminationType Type { get; set; }
        public DateTime Date { get; set; }
        public bool Healthy { get; set; }
        public string Comment { get; set; }
		public Snake Snake { get; set; }
	}
}
