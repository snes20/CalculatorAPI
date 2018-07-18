Calculator API:

Calculator WEB API based on .Net Core 2.0 with in memory DB (NMemory.net) and error log using Easy.Logger 


Deployment Instructions for IIS:

1.- Create a destination folder i.e. C:\CalculatorService  
2.- Copy the following files in the folder created in the previous step:  
	.-CalculatorService.dll  
	.-SQLDatabase.Net.dll  
	.-log4net.config  
	
3.- Create a folder for the Logs  

4.- Edit the file log4net.config. Look for the "file" tag in the xml, and modify the path i.e. [folder]/[filename], and verify the writing permissions for that folder.   

5.- Install the .NET Core modules for IIS following the directions described in the link located in the References section   

6.- Open de IIS control panel  

7.- Select the current server, and go to the "Application Pool" section  

8.- Create a new Application pool for the Application and give any name i.e CalculatorService_pool and select "No Managed Code" in the application pool that you are creating.   

9.- Scrool down into the "Sites" section of the IIS  

10.-Right click and select Add Website, select the folder where you copied the files and select the application pool that you just created and click OK.  

11.-Check the deployment. Open the Browser and go to http://localhots:[port]/calculator/index  


References:  
https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/?view=aspnetcore-2.1&tabs=aspnetcore2x  
http://nmemory.net/overview  
https://github.com/NimaAra/Easy.Logger  


	
