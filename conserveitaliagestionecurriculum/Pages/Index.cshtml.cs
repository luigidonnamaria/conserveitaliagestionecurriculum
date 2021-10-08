using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using conserveitaliagestionecurriculum.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Syncfusion.EJ2.Navigations;


namespace conserveitaliagestionecurriculum.Pages
{
  
    public class IndexModel : PageModel
    {
        private readonly IHostEnvironment _environment;

        [BindProperty]
        public Curriculum curriculum { get; set; }

        public IndexModel(IHostEnvironment environment)
        {
           
            _environment = environment;
        }

        public void OnGet()
        {
            curriculum = new Curriculum();
        }
       
        public IActionResult OnPost(Curriculum curriculum)
        {
      
            if (!ModelState.IsValid  || (curriculum.UploadedFile == null || curriculum.UploadedFile.Length == 0))
            {
                return Page();
            }
            else
            {
                string targetFileName = $"{_environment.ContentRootPath}/wwwroot/uploads/{curriculum.UploadedFile.FileName}";

                using (var stream = new FileStream(targetFileName, FileMode.Create))
                {
                    curriculum.UploadedFile.CopyToAsync(stream);
                }
                return RedirectToPage("Success", "CurriculumData", curriculum);

            }
        }

        public void Save(IList<IFormFile> UploadFiles)
        {
            long size = 0;
            
            try
            {
                foreach (var file in UploadFiles)
                {
                    var filename = ContentDispositionHeaderValue
                                    .Parse(file.ContentDisposition)
                                    .FileName
                                    .Trim('"');
                    filename = _environment.ContentRootPath + $@"\{filename}";
                    size += file.Length;
                    if (!System.IO.File.Exists(filename))
                    {
                        using (FileStream fs = System.IO.File.Create(filename))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Response.Clear();
                Response.StatusCode = 204;
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Errore salvataggio file";
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = e.Message;
            }
        }
    }
}
