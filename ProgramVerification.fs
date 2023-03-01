module ProgramVerification

(*
    This defines the input and output for the program verification analysis.
    Please do not change the definitions below as they are needed for the
    validation and evaluation tools!
*)

type Input = { post_condition: string }

type Output = { pre_condition: string }

let analysis (src: string) (input: Input) : Output =
    failwith "Program verification analysis not yet implemented" // TODO: start here
