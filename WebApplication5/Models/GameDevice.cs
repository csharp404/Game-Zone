using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace WebApplication5.Model
{
    public class GameDevice
    {
        [Key]
        public int GameId { get; set; }
        public  Game Game { get; set; }

        public int DeviceId { get; set; }
        public  Device Device { get; set; }

    }
}