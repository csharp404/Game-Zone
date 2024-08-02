using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Model
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<GameCategory>? Games { get; set; }
    }
}