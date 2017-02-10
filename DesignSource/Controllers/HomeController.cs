using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using DesignSource.Models;
using System.Text;
using System.Net;
using System.Threading;

namespace DesignSource.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Mobile()
        {
            ViewBag.Message = "Mobile description page.";

            return View();
        }

        public ActionResult Web()
        {
            return View();
        }

        public ActionResult Desktop()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpPost]
        public ActionResult Contact(ContactModels c)
        {
            if (ModelState.IsValid)
            {
                //try
                //{
                //MailMessage msg = new MailMessage();
                //SmtpClient smtp = new SmtpClient();
                //MailAddress from = new MailAddress(c.Email.ToString());
                string fromAddress = c.Email.ToString();
                StringBuilder sb = new StringBuilder();

                //msg.To.Add("ryanbmcnutt@gmail.com");
                string toAddress = "ryanbmcnutt@gmail.com";
                //msg.Subject = "Contact Me";
                string subject = "Contact Me";
                //msg.IsBodyHtml = false;

                /*
                SmtpClient client = new SmtpClient();
                // We use gmail as our smtp client
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new System.Net.NetworkCredential("ryanbmcnutt@gmail.com", "!C25a47d2ec");
                */

                sb.Append("First name: " + c.FirstName + "\n");
                sb.Append("Last name: " + c.LastName + "\n");
                sb.Append("Email: " + c.Email + "\n");
                sb.Append("Phone: " + c.Phone + "\n");
                sb.Append("Comments: " + c.Comment + "\n");

                //start email Thread


                //msg.Body = sb.ToString();
                string message = sb.ToString();
                // var tEmail = new Thread(() =>
                try
                {
                    SendEmail(toAddress, fromAddress, subject, message);
                    //tEmail.Start();
                    return View("Success");
                }
                catch (Exception e)
                {
                    Response.Write(e.ToString());
                    return View("Failure");
                }
                //client.Send(msg);
                //msg.Dispose();
                // return View("Success");
                //}
                //catch (Exception)
                //{
                //    return View("Error");
                //}
            }
            return View();
        }


        public void SendEmail(string toAddress, string fromAddress,
                      string subject, string message)
        {
            try
            {
                using (var mail = new MailMessage())
                {
                    //const string email = "ryanbmcnutt@gmail.com";
                    //const string password = "!C25a47d2ec";

                    //var loginInfo = new NetworkCredential(email, password);


                    mail.From = new MailAddress(fromAddress);
                    mail.To.Add(new MailAddress(toAddress));
                    mail.Subject = subject;
                    mail.Body = message;
                    mail.IsBodyHtml = true;

                    try
                    {
                        //Configure an SmtpClient to send the mail.
                        SmtpClient client = new SmtpClient();
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.EnableSsl = false;
                        client.Host = "relay-hosting.secureserver.net";
                        client.Port = 25;

                        //Setup credentials to login to our sender email address ("UserName", "Password")
                        //NetworkCredential credentials = new NetworkCredential("user@ryanbmcnutt.com", "!C25a47d2ec");
                        //client.UseDefaultCredentials = true;
                        //client.Credentials = credentials;

                        //Send the msg
                        client.Send(mail);

                        /*
                        using (var smtpClient = new SmtpClient(
                                                         "smtp.gmail.com", 587))
                        {
                            smtpClient.Host = "relay-hosting.secureserver.net";
                            smtpClient.EnableSsl = true;
                            smtpClient.UseDefaultCredentials = false;
                            smtpClient.Credentials = loginInfo;
                            smtpClient.Send(mail);
                            
                        }
                        */
                    }

                    finally
                    {
                        //dispose the client
                        mail.Dispose();

                    }

                }
            }
            catch (SmtpFailedRecipientsException ex)
            {
                foreach (SmtpFailedRecipientException t in ex.InnerExceptions)
                {
                    var status = t.StatusCode;
                    if (status == SmtpStatusCode.MailboxBusy ||
                        status == SmtpStatusCode.MailboxUnavailable)
                    {
                        Response.Write("Delivery failed - retrying in 5 seconds.");
                        System.Threading.Thread.Sleep(5000);
                        //resend
                        //smtpClient.Send(message);
                    }
                    else
                    {
                        Response.Write("Failed to deliver message to {0}");//,
                                                                           // t.FailedRecipient);
                    }
                }
            }
            catch (SmtpException Se)
            {
                // handle exception here
                Response.Write(Se.ToString());
            }

            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }

        }
    }
}