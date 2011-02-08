------- This is basic instructions on how to use this application for now -------

Step 1

Drop the database files into your sql server 2008 data folder and then attach the database using sql server management studio. Make sure your sql server is shut down before copying over the files. The database files are located in the databases folder.

Step 2

Modify the <property name="connection.connection_string"> in your web.config file with your specific database connection string.

Step 3

Build the project and then start up your web development server.

Test Account

email: admin@mail.com
password: coders

Test Urls

/
/users/all
/administration