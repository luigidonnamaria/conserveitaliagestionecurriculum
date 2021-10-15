using conserveitaliagestionecurriculum.Models;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace conserveitaliagestionecurriculum.Utils
{
    public class Utility
    {
        private ClientContext ctx;
        private List<Elements> it = new List<Elements>();
        

        public Utility(ClientContext ct)
        {
            ctx = ct;

        }
        public async Task setElements(List<string> listsname)
        {

            foreach (string item in listsname)
            {
                Elements el = new Elements();
                el.Name = item;
                el.items = await this.retrieveListItemAsync(item);
                it.Add(el);
            }
        }

        public async Task<List<string>> retrieveListItemAsync(string listname)
        {
            List<string> toRet = new List<string>();
            List sharepointlist = ctx.Web.Lists.GetByTitle(listname);
            // CamlQuery query = CamlQuery.CreateAllItemsQuery(100);
            CamlQuery query = new CamlQuery();
            query.ViewXml = string.Concat("<FieldRef Name='Title' />");
            ListItemCollection items = sharepointlist.GetItems(query);
            ctx.Load(items);
            await ctx.ExecuteQueryAsync();
            foreach (ListItem listItem in items)
            {

                toRet.Add(listItem["Title"].ToString());
            }

            return toRet;

        }

        public List<string> getListValues(string name)
        {
            List<string> toRet = new List<string>();
            foreach (Elements e in it)
            {
                if (e.Name == name)
                    toRet = e.items;

            }
            return toRet;
        }

        public ListItem saveData(string listname, Curriculum curriculum)
        {
            List curriculumlist = ctx.Web.Lists.GetByTitle(listname);
            ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
            ListItem newItem = curriculumlist.AddItem(itemCreateInfo);
            #region campi obbligatori  
            newItem["Nome"] = curriculum.Nome;
            newItem["Cognome"] = curriculum.Cognome;
            newItem["Indirizzo"] = curriculum.Indirizzo;
            newItem["Stato"] = curriculum.CurriculumStato;
            newItem["Citta"] = curriculum.Citta;          
            //Anno          
            if (Int32.TryParse(curriculum.AnnoUno, out int anno))
                newItem["TitoloStudioUno_Anno"] = anno;
            //DataNascita
            if (DateTime.TryParseExact(curriculum.DataNascita, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dn))
                newItem["DataNascita"] = dn;
            newItem["Provincia"] = curriculum.CurriculumProvincia;
            newItem["Cap"] = curriculum.Cap;
            newItem["Sesso"] = curriculum.Sesso;
            newItem["StatoCivile"] = curriculum.CurriculumStatoCivile;
            newItem["TelefonoAbitazione"] = curriculum.TelefonoAbitazione;
            newItem["Email"] = curriculum.Email;
            newItem["Nazionalita"] = curriculum.Nazionalita;
            newItem["TitoloStudioUno"] = curriculum.TitoloStudioUno;
            newItem["TitoloStudioUno_Sede"] = curriculum.Citta;
            newItem["TitoloStudioUno_Tipologia"] = curriculum.TipologiaUno;
            //Voto         
            if (Int32.TryParse(curriculum.VotoUno, out int votouno))
                newItem["TitoloStudioUno_Voto"] = votouno;
            newItem["Madrelingua"] = curriculum.Madrelingua;
            newItem["AreaAziendalePrioritaria"] = curriculum.AreaAziendalePrioritaria;

            #endregion
            newItem["Mansioni"] = curriculum.MansioniSvolte;
            //Traferimenti
            List<string> Items = new List<string>();
            if (curriculum.DispTrasferimenti) Items.Add("Trasferimenti");
            if (curriculum.DispEstero) Items.Add("Trasferte all'estero");
            if (curriculum.DispItalia) Items.Add("Trasferte in Italia");
            if (curriculum.DispTurni) Items.Add("Turni (anche notturni)");
            newItem["Disponibilita"] = Items;

            newItem.Update();
            ctx.Load(newItem);//Load the new item

            ctx.ExecuteQuery();
            return newItem;
        }

         public void setHyperlinkField(ListItem listItem,  string folderurl)
        {
            Folder targetFolder = ctx.Web.GetFolderByServerRelativeUrl(folderurl);
            FieldUrlValue url = new FieldUrlValue();
              ctx.Load(targetFolder);
              ctx.ExecuteQuery();
            url.Url =  targetFolder.ServerRelativeUrl;
            url.Description = "Vedi Curriculum";
            listItem["LinkCurriculum"] = url;
            listItem.Update();            
            ctx.ExecuteQuery();
        }

        public ListItem UploadFile(string uploadFolderUrl, Stream fileContent, string filename)
        {
            FileCreationInformation newfile = new FileCreationInformation();
            newfile.Url = filename;
            fileContent.Seek(0, SeekOrigin.Begin);
            newfile.ContentStream = fileContent;
            var targetFolder = ctx.Web.GetFolderByServerRelativeUrl(uploadFolderUrl);
            Microsoft.SharePoint.Client.File uploadFile = targetFolder.Files.Add(newfile);
            ctx.Load(uploadFile.ListItemAllFields);
            ctx.ExecuteQuery();
            return uploadFile.ListItemAllFields;

        }

        public void setLookupField(ListItem listItem, int lookupID, string fieldName)
        {
            listItem[fieldName] = lookupID;
            listItem.Update();
            ctx.ExecuteQuery();
        }
    
        public void createFolder(string listName, string folderName)
        {
            List list = ctx.Web.Lists.GetByTitle(listName);
            ListItemCreationInformation info = new ListItemCreationInformation();
            info.UnderlyingObjectType = FileSystemObjectType.Folder;
            info.LeafName = folderName.Trim();//Trim for spaces.Just extra check
            ListItem newItem = list.AddItem(info);
            newItem["Title"] = folderName;
            newItem.Update();
           
            ctx.ExecuteQuery();
            
            
        }
    
 
    }

    public class Elements
    {
        public string Name { get; set; }
        public List<string> items = new List<string>();
    }
}
