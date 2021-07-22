# ThemeHelper
### Last transformation added on 22/07/2021 | version 1.0.3
This is just a small project I made for myself and then thought other people might find it useful, it lets you apply and stack transformations to colours.

[Color inputs](#allowed-color-inputs) •
[Transformations](#implemented-transformations) •
[Download](#download)

---
This program will simply let you type in a colour in the left textbox (labelled as **Base**) it will then apply the transformations which you have specified in the textbox at the top, the resulting colour will then appear on the right with the it's corresponding hex value. Example below.

![example image that shows the gui](https://github.com/nightowl286/ThemeHelper/raw/master/example.PNG)

# Allowed color inputs
As you can see you can enter the base colour in multiple different formats (any that C#'s [`ColorConverter.ConvertFromString`](https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.colorconverter.convertfromstring) can recognise). Some of which include:

  - #rgb
  - argb
  - #rrggbb
  - #aarrggbb
  - sc# scA, scR, scG, scB
  - and also any recognised color name such as "red" or "yellow"
  - [more information here](https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.color)

---
# Implemented transformations
As you can also see in the example image above you can chain multiple transformations together, currently they are case-sensitive (as my other project requires it) but that may change in the future.
The most recent transformations file is at the version 1.0.3 and has been uploaded on 22/07/2021, the list below will be updated with each update if necessary.

The currently allowed transformations are
  - `Shade([value])` - will mix the colour with black by the specified amount, the value being a percentage (0% - 100%) which can also be negative, the value may be ommited and the default used will be 12.5%
  - `Tint([value])` - will mix the colour with white by the specified amount, the value being a percentage (0% - 100%) which can also be negative, the value may be ommited and the default used will be 12.5%
  - `Tone([value])` - will mix the colour with gray by the specified amount, the value being a percentage (0% - 100%) which can also be negative, the value may be ommited and the default used will be 6.25%
  - `Invert()` - will invert each channel of the colour in rgb space.
  - `Hue(value)` - will shift the hue of the colour by the specified amount, (0 - 360) a negative value may be used, it cannot be ommited.
  - `Complement()` - will shift the hue by a value of 180.
  - `Neutral(value)` will shift the hue by the value specified * 15, the value is expected to be an integer.
  - `Analogous(value)` will shift the hue by the value specified * 30, the value is expected to be an integer.
  - `Saturate(value)` will shift the saturation of the color (in the hsv color space) by the specified amount, the value being a percentage (0% - 100%) which can also be negative.
  - `Value(value)` will shift the value of the color (in the hsv color space) by the specified amount, the value being a percentage (0% - 100%) which can also be negative.

---
# Download
