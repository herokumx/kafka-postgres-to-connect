# kafka-postgres-to-connect
Heroku App written in ASP.NET Core that reads Heroku Kafka data from Postgres (public DB), searches for our "ClientID" (29481765) and when found, writes that to the Salesforce PG schema (for Heroku Connect)
