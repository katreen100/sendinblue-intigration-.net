using ConsoleApp1.Models;
using Nancy;
using Nancy.Json;
using Newtonsoft.Json.Linq;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1.Services
{
    public class Class1
    {

        public string AddContactToList(CreatContactModel creatContactModel)
        {
            if (creatContactModel == null)
            {
                return new string("the createcontact obj is null");
            }
            if (creatContactModel.Apikey == null)
            {
                return new string("ApiKey is required");
            }
            if (creatContactModel.FolderId == null)
            {
                return new string("enter folder id ");
            }
            //step 1 get all lists inside folderId 
            //check if the campaign name matches any of the lists regardless of letter case
            //if list exists add that lead into this list
            var list = GetListByName(creatContactModel.Campaign, creatContactModel.Apikey, creatContactModel.FolderId);
                if (list != null)
                {
                    creatContactModel.listId = list.id;
                    var NewContact=AddContact(creatContactModel);
                    return NewContact;
                }
            //else create a new list then add the lead
                else
                {
                    var NewList = AddList(creatContactModel.Campaign, creatContactModel.FolderId, creatContactModel.Apikey);
                    var jsonObj = new JavaScriptSerializer().Deserialize<CreatListReturn>(NewList);
                    var listId = jsonObj.id;
                    creatContactModel.listId=listId;
                    var NewContact = AddContact(creatContactModel);
                     return NewContact;
                }
               
         } 
        /* add contact*/
        public string AddContact (CreatContactModel creatContactModel)
        {
            try
            {
            var apiInstance = new ContactsApi();
            string email = creatContactModel.Email;
            JObject attributes = new JObject();
            attributes.Add("FIRSTNAME", creatContactModel.FristName);
            attributes.Add("LASTNAME", creatContactModel.LastName);
            attributes.Add("SMS", creatContactModel.Phonenumber);
            List<long?> listIds = new List<long?>();
            listIds.Add(creatContactModel.listId);
            bool emailBlacklisted = false;
            bool smsBlacklisted = false;
            bool updateEnabled = false;
            List<string> smtpBlacklistSender = new List<string>();
            var createContact = new CreateContact(email, attributes, emailBlacklisted, smsBlacklisted, listIds);
            CreateUpdateContactModel result = apiInstance.CreateContact(createContact);
            return new string(result.ToJson().ToString());
             }
            catch (Exception e)
            {
                return new string (e.Message);
            }
          }
        /*-------get alll list at folder -----------*/
        public string GetAllList(string apiKey, int FolderId)
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
        public list GetListByName(string name, string apikey, int folderid)
        {
            var json = GetAllList(apikey, folderid);
            var jsonObj = new JavaScriptSerializer().Deserialize<JsonListModel>(json);
            var list = jsonObj.lists.Where(l => l.name == name).FirstOrDefault();

            return list;
        }
        /*add new list */
        public string AddList(string ComapainName, int FolderId, string Apikey)
        {
            if (Apikey == null)
            {
                return new string("ApiKey is required");
            }
           
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
    }
}
