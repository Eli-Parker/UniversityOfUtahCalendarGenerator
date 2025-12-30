# EventList.cs


### Description

This class is responsible for storing all the events that come from the scraping of the University's website.
The priorities for storage was more about ease of access and ease of use, so the class is very simple.

Design considerations included storing lists in such a way so that they could be easily
grouped by the table they came from on the website. This makes it easier to display the events to the user.


### Notes 

This program is intended to be simple, and the accompanying [Test Suite](../WebsiteParserTests/EventListTests.cs)
is intended only to verify that the class is functional for its' intended purpose. 
This was a concession that I made intentionally, as this is a fun project. A more rigorous application
would warrant more rigorous testing, but for the purposes of this project I felt that this was sufficient.

## Table of Contents
- [Solution README](../README.md)
- [Usage](#usage)
- [Features](#features)
- [Contributing](#contributing)
- [Contact Information](#contact-information)
- [Acknowledgments](#acknowledgments)

## Usage

User exposed methods go as follows:
- `AddEvent(...	)` - Add a new event to the list. You're required to define which table it goes in, as well as the specific event name and dates
- `GetTableNames(...)` - Get the names of all the tables in the list
- `GetEvents(...)` - Get all events for a given table name, which are stored in the three out parameters

All of those methods have more detailed documentation within their XML comments,
and you can get more information on how to use them there.

Heres an example of what that may look like:

```csharp
// Initialize new list
EventList events = new();

// Add an event
events.AddEvent("Holidays", "Christmas", new DateOnly(2021, 12, 25), new DateOnly(2021, 12, 25));

// Grab table names
List<string> tableNames = events.GetTableNames();

// Grab events for a given table name
// Define the lists you want to store the events in
List<string>   tableEvents;
List<DateOnly> tableDatesStart;
List<DateOnly> tableDatesEnd;

// Get the events from the EventList
GetEvents("Holidays", out tableEvents, out tableDatesStart, out tableDatesEnd)
```

## Features

- Stores and keeps track of Events
- Stores and keeps track of the tables that the events came from
- Allows for easy access to the tables and the events within those tables,
making it easy to display them to the user

## Contributing

You can contribute by following the installation instructions above, then making a new branch
with your changes and submitting a pull request. I request that any changes be made in visual studio and follow
the design guidelines in the style cop and .editorconfig files.

## Contact Information

Feel free to contact me using my [LinkedIn profile](https://www.linkedin.com/in/eli-parker-a96338302/)
or shoot me an email at <me@eliparker.dev>

## Acknowledgments

- No acknowledgments, but I'd like to thank myself for making such a cool project!