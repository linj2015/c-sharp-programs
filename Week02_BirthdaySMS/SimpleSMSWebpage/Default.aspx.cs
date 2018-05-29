// Week 02
/* 
 * https://www.twilio.com/docs/sms/quickstart/csharp
 * This code creates a new instance of the Message resource and sends an 
 * HTTP POST to the Messages resource URI.
 */

using System;
using System.Web.UI;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace SimpleSMSWebpage
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Configuration.Configuration rootWebConfig1 =
                System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/");
            if (rootWebConfig1.AppSettings.Settings.Count > 0)
            {
                System.Configuration.KeyValueConfigurationElement accountSidSetting =
                    rootWebConfig1.AppSettings.Settings["myAccountSid"];
                System.Configuration.KeyValueConfigurationElement authTokenSetting =
                    rootWebConfig1.AppSettings.Settings["myAuthToken"];
                if (accountSidSetting != null && authTokenSetting != null)
                {
                    System.Diagnostics.Debug.Print("accountSidSetting application string = \"{0}\"",
                        accountSidSetting.Value);
                    TwilioClient.Init(accountSidSetting.Value, authTokenSetting.Value);
                }
                else
                {
                    System.Diagnostics.Debug.Print("Account sid or auth token not found");
                }
            }
        }

        protected void ButtonSend_Click(object sender, EventArgs e)
        {
            // TODO: validate phone number format
            var to = new PhoneNumber("+1" + TextBoxPhone.Text);
            var message = MessageResource.Create(
                to,
                from: new PhoneNumber("+14152126511"),
                body: TextBoxMsg.Text);
            System.Diagnostics.Debug.WriteLine(message.Sid);
        }
    }
}