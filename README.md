#Heroku (Kafka) Postgres-to-Heroku Connect Mapper

<br>
This demo application is written in ASP.NET Core that reads Heroku Kafka data from Postgres (public DB), searches for our messages with "ClientID" (29481765) and when found, writes those records to the Salesforce PG schema for Heroku Connect to sync it to our Salesforce Contact record. It has 2 brother apps, one which generates the simulated Kafka Web Traffic Messages and one which pulls the Kafka Messages off the topic and persists them to Heroku Postgres.<br><br>
Obviously you need Heroku Kafka in order to use this app, as well as the following Heroku App Config VARs:<br>
<br><p>
- ClientID (if using all 3 apps' defaults, should be set to our "Tracking ID" of 29481765)
- DATABASE_URL (should be set to the sames Heroku Postgres URL as our 2nd app that reads from Kafka and writes to Postgres)
<br><p>

Deploy this App to Heroku by clicking the button below:

<a href="https://heroku.com/deploy?template=https://github.com/herokumx/kafka-postgres-to-connect"> <img src="https://www.herokucdn.com/deploy/button.svg" alt="Deploy">
</a>
