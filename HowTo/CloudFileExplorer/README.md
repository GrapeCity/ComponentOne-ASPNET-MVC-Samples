## ASP.NET MVC Core Cloud File Explorer
#### [Download as zip](https://downgit.github.io/#/home?url=https://github.com/GrapeCity/ComponentOne-ASPNET-MVC-Samples/tree/master/HowTo/CloudFileExplorer)
____
#### This sample demonstrates how to use Cloud API in Web API to make a simple file explorer.
____
For now, sample suport OneDrive, Dropbox, Google Drive, AWS, Azure clouds.

To run CloudFileExplorer, you need to remove readonly property  for all loudFileExplorer folder. Need setup all information to connect to your clouds in file Web.config.
You also need to fill all "InitialPath" key, for example, if your folder in Dropbox cloud has path is DropBox/C1WebApi/test1 then your "DropBoxInitialPath" need value "C1WebApi/test1" to start.



* Azure :
You need to use your account to update the Azure connection string in the sample by following these steps:

step 1: login to your Azure account.

step 2: create a container and generate connection string for this container.

step 3: use the connection string created in step 2 to put into AzureStorageConnectionString key value in the Web.config.
 


* AWS:
step 1: you need prepare value for following 3 keys in the Web.config.
AccessToken
SecretKey
BucketName 

DropBox :
step 1: login to your DropBox acount.

step 2: create an App and generate Access Token for this app.

step 3: use Access Token  created at step 2 to put into DropBoxStorageAccessToken key value in the Web.config.


GoogleDrive:
step 1: login to your GoogleDrive acount.

step 2: create an App and generate credentials.json file for this app.

step 3: usecredentials.json  created at step 2 to put into sample folder.


OneDrive :

step 1: login to your OneDrive acount.

step 2: browse this link for access token
"https://login.live.com/oauth20_authorize.srf?client_id=000000004C16A865&scope=onedrive.readwrite&response_type=token".

step 3: use Access Token  created at step 2 to put into OneDriveAccessToken
key value in the Web.config.
