## Description
--------------
* This project contains a C# solution for a basic service provider. See About section for more info.

## Setup
--------
#### Prerequisites
* Visual Studio (tested with Visual Studio 2015 Enterprise)
* SQL Server (tested with SQL Server 2014 Express)

#### Editing data connection for the database and Logger
###### Data Base:
* ServiceProvider\DataMapper\App.config
* ServiceProvider\ServiceProviderUnitTests\App.config

Edit <code>&lt;connectionStrings&gt;</code> to fit your requirements.

* Logger:

Since this application is using log4net for parallel logging (file and database) you need to edit the next files:

ServiceProvider\ServiceLayer\log4.config

Under <code>&lt;appender name="FileAppender" ... &gt; &lt;file value="D:\\log.txt" /&gt;</code>, add logging location. The connection from App.config to log4.config is already configured so you don't have to worry about it.

Under <code>&lt;appender name="AdoNetAppender" ... &gt; &lt;connectionString value= /&gt;</code>, add database connection.

Go to SQL Server, create a database e.g. service_provider_log and paste the following code:

<code>CREATE TABLE [dbo].[Log] (</code>
<code>    [Id] [int] IDENTITY (1, 1) NOT NULL,</code>
<code>    [Date] [datetime] NOT NULL,</code>
<code>    [Thread] [varchar] (255) NOT NULL,</code>
<code>    [Level] [varchar] (50) NOT NULL,</code>
<code>    [Logger] [varchar] (255) NOT NULL,</code>
<code>    [Message] [varchar] (4000) NOT NULL,</code>
<code>    [Exception] [varchar] (2000) NULL</code>
<code>)</code>

## About
--------
