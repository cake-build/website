Order: 25
---

Sometimes PowerShell prevents you from running build.ps1.  Here are some common scenarios and what to do about it.

# Assumed security settings

Run the following command to view your PowerShell security settings:

```powershell
PS> Get-ExecutionPolicy -List
```

The docs assume you have the following settings:

```powershell
        Scope ExecutionPolicy
        ----- ---------------
MachinePolicy       Undefined
   UserPolicy       Undefined
      Process       Undefined
  CurrentUser       Undefined
 LocalMachine    RemoteSigned
```

Effectively this is "RemoteSigned".  Depending on how you start PowerShell, you could have different settings.  You can read how to change your settings here [http://go.microsoft.com/fwlink/?LinkID=135170](http://go.microsoft.com/fwlink/?LinkID=135170).  Assuming the MachinePolicy and UserPolicy settings are Undefined, you can running this relatively safe command to get RemoteSigned security:

```powershell
PS> Set-ExecutionPolicy RemoteSigned -Scope Process
```

# Downloaded files from the internet

If you downloaded build.ps1 and you have the above settings, you still might get the following error:

```powershell
.\build.ps1 : File C:\Users\CakeUser\Documents\my_cake_proj\build.ps1 cannot be loaded. The file
C:\Users\CakeUser\Documents\my_cake_proj\build.ps1 is not digitally signed. You cannot run this script
on the current system. For more information about running scripts and setting execution policy, see
about_Execution_Policies at http://go.microsoft.com/fwlink/?LinkID=135170.
At line:1 char:1
+ .\build.ps1
+ ~~~~~~~~~~~
    + CategoryInfo          : SecurityError: (:) [], PSSecurityException
    + FullyQualifiedErrorId : UnauthorizedAccess
```

This error can occur when the file is "blocked".  The docs assume that the build.ps1 file is "unblocked".  The following command will unblock the file:

```powershell
PS> Unblock-File path\to\build.ps1
```

You can read more about unblocking files here [https://technet.microsoft.com/en-us/library/hh849924.aspx](https://technet.microsoft.com/en-us/library/hh849924.aspx).
