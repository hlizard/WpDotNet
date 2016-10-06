WP.NET - The most popular CMS is now running on .NET/Mono

This package contains actual source code of WordPress and Phalanger - the PHP compiler for .NET and Mono. This makes it fastest way how to run WordPress and enables developers to use not just PHP, but also any other .NET language (e.g. C#) to extend WordPress.

WP.NET is fully managed ASP.NET application capabable of running 64bit under Windows or Linux.

System Requirements:
 - ASP.NET 4.0 or Mono 2.10.8 or higher
 - IIS 7.0 or Apache
 - MySql 5.0 or higher

System Recommandation:
 - for IIS: IIS URL Rewrite 2.0 (http://www.iis.net/download/urlrewrite)


IIS Instalation recommandation:
 - run permissions.bat - sets permission for IIS_IUSRS for this folder and all subfolders
 - run iis_setup.bat - sets "Phalanger v3.0" apppool in your IIS and asociate localhost/wordpress url with this wp installation
 - just access localhost/wordpress and folow traditional 5-minut installation of WordPress


Automatic Update of Phalanger
 - WP.NET contains plugins that recognize when new Phalanger is available and installs it on your system


Web.config
 - as ASP.NET application it needs to contain web.config where are necessary configuration for .NET and Phalanger


Support:
 - if you have any question or suggestion feel free to contact us at http://support.devsense.com 


License:
WP.NET is free software, and is released under the terms of the GPL version 2 or (at your option) any later version.
See license.txt
