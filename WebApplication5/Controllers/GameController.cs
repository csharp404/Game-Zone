using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Security.Cryptography.Xml;
using WebApplication5.Model;
using WebApplication5.ViewModel;

namespace WebApplication5.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly MyDbContext db;
        private readonly IWebHostEnvironment env;

        public GameController(MyDbContext d, IWebHostEnvironment r)
        {
            db = d;
            env = r;
        }
        public async Task<IActionResult> Index()
        {
            var data = await db.games
                .Include(x => x.Devices)
                .ThenInclude(x => x.Device)
                .Include(x => x.Categories)
                .ThenInclude(x => x.Category)
                .ToListAsync();

            return View(data);
        }
        public IActionResult Create()
        {
            MyGameViewCreate game = new MyGameViewCreate()
            {
                CategoriesList = db.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList(),
                DevicesList = db.Devices.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList()
            };
            return View(game);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(MyGameViewCreate game)
        {
            var path = Path.Combine(env.WebRootPath, "Images",  game.Image.FileName);
            using (var strm = new FileStream(path, FileMode.Create))
            {
                game.Image.CopyTo(strm);
            }
            Game gg = new Game()
            {
                Name = game.Name,
                Price = game.Price,
                Description = game.Description,
                ImgName = game.Image.FileName,
                ImgPath = path,
                Devices = game.Devices.Select(x => new GameDevice { DeviceId = x }).ToList(),
                Categories = game.Categories.Select(x => new GameCategory { CategoryId = x }).ToList(),
            };
            db.Add(gg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }







        public IActionResult Edit(int id)
        {
            var data =  db.games.Where(x=>x.Id==id)
               .Include(x => x.Devices)
               .ThenInclude(x => x.Device)
               .Include(x => x.Categories)
               .ThenInclude(x => x.Category)
               .First();
            MyGameViewCreate game = new MyGameViewCreate()
            {
               Price = data.Price,
               Description = data.Description,
               Name = data.Name,
               CategoriesList = data.Categories.Select(x=>new SelectListItem { Value = x.CategoryId.ToString(),Text=x.Category.Name }),
                DevicesList = data.Devices.Select(x => new SelectListItem { Value = x.DeviceId.ToString(), Text = x.Device.Name }),
            };
            return View("Create",game);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(MyGameViewCreate game)
        {
            var path = Path.Combine(env.WebRootPath, "Images", game.Image.FileName);
            using (var strm = new FileStream(path, FileMode.Create))
            {
                game.Image.CopyTo(strm);
            }
            Game gg = new Game()
            {
                Name = game.Name,
                Price = game.Price,
                Description = game.Description,
                ImgName = game.Image.FileName,
                ImgPath = path,
                Devices = game.Devices.Select(x => new GameDevice { DeviceId = x }).ToList(),
                Categories = game.Categories.Select(x => new GameCategory { CategoryId = x }).ToList(),
            };
            db.Update(gg);
            db.SaveChanges();
            return RedirectToAction ("Index");
        }
    }
}


