# Railway-Management-System
This project is a comprehensive Database Management System (DBMS) developed to manage the international railway corridor connecting Saudi Arabia, Jordan, Syria, and Türkiye. The system facilitates the management of passenger transportation, freight logistics, customs operations, railway infrastructure, schedules, staff assignments, maintenance records, and loyalty programs within a unified relational database.

# 🎓 Academic Context
This project was developed as a final term project for the Database Management Systems course at Kocaeli University. It focuses on applying relational database design, normalization principles, and modern full-stack development practices (ASP.NET Core & React) to deliver a robust end-to-end system architecture.

# 🚀 Technologies
Backend: C# (.NET 8 Web API), Entity Framework Core (ORM)

Frontend: React.js, Tailwind CSS

Database: MySQL Server

# 🛠 Project Setup and Execution
1. Database Setup
To set up the database, follow these steps:

Open MySQL Workbench.

Create a new schema named database_project_db.

Execute the following files in the project root directory in order:

create_tables.sql (Creates the schema structure and constraints)

insert_data.sql (Populates the tables with sample test data)

# 2. Backend Execution
Navigate to the Railway.Api directory.

Update the appsettings.json file with your local MySQL connection string.

Run the following command in your terminal:

Bash
dotnet run

# 3. Frontend Execution
1. Navigate to the `railway-frontend` directory.
2. Install the necessary dependencies:
   ```bash
npm install
Start the development server:

Bash
npm start

# 🏗 Project Structure
*   `Railway.Api/`: Backend source code.
*   `railway-frontend/`: React-based user interface code.
*   `create_tables.sql` & `insert_data.sql`: Database schema and seeding scripts.

# 👤 Project Owner
**Firdevs Eren**
