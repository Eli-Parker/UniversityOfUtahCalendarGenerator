# U of U Academic Calendar Generator


### Description
I can't stand that my university doesn't allow me to export the important calendar
dates to my calendar, so as a software engineer I decided to implement a fix for my problem!

Using the knowledge I've gained both from half a decade of software engineering practice and schooling from the Computer Science program here at the University of Utah,
I made a C# application which scrapes the university's websites to generate CUSTOM .ics links for the students here! 

There are currently two planned implementations, a blazor WASM app (made for ease of use for other students, and as an experiment with a new program type personally)
and a windows forms (or similar) desktop app which can scrape data live (meant for a full showcase of the program's ability)

**Note that this program is still a WIP, but one of the two planned implementations has been finished and is currently on the main branch.**

### The Program Idea

The program, works as follows:

1. **Data Grabbing:** Before the app is published, static JSON files are generated from scraped site data which contain dictionaries of all events grouped by the table they came from.
All university schedule websites from mid 2019-2025 are formatted exactly the same, so as long as the formatting doesn't change this program will remain fucntional for future semesters.

2. **Selecting Specific Site Data:** The user selects from a drop-down of semesters which correspond to that semester's schedule.

3. **Desired Event Selection:** The user is directed to a screen with a list of all events formatted into the same structure that they are on the website, and they can check off any events they want added.
This gives the user control over which events they care about. For example, a student could want to know when all the breaks
are and when they can register for the next semester, but they don't want to know about the last day to drop classes. This program gives the user that flexibility.

4. **Export of Event Data:** An .ics file is generated with the events they selected, and they can simply click a button or download the file to import it into their calendar of choice!

## Table of Contents
- [Installation](#installation)
- [Usage](#usage)
- [Features](#features)
- [Contributing](#contributing)
- [Contact Information](#contact-information)
- [Acknowledgments](#acknowledgments)

## Project READMEs
- [SiteParser README](./SiteParser/README.md)
- [EventList README](./EventList/README.md)
- [CalendarFileGenerator README](./CalendarFileGenerator/README.md)
- [CalendarController README](./CalendarController/README.md)
- [UofUCalGen README](./UofUCalGen/README.md)


## Installation
I recommend that you use **Visual Studio 2022** to run/modify this project, since that is what was used to develop it.

1. **Clone the repository** (either in visual studio or your git client of choice)
2. **Open the .sln file in Visual Studio**

## Usage

Within Visual Studio, you can run the program by pressing **F5** 
or hitting the green play button. If you
want to build the program you can do so by pressing **Ctrl+Shift+B**.

The program's UI has lots of helpful tool-tips to guide you
through the process of generating your .ics file, and if you have
any questions about specific projects within the solution
then refer to the [Project READMEs](#project-readmes) section above.

## Features

- Scrapes the university's website for calendar data
- Generates an .ics file ready to import to the user's calendar of choice
- Allows the user to select which events they want to add to their calendar on an event-by-event basis

## Contributing

You can contribute by following the installation instructions above, then making a new branch
with your changes and submitting a pull request. I request that any changes be made in visual studio and follow
the design guidelines in the style cop and .editorconfig files.

## Contact Information

Feel free to contact me using my [LinkedIn profile](https://www.linkedin.com/in/eli-parker-a96338302/)

## Acknowledgments

- Thanks to the Computer Science program at the University of Utah for teaching me the skills to make this program
- Thanks to my friends and family for supporting me in my software engineering journey
- Thanks to the Development community for providing the tools I used to make this program! Library maker heroes FTW
