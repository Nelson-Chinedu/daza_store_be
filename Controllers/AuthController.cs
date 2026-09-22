using daza_store_be.Dtos.Request;
using daza_store_be.Dtos.Response;
using daza_store_be.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace daza_store_be.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController: ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AuthController> _logger;
        public AuthController(UserManager<ApplicationUser> userManager, ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }


        [HttpPost("signup")]
        [ProducesResponseType(typeof(SignupResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Signup([FromBody] SignupRequestDto signupDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userManager.FindByEmailAsync(signupDto.Email);

            if(existingUser != null)
            {
                _logger.LogWarning("User with email already exist");
                return BadRequest("Email address already exist");
            }

            var newUser = new ApplicationUser
            {
                UserName = signupDto.Email,
                FirstName = signupDto.FirstName,
                LastName = signupDto.LastName,
                Email = signupDto.Email,  
            };

            var result = await _userManager.CreateAsync(newUser, signupDto.Password);

            if (!result.Succeeded)
            {
                // check if failure was caused by duplicate email during a race condition
                var duplicateError = result.Errors.FirstOrDefault(e => e.Code == "DuplicateEmail");

                if(duplicateError != null)
                {
                    return BadRequest("Email already exist");
                }

                // Return other validation errors if any
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            var responseDto = new SignupResponseDto
            {
                Message = "User registered successfully",
            };

            return StatusCode(StatusCodes.Status201Created, responseDto);
        }
    }
}