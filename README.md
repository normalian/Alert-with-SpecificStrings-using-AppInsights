# Alert-with-SpecificStrings-using-AppInsights
You probably confuse how to alert with specific strings when you use Azure PaaS Service( e.x. App Service, Cloud Service) especially OSS components. This sample enable you to alert with them using Application Insights, Azure Function and SendGrid. You can alert with this sample even if you use OSS components on Azure PaaS. 

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

Create "Azure Function" instance and open "Function app settings". Edit Environment variables in Azure Portal like below.
![Application Setting](https://raw.githubusercontent.com/normalian/Alert-with-SpecificStrings-using-AppInsights/master/img/ApplicationSetting.png "ApplicationSetting.png")

Setup SENDGRID_APIKEY, FROM_MAIL_ADDRESS, TO_MAIL_ADDRESS and ALERT_STRINGS. You can setup multi strings in ALERT_STRINGS variables such as "WEB01-E001;WEB01-E002".


And then, setup "Deploy - Configure continuous integration" and choose your repository project forked with this sample like below.
![Continuous Deployment](https://raw.githubusercontent.com/normalian/Alert-with-SpecificStrings-using-AppInsights/master/img/ContinuousDeployment.png "Continuous Deployment")

Finally, setup storage account via your Azure Function page like below.
![Storage Account Setup](https://raw.githubusercontent.com/normalian/Alert-with-SpecificStrings-using-AppInsights/master/img/SetupStrageAccount.png "Storage Account Setup")

Now, you can recieve alert from SendGrid.


## Reference
- https://libraries.io/nuget/Sendgrid
- https://docs.microsoft.com/azure/azure-functions/functions-bindings-storage-blob
- https://docs.microsoft.com/azure/application-insights/app-insights-export-telemetry

## Copyright
<table>
  <tr>
    <td>Copyright</td><td>Copyright (c) 2017- Daichi Isami</td>
  </tr>
  <tr>
    <td>License</td><td>MIT License</td>
  </tr>
</table>
