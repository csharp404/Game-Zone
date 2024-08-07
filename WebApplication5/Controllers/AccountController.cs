
namespace WebApplication5.Controllers
{
    public class AccountController : Controller
    {
        public readonly UserManager<IdentityUser> UserManager;
        public readonly SignInManager<IdentityUser> SignInManager;
        public readonly RoleManager<IdentityRole> RoleManager;
        public AccountController(UserManager<IdentityUser> usermanager, SignInManager<IdentityUser> Sign,
           RoleManager<IdentityRole> RoleManager)
        {
            UserManager = usermanager;
            SignInManager = Sign;
            this.RoleManager = RoleManager;

        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var res = await SignInManager.PasswordSignInAsync(user.Email, user.Password, true, false);
                if (res.Succeeded)
                {
                    return RedirectToAction("Index", "Game");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(user);
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel reg)
        {
            var user = new IdentityUser
            {
                Email = reg.Email,
                UserName = reg.UserName,
                NormalizedUserName = reg.Name
            };
            var res = await UserManager.CreateAsync(user, reg.Password);
            if (res.Succeeded)
            {
                var re = await UserManager.AddToRoleAsync(user, "User");
                if (!re.Succeeded)
                {
                }
                else
                    return RedirectToAction("Login");
            }
            return View(reg);

        }
        public async Task<IActionResult> SignOut()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> Manage()
        {
            var curr = await UserManager.GetUserAsync(User);

            if (curr != null)
            {
                var mvm = new ManageViewModel
                {
                    Name = curr.UserName,
                    Email = curr.Email,
                    PhoneNumber = curr.PhoneNumber,


                };
                return View(mvm);
            }
            return RedirectToAction("Index", "Game");
        }
        [HttpPost]
        public async Task<IActionResult> Manage(ManageViewModel manage)
        {
            var curr = await UserManager.GetUserAsync(User);

            curr.PhoneNumber = manage.PhoneNumber;
            curr.Email = manage.Email;
            var r = await UserManager.UpdateAsync(curr);

            return RedirectToAction("Index", "Game");
        }
    }
}
