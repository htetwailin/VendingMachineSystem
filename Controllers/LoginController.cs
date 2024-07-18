using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using VendingMachineSystem.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VendingMachineSystem.ViewModel;
using VendingMachineSystem.Common;
using VendingMachineSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace VendingMachineSystem.Controllers
{
    [AllowAnonymous] // Allow access to this controller without authentication
    public class LoginController : Controller
    {
        private readonly VendingMachineDBContext _context;
        private readonly IConfiguration _configuration;

        public LoginController(VendingMachineDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Account(LoginViewModel viewModel)
        {
            try
            {
                if (viewModel != null && !string.IsNullOrEmpty(viewModel.Username) && !string.IsNullOrEmpty(viewModel.Password))
                {
                    string username = viewModel.Username.ToString();
                    string password = viewModel.Password.ToString();
                    // Find the user by their username
                    var user = FindByName(username);
                    if (user != null)
                    {
                        // Check the user's password
                        bool checkpassword = CheckPassword(user, username, password);
                        if (checkpassword)
                        {
                            var claims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.Name, username),
                                    // Add additional claims as needed
                                };

                            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var principal = new ClaimsPrincipal(identity);

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                            //User user = _context.User.Where(u => u.user_name == username).FirstOrDefault();

                            return Json(new { status = true, message = "Login Successful" });
                        }
                        else
                        {
                            return Json(new { status = false, message = "Password is Incorrect" });
                        }
                    }
                    else
                    {
                        return Json(new { status = false, message = "User does not exit!" });
                    }
                }
                else
                {
                    return Json(new { status = false, message = "Field Required Data" });
                }

            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = $"Invalid Login!,{ex.Message} " });
            }

        }

        private bool CheckPassword(User user, string username, string password)
        {
            string secretKey = _configuration.GetSection("AppSettings:SecretKey").Value;
            string decryptedPassword = EncryptionHelper.Decrypt(user.password, secretKey);
            if (user.username == username && decryptedPassword == password)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private User FindByName(string username)
        {
            User user = _context.User.Where(u => u.username == username).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }

        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            // Perform logout operations
            // AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            // Redirect to login page
            return RedirectToAction("Index", "Login");
        }
    }
}
