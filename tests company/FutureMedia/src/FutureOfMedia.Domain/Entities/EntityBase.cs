using FluentValidator;
using System;

namespace FutureOfMedia.Domain.Entities
{
    //Note: I'm using a package called "FluentValidator" that allow to Add Domain Notifications. So all my validations will
    //be at my Domain, based on Contracts and i will have methods to add, get, see if my Entitie is valid. You'll see.

    public abstract class EntityBase : Notifiable
    {
        public EntityBase()
        {
            CreatedIn = DateTime.Now;
            UpdatedIn = DateTime.Now;
        }

        public DateTime CreatedIn { get; set; }
        public DateTime UpdatedIn { get; set; }
    }
}