# TypeLoadException with StreamJsonRpc

Steps to reproduce:

Restore and build the solution.
Run the unit test in the `Tests` project.
This should yield:

```
System.AggregateException : One or more errors occurred. (Error writing JSON RPC Message: TypeLoadException: Could not load type 'System.Runtime.CompilerServices.IsReadOnlyAttribute' from assembly 'System.Collections.Immutable, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'.)
  ----> Newtonsoft.Json.JsonSerializationException : Error writing JSON RPC Message: TypeLoadException: Could not load type 'System.Runtime.CompilerServices.IsReadOnlyAttribute' from assembly 'System.Collections.Immutable, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'.
  ----> System.TypeLoadException : Could not load type 'System.Runtime.CompilerServices.IsReadOnlyAttribute' from assembly 'System.Collections.Immutable, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'.
   at NUnit.Framework.Internal.TaskAwaitAdapter.GenericAdapter`1.GetResult()
   at NUnit.Framework.Internal.AsyncToSyncAdapter.Await(Func`1 invoke)
   at NUnit.Framework.Internal.Commands.TestMethodCommand.RunTestMethod(TestExecutionContext context)
   at NUnit.Framework.Internal.Commands.TestMethodCommand.Execute(TestExecutionContext context)
   at NUnit.Framework.Internal.Execution.SimpleWorkItem.<>c__DisplayClass4_0.<PerformWork>b__0()
   at NUnit.Framework.Internal.ContextUtils.<>c__DisplayClass1_0`1.<DoIsolated>b__0(Object _)
   at System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state)
