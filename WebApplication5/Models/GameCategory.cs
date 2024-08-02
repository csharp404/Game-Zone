using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Model
{
    public class GameCategory
    {
     [Key]
        public int GameId { get; set; }
        public  Game Game { get; set; }
  
        public int CategoryId { get; set; }
        public  Category Category { get; set; }

    }
}