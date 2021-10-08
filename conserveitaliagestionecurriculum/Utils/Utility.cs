using conserveitaliagestionecurriculum.Models;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace conserveitaliagestionecurriculum.Utils
{
    public class Utility
    {
        private ClientContext ctx;
        private List<Elements> it = new List<Elements>();

        public Utility()
        {
            
        }
        public async Task setInstances(ClientContext ct,List<string> listsname)
        {
            ctx = ct;
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

        public void saveData(ClientContext context, string listname,Curriculum curriculum)
        {
            List curriculumlist = context.Web.Lists.GetByTitle(listname);
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

            context.ExecuteQuery();
        }
    }

    public class Elements
    {
        public string Name { get; set; }
        public List<string> items = new List<string>();
    }
}
