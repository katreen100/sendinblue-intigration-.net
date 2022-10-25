using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using System;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Configuration;
using ConsoleApp1.Models;
using ConsoleApp1.Services;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {
/*            ListService creatList = new ListService();

            CreatListModel creatListModel = new CreatListModel()
            {
                Name = "test2",
                FolderID = 13
            };

             ApiKey api = new ApiKey();
           var test2 = creatList.AddList("test2",13,api.Apikey);
            Console.WriteLine(test2);*/

            CreatContactModel creatContactModel = new CreatContactModel()
            {
                FristName = "test8",
                LastName = "test8",
                Email = "katytest88@gmail.com",
                Phonenumber = "911234457888",
                Campaign="test 1",
                FolderId=11
            };

            Class1 Class1 = new Class1();
            string test = Class1.AddContactToList(creatContactModel);
            Console.WriteLine(test);

        }
    }
    }