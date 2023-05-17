using System.Diagnostics;
using AltiumSorter;

(int fileChunkSize, int totalLines) ParseArgs()
{
    var rawChunkSize = args.Length > 0 ? args[0] : "50";
    int.TryParse(rawChunkSize, out var fileChunkSizeInMb);
    var chunkSize = fileChunkSizeInMb * 1024 * 1024;

    var rawTotalLines = args.Length > 1 ? args[1] : "2000000";
    int.TryParse(rawTotalLines, out var lines);
    return (chunkSize, lines);
}

const string fileName = "generated.txt";
var (fileChunkSize, totalLines) = ParseArgs();

var timer = new Stopwatch();
Console.WriteLine($"started generating file with totalLines: {totalLines}");
timer.Start();
Generator.Generate(totalLines, fileName);
timer.Stop();
Console.WriteLine("ended generating in ms: " + timer.ElapsedMilliseconds);

timer.Reset();
timer.Start();
Console.WriteLine($"started sorting file with fileChunkSize in mb: {fileChunkSize}");
LargeTextSorter.Sort(fileName, fileChunkSize, totalLines);
timer.Stop();
Console.WriteLine("ended sorting in ms: " + timer.ElapsedMilliseconds);
Console.ReadLine();

      
