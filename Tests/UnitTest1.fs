module streamjsonrpc_repro

open System
open Nerdbank.Streams
open StreamJsonRpc
open NUnit.Framework
open Library

let private runWithDaemon (fn: JsonRpc -> Async<unit>) =
    async {
        let struct (serverStream, clientStream) = FullDuplexStream.CreatePair()

        let daemon = new Daemon(serverStream, serverStream)

        let client = new JsonRpc(clientStream, clientStream)
        client.StartListening()
        do! fn client
        client.Dispose()
        (daemon :> IDisposable).Dispose()
    }

[<Test>]
let reproduction () =
    runWithDaemon (fun client ->
        async {
            let request: FormatSelectionRequest =
                let range = FormatSelectionRange(3, 0, 3, 16)
                { Range = range }

            let! response = client.InvokeAsync<int>(FormatSelection, request) |> Async.AwaitTask

            Assert.AreEqual(0, response)
        })
