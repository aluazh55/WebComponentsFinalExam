
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using Villa.RealEstate.Models;


namespace HotelaATR4.RealtyPortal.Controllers
{
    public class EmployeeController : Controller
    {
        private string GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("_MySuperDuperMegaSecretKey_2025_Token!"));

            var credential = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256);

            var toke = new JwtSecurityToken(
                issuer: "http://satbayevproject.kz",
                audience: "http://satbayevproject.kz",
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credential);

            return new JwtSecurityTokenHandler().WriteToken(toke);
        }

        public async Task<IActionResult> Index()
        {
            var jwt = GenerateToken();//Request.Cookies["jwtCookie"];

            List<Employee> teams = new List<Employee>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", jwt);

                using (var responce = await client.GetAsync("http://localhost:5244/api/Employee/getEmployees"))
                {
                    var result = await responce.Content.ReadAsStringAsync();
                    teams = JsonConvert.DeserializeObject<List<Employee>>(result);
                }
            }

            return View(teams);
        }
    //    public async Task<IActionResult> Index()
    //    {
    //        List<Employee> employees = new List<Employee>();

    //        try
    //        {
    //            using (var client = new HttpClient())
    //            {
    //                var jwt = Request.Cookies["JwtToken"];
    //                client.DefaultRequestHeaders.Authorization =
    //                    new AuthenticationHeaderValue("Bearer", jwt);

    //                var response = await client.GetAsync("http://localhost:5244/api/Employee/getEmployees");

    //                if (response.IsSuccessStatusCode)
    //                {
    //                    var jsonString = await response.Content.ReadAsStringAsync();
    //                    employees = JsonConvert.DeserializeObject<List<Employee>>(jsonString) ?? new List<Employee>();
    //                }
    //                else
    //                {
    //                    // Логируем ошибку сервера
    //                    var error = await response.Content.ReadAsStringAsync();
    //                    Console.WriteLine($"API Error: {error}");
    //                    employees = new List<Employee>(); // Чтобы передать хотя бы пустой список
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine($"HTTP Client Error: {ex.Message}");
    //            employees = new List<Employee>(); // Тоже передаём пустой список
    //        }

    //        return View(employees);
    //    }
    }

    
}


