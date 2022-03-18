# Running a .NET core web app in Azure Container Apps

### Step 1:
Create resource group in Azure. 

### Step 2
Create container registry in Azure.

### Step 3
Create a key vault and secrets (key value pairs) you may need in Azure.

### Step 4
Go to Azure DevOps and create a Variable group (You can link the Key vault from Step 3 here if you wish to do so).

Grab the credentials for the registry created in Step 2.

<img src="https://user-images.githubusercontent.com/30603497/159083597-822f75df-8c3d-493a-a272-ff386f16a6b3.png" style="width: 50%;max-height: 50%" />

Store the credentials inside this variable group.

<img src="https://user-images.githubusercontent.com/30603497/159084026-88185d76-a408-42b8-8c4e-9e0aa82a2fd5.png" style="width: 50%;max-height: 50%" />

Note: I've already refreshed my password shown in the screenshot so you won't be able to connect to my registry. :)

### Step 5
Create Log Analytics workspace for the containers in Azure.

<img src="https://user-images.githubusercontent.com/30603497/159082707-f6e3e328-6bb4-41d1-b228-6dbdbeb507d1.png" style="width: 50%;max-height: 50%" />

### Step 6
Create Docker registry service connection in Azure DevOps
Azure DevOps -> Project settings -> Service Connections -> Create Service Connection -> Search 'Docker Registry'

<img src="https://user-images.githubusercontent.com/30603497/159085625-602a23ed-b92d-44e8-bdbb-cb3270d2e9c7.png" style="width: 50%;max-height: 50%" />

### Step 7
Create a pipeline
