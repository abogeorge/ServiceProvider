## Description
--------------
* This project contains a C# solution for a basic service provider. See About section for more info.

## Setup
--------
#### Prerequisites
* Visual Studio (tested with Visual Studio 2015 Enterprise)
* SQL Server (tested with SQL Server 2014 Express)

#### Editing data connection for the database and Logger
** Data Base:
- ServiceProvider\DataMapper\App.config
- ServiceProvider\ServiceProviderUnitTests\App.config
Edit <connectionStrings> to fit your requirements.

** Logger:
Since this application is using log4net for parallel logging (file and database) you need to edit the next files:
- ServiceProvider\ServiceLayer\log4.config
Under <appender name="FileAppender" ... > <file value="D:\\log.txt" />, add logging location.
- ServiceProvider\ServiceLayer\app.config

- ServiceProvider\ServiceProviderUnitTests\App.config

