module Predicate.Parse

open FSharp.Text.Lexing
open System

exception ParseError of Position * string * Exception

let parse src =
    let lexbuf = LexBuffer<char>.FromString src

    let parser = Predicate.Parser.start Predicate.Lexer.tokenize

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
