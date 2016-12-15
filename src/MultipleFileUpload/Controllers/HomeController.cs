using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MultipleFileUpload.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment _environment;

        public HomeController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");

            if(!Directory.Exists(uploads))
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
            return View();
        }
    }
}
