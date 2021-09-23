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
    [Route("api/notice/")]
    public class NoticeController : ControllerBase
    {
        private INoticeService _noticeService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public NoticeController(INoticeService noticeService, IWebHostEnvironment hostEnvironment)
        {
            _noticeService = noticeService;
            _webHostEnvironment = hostEnvironment;
        }
        /// <summary>
        /// Permita obtener todas las noticias
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<NoticeModel>> GetNotices()
        {
            try
            {
                return Ok(_noticeService.GetNotices());
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
        /// Permite registrar noticias a partir de un modelo de creacion para las noticias
        /// </summary>
        /// <param name="noticeModel"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<NoticeModel> CreateNotice([FromBody] NoticeModel noticeModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(noticeModel);
                }
                var url = HttpContext.Request.Host;
                return Ok(_noticeService.CreateNotice(noticeModel));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
        }
        /// <summary>
        /// Permita eliminar una noticia a partir de un id
        /// </summary>
        /// <param name="noticeId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{noticeId:int}")]
        public ActionResult<bool> DeleteNotice(int noticeId)
        {
            try
            {
                return Ok(_noticeService.DeleteNotice(noticeId));
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
        /// Permita editar campos a una noticia con un id especifico y un modelo de los campos a cambiar
        /// </summary>
        /// <param name="noticeId"></param>
        /// /// <param name="noticeModel"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{noticeId:int}")]
        public ActionResult<NoticeModel> UpdateNotice(int noticeId, [FromBody] NoticeModel noticeModel)
        {
            try
            {
                return Ok(_noticeService.UpdateNotice(noticeId, noticeModel));
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
                string uploadsFolder = Path.Combine(images_path, $"AlbergueFront/src/assets/notices");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }
            }
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
                string uploadsFolder = Path.Combine(images_path, $"AlbergueFront/src/assets/notices");
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
