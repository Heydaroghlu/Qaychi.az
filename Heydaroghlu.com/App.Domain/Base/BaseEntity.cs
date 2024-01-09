using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Base
{
    public class BaseEntity
    {
        public int Id { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string? NameOfModifier { get; set; }
    }
}
