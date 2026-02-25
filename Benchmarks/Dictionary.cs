using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Benchmarks;

[MemoryDiagnoser]
public class Dictionary_Benchmark
{
    private Dictionary<string, object> _regularDictionary;
    private ConcurrentDictionary<string, object> _concurrentDictionary;

    [GlobalSetup]
    public void Setup()
    {
        _regularDictionary = new Dictionary<string, object>
        {
            { "key1", "value1" },
            { "key2", "value2" },
            { "key3", "value3" },
        };
        _concurrentDictionary = new ConcurrentDictionary<string, object>();

        _concurrentDictionary.TryAdd("key1", "value1");
        _concurrentDictionary.TryAdd("key2", "value2");
        _concurrentDictionary.TryAdd("key3", "value3");
    }

    [Benchmark(Baseline = true)]
    public void Dictionary()
    {
        _regularDictionary.GetValueOrDefault("key2");
    }

    [Benchmark]
    public void ConcurrentDictionary()
    {
        _concurrentDictionary.GetOrAdd("key2", _ => "value2");
    }

    public static void Main()
    {
        BenchmarkRunner.Run<Dictionary_Benchmark>();
    }
}
