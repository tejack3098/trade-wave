# TradeWave

TradeWave is a real-time web application designed to help users manage and analyze their stock portfolios. Built with React and .NET Core, TradeWave provides real-time financial data, company information, and portfolio tracking, empowering users to make informed investment decisions.

## Table of Contents
1. [Features](#features)
2. [Technologies Used](#technologies-used)
3. [Installation and Setup](#installation-and-setup)
4. [Project Structure](#project-structure)
5. [Usage](#usage)
6. [Future Enhancements](#future-enhancements)
7. [License](#license)

---

## Features

- **Real-Time Financial Data**: Access live financial data, including company profiles, income statements, balance sheets, and peer comparisons using the Financial Modeling Prep API.
- **User Portfolio Management**: Create, track, and manage stock portfolios with JWT-based user authentication.
- **Sentiment Analysis on News**: Retrieve the latest news related to a user's portfolio and analyze sentiment to determine market outlook (future enhancement).
- **Risk Assessment and Diversification Tools**: Assess portfolio risk and receive optimization suggestions based on modern portfolio theory (future enhancement).
- **Secure Authentication**: Ensure secure access through JWT security, allowing personalized tracking and portfolio management.

## Technologies Used

- **Frontend**: React, JavaScript, HTML, CSS
- **Backend**: .NET Core, Entity Framework, REST API
- **Database**: SQL Server (for storing user and portfolio information)
- **APIs**: Financial Modeling Prep API (for financial data and analytics)
- **Authentication**: JSON Web Tokens (JWT)

## Installation and Setup

### Prerequisites

- **Node.js** and **npm**: Required for the React frontend.
- **.NET Core SDK**: Required for the backend API.
- **SQL Server**: Used for storing user and portfolio data.
- **Financial Modeling Prep API Key**: Sign up and obtain an API key from [Financial Modeling Prep](https://financialmodelingprep.com/developer/docs).

### Steps

2. **Backend Setup**
   - Navigate to the `backend` folder:
     ```bash
     cd backend
     ```
   - Update the `appsettings.json` file with your SQL Server connection string and Financial Modeling Prep API key.
   - Run the .NET Core application:
     ```bash
     dotnet run
     ```

3. **Frontend Setup**
   - Navigate to the `frontend` folder:
     ```bash
     cd ../frontend
     ```
   - Install the dependencies:
     ```bash
     npm install
     ```
   - Start the React application:
     ```bash
     npm start
     ```

4. **Access the Application**
   - The frontend will run on `http://localhost:3000`, while the backend API will be available on `http://localhost:5000`.
