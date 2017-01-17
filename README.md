# Alert-with-SpecificStrings-using-AppInsights
You probably confuse how to alert with some logs when you use Azure PaaS Service( e.x. App Service, Cloud Service) especially OSS components. This sample enable you to alert with specific strings using Application Insights. 
A feature of Application Insights called "Continuous export" can export its log data into Azure Blob Storage. 

## Requirement
- SendGrid account
- Azure Function
- Github account

## How to use this sample?
Create "Application Insights" instance and setup continuous export like below.
![Continuous Export](https://raw.githubusercontent.com/normalian/Alert-with-SpecificStrings-using-AppInsights/master/img/ContinuousExport.png "Continuous Export")

Fork this sample into your repository and edit AlertBlobTrigger/function.json. Edit from 

```csharp:function.json
- "path": "<your container name>/<your appinsights name and subscription id>/Messages/{date}/{time}/{name}",
```

to like below.

```csharp:function.json
- "path": "logs/diagnosticwebapp_8a0933360f7c4585a965efc81905e5fe/Messages/{date}/{time}/{name}",
```

And then, edit variables in AlertBlobTrigger/run.csx.

```csharp:run.csx
using SendGrid;
using SendGrid.Helpers.Mail;

const string SENDGRID_APIKEY = "[SENDGRID APIKEY]";
const string FROM_MAIL_ADDRESS = "FROM MAIL ADDRESS. e.g. daisami@microsoft.com";
const string TO_MAIL_ADDRESS = "TO MAIL ADDRESS. e.g. daisami@microsoft.com";
const string ALERT_STRING = "string for alert. e.g. WEB01-E001";

```


Create "Azure Function" instance and open "Function app settings". Setup "Deploy - Configure continuous integration" and choose your repository project forked with this sample like below.
![Continuous Deployment](https://raw.githubusercontent.com/normalian/Alert-with-SpecificStrings-using-AppInsights/master/img/ContinuousDeployment.png "Continuous Deployment")

Finally, setup storage account via your Azure Function page like below.
![Storage Account Setup](https://raw.githubusercontent.com/normalian/Alert-with-SpecificStrings-using-AppInsights/master/img/SetupStrageAccount.png "Storage Account Setup")

Now, you can recieve alert from SendGrid.
