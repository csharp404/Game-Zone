using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Model
{
    public class Game
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public required string Description { get; set; } 
        public decimal Price { get; set; } 
        public string? ImgName { get; set; } 
        public string? ImgPath { get; set; } 
        public required ICollection<GameDevice> Devices{ get; set; }

        public required ICollection<GameCategory> Categories{ get; set; }
    }

   
}