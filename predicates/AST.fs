module Predicate.AST

type AOp =
    | Add
    | Sub
    | Mul
    | Pow
    override this.ToString() =
        match this with
        | Add -> "+"
        | Sub -> "-"
        | Mul -> "*"
        | Pow -> "^"

type AExpr =
    | Number of int
    | Variable of string
    | LogicalVariable of string
    | Array of string * AExpr
    | LogicalArray of string * AExpr
    | Binary of AExpr * AOp * AExpr
    | Function of Function
    override this.ToString() =
        match this with
        | Number n -> $"{n}"
        | Variable v -> v
        | LogicalVariable v -> v
        | Array (a, idx) -> $"{a}[{idx}]"
        | LogicalArray (a, idx) -> $"{a}[{idx}]"
        | Binary (lhs, op, rhs) -> $"({lhs} {op} {rhs})"
        | Function f -> $"{f}"

and Function =
    | Division of AExpr * AExpr
    | Min of AExpr * AExpr
    | Max of AExpr * AExpr
    | Count of string * AExpr
    | LogicalCount of string * AExpr
    | Length of string
    | LogicalLength of string
    | Fac of AExpr
    | Fib of AExpr
    override this.ToString() =
        match this with
        | Division (a, b) -> $"division({a}, {b})"
        | Min (a, b) -> $"min({a}, {b})"
        | Max (a, b) -> $"max({a}, {b})"
        | Count (a, b) -> $"count({a}, {b})"
        | LogicalCount (a, b) -> $"count({a}, {b})"
        | Length x -> $"length({x})"
        | LogicalLength x -> $"length({x})"
        | Fac x -> $"fac({x})"
        | Fib x -> $"fib({x})"

type BOp =
    | Or
    | LOr
    | And
    | LAnd
    | Implies
    override this.ToString() =
        match this with
        | Or -> "||"
        | LOr -> "|"
        | And -> "&&"
        | LAnd -> "&"
        | Implies -> "==>"

type ROp =
    | Lt
    | Le
    | Gt
    | Ge
    | Eq
    | Ne
    override this.ToString() =
        match this with
        | Lt -> "<"
        | Le -> "<="
        | Gt -> ">"
        | Ge -> ">="
        | Eq -> "="
        | Ne -> "!="

type Predicate =
    | Bool of bool
    | RelationalOp of AExpr * ROp * AExpr
    | BooleanOp of Predicate * BOp * Predicate
    | Exists of string * Predicate
    | Forall of string * Predicate
    | Not of Predicate
    override this.ToString() =
        match this with
        | Bool true -> "true"
        | Bool false -> "false"
        | RelationalOp (lhs, op, rhs) -> $"({lhs} {op} {rhs})"
        | BooleanOp (lhs, op, rhs) -> $"({lhs} {op} {rhs})"
        | Exists (x, pred) -> $"(exists {x} :: {pred})"
        | Forall (x, pred) -> $"(forall {x} :: {pred})"
        | Not pred -> $"!{pred}"

type SerializedPredicate = { predicate: string }

let serialize_predicate (pred: Predicate) = { predicate = string pred }

type BExpr = Predicate

type Command =
    | Skip
    | Assign of string * AExpr
    | ArrayAssign of string * AExpr * AExpr
    | Sep of Command * Command
    | If of GuardedCommand
    | Do of Predicate * GuardedCommand
    override this.ToString() =
        match this with
        | Skip -> "skip"
        | Assign (x, e) -> $"{x} := {e}"
        | ArrayAssign (a, idx, e) -> $"{a}[{idx}] := {e}"
        | Sep (a, b) -> $"{a} ; {b}"
        | If g -> $"if {g} fi"
        | Do (inv, g) -> $"do {{{inv}}} {g} od"

and GuardedCommand =
    | Guard of BExpr * Command
    | Choice of GuardedCommand * GuardedCommand
    override this.ToString() =
        match this with
        | Guard (b, cmd) -> $"{b} -> {cmd}"
        | Choice (cmd1, cmd2) -> $"{cmd1} [] {cmd2}"

type AnnotatedCommand =
    | AnnotatedCommand of Predicate * Command * Predicate
    override this.ToString() =
        match this with
        | AnnotatedCommand (p, c, q) -> $"{{{p}}} {c} {{{q}}}"
