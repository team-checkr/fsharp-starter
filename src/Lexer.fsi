module Lexer

open FSharp.Text.Lexing
open System
open Grammar/// Rule tokenize
val tokenize: lexbuf: LexBuffer<char> -> token
