using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace JaheshBoom.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FarazAuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string appSecretKey;
        private readonly string appKey;
        private readonly string userToken;

        public FarazAuthController(IConfiguration configuration)
        {
            _configuration = configuration;
            var appSecretKey = _configuration["FarazSettings:AppSecretKey"];
            var appKey = _configuration["FarazSettings:AppKey"];
            var userToken = _configuration["FarazSettings:UserToken"];
        }

        [HttpGet("Enter")]
        public async Task<IActionResult> Enter(string post_token, string return_url,string mobileNumber)
        {
            try
            {
                    return authenticationRedirect(post_token,mobileNumber);               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }




        [HttpGet("oath")]
        public async Task<IActionResult> oath(string code, string scope, string state)
        {
            var token = await GetToken(code);
            if (token == null)
                return BadRequest("token not found");

             Consts.Token=token;
          
            return Ok();

        }
        #region Methods
        private IActionResult authenticationRedirect(string postToken,string mobileNumber)
        {
            string redirectUri = "https://sbxauth.izbank.ir";
            string encodedRedirectUri = Uri.EscapeDataString(redirectUri);
            string scope = $"USER_PHONE";
            var random = Guid.NewGuid();                      
            string encodedscope = Uri.EscapeDataString(scope);

            string url = $"https://sbxauth.izbank.ir/oauth/token?mobileNumber={mobileNumber}&bankId=69&grant_type=client_credentials";

            // ایجاد یک RedirectResult برای امکان افزودن هدر
            var redirectResult = new RedirectResult(url);

            // تنظیم هدرهای مورد نیاز
            Response.Headers.Add("Authorization", "55313095cac30123f1cf7f4b0b6357a82688f438e63ac46369ada052d90699e411c477157aff052d7f1c8b4f04396e002c542fe1a67ffb30e0e02c9936bef4fc");

            return redirectResult;
        }

        private async Task<string?> GetToken(string code)
        {
            using (var client = new HttpClient())
            {
                // URL مربوط به API
                var url = "https://api.divar.ir/oauth2/token";

                // محتوای درخواست به صورت FormUrlEncoded
                var postData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("client_id", Consts.TempToken),
                new KeyValuePair<string, string>("client_secret", "55313095cac30123f1cf7f4b0b6357a82688f438e63ac46369ada052d90699e411c477157aff052d7f1c8b4f04396e002c542fe1a67ffb30e0e02c9936bef4fc"),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("redirect_uri", "https://jpneo.com/oath")
            };

                var content = new FormUrlEncodedContent(postData);

                // ارسال درخواست POST
                var response = await client.PostAsync(url, content);

                // بررسی وضعیت پاسخ
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var jsonDocument = JsonDocument.Parse(responseBody);
                    var token = jsonDocument.RootElement.GetProperty("access_token").GetString();
                    return token;

                }
                else
                {
                    return null;
                }
            }
        }
        #endregion
    }
}
