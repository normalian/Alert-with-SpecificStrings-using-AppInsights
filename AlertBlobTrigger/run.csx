using Microsoft.Azure.Search;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using SendGrid;
using SendGrid.Helpers.Mail;

const string SENDGRID_APIKEY = "[SENDGRID APIKEY]";
const string FROM_MAIL_ADDRESS = "FROM MAIL ADDRESS. e.g. daisami@microsoft.com";
const string TO_MAIL_ADDRESS = "TO MAIL ADDRESS. e.g. daisami@microsoft.com";
const string ALERT_STRING = "string for alert. e.g. WEB01-E001";

public static void Run(TextReader myBlob, string name, TraceWriter log)
{
    log.Info($"C# Blob trigger function Processed blob\n Name:{name}");
    dynamic sg = new SendGridAPIClient(SENDGRID_APIKEY);
    
    string line;
    while ((line = myBlob.ReadLine()) != null)
    {
        log.Info("Raw Message = " + line);
        var messageModel = JsonConvert.DeserializeObject<AIMessageModel>(line);
        foreach (var message in messageModel.message)
        {
            log.Info(message.raw);        
            if(message.raw.Contains(ALERT_STRING))
            {
                log.Info("This is alert message.");

                Email mailFrom = new Email(FROM_MAIL_ADDRESS);
                string subject = "This is alert message from Application Insights!";
                Email to = new Email(TO_MAIL_ADDRESS);
                Content content = new Content("text/plain", message.raw);
                Mail mail = new Mail(mailFrom, subject, to, content);

                sg.client.mail.send.post(requestBody: mail.Get());
            }
        }
    }
}


public class AIMessageModel
{
    public Message[] message { get; set; }
    public Internal _internal { get; set; }
    public Context context { get; set; }
}

public class Internal
{
    public Data data { get; set; }
}

public class Data
{
    public string id { get; set; }
    public string documentVersion { get; set; }
}

public class Context
{
    public Data1 data { get; set; }
    public Cloud cloud { get; set; }
    public Device device { get; set; }
    public User user { get; set; }
    public Session session { get; set; }
    public Operation operation { get; set; }
    public Location location { get; set; }
}

public class Data1
{
    public DateTime eventTime { get; set; }
    public bool isSynthetic { get; set; }
    public float samplingRate { get; set; }
}

public class Cloud
{
}

public class Device
{
    public string type { get; set; }
    public string roleName { get; set; }
    public string roleInstance { get; set; }
    public Screenresolution screenResolution { get; set; }
}

public class Screenresolution
{
}

public class User
{
    public string anonId { get; set; }
    public bool isAuthenticated { get; set; }
}

public class Session
{
    public string id { get; set; }
    public bool isFirst { get; set; }
}

public class Operation
{
    public string id { get; set; }
    public string parentId { get; set; }
    public string name { get; set; }
}

public class Location
{
    public string clientip { get; set; }
    public string continent { get; set; }
    public string country { get; set; }
}

public class Message
{
    public string raw { get; set; }
}
