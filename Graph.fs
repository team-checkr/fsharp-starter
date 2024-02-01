module Graph

open Types

(*
    This defines the input and output for graphs. Please do not
    change the definitions below as they are needed for the validation and
    evaluation tools!
*)

type Input = { commands: string
               determinism: Determinism }

type Output = { dot: string }

let analysis (input: Input) : Output =
    failwith "Graph analysis not yet implemented" // TODO: start here