--- End of stack trace from previous location ---
   at System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state)
   at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state)
   at NUnit.Framework.Internal.ContextUtils.DoIsolated(ContextCallback callback, Object state)
   at NUnit.Framework.Internal.ContextUtils.DoIsolated[T](Func`1 func)
   at NUnit.Framework.Internal.Execution.SimpleWorkItem.PerformWork()
--JsonSerializationException
   at StreamJsonRpc.JsonMessageFormatter.Serialize(JsonRpcMessage message)
   at StreamJsonRpc.JsonMessageFormatter.Serialize(IBufferWriter`1 contentBuffer, JsonRpcMessage message)
   at StreamJsonRpc.HeaderDelimitedMessageHandler.Write(JsonRpcMessage content, CancellationToken cancellationToken)
   at StreamJsonRpc.PipeMessageHandler.WriteCoreAsync(JsonRpcMessage content, CancellationToken cancellationToken)
   at StreamJsonRpc.MessageHandlerBase.WriteAsync(JsonRpcMessage content, CancellationToken cancellationToken)
   at StreamJsonRpc.JsonRpc.TransmitAsync(JsonRpcMessage message, CancellationToken cancellationToken)
   at StreamJsonRpc.JsonRpc.InvokeCoreAsync(JsonRpcRequest request, Type expectedResultType, CancellationToken cancellationToken)
   at StreamJsonRpc.JsonRpc.InvokeCoreAsync[TResult](RequestId id, String targetName, IReadOnlyList`1 arguments, IReadOnlyList`1 positionalArgumentDeclaredTypes, IReadOnlyDictionary`2 namedArgumentDeclaredTypes, CancellationToken cancellationToken, Boolean isParameterObject)
--TypeLoadException
   at System.ModuleHandle.ResolveType(QCallModule module, Int32 typeToken, IntPtr* typeInstArgs, Int32 typeInstCount, IntPtr* methodInstArgs, Int32 methodInstCount, ObjectHandleOnStack type)
   at System.ModuleHandle.ResolveTypeHandle(Int32 typeToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
   at System.Reflection.RuntimeModule.ResolveType(Int32 metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
   at System.Reflection.CustomAttribute.FilterCustomAttributeRecord(MetadataToken caCtorToken, MetadataImport& scope, RuntimeModule decoratedModule, MetadataToken decoratedToken, RuntimeType attributeFilterType, Boolean mustBeInheritable, ListBuilder`1& derivedAttributes, RuntimeType& attributeType, IRuntimeMethodInfo& ctorWithParameters, Boolean& isVarArg)
   at System.Reflection.CustomAttribute.IsCustomAttributeDefined(RuntimeModule decoratedModule, Int32 decoratedMetadataToken, RuntimeType attributeFilterType, Int32 attributeCtorToken, Boolean mustBeInheritable)
   at System.Reflection.CustomAttribute.IsDefined(RuntimeMethodInfo method, RuntimeType caType, Boolean inherit)
   at System.Reflection.RuntimeMethodInfo.IsDefined(Type attributeType, Boolean inherit)
   at Newtonsoft.Json.Serialization.DefaultContractResolver.IsValidCallback(MethodInfo method, ParameterInfo[] parameters, Type attributeType, MethodInfo currentCallback, Type& prevAttributeType)
   at Newtonsoft.Json.Serialization.DefaultContractResolver.GetCallbackMethodsForType(Type type, List`1& onSerializing, List`1& onSerialized, List`1& onDeserializing, List`1& onDeserialized, List`1& onError)
   at Newtonsoft.Json.Serialization.DefaultContractResolver.ResolveCallbackMethods(JsonContract contract, Type t)
   at Newtonsoft.Json.Serialization.DefaultContractResolver.InitializeContract(JsonContract contract)
   at Newtonsoft.Json.Serialization.DefaultContractResolver.CreateObjectContract(Type objectType)
   at Newtonsoft.Json.Serialization.DefaultContractResolver.CreateContract(Type objectType)
   at System.Collections.Concurrent.ConcurrentDictionary`2.GetOrAdd(TKey key, Func`2 valueFactory)
   at Newtonsoft.Json.Utilities.ThreadSafeStore`2.Get(TKey key)
   at Newtonsoft.Json.Serialization.DefaultContractResolver.ResolveContract(Type type)
   at StreamJsonRpc.JsonMessageFormatter.ImplicitMarshalContractResolver.ResolveContract(Type type)
   at Newtonsoft.Json.Serialization.JsonSerializerInternalWriter.CalculatePropertyValues(JsonWriter writer, Object value, JsonContainerContract contract, JsonProperty member, JsonProperty property, JsonContract& memberContract, Object& memberValue)
   at Newtonsoft.Json.Serialization.JsonSerializerInternalWriter.SerializeObject(JsonWriter writer, Object value, JsonObjectContract contract, JsonProperty member, JsonContainerContract collectionContract, JsonProperty containerProperty)
   at Newtonsoft.Json.Serialization.JsonSerializerInternalWriter.SerializeValue(JsonWriter writer, Object value, JsonContract valueContract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerProperty)
   at Newtonsoft.Json.Serialization.JsonSerializerInternalWriter.Serialize(JsonWriter jsonWriter, Object value, Type objectType)
   at Newtonsoft.Json.JsonSerializer.SerializeInternal(JsonWriter jsonWriter, Object value, Type objectType)
   at Newtonsoft.Json.JsonSerializer.Serialize(JsonWriter jsonWriter, Object value)
   at Newtonsoft.Json.Linq.JToken.FromObjectInternal(Object o, JsonSerializer jsonSerializer)
   at Newtonsoft.Json.Linq.JToken.FromObject(Object o, JsonSerializer jsonSerializer)
   at StreamJsonRpc.JsonMessageFormatter.TokenizeUserData(Type declaredType, Object value)
   at StreamJsonRpc.JsonMessageFormatter.TokenizeUserData(JsonRpcMessage jsonRpcMessage)
   at StreamJsonRpc.JsonMessageFormatter.Serialize(JsonRpcMessage message)



-----

One or more child tests had errors
  Exception doesn't have a stacktrace
```

The problem occurs because the payload of the request contains a nested struct.
Updating:
```fsharp
and FormatSelectionRange =
    struct
        val StartLine: int
        val StartColumn: int
        val EndLine: int
        val EndColumn: int

        new(startLine: int, startColumn: int, endLine: int, endColumn: int) =
            { StartLine = startLine
              StartColumn = startColumn
              EndLine = endLine
              EndColumn = endColumn }
    end
```

to

```fsharp
and FormatSelectionRange =
    class
        val StartLine: int
        val StartColumn: int
        val EndLine: int
        val EndColumn: int

        new(startLine: int, startColumn: int, endLine: int, endColumn: int) =
            { StartLine = startLine
              StartColumn = startColumn
              EndLine = endLine
              EndColumn = endColumn }
    end
```

will resolve the problem.

Downgrading the `SDK` to `6.0.400` instead of `7.0.100` in the global.json will also resolve the problem. (when you leave the `struct` code alone).
I do believe there is somewhere some serialization bug with the nested struct and dotnet 7 runtime.
