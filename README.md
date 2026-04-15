# 🔗 SCSM Integration API

> A modern REST API to quickly and easily integrate **System Center Service Manager (SCSM)** functionalities with other systems — removing the complexity of traditional integrations.

![.NET](https://img.shields.io/badge/.NET_9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![PowerShell](https://img.shields.io/badge/PowerShell-5391FE?style=for-the-badge&logo=powershell&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge)

---

## 📸 Preview

![Swagger UI](docs/images/swagger-ui.png)
![API Endpoint Example](docs/images/api-preview.png)


---

## 📋 Overview

**SCSM Integration API** is a C# solution that exposes **System Center Service Manager** functionalities through a modern REST interface, allowing external systems and teams to consume them simply — without requiring any knowledge of SCSM's internal complexity.

### What does it do?
- Create, query, and update **Work Items** (Incidents, Service Requests, etc.) in SCSM from any external system.
- Execute embedded **PowerShell scripts** in a controlled and secure manner through the API.
- Simplify integrations without requiring SCSM client installation on consuming systems.

---

## 🏗️ Architecture

The solution is composed of **three projects** that work together:

```
SCSMIntegrationAPI/
│
├── 📦 OPENSCSMModel/        ← Model Project (.NET Framework 2.0)
│   └── Shared classes, entities, and DTOs used across all projects.
│
├── ⚙️  OPENSCSMAccess/      ← Worker Project (.NET Framework 4.8.1)
│   └── Execution and management of embedded PowerShell scripts.
│
└── 🌐 OPENSCSMAAPI/          ← REST API Project (.NET 9.0 / ASP.NET Core Minimal API)
    └── HTTP interface for communication with external systems.
```

### Flow Diagram

```
External System
      │
      ▼
┌─────────────────┐
│   REST API      │  ← ASP.NET Core / Minimal API (.NET 9.0)
│  (HTTP/HTTPS)   │
└────────┬────────┘
         │     ▲
         │     │
		 │  ┌──┴──────────────┐
		 │  │  Model / DTOs   │  ← .NET Framework 2.0
		 │  └──┬──────────────┘
		 │     │	 
         ▼	   ▼	 
┌─────────────────┐
│  Worker Service │
│  (PowerShell)   │  ← .NET Framework 4.8.1
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│      SCSM       │
└─────────────────┘
```

---

## ✅ Prerequisites

Before installing and running the project, make sure you have the following:

### Required Software
 
| Component                     | Minimum Version          | Notes                                    |
|-------------------------------|--------------------------|------------------------------------------|
| Windows Server / Windows 10+  | —                        | Required by SCSM                         |
| .NET Framework                | 4.8.1                    | For the Worker project                   |
| .NET SDK                      | 9.0                      | For the API project                      |
| PowerShell                    | 5.1+                     | For script execution                     |
| Visual Studio                 | 2022+                    | For building and development             |
| System Center Service Manager | 2016 / 2019 / 2022 / 2025| Must be installed and network-accessible |
| SMLets 2016                   | 1.0                      | For building, development and production |

### Required Permissions
- Read/write access to the SCSM database.
- Permission to run PowerShell cmdlets from the SCSM module.
- Open port for the API (default: `5000` for HTTP / `5001` for HTTPS).

---

## 🚀 Installation

See the [INSTALL.md](INSTALL.md) file for detailed installation and configuration instructions for each component.

---

## 📁 Repository Structure

```
/
├── README.md                          ← This file
├── INSTALL.md                         ← General installation guide
│
├── OPENSCSMModel/
│   └── ...
│
├── OPENSCSMAccess/
│   └── ...
│
├── OPENSCSMAAPI/
│   ├── OPENSCSMAAPI.sln             ← Visual Studio solution
│   └── ...
│
└── docs/
    └── images/                        ← Screenshots and visual assets
```

---

## 🤝 Contributing

Contributions are welcome! Please open an **Issue** before submitting a Pull Request to discuss proposed changes.

1. Fork the repository.
2. Create a branch: `git checkout -b feature/new-feature`
3. Commit your changes: `git commit -m 'Add new feature'`
4. Push to the branch: `git push origin feature/new-feature`
5. Open a Pull Request.

---

## 📄 License

This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for details.

---

## 👤 Credits

Developed by **[René Vázquez]**  
Contact: [revazquezt@hotmail.com](mailto:revazquezt@hotmail.com)  
GitHub: [@revazquezt](https://github.com/revazquezt)
