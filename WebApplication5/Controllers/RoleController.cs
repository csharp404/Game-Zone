using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.ViewModel;

namespace WebApplication5.Controllers
{
    [Authorize("ADMIN")]
    public class RoleController : Controller
    {
        public RoleManager<IdentityRole> RoleManager;
        public RoleController(RoleManager<IdentityRole> role)
        {
            this.RoleManager = role;
        }


        public IActionResult Index()
        {
            var data = RoleManager.Roles.AsNoTracking().Select(x=>new RoleViewModel { ID=x.Id,Name=x.Name}).OrderBy(X=>X.Name).ToList();
            return View(data);
        }
      
        
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel role) {
            var res = await RoleManager.RoleExistsAsync(role.Name);
            if (res)
            {
                ModelState.AddModelError("Name", "the role already exist !");
            }
            var ress = await RoleManager.CreateAsync(new IdentityRole { Name=role.Name});
            if (ress.Succeeded) { 
            return RedirectToAction("Index",RoleManager.Roles.ToList());
            }
            return View("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id,RoleViewModel role)
        {
            var res = await RoleManager.FindByIdAsync(id);
            res.Name= role.Name;    
            await RoleManager.UpdateAsync(res);
            return RedirectToAction("Index", RoleManager.Roles.ToList());
        }
        [HttpPost]
        public async Task<IActionResult> Delete( RoleViewModel role)
        {
            var id = role.ID.Substring(1);
            var name = role.Name.Substring(1);
            var res = await RoleManager.DeleteAsync(new IdentityRole { Id = id, Name = name });
             return RedirectToAction("Index", RoleManager.Roles.ToList());
        }


    }
}
