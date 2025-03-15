-------------------------------------- Project Setup Guidelines--------------------------------------------------

Author: Yashoman Wickramasinghe  
Date: March 15, 2025  

--------------------Prerequisites-------------------------------------------

Ensure you have the following installed on your system:
-Node.js (for React project)
-VS Code (or any preferred code editor)
-.NET Core 7.0 (for Web API project)
-SQL Server (or any compatible database system)


--------------------Frontend Setup (React)----------------------------------
1.Download the entire project folder./And Use the link to download the React Project: https://github.com/Yashoman-Wickramasinghe/ReactJs_CRUD_DotNET_WebAPI.git
2.Open the React project in VS Code.
3.Install dependencies: npm install  
4.Run the project: npm start


--------------------Backend Setup (Web API - .NET Core 7.0)------------------
1.Open the Web API project in Visual Studio.
2.Configure the database:
    - Open 'appsettings.json'.
    - Ensure the database connection string is correctly set up.
    -Create a database with the same name or modify the connection string to match an existing database.
3.Handle Migrations:
    -This project does not include database initialization.
    -Remove existing migration files and create fresh migrations.
    -Open the Package Manager Console in Visual Studio and run the following commands:
     	add-migration "Initial"
     	update-database


Two links to Download Projects:
React: https://github.com/Yashoman-Wickramasinghe/ReactJs_CRUD_DotNET_WebAPI.git
WebAPI: https://github.com/Yashoman-Wickramasinghe/crud_web_api.git


