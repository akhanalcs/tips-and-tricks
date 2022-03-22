# Running a .NET core web app in Azure Container Apps

### Step 1:
Create resource group in Azure. 

### Step 2
Create container registry in Azure.

### Step 3
Create Log Analytics workspace for the containers in Azure.

<img src="https://user-images.githubusercontent.com/30603497/159082707-f6e3e328-6bb4-41d1-b228-6dbdbeb507d1.png" style="width: 50%;max-height: 50%" />

### Step 4
Create a key vault and secrets (key value pairs) you may need in Azure.

### Step 5
Go to Azure DevOps and create a Variable group (You can link the Key vault from Step 3 here if you wish to do so).

Grab the credentials for the registry created in Step 2.

<img src="https://user-images.githubusercontent.com/30603497/159083597-822f75df-8c3d-493a-a272-ff386f16a6b3.png" style="width: 50%;max-height: 50%" />

Store the credentials inside _demo-app-DEPLOYMENT_ variable group.

<img src="https://user-images.githubusercontent.com/30603497/159363404-00fddb15-6760-4e14-bbb5-1969c845285c.png" style="width: 50%;max-height: 50%" />

<img src="https://user-images.githubusercontent.com/30603497/159363803-bb13e147-c2ca-4f7b-859a-4f10082c1b04.png" style="width: 50%;max-height: 50%" />

Note: I've already refreshed my password shown in the screenshot so you won't be able to connect to my registry. :)

### Step 6
Create different environments: DEV and PROD

<img src="https://user-images.githubusercontent.com/30603497/159365873-58bdf243-0e18-4f57-ba69-47e4dab77ac0.png" style="width: 50%;max-height: 50%" />

<img src="https://user-images.githubusercontent.com/30603497/159366399-725697e9-4289-44fc-a275-d536d87d9ea6.png" style="width: 50%;max-height: 50%" />

### Step 7
Create a service connection that enables Azure Pipelines to access our Azure subscription

Azure DevOps -> Project settings -> Service Connections -> Create Service Connection -> Search 'Azure Resource Manager'

<img src="https://user-images.githubusercontent.com/30603497/159367822-098ff25a-66a1-4cc3-ae5c-f3d67adbbf6c.png" style="width: 50%;max-height: 50%" />

Next -> Service Principal (Automatic)

<img src="https://user-images.githubusercontent.com/30603497/159368901-66a7a1bc-b501-4d7a-b59a-0d08bb35e9a8.png" style="width: 30%;max-height: 20%" />

<img src="https://user-images.githubusercontent.com/30603497/159512090-069eeef0-2c39-4f9e-b066-28be97ed694d.png" style="width: 50%;max-height: 50%" />



| Syntax | Description |
| ----------- | ----------- |
| Header | Title |
| Paragraph | Text |


### Step 7
Create a pipeline
