namespace SmartPOS.Domain.Entities.Base
{
    public abstract class Entity
    {
        public Guid Id {get; protected set;}
        public DateTime CreatedAt {get; protected set;}
        public DateTime? UpdatedAt {get; protected set;}
        public bool IsActive {get; protected set;}

        protected Entity ()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}