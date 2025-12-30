# CalendarFileGenerator.cs


### Description

This class is responsible for generating a calendar file that can be imported into most calendar applications.
It handles the creation of the file and the addition of events to the file, so that way all the front-end
has to do is construct the file from bytes and send it to the user.

### Notes 

An implementation consideration I made was this: *Why have `EventList` if this can store the events*? 

The EventList class is very useful here because it allows us to pick and choose events based on user preferences.
Why would we build the full calendar file, then remove events from it individually? It would be much more efficient
to only add the events we want to the calendar file in the first place, and that's the purpose of EventList class.

It would also be difficult to group events by which table they came from if we only had the calendar class
(which was one of the main design points I wanted for the program). 

## Table of Contents
- [Solution README](../README.md)
- [Usage](#usage)
- [Features](#features)
- [Contributing](#contributing)
- [Contact Information](#contact-information)
- [Acknowledgments](#acknowledgments)

## Usage

- Make a new instance of the Calendar class:

```csharp
var calendar = new Calendar();
```

- Add Events to the calendar file:

```csharp
calendar.AddCalendarEvent("Jack's Birthday", new DateOnly(...), new DateOnly(...));
//                          [Event Name]        [Start Date]        [End Date]
```

- Export the calendar file:

```csharp
byte[] icalFileBytes = calendar.ExportCalendarFile();
```


## Features

- Create a calendar file that can be imported into most calendar applications
- Add events to the calendar file
- Export the calendar file as a byte array

## Contributing

You can contribute by following the installation instructions above, then making a new branch
with your changes and submitting a pull request. I request that any changes be made in visual studio and follow
the design guidelines in the style cop and .editorconfig files.

## Contact Information

Feel free to contact me using my [LinkedIn profile](https://www.linkedin.com/in/eli-parker-a96338302/)
or shoot me an email at <me@eliparker.dev>

## Acknowledgments

Thanks to the people who created the `iCal.Net` library, which this project would've been extremely hard to create without.