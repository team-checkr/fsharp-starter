module Calc

open AST
open Parse
open System

let rec evaluate (expr: expr) : Result<int, string> =
    // TODO: start here
    failwith "expression evaluator not yet implemented"

(*
    This defines the input and output for the calculator. Please do not
    change the definitions below as they are needed for the validation and
    evaluation tools!
*)

type Input = { expression: string }

type Output = { result: string; error: string }

let analysis (input: Input) : Output =
    match parse Parser.start_expression input.expression with
    | Ok ast ->
        Console.Error.WriteLine("> {0}", ast)
        match evaluate ast with
        | Ok result -> { result = result.ToString(); error = "" }
        | Error e -> { result = ""; error = String.Format("Evaluation error: {0}", e) }
    | Error e -> { result = ""; error = String.Format("Parse error: {0}", e) }
