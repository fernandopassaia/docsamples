using System;
using ModernStore.Shared.Commands;

namespace ModernStore.Domain.Commands.Inputs
{
    public class RegisterOrderItemCommand : ICommand
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
