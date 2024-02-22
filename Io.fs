module Io

open System.Text.Json.Serialization

module rec Calc =
  type Input = { expression: string }
  type Output = { result: string ; error: string }

module rec GCL =
  [<JsonFSharpConverter(BaseUnionEncoding = JsonUnionEncoding.ExternalTag + JsonUnionEncoding.UnwrapFieldlessTags + JsonUnionEncoding.UnwrapSingleFieldCases)>]
  type Determinism =
    | Deterministic
    | NonDeterministic
  type TargetDef = { name: string ; kind: GCL.TargetKind }
  [<JsonFSharpConverter(BaseUnionEncoding = JsonUnionEncoding.ExternalTag + JsonUnionEncoding.UnwrapFieldlessTags + JsonUnionEncoding.UnwrapSingleFieldCases)>]
  type TargetKind =
    | Variable
    | Array
  type Variable = string
  type Array = string

module rec Graph =
  type Input = { commands: string ; determinism: GCL.Determinism }
  type Output = { dot: string }

module rec Interpreter =
  type Input = { commands: string ; determinism: GCL.Determinism ; assignment: Interpreter.InterpreterMemory ; trace_length: int64 }
  type Output = { initial_node: string ; final_node: string ; dot: string ; trace: List<Interpreter.Step> ; termination: Interpreter.TerminationState }
  type InterpreterMemory = { variables: Map<GCL.Variable, int64> ; arrays: Map<GCL.Array, List<int64>> }
  [<JsonFSharpConverter(BaseUnionEncoding = JsonUnionEncoding.ExternalTag + JsonUnionEncoding.UnwrapFieldlessTags + JsonUnionEncoding.UnwrapSingleFieldCases)>]
  type TerminationState =
    | Running
    | Stuck
    | Terminated
  type Step = { action: string ; node: string ; memory: Interpreter.InterpreterMemory }

module rec Parser =
  type Input = { commands: string }
  type Output = { pretty: string }

module rec SignAnalysis =
  type Input = { commands: string ; determinism: GCL.Determinism ; assignment: SignAnalysis.SignMemory }
  type Output = { initial_node: string ; final_node: string ; nodes: Map<string, List<SignAnalysis.SignMemory>> ; dot: string }
  type SignMemory = { variables: Map<GCL.Variable, SignAnalysis.Sign> ; arrays: Map<GCL.Array, List<SignAnalysis.Sign>> }
  [<JsonFSharpConverter(BaseUnionEncoding = JsonUnionEncoding.ExternalTag + JsonUnionEncoding.UnwrapFieldlessTags + JsonUnionEncoding.UnwrapSingleFieldCases)>]
  type Sign =
    | Positive
    | Zero
    | Negative

module rec ce_shell =
  [<JsonFSharpConverter(BaseUnionEncoding = JsonUnionEncoding.UnwrapSingleFieldCases, UnionTagName = "analysis", UnionFieldsName = "io")>]
  type Envs =
    | Calc of input: Calc.Input * output: Calc.Output * meta: unit
    | Parse of input: Parser.Input * output: Parser.Output * meta: unit
    | Graph of input: Graph.Input * output: Graph.Output * meta: unit
    | Interpreter of input: Interpreter.Input * output: Interpreter.Output * meta: List<GCL.TargetDef>
    | Sign of input: SignAnalysis.Input * output: SignAnalysis.Output * meta: List<GCL.TargetDef>

