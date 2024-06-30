using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
    }
}
