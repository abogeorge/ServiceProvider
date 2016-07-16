## Description
--------------
* The project contains a C# solution for a basic service provider. See About section for more info.

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

Go to SQL Server, create a database e.g. service_provider_log and paste the code from [MS SQL Server section](https://logging.apache.org/log4net/release/config-examples.html). 

## About
--------

This application is meant as an exercise. A service provider can offer multiple types of subscriptions (mobile, data etc.). Every type has a certain price and can expire after a determined period, possible unknown in the beggining. Until the subscription expires the price remains a constant. For each type of subscription there's a number of included minutes, of multiple types (inside the network, international etc.). After the user uses all included minuttes, a custom extra charge is applied that remain a constant until the contract ends.The same goes for the short messages system. When a contract expires, it can be extended, if that subscription exists. The price can be regitered in multimple currencies (Euro, USD etc.), and for every month a currency rate to RON is applied.

As I said this application was meant as a exercise for me to get familiar with the following thigs. It uses:
* Test Driven Development
* Code First Entity generation
* Entity Framework
* MS SQL Server
* Static libraries building
* log4net
* Validation Application Block
* Layers development: Domain Model, Service Layer, Data Mapper. It does not contain any form of GUI.
* Singleton and Abstract Factory DP.
