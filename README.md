# mapguide-mvc-sample

The Sheboygan .net viewer sample ported over to ASP.net MVC

This is a work in progress and does not represent all of the existing .net code samples out there.

This sample should be considered nothing more than a basic guidance on how to use the MapGuide API from within the ASP.net MVC framework

# Requirements

 * MapGuide Open Source 2.6 (64-bit, using IIS/.net)
 * Visual Studio 2013 Community or higher

All other requirements will be pulled down via NuGet when you open/build the solution for the first time

# Running

After building the solution, go to http://localhost/SheboyganSample to enter the landing page. The landing page will also check if the required sample data is installed.
 
If the data is not loaded, there will be a link to a page where you can load the Sheboygan data package.

Once done. You can view the sample application in either the AJAX or Fusion viewer.

# Notes

This sample project will create and run on a local IIS virtual directory, so you will generally need to run Visual Studio as an administrator.

Also ensure the sample project runs under the same IIS application pool as MapGuide, otherwise the application may run on an incorrect .net Framework version or run on an incorrect bitness