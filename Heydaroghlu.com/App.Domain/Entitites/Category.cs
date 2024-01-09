using App.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entitites
{
    public class Category:BaseEntity
    { 
        public string Name { get; set; }  
        public string Icon { get; set; }
        public List<Store> Stores { get; set; } 
    }
}
