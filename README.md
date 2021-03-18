# BookLover
This Project was created and built by Eric Lajeunesse, Ben Chamness, and Marley Boone. This API stores data about Books and Authors and allows a user to create books, authors and reviews for others to see. Users have profiles where they can create lists to books they would like to read.

Steps on Installing Project
To install this project you must first clone entire project from github. In this repository locate the green "Code" drop down button. Inside of the drop down copy the HTTPS url underneath Clone. Create a folder in your file explorer and open command line- once open type "git clone https://github.com/e-lajeunesse/BookLover.git". Once this is done open Visual Studio then locate the Manage Nuget Packages for Solution. Browse for Microsoft.AspNet.Identity.EntityFrameWork - install this to the whole project. Then Browse for Microsoft.AspNet.Identity.Owin and Microsoft.AspNet.WebAPI.Owin this two will be installed to ONLY the data layer of the project. After this is completed The API can be ran and the PostMan link below will allow you to test the endpoints for the API.

To make things a little easier and more fun to work with you can open the github link below and Clone the repo as well. This repository contains a functioning UI that allows you to communicate between the front end and back interfaces. Once you have clone this repo you will start both of the projects and communicate through the Console App. This is purely optional.
https://github.com/e-lajeunesse/BookLoverUI
[GitHub link for UI]

This Postman link is a documented postman with endpoints and descriptions on how to test the endpoints. Once this link it opened it will prompt you to Add the collection BookLover to a workspace. If you already have a workspace created then you can add this collection to That workspace or create a new one. Once this is downloaded to a workspace you can run the project and start testing out the endpoints.
[![Run in Postman](https://run.pstmn.io/button.svg)](https://god.postman.co/run-collection/b2cb68fadd00a577a1ac)

There is a Seed.txt file in the BookLover.Data project which can be used to Seed some authors and books into the database for testing purposes. To do this you will need to enable migrations in the Package Manager Console, then copy and paste the code from the Seed.txt file(starting at line 3) into the Configuration.cs file found in the Migrations folder and run the command update-database in the Package Manager Console. Note that this should be done before adding any other data or else the Foreign Keys for the books and authors in the Seed.txt file will be incorrect.  

Links -

https://documenter.getpostman.com/view/14674949/Tz5s5xP2
[Postman Documentation]

https://sway.office.com/Pd8qKbuZ7yu4uJaF?ref=Link
[Planning Documentation] (https://office.com)

https://trello.com/b/2bpq8lS2/books-by-the-dozen
[Trello](https://trello.com)

https://dbdiagram.io/d/604a9102fcdcb6230b23bf20
[Data Base Diagram](https://dbdiagram.com)

https://sdjanuary2021.slack.com/files/U01JA8D9D7V/F01Q1FPRDRC/whiteboard_2_-01.png
[Slack](https://slack.com)

