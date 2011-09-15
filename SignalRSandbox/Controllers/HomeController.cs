using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SignalR;
using SignalR.Hubs;
using Twilio;
using Twilio.TwiML;
using Twilio.TwiML.Mvc;

namespace SignalRSandbox.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _twilioSID = ConfigurationManager.AppSettings["TwilioSID"];
        private readonly string _twilioAuthToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
        private readonly string _twilioSandBoxNumber = ConfigurationManager.AppSettings["TwilioSandBoxNumber"];
        private readonly string _twilioSandBoxPIN = ConfigurationManager.AppSettings["TwilioSandBoxPIN"];

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SideBarList()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetData()
        {
            var jsonData = new
            {
                total = 1,//pageCount,
                page = 1,
                records = 100, // implement later 
                rows = (from n in Enumerable.Range(0, 10)
                        select new
                        {
                            id = n,
                            cell = new[]
                                      {
                                          "First", "Last", "555-5555", "Kansas City", "KS", "66109"
                                      }
                        }).ToArray()
            };
            return Json(jsonData);
        }

        [HttpPost]
        public ActionResult Send(string message)
        {
            var twilioClient = new TwilioRestClient(_twilioSID, _twilioAuthToken);

            
            twilioClient.SendSmsMessage(_twilioSandBoxNumber, "+18168041829", String.Format("{0} {1}", _twilioSandBoxPIN, message));

            //new Chat().Send("got this");
            //var chatClients = Hub.GetClients<Chat>();

            //Connection.GetConnection<Streaming.Streaming>();
            //chatClients.

            return Json(1);
        }

        [HttpPost]
        public TwiMLResult HandleSms(string Sid, string From, string To, string Body, string Status)
        {
            //_db.Log.Insert(Text: String.Format("From: {0}, To: {1}, Body: {2}", From, To, Body));
            var response = new TwilioResponse();
            response.Say("Received");
            return new TwiMLResult(response);
        }
    }
}