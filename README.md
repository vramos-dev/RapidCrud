# RapidCrud

Installing

This project is still in initial development, so there's nothing to install at the moment :)

But when there would be a first release, then it would be something like this:

1. Deploy the database using a dacpac file, or by restoring from a backup, or by running a TSQL script.
2. Copy the website folder to a subfolder in c:\inetpub\wwwroot
3. Update configuration file (web.config) </br>
  3.1 Update connection strings.  </br>
  3.2 Update seller email: "EmailVendedor". </br>
  3.3 Update seller phone number: "TelefonoVendedor". </br>
  3.4 Update seller token whatsapp: "TokenWhatsapp". Help link to configure: https://www.youtube.com/watch?v=eOx5Zbcl_DI </br>
  3.5 Update notifications email: "EmailFrom". </br>
  3.6 Update notifications email password: "EmailFromPassword". Help link to configure: https://support.google.com/accounts/answer/6010255?hl=es&visit_id=638029669780501635-3948193125&p=less-secure-apps&rd=1  </br>
4. If your connection strings use Windows Authentication, you'll need to configure anonymous authentication in IIS manager: </br>
  4.1 Open IIS Manager </br>
  4.2 Find your site under the sites list </br>
  4.3 Under the "IIS" section, open the "Authentication" page </br>
  4.4 Make sure "Anonymous Authentication" is enabled </br>
  4.5 Click on "Anonymous Authentication" and Edit it </br>
  4.6 Configure the username and password of a specific Windows user to be impersonated by every visitor </br>
  4.7 In the database, create a Windows Authentication login for the above user, and give it permissions as necessary (i.e. read+write+execute) </br>
5. Other authentication methods are also available in IIS, such as Windows Authentication, Forms, URL and more. But those are more advanced and far beyond the scope of this project.
6. That's it! The site should be immediately operational and you should be able to start using it and constructing data views.
