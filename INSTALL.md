# 📦 Installation Guide — SCSM Integration API

This guide describes the steps required to install and configure the complete solution, including all three projects.

---

## 📋 Table of Contents

1. [General Prerequisites](#1-general-prerequisites)
2. [Clone the Repository](#2-clone-the-repository)
3. [Model Project Installation](#3-model-project-installation-net-framework-20)
4. [Worker Project Installation](#4-worker-project-installation-net-framework-481)
5. [REST API Project Installation](#5-rest-api-project-installation-net-90)
6. [Final Verification](#6-final-verification)
7. [Troubleshooting](#7-troubleshooting)

---

## 1. General Prerequisites

Make sure the following are installed on the target server or machine before proceeding:

| Component                     | Version                            | Download                                                                        |
|-------------------------------|------------------------------------|---------------------------------------------------------------------------------|
| Windows OS                    | Windows Server 2016+ / Windows 10+ | —                                                                               |
| .NET Framework                | 4.8.1                              | [Download](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net481) |
| .NET SDK                      | 9.0                                | [Download](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)              |
| PowerShell                    | 5.1 or higher                      | Included in Windows                                                             |
| Visual Studio                 | 2022 (optional, for development)   | [Download](https://visualstudio.microsoft.com/)                                 |
| Git                           | Any recent version                 | [Download](https://git-scm.com/)                                                |
| System Center Service Manager | 2016 / 2019 / 2022 / 2025          | License required                                                                |
| SMLets                        | 1.0                                | [Download](https://github.com/SMLets/SMLets/releases/)                          |

> ⚠️ **Important:** The server hosting the Worker must have network access to the SCSM server, and the SCSM PowerShell module cmdlets must be available.

---

## 2. Clone the Repository

```bash
git clone https://github.com/yourusername/scsm-integration-api.git
cd scsm-integration-api
```

Or download the ZIP directly from GitHub and extract its contents.

---

## 3. Model Project Installation (.NET Framework 2.0)

This project contains the shared classes, entities, and DTOs. **It does not require independent installation** — it is a dependency that the other two projects reference automatically when building the solution.

### Steps

1. Open `SCSMIntegrationAPI.sln` in Visual Studio 2022.
2. Verify that the `SCSMIntegrationAPI.Model` project builds without errors:
   ```
   Right-click on the project → Build
   ```
3. Confirm the output file is generated at:
   ```
   SCSMIntegrationAPI.Model/bin/Debug/SCSMIntegrationAPI.Model.dll
   ```

> ℹ️ No separate deployment is needed. The Worker and API projects reference it directly.

---

## 4. Worker Project Installation (.NET Framework 4.8.1)

This project is responsible for executing embedded PowerShell scripts in a controlled manner.

### 4.1 Build

1. In Visual Studio, right-click `SCSMIntegrationAPI.Worker` → **Publish** (or **Build** for development).
2. Select a publish folder, for example:
   ```
   C:\Services\SCSMWorker\
   ```

### 4.2 Configuration

1. Edit the `appsettings.json` file located in the Worker directory:

```json

{
  "SCSM": {
    "Server": "YOUR_SCSM_SERVER_NAME",
    "Enviroment": "YOUR_SCSM_SERVICE_NAME",  
    "ControlerDomain": "YOUR_DOMAIN",  
    "SdkPath": "YOUR_SCSM_SDK_PATH"
  },
  "Database": {
    "Name": "YOUR_DATABASE_SCSM_NAME",
    "Server": "YOUR_SERVER_DATABASE_SCSM_NAME",
    "ConnectionString": "YOUR_CONNECTION_STRING"
  },
  "Paths": {
    "Scripts": "YOUR_PATH_4_SCRIPTS",
    "Logs": "YOUR_PATH_4_LOGS"
  },
  "Worker": {
    "Url": "YOUR_WORKER_URL"
  },

  "AllowedHosts": "*"
}


```

2. Verify that the SCSM PowerShell module can be imported:
```powershell
Import-Module SMLets ;

```

### 4.3 Register as a Windows Service (Optional)

To run the Worker as a system service:

```powershell
sc create "SCSMWorkerService" binPath= "C:\Services\SCSMWorker\SCSMIntegrationAPI.Worker.exe" start= auto
sc start "SCSMWorkerService"
```

---

## 5. REST API Project Installation (.NET 9.0)

This project exposes the HTTP endpoints so that other systems can consume SCSM functionalities.

### 5.1 Build and Publish

From the terminal, inside the API project folder:

```bash
cd SCSMIntegrationAPI.Api

# Restore dependencies
dotnet restore

# Build
dotnet build --configuration Release

# Publish
dotnet publish --configuration Release --output ./publish
```

### 5.2 Configuration

Edit the `appsettings.json` file in the API project folder:

```json

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "WorkerUrlCatalogs": "YOUR_URL_4_CATALOGS",
  "WorkerUrlSrCatalogs": "YOUR_URL_4_SR_CATALOGS",
  "WorkerUrlToolsExec": "YOUR_URL_4_TOOLS",
  "WorkerUrlExec": "YOUR_URL_4_EXEC"
}
```

### 5.3 Run

**Development mode:**
```bash
dotnet run --project SCSMIntegrationAPI.Api
```

**Production mode (from publish folder):**
```bash
dotnet SCSMIntegrationAPI.Api.dll
```

The API will be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger UI: `https://localhost:5001/swagger`

### 5.4 Register as a Windows Service (Production)

```powershell
sc create "SCSMApiService" binPath= "dotnet C:\Services\SCSMApi\SCSMIntegrationAPI.Api.dll" start= auto
sc start "SCSMApiService"
```

> 💡 **Recommended alternative:** Use **IIS** with the ASP.NET Core Hosting Bundle for enterprise production environments.

---

## 6. Final Verification

Once all three components are installed, verify everything is working:

1. **Worker running:**
   ```powershell
   Get-Service "SCSMWorkerService"
   # Status should be: Running
   ```

2. **API responding:**
   ```bash
   curl http://localhost:5000/health
   # Expected response: {"status":"Healthy"}
   ```

3. **Swagger UI accessible:**  
   Open in browser: `https://localhost:5001/swagger`

4. **Basic endpoint test:**
   ```bash
   curl -X GET "https://localhost:5001/api/workitems" \
        -H "Authorization: Bearer YOUR_JWT_TOKEN"
   ```

---

## 7. Troubleshooting

| Problem | Likely Cause | Solution |
|---|---|---|
| Worker cannot connect to SCSM | Wrong credentials or no network access | Check `ServerName` and credentials in `appsettings.json` |
| `Could not load file or assembly` error in Worker | Incorrect reference to Model project | Rebuild the entire solution from Visual Studio |
| API returns `503 Service Unavailable` | Worker is not running | Start the Worker service first |
| SSL certificate error | Development certificate not trusted | Run `dotnet dev-certs https --trust` |
| PowerShell: `Import-Module` fails | SCSM module not installed | Install the SCSM Administration Console on the server |

---

## 📬 Support

If you encounter any issues during installation, please open an [Issue on GitHub](https://github.com/yourusername/scsm-integration-api/issues) with the error details.
