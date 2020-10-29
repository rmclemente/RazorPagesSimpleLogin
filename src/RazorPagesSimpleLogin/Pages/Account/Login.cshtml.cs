using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesSimpleLogin.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty, Required, EmailAddress]
        public string Email { get; set; }
        
        [BindProperty, Required, PasswordPropertyText(true)]
        public string Password { get; set; }

        public async Task<IActionResult> OnPost(string returnUrl)
        {
            if (!ModelState.IsValid) return Page();

            var loginAtual = new { Email = "rmc@email.com", Password = "123" };

            if (!loginAtual.Email.Equals(Email) || !loginAtual.Password.Equals(Password))
            {
                ModelState.AddModelError(string.Empty, "Credenciais Inválidas!");
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Email),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            if (string.IsNullOrWhiteSpace(returnUrl))
                return LocalRedirect("/");

            return LocalRedirect(returnUrl);
        }
    }

    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
