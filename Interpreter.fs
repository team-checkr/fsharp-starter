module Interpreter

open Types

(*
    This defines the input and output for the interpreter. Please do not
    change the definitions below as they are needed for the validation and
    evaluation tools!
*)
type InterpreterMemory =
    { variables: Map<string, int>
      arrays: Map<string, List<int>> }

type Input =
    { determinism: Determinism
      assignment: InterpreterMemory
      trace_length: int }

// TODO: Change this to you internal version of node
// If your node type is defined in graph, consider using the following:
// type Node = Graph.Node
type Node = string

type TerminationState =
    | Running
    | Stuck
    | Terminated

type Configuration<'node> =
    { node: 'node
      memory: InterpreterMemory }

type Output =
    { execution_sequence: List<Configuration<string>>
      final: TerminationState }

let stringifyNode (internalNode: Node) : string =
    // TODO: implement for internal node type
    internalNode

let prepareConfiguration (c: Configuration<Node>) : Configuration<string> =
    { node = stringifyNode c.node
      memory = c.memory }


let analysis (src: string) (input: Input) : Output =
    failwith "Interpreter not yet implemented" // TODO: start here
    let execution_sequence: List<Configuration<Node>> = failwith "TODO"
    let final: TerminationState = failwith "TODO"

    { execution_sequence = List.map prepareConfiguration execution_sequence
      final = final }
