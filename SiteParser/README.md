# SiteParser.cs


### Description

This class is responsible for scraping and parsing website data from the university
registrar's website. It uses the ``HtmlAgilityPack`` library to grab and parse HTML,
and uses a combination of HTML navigation and regular expressions to extract
the date and time from the HTML document.


### Notes 

I'd like to make note that some significant concessions were made here.
This is a deterministic program which I recognize will break once the
university changes their page's format. For my use case, I'm okay with that.
This isn't a program that is supposed to last forever (even though it's quality Dev work),
I just want a program that will auto-upload the calendar info into my calendar, and I'm
okay to fix it if they change it.

## Table of Contents
- [Solution README](../README.md)
- [Usage](#usage)
- [Features](#features)
- [Contributing](#contributing)
- [Contact Information](#contact-information)
- [Acknowledgments](#acknowledgments)

## Usage

The only exposed method from SiteParser is the ParseSite method. This method takes in a string URL
and returns a list of Event objects. These Event objects contain the event's name, date, and time.

Note that the URL must be a link to the university registrar website, and that link MUST point to a semester calendar.

Heres an example of what that may look like:

```csharp
EventList events = parser.ParseSite("https://registrar.utah.edu/academic-calendars/spring2024.php");
```

## Features

- Uses the HtmlAgilityPack library to grab and parse HTML
- Uses regular expressions to extract event data from the HTML
- Returns an EventList object containing all the events from the site

## Contributing

You can contribute by following the installation instructions above, then making a new branch
with your changes and submitting a pull request. I request that any changes be made in visual studio and follow
the design guidelines in the style cop and .editorconfig files.

## Contact Information

Feel free to contact me using my [LinkedIn profile](https://www.linkedin.com/in/eli-parker-a96338302/)
or shoot me an email at <racecar47@icloud.com>

## Acknowledgments

- Thanks to the team at [Regex 101](https://www.regex101.com/) for
making the Regex development process so much easier!
- Thanks to the team at [HtmlAgilityPack](https://html-agility-pack.net/) for
making the HTML parsing process so simple, it was a breeze to use!