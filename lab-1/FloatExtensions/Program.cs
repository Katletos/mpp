using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using lab_1;

BenchmarkRunner.Run<Bench>();

[MemoryDiagnoser]
[RPlotExporter]
public class Bench
{
    private readonly float _number = float.Pi;
    
    [Benchmark]
    public void NormalTest()
    {
        _number.ToIeee754Representation();
    }

    [Benchmark]
    public void LinqTest()
    {
        _number.ToIeee754RepresentationLinq();
    }
}
