using ConsoleApp1.Models;
using Nancy.Json;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Services
{
    internal class ListService
    {
            /*------------- creat list at certain folder------------------------  */
        public string AddList(string ComapainName,int FolderId,string Apikey)
        {
            if (Apikey == null)
            {
                return new string("ApiKey is required");
            }
            Configuration.Default.ApiKey.Add("api-key",Apikey);
            var apiInstance = new ContactsApi();
            string name = ComapainName;
            long? folderId = FolderId;
            try
            {
                var createList = new CreateList(name, folderId);
                CreateModel result = apiInstance.CreateList(createList);
                return new string(result.ToJson().ToString());
            }
            catch (Exception e)
            {
                return new string(e.Message);
            }

        }
         /*-------get alll list at folder -----------*/
        public string GetAllList(string apiKey,int FolderId)
        {
            Configuration.Default.ApiKey.Add("api-key", apiKey);

            var apiInstance = new ContactsApi();
            long? folderId = FolderId;
            try
            {
                GetFolderLists result = apiInstance.GetFolderLists(folderId);
                return new string(result.ToJson().ToString());
            }
            catch (Exception e)
            {
                return new string(e.Message);
            }

        }
        /*get the id of the list at certain folder to send the list id to the function which creat a contact    */
        public list GetListByName(string name, string apikey,int folderid)
        {
            var json = GetAllList(apikey, folderid);
            var jsonObj = new JavaScriptSerializer().Deserialize<JsonListModel>(json);
            var list = jsonObj.lists.Where(l => l.name == name).FirstOrDefault();

            return list;
        }
    }
}
