using FluentValidator;
using System;

namespace Natific.Domain.Entities
{    
    public abstract class Entity : Notifiable
    {        
        public Entity()
        {
            CreatedIn = DateTime.Now;
            UpdatedIn = DateTime.Now;
        }

        public DateTime CreatedIn { get; set; }
        public DateTime UpdatedIn { get; set; }
    }
}
