SET mypath=%~dp0
SET mypath=%mypath:~0,-1%


%systemroot%\system32\inetsrv\appcmd.exe add apppool /name:"Phalanger v4.0"

%systemroot%\system32\inetsrv\appcmd.exe set apppool "Phalanger v4.0" /managedRuntimeVersion:v4.0 /managedPipelineMode:Integrated 

%systemroot%\system32\inetsrv\appcmd.exe add app /site.name:"Default Web Site" /path:/wordpress /physicalpath:"%mypath%"
%systemroot%\system32\inetsrv\appcmd.exe set app /app.name:"Default Web Site/wordpress" /applicationPool:"Phalanger v4.0"