using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndAlbergue.Exceptions;
using BackEndAlbergue.Models.Auths;
using BackEndAlbergue.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEndAlbergue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService userService;

        public AuthController(IUserService userService)
        {
            this.userService = userService;
        }
        /// <summary>
        /// Permita registrar un usuario a partir de un modelo de registro
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("User")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.RegisterUserAsync(model);

                if (result.IsSuccess)
                    return Ok(result); // Status Code: 200 

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid"); // Status code: 400
        }
        /// <summary>
        /// Permita crear un rol a partir de un modelo de rol
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Role")]
        public async Task<IActionResult> CreateRolenAsync([FromBody] CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.CreateRoleAsync(model);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid");
        }
        /// <summary>
        /// Permita asgignar un rol a un usuario a partir de un modelo de relacion.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("UserRole")]
        public async Task<IActionResult> CreateUserRolenAsync([FromBody] CreateUserRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.CreateUserRoleAsync(model);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid");
        }
        /// <summary>
        /// Permita iniciar sesion a un usuario a partir de un modelo con sus credenciales
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.LoginUserAsync(model);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }

        /// <summary>
        /// Permita obtener todos los usuarios registrados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<UserModel>> GetUsersAsync()
        {
            try
            {
                return Ok( userService.GetUsersAsync());
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
        }
        /// <summary>
        /// Permita cambiar contraseña a  un usuario a partir de un modelo de registro
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("User")]
        public async Task<ActionResult<bool>> UpdatePassword([FromBody] RegisterViewModel model)
        {
            try
            {
                return Ok(await userService.UpdateUserAsync(model));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
            
        }
    }
}
