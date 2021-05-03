using Core.Entities;

namespace Entities.Concrete
{
    public class Example : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}