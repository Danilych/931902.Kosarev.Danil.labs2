using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web6.Data;
using Web6.Models;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Web6.Controllers
{
    public class AccountController : Controller
    {
        private readonly Web6Context _context;

        public AccountController(Web6Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel regModel)
        {
            if(ModelState.IsValid)
            {
                //Check password strength
                string numbers = "1234567890";
                string smallLetters = "abcdefghijklmnopqrstuvwxyz";
                string bigLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                bool hasNumber = false;
                bool hasSmallLetter = false;
                bool hasBigLetter = false;

                for(int i = 0; i < regModel.Password.Length; i++)
                {
                    if(!hasNumber)
                        for(int j = 0; j<numbers.Length; j++)
                            if(regModel.Password[i] == numbers[j])
                            {
                                hasNumber = true;
                                break;
                            }

                    if (!hasSmallLetter)
                        for (int j = 0; j < smallLetters.Length; j++)
                            if (regModel.Password[i] == smallLetters[j])
                            {
                                hasSmallLetter = true;
                                break;
                            }

                    if (!hasBigLetter)
                        for (int j = 0; j < bigLetters.Length; j++)
                            if (regModel.Password[i] == bigLetters[j])
                            {
                                hasBigLetter = true;
                                break;
                            }
                }

                if(!hasNumber || !hasSmallLetter || !hasBigLetter)
                {
                    if(!hasNumber) ModelState.AddModelError("", "Passwords must have at least one non alphanumeric character.");
                    if(!hasSmallLetter) ModelState.AddModelError("", "Passwords must have at least one lowercase ('a'-'z').");
                    if(!hasBigLetter) ModelState.AddModelError("", "Passwords must have at least one uppercase ('A'-'Z').");

                    return View(regModel);
                }

                User user = await _context.User.FirstOrDefaultAsync(u => u.Email == regModel.Email);
                if(user == null)
                {
                    //Add user to database
                    user = new User { Email = regModel.Email, Password = regModel.Password };
                    Role userRole = await _context.Role.FirstOrDefaultAsync(r => r.Name == "user");
                    
                    if(userRole != null)                  
                        user.Role = userRole;

                    _context.User.Add(user);
                    await _context.SaveChangesAsync();

                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect email and(or) password");
                }
            }

            return View(regModel);
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginModel loginModel)
        {
            if(ModelState.IsValid)
            {
                //Seacrh user
                User user = await _context.User.Include(u=>u.Role).FirstOrDefaultAsync(u=>u.Email == loginModel.Email && u.Password == loginModel.Password);

                if(user != null)
                {
                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorrect email and(or) password");
            }

            return View(loginModel);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name),
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            //Cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
