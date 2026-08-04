using SmartPOS.Domain.Entities.Base;

namespace SmartPOS.Domain.Entities
{
    public sealed class Category : Entity
    {
        public string Name {get; private set;}
        public string? Description {get; private set;}

        private Category()
        {
            Name = string.Empty;
        }

        public Category(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }

        public void Update(string name, string? description)
        {
            Name = name;
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}