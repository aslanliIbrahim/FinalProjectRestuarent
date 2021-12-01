using FinalProjectRestorant.Models;
using FinalProjectRestorant.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using FinalProjectRestorant.DAL;

namespace FinalProjectRestorant.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebHostEnvironment _env;


        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IWebHostEnvironment env, AppDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
            _dbContext = dbContext;
        }
        
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View(register);
            AppUser user = new AppUser
            {
                UserName = register.UserName,
                Fullname = register.FullName,
                Email = register.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user,register.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return BadRequest();
            }
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Ibrahim Aslanli", "ibrahimra@code.edu.az"));
            message.To.Add(new MailboxAddress(user.UserName, user.Email));
            message.Subject = " Zehmet olmasa Emaili Tesdiqleyin ";

            string emailbody = string.Empty;

            using (StreamReader stream = new StreamReader(Path.Combine(_env.WebRootPath, "Templates", "EmailConfirm.html")))
            {
                emailbody = stream.ReadToEnd();
            };


            string emailconfirmtoken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string url = Url.Action("confirmemail", "account",new {Id=user.Id,token=emailconfirmtoken },Request.Scheme);

            emailbody = emailbody.Replace("{{fullname}}", $"{user.Fullname}").Replace("{{url}}", $"{url}");

            message.Body = new TextPart(TextFormat.Html) {Text=emailbody };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("ibrahimra@code.edu.az", "Y7GDj5BR");
            smtp.Send(message);
            smtp.Disconnect(true);
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> ConfirmEmail(string Id, string token)
        {
            
            if (string.IsNullOrWhiteSpace(Id) || string.IsNullOrWhiteSpace(token))
            {
                return NotFound();
            }

            AppUser user = await _userManager.FindByIdAsync(Id);
            
            if (user == null)
            {
                return Content("User is null");
            }

            IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return NotFound();
            }

            return RedirectToAction("Login", "Account");

        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Login(LoginVM login)
        {
            AppUser user = await _userManager.FindByNameAsync(login.UserName);
            if (!ModelState.IsValid) return View(login);
            if (user == null)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View(login);
            }
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user,login.Password,false,false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View(login);
            }
            if (user.EmailConfirmed == false)
            {
                ModelState.AddModelError("","E-mailinizi tesdiqlemek teleb olunur");
                return View(login);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPassWordVM model)
        {
            if (!ModelState.IsValid) return View(model);

            AppUser user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return NotFound();
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Ibrahim Aslanli", "ibrahimra@code.edu.az"));
            message.To.Add(new MailboxAddress(user.UserName, user.Email));
            message.Subject = "Emaili Tesdiqleyin";

            string emailbody = string.Empty;

            using (StreamReader stream = new StreamReader(Path.Combine(_env.WebRootPath, "Templates", "forgotpassword.html")))
            {
                emailbody = stream.ReadToEnd();
            };


            string forgotpasswordtoken = await _userManager.GeneratePasswordResetTokenAsync(user);

            string url = Url.Action("ResetPassword", "account", new { Id = user.Id, token = forgotpasswordtoken }, Request.Scheme);

            emailbody = emailbody.Replace("{{fullname}}", $"{user.Fullname}").Replace("{{url}}", $"{url}");

            message.Body = new TextPart(TextFormat.Html) { Text = emailbody };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("ibrahimra@code.edu.az", "Y7GDj5BR");
            smtp.Send(message);
            smtp.Disconnect(true);

            return View();
        }

        public async Task<IActionResult> ResetPassword(string Id, string token)
        {
            if (string.IsNullOrWhiteSpace(Id) || string.IsNullOrWhiteSpace(token))
            {
                return NotFound();
            }

            AppUser user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return NotFound();
            }

            ResetPassWordVM reset = new ResetPassWordVM
            {
                Id = Id,
                Token = token
            };
            return View(reset);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPassWordVM reset)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (string.IsNullOrWhiteSpace(reset.Id) || string.IsNullOrWhiteSpace(reset.Token))
            {
                return NotFound();
            }

            AppUser user = await _userManager.FindByIdAsync(reset.Id);
            if (user == null)
            {
                return NotFound();
            }

            IdentityResult result = await _userManager.ResetPasswordAsync(user, reset.Token, reset.Password);

            if (!result.Succeeded)
            {
                foreach (var eror in result.Errors)
                {
                    ModelState.AddModelError("", eror.Description);
                }
                return View(reset);
            }

            return RedirectToAction("login", "account");
        }

    }
}
