# School Vaccination Portal - .NET Core API

This is the backend REST API for the School Vaccination Portal. It provides endpoints to manage students, vaccination drives, vaccination records, and reports.

## 🛠 Tech Stack

- ASP.NET Core Web API
- Entity Framework Core (Code First)
- MS SQL Server
- Repository-Service Pattern
- JWT Authentication

## 📁 Project Structure

/Controllers -> API Controllers /Services -> Business logic services /Repositories -> Data access logic /Models -> Entity models /Data -> DbContext and seeders /appsettings.json -> Configuration


## 🚀 How to Run

### Prerequisites

- [.NET 7 SDK or higher](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)

### Steps

1. Clone the repository:
   git clone this repo.
   cd SchoolVaccinationAPI
   
2. Configure the connection string in appsettings.json:
   "ConnectionStrings": {
		"DefaultConnection": "Server=YOUR_SERVER;Database=Student_Vaccination;Trusted_Connection=True;"
	}

3. Run the application:
   Configure the connection string in appsettings.json:
   
4. The swagger UI will be available at:
   https://localhost:5001/swagger



