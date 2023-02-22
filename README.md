# Mandatory Assignment

Everything you need to work on the mandatory assignment is found in this git repository.

**Please read this document carefully as it explains the tasks and rules of the mandatory assignment.**

This document is structured as follows:

1. Goals of the mandatory assignment
2. Deadlines
3. Guidelines
4. Definition of Yet Another GCL Language
5. Task descriptions
6. Getting started with the code framework

## Goals of the mandatory assignment

The overall goal of the mandatory assignment is to build a tool for running and analysing programs written in a variant of the Guarded Command Language (GCL).
That is, you will learn about the most common steps one encounters when developing a new programming language.
You will also implement various tools that help programmers to write correct programs in that language.
Most components of the mandatory assignment can be seen as a basic version of [http://www.formalmethods.dk/fm4fun](http://www.formalmethods.dk/fm4fun).

The assignment is divided into seven tasks, where each tasks corresponds to a module of your tool.
The overall structure of the assignment is illustrated below, where green boxes are inputs and outputs of your tool; blue boxes represent components that you have to implement.

![Structure of the Assignment]()

We briefly describe the aims of each task:

1. In task 1, you will implement a *parser* that takes a program in our new programming language and turns it into an abstract syntax tree (AST) - one of the main data structures used by the other components. To test your implementation, you will also implement a *pretty printer* that traverses the AST and outputs the original program in a nice format.
2. In task 2, you will implement a *compiler* that takes the AST of a program and constructs its program graph (PG) - another data structure used for running and analyzing programs. To simplify debugging, you will also implement a *printer* that outputs a graphical representation of program graphs.
3. In task 3, you will implement an *interpreter* that takes a program graph and an initial memory and computes the program's (complete) execution sequences when started on that memory.
4. In tasks 4 - 7, you will implement tools that help programmers with writing correct programs:
    - In task 4, you will implement a *verifier* that checks whether a program does what it has been specified to do.
    - In task 5, you will implement a *signs analysis* that determines the signs of variables at every point of a program's execution; such information can be used to detect bugs, such as a division by zero, before actually running the program.
    - In task 6, you will implement another program analysis that checks whether your program leaks confidential information.
    - In task 7, you will implement a small model checker to analyse more advanced properties.


## Deadlines

**For each task, you have to hand in your solution by comitting and *pushing* them into your group's repository before the task's deadline listed below.**
 
- Task 1: 
- Task 2:
- Task 3:
- Task 4:
- Task 5:
- Task 6:
- Task 7:

## Guidelines for working on the mandatory assignment

### Rules

- You should work on the project in groups of size 2 - 3.
- You need to use your group's git repository (which also contains this document) to work on the mandatory assignments. All solutions need to be handed in by comitting *and pushing* them to your group's git repository.
    * If you have not used git before, you can find [tutorials online](https://git-scm.com/docs/gittutorial).
- You can push to your group's repository as often as you want. We will consider the last push before the individual task's [deadline]() as your submission.
- You can continue working on the module of a task after the deadline. However, such updates will not be evaluated.
- You have to implement your solutions within the code framework provided in this repository. 
- Your solution must be implemented in F#.
- You have to implement the techniques presented in the teaching material.
- You are **not** allowed to change any code in the existing code framework unless there is a comment that explicitly allows you to change code.
- You are allowed to add code in the marked areas of the code framework.
- You are allowed to add files but do not forget to add them to your git repository.


### Feedback and Evaluation

We will *not* publish solutions of any mandatory assignment tasks.
Instead, the code framework comes with an evaluation tool that automatically gives you feedback on your solutions, whenever you push your solutions to this repository. 
Furthermore, we encourage you to practively seek feedback from the TAs and the teacher in class during lab days.

We want to encourage you to design, implement, test, and analyze your code carefully. 
Hence, your final submissions will be checked by a more powerful version of the evaluation tool available to you.
In other words: even if the evaluation tool available to you does not detect any errors, the final evaluation might still be able to spot some.

The assignment is *mandatory*: you need to hand-in a *reasonable* solution for each task in order to be admitted to the final exam.

By *reasonable*, we mean that your solution does not always have to work perfectly, but your solution should demonstrate that you can faithfully implement the techniques covered in class.
As a general guideline, if the evaluation tool available to you does not report any errors and there is no obvious cheating (e.g. hard-coding certain examples), you can safely assume that your solution is reasonable.

### Assignment Competition

After every deadline, we will publish a overview of the solutions of all groups such that you can compare the quality of your solution against those of your classmates. The solutions will be ranked according to the number of detected errors and their efficiency.

The group names will be anonymised, i.e. you will only be able to identify the position of your own group in the ranking.
The code to identify your own group is found in this repository [TODO: link to your code]().

## Yet Another GCL Variant

The variant of the Guarded Command Language (GCL) that you have to consider throughout the mandatory assignment is a subset of the language used by [http://www.formalmethods.dk/fm4fun](http://www.formalmethods.dk/fm4fun).
More precisely, the language is given by the following BNF grammar:

```
C  ::=  x := a  |  A[a] := a  |  skip  |  C ; C  |  if GC fi  |  do GC od
GC ::=  b -> C  |  GC [] GC
a  ::=  n  |  x  |  A[a]  |  a + a  |  a - a  |  a * a  |  a / a  |  - a  |  a ^ a  |  (a)
b  ::=  true  |  false  |  b & b  |  b | b  |  b && b  |  b || b  |  ! b
     |  a = a  |  a != a  |  a > a  |  a >= a  |  a < a  |  a <= a  |  (b)
```
where `n` is an integer number, `x` is a program variable, and `A` is an array.

The syntax of variables and numbers, and the associativity and precedence of operators must be the same as in [http://www.formalmethods.dk/fm4fun](http://www.formalmethods.dk/fm4fun); you can find more details on FM4FUN by clicking on the question mark of besides "Examples".
We reproduce parts of the rules here for your convenience:

- Variables `x` and arrays `A` are strings matching the regular expression `[a-zA-Z][a-zA-Z\d ]*` and cannot be any of the language's keywords (e.g. no variable may be named `if` or `od`).
- Numbers `n` match the regular expression `\d+`.
- A whitespace matches the regular expression `[\u00A0 \n \r \t]`, with a mandatory whitespace after if, do, and before fi, od. Whitespaces are ignored anywhere else.
- Precedence and associativity rules:
    * In arithmetic expressions, precedence is highest for `-` (unary minus), then `^`, then `*` and `/`, and lowest for `+` and `-` (binary minus).
    * In boolean expressions, precedence is highest for `!`, then `&` and `&&`, and lowest for `|` and `||`.
    * Operators `*`, `/`, `+`, `-`, `&`, `|`, `&&`, and `||` are left-associative.
    * Operators `^`, `[]`, and `;` are right associative.

**In the rest of the document GCL refers to the above language.**




## F# Starter

This folder contains the skeleton of a parser along with the input and output types for each analysis given in the assignment. It also contain an example of a "calculator" program in F# that reads an arithmetic expression from the command line and print the result of evaluating such expression for initial testing.

### Files

F#/FsLexYacc
* [Lexer.fsl](Lexer.fsl): The lexer for arithmetic expressions
* [Parser.fsp](Parser.fsp): The parser for arithmetic expressions
* [Types.fs](Types.fs): Global types that are used in many analysis
* [AST.fs](AST.fs): Types for AST of arithmetic expressions
* [Program.fs](Program.fs): The entrypoint for the program
* [Security.fs](Security.fs): File for the security analysis
* [SignAnalysis.fs](SignAnalysis.fs): File for the sign analysis
* [ProgramVerification.fs](ProgramVerification.fs): File for program verification
* [Graph.fs](Graph.fs): File for graphs
* [Interpreter.fs](Interpreter.fs): File for the interpreter


### Getting started

Building this project requires .NET 7.0. Installation

- **Windows:** Installation instructions for this, can be found [here](https://dotnet.microsoft.com/en-us/download).
- **macOS:** Building on macOS requires the `dotnet-sdk` package. This can be installed using [Homebrew](https://brew.sh) and running `brew install dotnet-sdk`
- **Linux:** There are many ways to install on Linux, but a good starting point might be [this](https://fsharp.org/use/linux/).


### Running the code

To run the program do:

```bash
dotnet run
```

This should display a list of the available commands to run. Among these are the calculator, which is a good starting point.

To run the calculator do:

```bash
dotnet run calc "1 + 52 * 23"
```

### Interactive UI

When you get further, the analysis can be explored in the interactive tool. Run the program in `dev/` folder matching you operating system.

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

#### Downloading updates

It is recommended to update the binaries in `dev/` regularly. You do this by running the command below matching your platform, and following the instructions when prompted:

```bash
# Windows
./dev/win.exe --self-update

# macOS
./dev/macos --self-update

# linux
./dev/linux --self-update
```

### Evaluation

Every time you push to git, the program is ready to be evaluated automatically by your teachers.

The results as they are produced, can be seen (at GitLab) in the `result` branch.
