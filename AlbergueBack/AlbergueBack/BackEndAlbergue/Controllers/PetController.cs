using BackEndAlbergue.Exceptions;
using BackEndAlbergue.Models;
using BackEndAlbergue.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAlbergue.Controllers
{
    [Route("api/[controller]")]
    public class PetController : ControllerBase
    {
        private readonly IPetServices _petService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PetController(IPetServices petService, IWebHostEnvironment hostEnvironment)
        {
            _petService = petService;
            _webHostEnvironment = hostEnvironment;
        }

        /// <summary>
        /// Obtiene todas las mascotas del albergue
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<PetModel>> GetPets()
        {
            try
            {
                return Ok(_petService.GetPets());
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
        /// Obtiene los datos de una sola mascota especificando su id
        /// </summary>
        /// <param name="petId"></param>
        /// <returns></returns>
        [HttpGet("{petId:int}", Name = "GetPet")]
        public ActionResult<PetModel> GetPet(int petId)
        {
            try
            {
                return Ok(_petService.GetPet(petId));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
        }
        /// <summary>
        /// Crear una mascota enviando un modelo de pet
        /// </summary>
        /// <param name="petModel"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<PetModel> CreatePet([FromBody] PetModel petModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(petModel);
                }
                var createdPet = _petService.CreatePet(petModel);
                return CreatedAtRoute("GetPet", new { petId = createdPet.Id }, createdPet);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
        }
        /// <summary>
        /// Elimina los datos de una mascota especificando su id
        /// </summary>
        /// <param name="petId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{petId:int}")]
        public ActionResult<bool> DeletePet(int petId)
        {
            try
            {
                return Ok(_petService.DeletePet(petId));
            }
            catch (NotFoundException ex)
            {

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
        }
        /// <summary>
        /// Actualiza los datos de una mascota enviando un modelo de pet
        /// </summary>
        /// <param name="petId"></param>
        /// <param name="petModel"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{petId:int}")]
        public ActionResult<PetModelImpostor> UpdatePet(int petId, [FromBody] PetModel petModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    foreach (var pair in ModelState)
                    {
                        if (pair.Key == nameof(petModel.Description) && pair.Value.Errors.Count > 0)
                        {
                            return BadRequest(pair.Value.Errors);
                        }
                    }
                }
                return _petService.UpdatePet(petId, petModel);
            }

            catch (NotFoundException ex)
            {

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
        }
        /// <summary>
        /// Permita ingresar la foto de una mascota
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        [HttpPost("photo")]
        public ActionResult<string> photo([FromForm] IFormFile photo)
        {
            string uniqueFileName = null;
            string filePath = "";
            if (photo != null)
            {
                string images_path = _webHostEnvironment.ContentRootPath;
                images_path = images_path.Substring(0, images_path.Length - 29);
                string uploadsFolder = Path.Combine(images_path, $"AlbergueFront/src/assets/pets");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }
            }
            return Ok(uniqueFileName);
        }



        /// <summary>
        /// Permita Eliminar la foto de una mascota
        /// </summary>
        /// <param name="photo_name"></param>
        /// <returns></returns>
        [HttpPost("photoDelete")]
        public ActionResult<string> photoDelete([FromForm] string photo_name)
        {
            if (photo_name != null)
            {
                string images_path = _webHostEnvironment.ContentRootPath;
                images_path = images_path.Substring(0, images_path.Length - 29);
                string uploadsFolder = Path.Combine(images_path, $"AlbergueFront/src/assets/pets");
                var filePath = Path.Combine(uploadsFolder, photo_name);
                System.IO.File.Delete(filePath);
            }
            return Ok(true);
        }
    }
}
