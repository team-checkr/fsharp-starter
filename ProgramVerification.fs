module ProgramVerification

open System
open Predicate.AST

(*
    This defines the input and output for the program verification analysis.
    Please do not change the definitions below as they are needed for the
    validation and evaluation tools!
*)

type Input = unit

type Output =
    { verification_conditions: List<SerializedPredicate> }

let analysis (src: string) (input: Input) : Output =
    let (P, C, Q) =
        match Predicate.Parse.parse src with
        | Ok (AnnotatedCommand (P, C, Q)) -> P, C, Q
        | Error e ->
            failwith
                $"Failed to parse.\n\nDid you remember to surround your program with predicate blocks, like so?\n\n  {{ true }} skip {{ true }}\n\n{e}"

    // TODO: Remove these print statements
    Console.Error.WriteLine("P = {0}", P)
    Console.Error.WriteLine("C = {0}", C)
    Console.Error.WriteLine("Q = {0}", Q)

    let verification_conditions: List<Predicate> =
        failwith "Program verification not yet implemented" // TODO: start here

    // Let this line stay as it is.
    { verification_conditions = List.map serialize_predicate verification_conditions }
