<center>
<img src="https://s6.imgcdn.dev/SXhlS.png" alt="OneClass Logo" width="250" />

[![Hack Together: Microsoft Graph and .NET](https://img.shields.io/badge/Microsoft%20-Hack--Together-orange?style=for-the-badge&logo=microsoft)](https://github.com/microsoft/hack-together)
</center>

# OneClass

OneClass is an easy-to-use online platform that helps teachers and students collaborate, communicate and create!

With OneClass,
- You can access your assignments and add them to your Microsoft To Do.
- You can access your class resources anytime and anywhere.
- You can share your work with OneDrive.

OneClass is the ultimate tool for learning in the 21st century!

# Team

<p>
We are thrilled to introduce our hackathon team, composed of four software engineering students from Morocco. Meet Brahim ELHOUBE, El Bachir OUTIDRARINE, Youssef BEN SADIK, and Abderrazzaq LAANAOUI (as shown in the picture from left to right). We have diverse skills and backgrounds in web and mobile applications. We are passionate about solving real-world problems with innovative solutions and cutting-edge technologies. We are eager to learn from other participants and mentors, and to showcase our creativity and potential in this hackathon.
</p>
<img src="https://s6.imgcdn.dev/S8yDS.jpg" alt="OneClass Team" width="500" />

## About The Competition

Hack Together: Microsoft Graph and .NET is a hackathon for beginners to get started building scenario-based apps using .NET and Microsoft Graph.

We are thrilled to be part of this hackathon and have the opportunity to collaborate with other talented people to create innovative solutions using Microsoft technologies.

We are excited to learn new skills, gain new experiences, and contribute to the success of the event.

## Features

- **Classroom Management**: Create, edit, delete, and join classes.
- **Assignment Management**: Teachers can create and manage assignments, and students can submit them.
- **Todo Integration**: Students can add assignments to their todo list.
- **Authentication**: Users can sign in with their Microsoft account.
- **Mail Notifications**: Teachers can send email notifications to students whenever a new resource or assignment is created.

## Technologies

- **ASP.NET Core**: The backend is built with ASP.NET Core Web API (.NET 6).
- **Microsoft Graph .NET SDK**: The app uses the Microsoft Graph API to authenticate users and access their data.
- **Azure Database for PostgreSQL flexible server**: The app data is persisted in an instance of PostgreSQL database in Azure.
- **MSAL.NET and @azure/msal**: The app uses the MSAL.NET to authenticate users.
- **React**: The frontend is built with React.

## Architecture

The application is a web-based solution that leverages Microsoft technologies to provide a seamless user experience across different platforms. The application consists of two main components: a back-end service and a front-end client. 

The back-end service is built with ASP.NET, a framework that allows developers to create web applications using C# as programming languages. The back-end service communicates with various Microsoft apps and services, such as Outlook, OneDrive, To Do, etc., through the Microsoft Graph .NET SDK , which is a library that simplifies accessing Microsoft Graph data and functionality. The back-end service also stores additional data in a serverless PostgreSQL database running in Azure, which is treated as a document database thanks to <a href="http://martendb.io/">Marten</a>, an open source library that enables PostgreSQL features for .NET applications. The back-end service exposes a RESTful API that can be consumed by any client application that supports HTTP requests.

The front-end client consists of two different applications: a React Single Page Application (SPA) and a .NET MAUI app (provisional). React is a JavaScript library for building user interfaces that can run on any web browser. .NET MAUI is a cross-platform framework for building native mobile and desktop applications using C# and XAML. Both client applications authenticate the user using the corresponding <a href="https://learn.microsoft.com/azure/active-directory/develop/msal-overview">MSAL</a> (Microsoft Authentication Library) library, which handles acquiring tokens from Azure Active Directory (AAD) or Microsoft identity platform endpoints. The client applications then provide the access token of the authenticated user to the back-end service when making API calls.

<img src="https://s6.imgcdn.dev/S8KAe.png" alt="OneClass App Architecture" width="1000" />

## Installation

To install OneClass, follow these steps:

- Clone the repository to your local machine.
- Install the required dependencies by running `npm install` and `dotnet restore`.
- Configure the Microsoft Graph API credentials by setting the clientId, clientSecret, and tenantId environment variables.
- Start the development server by running `npm start` and `dotnet run`.

## Usage

To use OneClass, follow these steps:

- Navigate to http://localhost:3000 in your web browser.
- Log in with your Microsoft account credentials.
- Create or enroll in a course.
- Create or submit an assignment.
- View grades and upcoming assignments in the gradebook and calendar, respectively.

## Future Development Plans

Note that while the main features of the app are functional, there are still some areas that require additional development and refinement. Specifically, we are working on improving the project structure, backend architecture and front-end integration to make the app more robust, efficient, and user-friendly.

We also plan to implement additional features in the future to enhance the overall user experience. These include:

- **Fix UI Bugs**: Fix UI bugs to improve the overall user experience.
- **Classroom Chat**: Add a chat feature to allow students and teachers to communicate with each other.
- **Classroom Video Conferencing**: Add a video conferencing feature to allow students and teachers to meet virtually.
- **Classroom File Sharing**: Add a file sharing feature to allow students and teachers to share files with each other.
- **Optimize Assignment Submission**: Improve the assignment submission process to allow students to submit assignments in a more efficient manner.
- **Gradebook**: Add a gradebook feature to allow teachers to view and manage student grades.

However, despite these areas for improvement, we believe that the current version of the app is still usable and provides value to users. 

We plan to continue working on these enhancements in future updates to the app. Please feel free to provide feedback or suggestions for improvement as we continue to develop and refine the app.
