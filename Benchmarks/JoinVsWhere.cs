using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Benchmarks.Common;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Benchmarks;

[MemoryDiagnoser]
public class JoinVsWhere_Benchmark
{
    private List<UserDetails> _users;
    private int[] _userIds;

    [Params(20, 200, 2000)]
    public int N;

    [GlobalSetup]
    public void Setup()
    {
        _users = Enumerable
            .Range(0, N)
            .Select(x => new UserDetails
            {
                Id = x,
                FirstName = x.ToString(),
                LastName = x.ToString(),
                Email = x.ToString()
            })
            .ToList();

        _userIds = Enumerable.Range(0, N).Where(x => x % 3 == 0).ToArray();
    }

    [Benchmark(Baseline = true)]
    public List<UserDetails> Join()
    {
        return _users.Join(_userIds, f => f.Id, b => b, (f, _) => f).ToList();
    }

    [Benchmark]
    public List<UserDetails> Where()
    {
        return _users.Where(f => _userIds.Contains(f.Id)).ToList();
    }

    public static void Main()
    {
        BenchmarkRunner.Run<JoinVsWhere_Benchmark>();
    }
}
