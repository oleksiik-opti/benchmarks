using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Benchmarks.Common;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Benchmarks;

[MemoryDiagnoser]
public class QueueVsList_Benchmark
{
    public static readonly UserDetails SampleRecord = new()
    {
        Id = 1,
        FirstName = "John",
        LastName = "Doe",
        Email = "",
    };


    private List<(UserDetails, int)> _usersList;
    private Queue<(UserDetails, int)> _usersQueue;

    [Benchmark(Baseline = true)]
    public void Queue()
    {
        _usersQueue = [];
        _usersQueue.Enqueue((SampleRecord, 2));
        _usersQueue.Dequeue();
    }

    [Benchmark]
    public void List()
    {
        _usersList = [];
        _usersList.Add((SampleRecord, 2));
        _ = _usersList.First();
        _usersList.RemoveAt(0);
    }

    public static void Main()
    {
        BenchmarkRunner.Run<QueueVsList_Benchmark>();
    }
}
