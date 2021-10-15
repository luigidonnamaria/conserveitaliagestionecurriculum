using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using conserveitaliagestionecurriculum.Models;
using conserveitaliagestionecurriculum.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.SharePoint.Client;


namespace conserveitaliagestionecurriculum.Pages
{
  
    public class IndexModel : PageModel
    {
        private readonly IHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly ClientContext ctx;
        private readonly Utility utility;
        private List<string> shpListsName = new List<string>();
        private string targetList;
        private string targetLibrary;
        private string lookupFieldName;

        [BindProperty]
        public Curriculum curriculum { get; set; }
        [BindProperty]
        public List<Elements> formControls { get; set; } 

        public  IndexModel(IHostEnvironment environment,ClientContext clientContext,IConfiguration configuration)
        {
            
             ctx = clientContext;
            _environment = environment;
            _configuration = configuration;
            utility = new Utility(clientContext);
            //recupero valori da appsettings
            string[] myLists = _configuration.GetSection("ControlsList").Get<string[]>();
            shpListsName = myLists.ToList<string>();          
            targetList = _configuration.GetValue<string>("SharePoint:TargetList");
            targetLibrary = _configuration.GetValue<string>("SharePoint:TargetLibrary");
            lookupFieldName = _configuration.GetValue<string>("SharePoint:LibraryLookUpFieldName");
        }

        public  async Task OnGetAsync()
        {
            curriculum = new Curriculum();
            await utility.setElements(shpListsName);
            setFormControls();
 
        }
       
        public async Task<IActionResult> OnPostAsync(Curriculum curriculum)
        {
      
            if (!ModelState.IsValid  )
            {
            
                return Page();
                
            }
            else
            {
                //Scrivo su sharepoint
                ListItem curItem= utility.saveData(targetList, curriculum);
                //carico il file se è stato caricato
                if (curriculum.UploadedFile != null && curriculum.UploadedFile.Length != 0)
                {

                    using (var ms = new MemoryStream())
                    {
                      await curriculum.UploadedFile.CopyToAsync(ms);
                        string nameFolder = curItem.Id + "_" + curriculum.Nome + curriculum.Cognome;                      
                        utility.createFolder(targetLibrary, nameFolder);
                        ListItem item = utility.UploadFile(targetLibrary+"/" + nameFolder, ms, curriculum.UploadedFile.FileName);
                        utility.setLookupField(item, curItem.Id, lookupFieldName);
                        utility.setHyperlinkField(curItem, targetLibrary + "/" + nameFolder);
                    }                  
                };
                                        
                return RedirectToPage("Success", "CurriculumData", curriculum);

            }
        }

        public List<string> getControl(string name)
        {
           
            List<string> val = new List<string>();
            foreach(Elements e in formControls)
            {
                if (e.Name == name)
                    val = e.items;
            }

            return val;
        }

        public void setFormControls()
        {
            formControls = new List<Elements>() ;
            
            foreach (string it in shpListsName)
            {
                Elements el = new Elements();
                el.Name = it;
                el.items = utility.getListValues(it);
                
                formControls.Add(el);

            };
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
