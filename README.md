# F# Starter

This folder contains the skeleton of a parser along with the input and output types for each analysis given in the assignment. It also contains an example of a "calculator" program in F# that reads an arithmetic expression from the command line and print the result of evaluating such expression for initial testing.

## Files

F#/FsLexYacc
* [Lexer.fsl](Lexer.fsl): The lexer for arithmetic expressions
* [Parser.fsp](Parser.fsp): The parser for arithmetic expressions
* [Types.fs](Types.fs): Global types that are used in many analysis
* [AST.fs](AST.fs): Types for AST of arithmetic expressions
* [Program.fs](Program.fs): The entry point for the program
* [Security.fs](Security.fs): File for the security analysis
* [SignAnalysis.fs](SignAnalysis.fs): File for the sign analysis
* [ProgramVerification.fs](ProgramVerification.fs): File for program verification
* [Graph.fs](Graph.fs): File for graphs
* [Interpreter.fs](Interpreter.fs): File for the interpreter


## Getting started

Building this project requires .NET 7.0. For installation, follow the description matching your platform:

- **Windows:** Installation instructions for this, can be found [here](https://dotnet.microsoft.com/en-us/download).
- **macOS:** Building on macOS requires the `dotnet-sdk` package. This can be installed using [Homebrew](https://brew.sh) and running `brew install dotnet-sdk`
- **Linux:** There are many ways to install on Linux, but a good starting point might be [this](https://fsharp.org/use/linux/).

To check that you have an up-to-date version run `dotnet --version` to display the version number, which should be something starting with 7. If it does not, consider updating your installation, and if that doesn't work, try uninstalling your current version and installing from scratch.

The next step is getting the code, which is done by cloning this repository and using `cd` to change directory to the newly cloned folder. To do this, make sure that you have your SSH keys set up correctly (instructions for [GitLab](https://docs.gitlab.com/ee/user/ssh.html)).

## Running the code

To run the program, navigate to the directory of your cloned repository and do:

```bash
dotnet run
```

This should display a list of the available commands to run. Among these are the calculator, which is a good starting point.

To run the calculator do:

```bash
dotnet run calc "1 + 52 * 23"
```

## Interactive UI

When you get further, the analysis can be explored in the interactive tool. Run the program in the `dev/` folder matching your operating system.

```bash
# Windows
./dev/win.exe --open

# macOS
./dev/macos --open

# linux
./dev/linux --open
```

With the `--open` flag this should open the tool at `http://localhost:3000/` in your browser.

The tool knows how to compile your program by the instructions in `run.toml`.

### Downloading updates

It is recommended to update the binaries in `dev/` regularly. You do this by running the command below matching your platform, and following the instructions when prompted:

```bash
# Windows
./dev/win.exe --self-update

# macOS
./dev/macos --self-update

# linux
./dev/linux --self-update
```

## Evaluation

Every time you push your Git repository, your code is ready to be evaluated automatically by your teachers.

When your project has been evaluated, the results can be seen (at GitLab) in the `result` branch.
