# Dictonary Benchmarks

---

## `Dictionary<,>` vs `ConcurrentDictionary<,>` for reads

### Description
```csharp
var _regularDictionary = new Dictionary<string, object>
{
    { "key1", "value1" },
    { "key2", "value2" },
    { "key3", "value3" },
};

var _concurrentDictionary = new ConcurrentDictionary<string, object>();
_concurrentDictionary.TryAdd("key1", "value1");
_concurrentDictionary.TryAdd("key2", "value2");
_concurrentDictionary.TryAdd("key3", "value3");

// regular dictionary
_regularDictionary.GetValueOrDefault("key2");

// concurrent dictionary
_concurrentDictionary.GetOrAdd("key2", _ => "value2");
```

### Environment
```
BenchmarkDotNet v0.14.0, macOS 26.3 (25D125) [Darwin 25.3.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 10.0.100
  [Host]     : .NET 8.0.8 (8.0.824.36612), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 8.0.8 (8.0.824.36612), Arm64 RyuJIT AdvSIMD
```

### Results
```
| Method               | Mean     | Error     | StdDev    | Ratio | Allocated | Alloc Ratio |
|--------------------- |---------:|----------:|----------:|------:|----------:|------------:|
| Dictionary           | 6.235 ns | 0.0510 ns | 0.0477 ns |  1.00 |         - |          NA |
| ConcurrentDictionary | 5.446 ns | 0.0527 ns | 0.0467 ns |  0.87 |         - |          NA |
```

Check the source code [here](../Benchmarks/Dictionary.cs).

---