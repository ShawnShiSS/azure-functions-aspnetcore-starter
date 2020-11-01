using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunctions.Core.Entities.Base
{
    public abstract class BaseEntity
    {
        public virtual string Id { get; set; }
    }
}
