using System.Diagnostics;
using AltiumSorter;

Solve();

void Solve()
{
    const string fileName = "generated.txt";
    var (fileChunkSize, totalLines) = ParseArgs();

    var timer = new Stopwatch();
    GenerateRandomFile(fileChunkSize ,fileName, totalLines, timer);
    
    timer.Reset();
    Sort(fileChunkSize, fileName, totalLines, timer);
}
      
(int fileChunkSize, int totalLines) ParseArgs()
{
    var rawChunkSize = args.Length > 0 ? args[0] : "50";
    int.TryParse(rawChunkSize, out var fileChunkSizeInMb);
    var chunkSize = fileChunkSizeInMb * 1024 * 1024;

    var rawTotalLines = args.Length > 1 ? args[1] : "2000000";
    int.TryParse(rawTotalLines, out var lines);
    return (chunkSize, lines);
}

void GenerateRandomFile(long fileChunkSize, string fileName, int totalLines, Stopwatch stopwatch)
{
    Console.WriteLine($"started generating file with totalLines: {totalLines}");
    stopwatch.Start();
    Generator.Generate(totalLines, fileChunkSize, fileName);
    stopwatch.Stop();
    Console.WriteLine("ended generating in ms: " + stopwatch.ElapsedMilliseconds);
}

void Sort(int chunkSize, string fileName, int totalLines, Stopwatch stopwatch)
{
    stopwatch.Start();
    Console.WriteLine($"started sorting file with fileChunkSize in mb: {chunkSize/(1024*1024)}");
    LargeTextSorter.Sort(fileName, chunkSize, totalLines);
    stopwatch.Stop();
    Console.WriteLine("ended sorting in ms: " + stopwatch.ElapsedMilliseconds);
    Console.ReadLine();
}