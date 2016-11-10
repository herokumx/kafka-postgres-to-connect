using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using FXPostgresMap.Models;
using Npgsql;
using System.Text;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FXPostgresMap.Controllers
{
    public class PostgresController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.ClientID = ConfigVars.Instance.clientID;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> ReadWebTrafficByClientID()
        {
            try
            {
                List<PostgresMessage> postgresMessages = new List<PostgresMessage>();
                using (var sqlCon = new NpgsqlConnection(ConfigVars.Instance.PostgresDBConnectionString))
                {
                    sqlCon.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(string.Format("select \"Offset\",\"Message\",\"Topic\",\"Partition\" from fxvip where \"Message\" like '%{0}%';", ConfigVars.Instance.clientID), sqlCon))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    PostgresMessage kafkaMessage = new PostgresMessage();
                                    kafkaMessage.offset = (int)reader["offset"];
                                    kafkaMessage.partition = (int)reader["partition"];
                                    kafkaMessage.topic = (string)reader["topic"];
                                    kafkaMessage.message = (string)reader["message"];
                                    postgresMessages.Add(kafkaMessage);
                                }

                                if (reader.NextResult() == false)
                                    break;
                            }
                        }
                    }

                    return Json(postgresMessages);
                }
            }
            catch (Exception exception)
            {
                return Json(null);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> FlagContactAsBrowser()
        {
            try
            {
                List<SFContact> sfContacts = new List<SFContact>();
                using (var sqlCon = new NpgsqlConnection(ConfigVars.Instance.PostgresDBConnectionString))
                {
                    sqlCon.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(string.Format("update salesforce.contact set browsed_vip_website__c=true where cookieid__c='{0}';", ConfigVars.Instance.clientID), sqlCon))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                using (var sqlCon = new NpgsqlConnection(ConfigVars.Instance.PostgresDBConnectionString))
                {
                    sqlCon.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(string.Format("select * from salesforce.contact where cookieid__c='{0}' and browsed_vip_website__c=true;", ConfigVars.Instance.clientID), sqlCon))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    SFContact sfContact = new SFContact();
                                    sfContact.masterrecordid = reader["masterrecordid"].ToString();
                                    sfContact.sfid = reader["sfid"].ToString();
                                    sfContact.firstname = reader["firstname"].ToString();
                                    sfContact.createddate = (DateTime)reader["createddate"];
                                    sfContact.cookieid__c = reader["cookieid__c"].ToString();
                                    sfContact.name = reader["name"].ToString();
                                    sfContact.systemmodstamp = (DateTime)reader["systemmodstamp"];
                                    sfContact.isdeleted = (bool)reader["isdeleted"];
                                    sfContact.lastname = reader["lastname"].ToString();
                                    sfContact.email = reader["email"].ToString();
                                    sfContact.id = (int)reader["id"];
                                    sfContact._hc_err = reader["_hc_err"].ToString();
                                    sfContact.responded_to_vip_survey__c = (bool)reader["responded_to_vip_survey__c"];
                                    sfContact.followed_vip_on_facebook__c = (bool)reader["followed_vip_on_facebook__c"];
                                    sfContact.eventsource__c = reader["eventsource__c"].ToString();
                                    sfContact.accountid = reader["accountid"].ToString();
                                    sfContact.entrolled_in_vip__c = (bool)reader["enrolled_in_vip__c"];
                                    sfContact.browsed_vip_website__c = (bool)reader["browsed_vip_website__c"];
                                    sfContacts.Add(sfContact);
                                }

                                if (reader.NextResult() == false)
                                    break;
                            }
                        }
                    }

                    return Json(sfContacts);
                }
            }
            catch (Exception exception)
            {
                return Json(null);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> ResetWebFlagFor()
        {
            try
            {
                using (var sqlCon = new NpgsqlConnection(ConfigVars.Instance.PostgresDBConnectionString))
                {
                    sqlCon.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(string.Format("update salesforce.contact set browsed_vip_website__c=false where cookieid__c='{0}';", ConfigVars.Instance.clientID), sqlCon))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                return Json("success");
            }
            catch (Exception exception)
            {
                return Json(null);
            }
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
