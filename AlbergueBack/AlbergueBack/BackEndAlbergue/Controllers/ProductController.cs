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
    public class ProductController : ControllerBase
    {
        private IProductService _petShopService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IProductService petShopService, IWebHostEnvironment hostEnvironment)
        {
            _petShopService = petShopService;
            _webHostEnvironment = hostEnvironment;
        }
        /// <summary>
        /// Obtiene todos los productos que ofrece el albergue
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProductModel>> GetPetShop()
        {
            try
            {
                return Ok(_petShopService.GetProducts());
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
        /// Obtiene los datos de un solo producto especificando su id
        /// </summary>
        /// <param name="itemPetShopId"></param>
        /// <returns></returns>
        [HttpGet("{itemPetShopId:int}", Name = "GetPetShopItem")]
        public ActionResult<PetModel> GetSinglePetShopItem(int itemPetShopId)
        {
            try
            {
                return Ok(_petShopService.GetProduct(itemPetShopId));
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
        /// Crea un producto especificando el modelo para su creacion
        /// </summary>
        /// <param name="petShopModel"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<ProductModel> CreateItemPetShop([FromBody] ProductModel petShopModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(petShopModel);
                }
                var url = HttpContext.Request.Host;
                var createdPet = _petShopService.CreateProduct(petShopModel);
                return Ok(createdPet);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
        }
        /// <summary>
        /// Elimina un producto especificando su id
        /// </summary>
        /// <param name="itemPetShopId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{itemPetShopId:int}")]
        public ActionResult<bool> DeleteItemPetShop(int itemPetShopId)
        {
            try
            {
                return Ok(_petShopService.DeleteProduct(itemPetShopId));
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
        /// Edita un producto especificando el modelo para su creacion y su id
        /// </summary>
        /// <param name="petShopModel"></param>
        /// <param name="itemPetShopId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{itemPetShopId:int}")]
        public ActionResult<PetModel> UpdatePet(int itemPetShopId, [FromBody] ProductModel petShopModel)
        {
            try
            {
                return Ok(_petShopService.UpdateProduct(itemPetShopId, petShopModel));
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
            //string uniqueFileName = null;
            //string filePath = "";
            //if (photo != null)
            //{
            //    string uploadsFolder = "C://Xamp/balbl";
            //    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
            //    filePath = Path.Combine(uploadsFolder, uniqueFileName);
            //    using (var fileStream = new FileStream(filePath, FileMode.Create))
            //    {
            //        photo.CopyTo(fileStream);
            //    }
            //}
            string uniqueFileName = null;
            string filePath = "";
            if (photo != null)
            {
                string images_path = _webHostEnvironment.ContentRootPath;
                images_path = images_path.Substring(0, images_path.Length - 29);
                string uploadsFolder = Path.Combine(images_path, $"AlbergueFront/src/assets/Petshop");
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
                string uploadsFolder = Path.Combine(images_path, $"AlbergueFront/src/assets/Petshop");
                var filePath = Path.Combine(uploadsFolder, photo_name);
                //using (var fileStream = new FileStream(filePath, FileMode.Open))
                //{
                //}
                System.IO.File.Delete(filePath);
            }
            return Ok(true);
        }
    }
}