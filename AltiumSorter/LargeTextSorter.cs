using System.Text;

namespace AltiumSorter
{
    public static class LargeTextSorter
    {
        private const string AnySortedPattern = "sorted*.txt";
        private const string SortedChunkPattern = "sorted_*.txt";
        public static void Sort(string fileName, long chunkSize, long totalLines)
        {
            RemovePrevious(AnySortedPattern);

            using var readFileStream = File.OpenRead(fileName);
            var buffer = new byte[chunkSize];
            var encoding = new UTF8Encoding(true);
            string? memorizedPrefix = null;
            var writtenChunks = 0;

            int readLen;
            while ((readLen = readFileStream.Read(buffer,0,buffer.Length)) > 0)
            {
                var splitString = encoding.GetString(buffer, 0, readLen).Split(Environment.NewLine);
                if (memorizedPrefix != null)
                    splitString[0] = $"{memorizedPrefix}{splitString[0]}";
                memorizedPrefix = splitString.Last();
                var lines = splitString.Take(splitString.Length - 1).Select(Parse).ToArray();

                Array.Sort(lines);
                
                File.WriteAllLines($"sorted_{writtenChunks}.txt", lines.Select(x => x.ToString()));
                writtenChunks++;
            }
            readFileStream.Close();

            MergeAllSortedChunks(totalLines, writtenChunks);
            RemovePrevious(SortedChunkPattern);
        }

        private static void MergeAllSortedChunks(long totalLines, int writtenChunks)
        {
            using var writeFileStream = File.OpenWrite("sorted.txt");
            var streams = new StreamReader[writtenChunks];
            var firstLines = new Line?[writtenChunks];
            for (var i = 0; i < writtenChunks; i++)
            {
                streams[i] = new StreamReader($"sorted_{i}.txt");
                firstLines[i] = Parse(streams[i].ReadLine());
            }

            while (totalLines > 0)
            {
                var minIndex = Array.IndexOf(firstLines, firstLines.Min());
                var data = new UTF8Encoding(true).GetBytes(firstLines[minIndex] + Environment.NewLine);
                writeFileStream.Write(data, 0, data.Length);
                firstLines[minIndex] = Parse(streams[minIndex].ReadLine());
                totalLines--;
            }

            foreach (var stream in streams)
                stream.Close();
            writeFileStream.Close();
        }

        private static void RemovePrevious(string pattern)
        {
            var dir = new DirectoryInfo(Directory.GetCurrentDirectory());
            foreach (var file in dir.EnumerateFiles(pattern))
                file.Delete();
        }

        private static Line? Parse(string? line)
        {
            if (line == null)
                return null;
            
            var dotIdx = line.IndexOf('.');
            if (dotIdx == -1)
                throw new Exception("wrong format, no number");

            return new Line
            {
                Number = long.Parse(line[..dotIdx]),
                Text = line[(dotIdx + Line.SplitLength)..]
            };
        }
    }
}