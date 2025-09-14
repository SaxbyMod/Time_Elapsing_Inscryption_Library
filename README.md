# Time Elapsing Console Plugin

This mod essentially enables the ability show Elapsed Time in a nice way within BepInEx.

## Functions;

This mod Includes 3 Nifty functions.

* [OUTDATED] WaitType; This function waits for a set amount of time determined by an Integer, and a string which associates with a value.
* SwitchToUnits: Takes a time Unit and converts it into a simplified string.
* StartWaitTime: Takes three strings representing; What is being waited for, the Unit of Time, and the mod's name thats calling it.
* StopWaitTime: Takes the returned Info from Start to stop the logging.
* GetTimeFromType: Gets the time unit from the passed Unit of Time and a TimeSpan.
* HandleStopwatch: A function that takes a string representing the action and a stopwatch and handles the stop watch.

## Defining The Function Variables;

Unit of Time table:

|     Unit      | Simplified |  Elongated  |
|:-------------:|:----------:|:-----------:|
| Milli-seconds |     ms     | millisecond |
|    Seconds    |     s      |   second    |
|    Minutes    |     m      |   minute    |
|     Hours     |     h      |    hour     |
|     Days      |     d      |     day     |
|     Ticks     |     t      |    ticks    |

Actions for HandleStopwatch:

| Action  |                        What it will do                         |
|:-------:|:--------------------------------------------------------------:|
|  Start  |                 Starts the passed stop watch.                  |
|  Stop   |                  Stops the passed stop watch.                  |
|  Reset  |              Resets the passed stop watch to `0`.              |
| Restart |          Restarts the passed stop watch back to `0`.           |
| Elapsed | Gets the TimeSpan that has elapsed from the passed stop watch. |

## Maintaining and or Updating/Porting:

Send a PR to the repository if you would like to update the mod in any way shape or form.

Porting is allowed as long as proper and full credits are instated and the provided License is KEPT.

If you would like this mod/lib added into your community shoot me a message over on Discord at `@thincreator3483`

Also while im at it, if anything here is inaccurate and or needs changes lemme know ^^

## Credits:

As of 1.1.0

* Creator - All of the Plugins code, as well as everything else associated with the mod.