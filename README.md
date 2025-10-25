# Obelyx
**Obelyx** is a self-hosted game backlog manager that helps users organize, track, and view their game collections.

![Screenshot of Obelyx](Docs/screenshot.png)

## Overview
**Obelyx** is a locally hosted suite of tools designed to help users manage their game libraries and keep track of what they’re playing, finished, or planning to start, similar in spirit to *MyAnimeList* or *IMDb*, but focused on games.

**Obelyx** lets users:
* Log and categorize their games
* Track backlog status and other useful stats
* Record personal notes or thoughts
* Search and filter through their collection

## Tech Stack
### Backend
- **C#**
- **.NET 8**
- **SQL Server**
- **REST API architecture**

### Frontend
- **React (Vite)**
- **JavaScript**
- **Sass**

## Project Goals
The goal of **Obelyx** is to provide a locally hosted and customizable way to manage your game backlog without relying on third-party websites or cumbersome spreadsheets.

**Obelyx** is built with privacy and personalization in mind. All data is stored locally on your machine and can be freely modified to suit your needs. The self-hosted web interface offers a visual and intuitive way to explore your gaming history.

The development of Obelyx was guided by a simple vision: 

>*Build a self-hosted program using modern .NET, REST APIs, and a React-based frontend that can replace my current backlog tracking in Google Sheets.*

## Why "Obelyx"? 
The name **Obelyx** comes from *Obelisk* — a monument that, much like your game backlog, stands the test of time. 

Conquering it is an adventure in itself, not unlike building the monuments of old. It takes patience, motivation, and the drive to overcome even the smallest challenges life throws your way.

## Installation
Follow these steps to run **Obelyx** locally.

*(The application currently assumes a local SQL Server instance and an API running on .NET 8.)*

### 1. Prerequisites
- .NET 8 SDK  
- Node.js (v18 or later)  
- SQL Server (local instance or Docker)  
- Git

### 2. Clone the Repository
```
git clone https://github.com/SirSalsa/Obelyx.git
cd Obelyx
```

### 3. Configure the Database Connection
Open `Obelyx.API/appsettings.json` and update the connection string if needed.
```json
"ConnectionStrings": {
  "SqlServer": "Server=localhost;Database=GameBacklog;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 4. Run the API
From the `Obelyx.API` folder:
```
dotnet run
```
The database is automatically created and migrated when you launch the API.

Swagger docs will be available at `https://localhost:7125/swagger`.

### 5. Run the Frontend
From the `Obelyx.Frontend` folder:
```
npm install
npm run dev
```
The frontend runs at `http://localhost:51564` (as configured in `Program.cs` and `vite.config.js`).

### 6. Start Using Obelyx
You can now start using the app in your browser and start adding games and update their data. No authentication required in this version.

## Roadmap
- ✅ ~~Add, edit, and delete games~~
- ✅ ~~Search and filter functionality~~
- ⬜ Docker support
- ⬜ Import functionality
- ⬜ Manage archived games
- ⬜ Settings page
- ⬜ Improved interface with theme support
- ⬜ Expanded search and filter options
- ⬜ Export support

## Contributing
Contributions are welcome!
Feel free to open an issue or submit a pull request for improvements, bug fixes, or new features.

Frontend help (UI/UX, styling, and layout improvements) is especially appreciated.

## License
This project is open source and available under the [Apache License 2.0](LICENSE).

You’re free to use, modify, and distribute this software, provided that you comply with the terms of the license.
