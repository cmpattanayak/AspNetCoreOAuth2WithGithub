using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OAuth2WithGithub.Models;

namespace OAuth2WithGithub.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            return Challenge(new AuthenticationProperties() { RedirectUri = returnUrl });
        }

        public IActionResult Index()
        {
            Login model = new Login();
            if (User.Identity.IsAuthenticated)
            {
                model.GitHubName = User.FindFirst(c => c.Type == ClaimTypes.Name)?.Value;
                model.GitHubLogin = User.FindFirst(c => c.Type == "urn:github:login")?.Value;
                model.GitHubUrl = User.FindFirst(c => c.Type == "urn:github:url")?.Value;
                model.GitHubAvatar = User.FindFirst(c => c.Type == "urn:github:avatar")?.Value;
            }
            return View(model);
        }
    }
}