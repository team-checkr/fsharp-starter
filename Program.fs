open FSharp.Text.Lexing
open System
open AST
open System.Text.Json

exception ParseError of Position * string * Exception

let parse parser src =
    let lexbuf = LexBuffer<char>.FromString src

    let parser = parser Lexer.tokenize

    try
        Ok(parser lexbuf)
    with
    | e ->
        let pos = lexbuf.EndPos
        let line = pos.Line
        let column = pos.Column
        let message = e.Message
        let lastToken = new String(lexbuf.Lexeme)
        eprintf "Parse failed at line %d, column %d:\n" line column
        eprintf "Last token: %s" lastToken
        eprintf "\n"
        Error(ParseError(pos, lastToken, e))

let rec evaluate: expr -> float =
    function
    | Num x -> x
    | TimesExpr (a, b) -> evaluate a * evaluate b
    | DivExpr (a, b) -> evaluate a / evaluate b
    | PlusExpr (a, b) -> evaluate a + evaluate b
    | MinusExpr (a, b) -> evaluate a - evaluate b
    | PowExpr (a, b) -> evaluate a ** evaluate b
    | UPlusExpr a -> evaluate a
    | UMinusExpr a -> -evaluate a

// Please do not change the main function.
// The cases are needed for the validation and evaluation tools!

[<EntryPoint>]
let main (args) =
    match args |> List.ofArray with
    | ["Calc"; input] ->
        let input = JsonSerializer.Deserialize<Io.Calc.Input> input
        let output: Io.Calc.Output = Calc.analysis input
        Console.WriteLine("{0}", JsonSerializer.Serialize output)

        0
    | ["Parse"; input ] ->
        let input = JsonSerializer.Deserialize<Io.Parser.Input> input
        let output: Io.Parser.Output = Parse.analysis input
        Console.WriteLine("{0}", JsonSerializer.Serialize output)

        0
    | [ "Compiler"; input ] ->
        let input = JsonSerializer.Deserialize<Io.Compiler.Input> input
        let output: Io.Compiler.Output = Compiler.analysis input
        Console.WriteLine("{0}", JsonSerializer.Serialize output)

        0
    | [ "Interpreter"; input ] ->
        let input = JsonSerializer.Deserialize<Io.Interpreter.Input> input
        let output: Io.Interpreter.Output = Interpreter.analysis input
        Console.WriteLine("{0}", JsonSerializer.Serialize output)

        0
    | [ "Sign"; input ] ->
        let input = JsonSerializer.Deserialize<Io.SignAnalysis.Input> input
        let output: Io.SignAnalysis.Output = SignAnalysis.analysis input
        Console.WriteLine("{0}", JsonSerializer.Serialize output)

        0
    | [ "Security"; input ] ->
        let input = JsonSerializer.Deserialize<Security.Input> input
        let output: Security.Output = Security.analysis input
        Console.WriteLine("{0}", JsonSerializer.Serialize output)

        0
    | _ ->
        let commands =
            [ "Calc <INPUT>"
              "Parse <INPUT>"
              "Compiler <INPUT>"
              "Interpreter <INPUT>"
              "Sign <INPUT>"
              "Security <INPUT>" ]

        Console.Error.WriteLine(
            "\x1B[1;31merror:\x1B[0m unrecognized input: \x1B[0;33m'{0}'\x1B[0m\n\n{1}\n\nAvailable commands:\n{2}",
            String.concat " " args,
            "\x1B[1mUsage: dotnet run\x1B[0m <COMMAND>",
            (List.fold (fun acc cmd -> acc + sprintf " - \x1B[1m%s\x1B[0m\n" cmd) "" commands)
        )

        1
