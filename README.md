# PH.Time - [![NuGet Badge](https://buildstats.info/nuget/PH.Time)](https://www.nuget.org/packages/PH.Time/)

this is a simple c# struct for exress  time value to avoid use of `TimeStamp`.

```csharp
var time = new Time(9,0,0);
Console.WriteLine(time);
//produce output : 09:00:00
```

## Code Examples

**Time.MinValue**
```csharp
var time = Time.MinValue;
Console.WriteLine(time);
//produce output : 00:00:00
```

**Compare tho times**
```csharp
var time                   = Time.MinValue;
var nine                   = new Time(9,0);
var midNight               = new Time(0, 0, 0);

var nineSmallerThanMidnith = nine < time;
var zeroIsMidnight         = time == midNight;


Console.WriteLine($"{nineSmallerThanMidnith}");
//produce output : False

Console.WriteLine($"{zeroIsMidnight}");
//produce output : True
```

**Build a time array as ["9:00:00","9:01:00","9:02:00"]**
```csharp
var myArray = TimeFactory.BuildTimeArray(new Time(9, 0, 0)
                                         , new Time(9, 2, 0)
                                         , TimePart.Minutes
                                         ,includeExtremes: true);
```

**Build a time array as ["9:30:00","10:0:00","10:30:00","11:00:00","11:30:00","12:00:00"]**
```csharp
var arr = TimeFactory.BuildTimeArrayBySteps(new Time(9, 0), new Time(12, 30), 30, TimePart.Minutes, includeExtremes:false);
```