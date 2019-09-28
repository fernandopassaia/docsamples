using FluentValidator;
using System;

namespace ModernStore.Shared.Entities
{
    public abstract class Entity : Notifiable
    {
        protected Entity()
        {
            CreatedIn = DateTime.Now;
            UpdatedIn = DateTime.Now;
        }

        public DateTime CreatedIn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedIn { get; set; }
        public int UpdatedBy { get; set; }
    }
}