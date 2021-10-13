﻿using conserveitaliagestionecurriculum.Models;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace conserveitaliagestionecurriculum.Utils
{
    public class Utility
    {
        private ClientContext ctx;
        private List<Elements> it = new List<Elements>();
        private ClientResult<long> bytesUploaded;
        private long fileoffset;

        public Utility(ClientContext ct)
        {
            ctx = ct;
            
        }
        public async Task setElements(List<string> listsname)
        {
            
            foreach(string item in listsname)
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
            foreach(Elements e in it)
            {
                if (e.Name == name)
                    toRet = e.items;

            }
            return toRet;
        }

        public int saveData(string listname,Curriculum curriculum)
        {
            List curriculumlist = ctx.Web.Lists.GetByTitle(listname);
            ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
            ListItem newItem = curriculumlist.AddItem(itemCreateInfo);
            newItem["Nome"] = curriculum.Nome;
            newItem["Cognome"] = curriculum.Cognome;
            newItem["Indirizzo"] = curriculum.Indirizzo;
            newItem["DataDiNascita"] = curriculum.DataNascita;
            newItem["Paese"] = curriculum.CurriculumStato;
            newItem["Citta"] = curriculum.Citta;
            newItem["Mansioni"] = curriculum.MansioniSvolte;
            newItem.Update();
            ctx.Load(newItem);//Load the new item
            
            ctx.ExecuteQuery();
            return newItem.Id;
        }

         public  ListItem UploadFile(string uploadFolderUrl, Stream fileContent,string filename)
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

        public void setLookupField(ListItem listItem, int lookupID,string fieldName)
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
