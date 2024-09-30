# Introduction
I can't stand that my university doesn't allow me to export the important calendar
dates to my calendar, so as a software engineer I decided to implement a fix for my problem!

Using the knowledge I've gained both from half a decade of software engineering practice and schooling from the Computer Science program here at the University of Utah,
I made a windows forms application which scrapes the university's websites to generate CUSTOM .ics links for the students here!


## The Program Idea
The program, as I see it currently, works as follows:

1. The user inputs a website URL which is the semester schedule for the university.
All university schedules from 2019-2024 are formatted exactly the same, so as long as the formatting doesn't change they can input any link for their semester
2. They are directed to a screen with a drop-down of all events formatted into the same structure that they are on the website, and they can check off any events they want added.
This gives the user control over which events they care about. For example, a student could want to know when all the breaks
are and when they can register for the next semester, but they don't want to know about the last day to drop classes. This program gives the user that flexibilty.
3. A .ics file is generated with the events they selected, and they can simply click a button to import it into their calendar of choice!