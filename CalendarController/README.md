# CalendarController.cs


### Description

This class acts as an interface between the front-end and the three "back-end" classes that handle the creation of the calendar file.
It simplifies the process of creating a calendar file by allowing the front-end to add
events to the calendar file without having to worry about the specifics of how the file is created.

### Notes 

Some eagle-eyed readers may notice that the `CalendarController` class returns a dictionary containing all the event names
grouped by the table they came from, and say "Hey, that's the same type of data that the EventList class uses!".
You'd be correct, but I chose to reconstruct this because the `EventList` class uses the `Event` class in its' dictionary,
which is useful for creating events and for storage, but the front end only requires the event name to display to the user.
It was easier to reconstruct the Dictionary here, and return it to the view side.

## Table of Contents
- [Solution README](../README.md)
- [Usage](#usage)
- [Features](#features)
- [Contributing](#contributing)
- [Contact Information](#contact-information)
- [Acknowledgments](#acknowledgments)

## Usage

- Make a new instance of the CalendarController class:

```csharp
var ctlr = new CalendarController("https://registrar.utah.edu/academic-calendars/fall2024.php");
```

- Get list of events to display to the user:

```csharp
Dictionary<string, List<string>> events = ctlr.GetEvents();
```

- Get the .ics file to download:

```csharp
List<string> selectedEvents = ...;

byte[] calendarFile = ctlr.GetCalendarFileFromSelected( selectedEvents );

// Process file and send file to user
```


## Features

- Create an easy-access solution so that the view doesn't need to worry about the specifics of creating a calendar file.
- Return a dictionary of event names grouped by the table they came from.
- Add events to the calendar file and return it to the view.

## Contributing

You can contribute by following the installation instructions above, then making a new branch
with your changes and submitting a pull request. I request that any changes be made in visual studio and follow
the design guidelines in the style cop and .editorconfig files.

## Contact Information

Feel free to contact me using my [LinkedIn profile](https://www.linkedin.com/in/eli-parker-a96338302/)
or shoot me an email at <me@eliparker.dev>

## Acknowledgments

No acknowledgments at this time.