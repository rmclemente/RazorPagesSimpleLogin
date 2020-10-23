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
        [BindProperty]
        public Login Login { get; set; }

        public string Message { get; set; }
        public async Task<IActionResult> OnPost(string returnUrl)
        {
            var loginAtual = new Login { Email = "rmc@email.com", Password = "123" };

            //if (!loginAtual.Email.Equals(Login.Email) || !loginAtual.Password.Equals(Login.Password))
            //{
            //    Message = "Invalid credentials";
            //    return Page();
            //}

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Login.Email),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            if (string.IsNullOrWhiteSpace(returnUrl))
                return RedirectToPage("/Index");

            var urlParts = returnUrl.Split('/');
            if (urlParts.Count() <= 2)
                return RedirectToPage($"{returnUrl}/Index");

            return RedirectToPage(returnUrl);
        }
    }

    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
