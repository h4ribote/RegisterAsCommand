# RegisterAsCommand

A tool for Windows 11 to easily register executable files as commands. By selecting "Register as Command" from the right-click context menu, an executable is registered as a batch file, allowing it to be called directly from the command prompt.

## Features

- Stores batch files in `%AppData%\Local\RegisterAsCommand\commands`
- Adds `%AppData%\Local\RegisterAsCommand\commands` to the user's PATH
- Adds a "Register as Command" option to the right-click context menu
- Enables registered executables to be run from the command prompt

## Requirements

- Windows 11
- .NET Framework 4.0 or higher
- `register_as_command.exe` (must be placed in the same directory as `setup_tool.exe`)

## Installation

1. Clone or download the repository:
   ```bash
   git clone https://github.com/h4ribote/RegisterAsCommand.git
   ```
2. Build the project (using Visual Studio or MSBuild):
   - Target framework: .NET Framework 4.0
   - Output: `setup_tool.exe` and `register_as_command.exe`
3. Place `setup_tool.exe` and `register_as_command.exe` in the same directory.

## Usage

1. **Setup**:
   - Run `setup_tool.exe`.
   - It automatically copies `register_as_command.exe` to `%AppData%\Local\RegisterAsCommand` and adds `%AppData%\Local\RegisterAsCommand\commands` to the PATH.
   - Log out and log in to apply PATH changes.

2. **Registering a Command**:
   - Right-click an executable file (e.g., `test.exe`) and select "Show more options".
   - Choose "Register as Command" to create a batch file (e.g., `test.bat`) in `%AppData%\Local\RegisterAsCommand\commands`.

3. **Running a Command**:
   - Open a command prompt and type the registered command name (e.g., `test`) to execute it.

## Notes

- PATH changes require logging out and logging in to take effect.
- The "Register as Command" option appears in the legacy context menu of Windows 11.
- Check the console output for any errors.
