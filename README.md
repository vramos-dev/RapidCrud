# RapidCrud

Installing

This project is still in initial development, so there's nothing to install at the moment :)

But when there would be a first release, then it would be something like this:

1. Deploy the database using a dacpac file, or by restoring from a backup, or by running a TSQL script.
2. Copy the website folder to a subfolder in c:\inetpub\wwwroot
3. Update the connection strings in the configuration file (web.config)
4. If your connection strings use Windows Authentication, you'll need to configure anonymous authentication in IIS manager:
  4.1 Open IIS Manager
  4.2 Find your site under the sites list
  4.3 Under the "IIS" section, open the "Authentication" page
  4.4 Make sure "Anonymous Authentication" is enabled
  4.5 Click on "Anonymous Authentication" and Edit it
  4.6 Configure the username and password of a specific Windows user to be impersonated by every visitor
  4.7 In the database, create a Windows Authentication login for the above user, and give it permissions as necessary (i.e. read+write+execute)
5. Other authentication methods are also available in IIS, such as Windows Authentication, Forms, URL and more. But those are more advanced and far beyond the scope of this project.
6. That's it! The site should be immediately operational and you should be able to start using it and constructing data views.
