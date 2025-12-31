//using Microsoft.AspNetCore.Identity.Data;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.Security.Claims;
//using System.Text;
//using System.Text.Json;

//namespace Eva.WhatsApp.Controllers.v1
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly IConfiguration _config;
//        private readonly ILogger<AuthController> _logger;

//        public AuthController(IConfiguration config, ILogger<AuthController> logger)
//        {
//            _config = config;
//            _logger = logger;
//        }

//        [HttpPost("login")]
//        public IActionResult Login([FromBody] LoginRequest request)
//        {
//            try
//            {
//                var jwtAccounts = _config.GetSection("Accounts");
//                if (request.Username != jwtAccounts["Login"] || request.Password != jwtAccounts["Password"])
//                    return Unauthorized();

//                var token = GenerateJwtToken(request.Username);
//                _logger.LogInformation("User {Username} logged in successfully.", request.Username);

//                //_telemetry.TrackTrace("Login User successfully",
//                //   new Dictionary<string, string>
//                //   {
//                //       ["Message"] = @$"User {request.Username} logged in successfully."
//                //   });

//                //_telemetry.TrackEvent("LoginSuccess",
//                //   new Dictionary<string, string>
//                //   {
//                //       ["Message"] = @$"User {request.Username} logged in successfully."
//                //   });


//                return Ok(new { token });
//            }
//            catch (Exception ex)
//            {
//                var erro = new { Erro = "Erro login", Detalhes = ex.Message };
//                _logger.LogError(@$"Erro login: {JsonSerializer.Serialize(erro)}");

//                //_telemetry.TrackEvent("ErroApiHub",
//                //     new Dictionary<string, string>
//                //     {
//                //         ["Message"] = @$"{JsonSerializer.Serialize(ex)}"
//                //     });
//                return BadRequest(erro);
//            }


//        }


//        private string GenerateJwtToken(string username)
//        {
//            var jwtSettings = _config.GetSection("JwtSettings");
//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            //var claims = new[]
//            //{
//            //    new Claim(JwtRegisteredClaimNames.Sub, username),
//            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
//            //};
//            var claims = new Dictionary<string, object>
//            {
//                [ClaimTypes.Email] = username,
//                [ClaimTypes.Sid] = "3c545f1c-cc1b-4cd5-985b-8666886f985b"
//            };
//            var token = new SecurityTokenDescriptor()
//            {
//                Claims = claims,
//                IssuedAt = null,
//                NotBefore = DateTime.UtcNow,
//                Expires = DateTime.Now.AddMinutes(Convert.ToInt32(jwtSettings["ExpirationMinutes"])),
//                Issuer = jwtSettings["Issuer"],
//                Audience = jwtSettings["Audience"],
//                SigningCredentials = new SigningCredentials(key, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature)
//            };
//            var handler = new Microsoft.IdentityModel.JsonWebTokens.JsonWebTokenHandler();
//            handler.SetDefaultTimesOnTokenCreation = false;

//            var tokenString = handler.CreateToken(token);
//            return tokenString;
//        }
//    }
//}
