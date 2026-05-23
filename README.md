# AutoLauncherGaugeview

AutoLauncherGaugeview is a lightweight Windows startup launcher designed for industrial systems. It automatically starts one or more configured applications after Windows login, displays a startup warning screen, prevents duplicate application launches, and maintains execution logs.

## Features

- Automatic startup through Windows Task Scheduler
- Configurable application list through `Config.ini`
- Sequential application launching with configurable delays
- Prevents launching duplicate application instances
- Startup warning screen with audible notification
- Execution logging
- Runs with administrator privileges when configured through Task Scheduler
- No hardcoded application paths

---

## Folder Structure

```
AutoLauncherGaugeview
│
├── AutoLauncherGaugeview.exe
├── Config.ini
└── LauncherLog.txt
```

---

## Configuration

Create a file named `Config.ini` in the same directory as the executable.

Example:

```ini
[SETTINGS]
StartupDelay=60000
ProgramDelay=5000

[PROGRAMS]
Path1=C:\GaugeView\GV1.exe
Path2=C:\GaugeView\GV2.exe
Path3=C:\GaugeView\GV3.exe
```

### Parameters

| Parameter | Description |
|------------|------------|
| StartupDelay | Delay after Windows login before launching applications (milliseconds) |
| ProgramDelay | Delay between launching consecutive applications (milliseconds) |
| Path1..PathN | Full path of applications to launch |

---

## Startup Process

1. User logs into Windows
2. Task Scheduler launches AutoLauncherGaugeview
3. Warning screen is displayed
4. Audible notification is played
5. Configured applications are launched sequentially
6. Launcher exits automatically

---

## Duplicate Instance Protection

Before launching an application, the launcher checks whether the process is already running.

Example:

- GV1.exe already running → skipped
- GV2.exe not running → started
- GV3.exe not running → started

This prevents:

- Duplicate OPC connections
- Duplicate database access
- Multiple hardware communication sessions
- Unnecessary resource consumption

---

## Logging

Execution details are written to:

```
LauncherLog.txt
```

Example:

```
2026-05-23 08:15:01 : Started GV1.exe
2026-05-23 08:15:06 : Started GV2.exe
2026-05-23 08:15:11 : GV3 already running
```

---

## Windows Task Scheduler Setup

### Create Task

1. Open Task Scheduler
2. Select **Create Task**
3. Configure:

#### General

- Run only when user is logged on
- Run with highest privileges

#### Trigger

- At log on

#### Action

Program:

```
C:\Launcher\AutoLauncherGaugeview.exe
```

Start In:

```
C:\Launcher
```

#### Settings

- Allow task to be run on demand
- Run task as soon as possible after a scheduled start is missed
- Do not start a new instance if task is already running

---

## Build Requirements

- Visual Studio 2022
- .NET Framework 4.6.2 or later
- Windows 7 / Windows 10 / Windows 11

---

## Typical Use Cases

- GaugeView startup automation
- Industrial HMI startup
- OPC client startup
- Production line software initialization
- Plant-floor workstation automation

---

## License

Internal company use only.
