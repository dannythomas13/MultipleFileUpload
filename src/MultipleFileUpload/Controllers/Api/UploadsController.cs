using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultipleFileUpload.Models;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace MultipleFileUpload.Controllers.Api
{
    [Route("api/uploads")]
    [Authorize]
    public class UploadsController : Controller
    {
        private IHostingEnvironment _environment;

        public UploadsController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok("Get");
        }

        [HttpPost("")]
        public async Task<IActionResult> Post(ICollection<IFormFile> files)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");

            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }

            return Ok(true);
        }
    }
}