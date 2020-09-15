# Lost in Lublin
App uses **Facebook Graph API** to search on public facebook groups. App looking for keywords related with lost/found stuff somewhere in Lublin.

## ASP .NET Core API 
WebApi application communicates with predefined sites via Facebook API and saves indexed data to SQL DB.
Loaded data are exposed to client app by REST API. 
Application uses **Google Firebase** and **Azore Notification Hubs** to send push notification to client.

## Android
Client app made in Xamarin.Android, built on MVVM Cross.