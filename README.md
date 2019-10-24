# demo-api-stuff


## Getting started

If running through Visual Studio set the docker compose project as the start project, this should give you the option to launch it via docker compose from within VS using the "play" button.

If you're not using VS and you can't do the same thing in Rider; use the following command in powershell or whatever the Linux equivalent is.

docker-compose up


This will kick start everything and you should be able to access the api on https://localhost:44318

## Endpoints

We currently have three endpoints

### /weatherforecast

This just a Get call, it will add some data to the datastore and send back everything that has been added. Keep refreshing to see more added.

### /api/profile

We have a Get and a Post for this guy.

The Get retrieves all the added users.

The Post allows you to add a user, using a body as follows:

{  
    "Username" : "SomeUsername"  
}