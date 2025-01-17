﻿using FullMamba.DTOs;
using FullMamba_Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FullMamba.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = new AppUser()
            {
                FullName = registerDTO.Name,
                UserName = registerDTO.UserName,
                Email = registerDTO.Email

            };
            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }

            await _userManager.AddToRoleAsync(user, "Admin");

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> CreateRole()
        {
            IdentityRole role1 = new IdentityRole("Admin");
            IdentityRole role2 = new IdentityRole("Member");

            await _roleManager.CreateAsync(role1);
            await _roleManager.CreateAsync(role2);

            return Ok("Rollar yarandi");
        }
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return View();

            AppUser user;

            if (loginDTO.UserNameOrEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(loginDTO.UserNameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(loginDTO.UserNameOrEmail);
            }

            if(user == null) 
            {
                ModelState.AddModelError("", "UserNameOrEmail or password is not valid");
                return View();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Try again shortly");
                return View();
            }

            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "UserNameOrEmail or password is not correct");
                return View();
            }

            await _signInManager.SignInAsync(user, loginDTO.IsPersistent);

            var role = await _userManager.GetRolesAsync(user);

            if (role.Contains("Admin"))
            {
                return RedirectToAction("Index", "Dashboard", new {area = "Admin"});
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
