using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SessionToken _sessionToken;

        public HomeController(IHttpClientFactory httpClientFactory, SessionToken sessionToken)
        {
            _httpClientFactory = httpClientFactory;
            _sessionToken = sessionToken;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var client = _httpClientFactory.CreateClient("API");

            var content = new StringContent(
                JsonSerializer.Serialize(new { Username = username, Password = password }),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("api/auth/login", content);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Invalid credentials";
                return View("Index");
            }

            var result = JsonSerializer.Deserialize<JsonElement>(await response.Content.ReadAsStringAsync());
            _sessionToken.Token = result.GetProperty("token").GetString() ?? string.Empty;

            return RedirectToAction("Index", "Emission");
        }
    }
}
