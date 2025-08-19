using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace UI.Controllers
{
    public class EmissionController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SessionToken _sessionToken;

        public EmissionController(IHttpClientFactory httpClientFactory, SessionToken sessionToken)
        {
            _httpClientFactory = httpClientFactory;
            _sessionToken = sessionToken;
        }


        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("API");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sessionToken.Token);

            var response = await client.GetAsync("api/emissions/summary");
            var summaryJson = await response.Content.ReadAsStringAsync();
            var summary = JsonDocument.Parse(summaryJson).RootElement;

            return View(summary);
        }

        // POST: Add emission
        [HttpPost]
        public async Task<IActionResult> Add(string type, double amount)
        {
            var client = _httpClientFactory.CreateClient("API");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sessionToken.Token);

            var content = new StringContent(
                JsonSerializer.Serialize(new { type, amount }),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("api/emissions", content);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to add emission";
            }

            // After adding, reload the summary
            var summaryResponse = await client.GetAsync("api/emissions/summary");
            var summaryJson = await summaryResponse.Content.ReadAsStringAsync();
            var summary = JsonDocument.Parse(summaryJson).RootElement;

            return View("Index", summary); // Show both Add + Summary
        }

        public IActionResult Add() => View();
    }
}
