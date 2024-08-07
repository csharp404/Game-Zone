

namespace WebApplication5.Controllers
{
   /* [Authorize("Admin")]*/
    public class UserController : Controller
    {
        public RoleManager<IdentityRole> Rm;
        public UserManager<IdentityUser> Um ;
        public UserController(RoleManager<IdentityRole> Rm , UserManager<IdentityUser> Um)
        {
            this.Rm = Rm;
            this.Um= Um;    
        }


        public async Task<IActionResult> Index()
        {
            var users = await Um.Users.AsNoTracking().ToListAsync();

            var data = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await Um.GetRolesAsync(user);
                data.Add(new UserViewModel
                {
                    Id = user.Id,
                    Name = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }

            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Manage(string id)
        {
           var usr = await Um.FindByIdAsync(id);
            var rols = await Rm.Roles.AsNoTracking().ToListAsync();
            var data = new UserRoleViewModel
            {
                UserId = usr.Id,
                UserName = usr.UserName,
                UserRoles = rols.Select(r => new SelextionViewRoleModel
                {
                  Id=r.Id,
                  Name=r.Name,
                  IsSelected= Um.IsInRoleAsync(usr,r.Name).Result,
                }).ToList(),
            };
            return View(data);  
        }
        [HttpPost]
        public async Task<IActionResult> Manage(string id, UserRoleViewModel usr)
        {
            var cusr = await Um.FindByIdAsync(id);

            var rols = await Um.GetRolesAsync(cusr);
            var newRoles = usr.UserRoles.Where(x=>x.IsSelected==true).Select(x=>x.Name).ToList();
         if(rols.Count != 0)
            {
                await Um.RemoveFromRolesAsync(cusr, rols);

            }
                await Um.AddToRolesAsync(cusr, newRoles);
           
            
            return RedirectToAction("Index");   
        }
 

    }
}
