using System.Text;

namespace AltiumSorter
{
    public static class Generator
    {
        private static readonly Random Rnd = new();
        private const int MaxLength = 1024;
        private const int ChunkSize = 100000;

        private static readonly string[] Fruits =
        {
            "apple", "banana", "apricot", "atemoya", "avocados", "blueberry", "blackcurrant", "ackee", "cranberry",
            "cantaloupe", "cherry", "dragonrfruit", "dates", "cherimoya", "buddha’s hand fruit", "finger lime", "fig",
            "coconut", "grapefruit", "gooseberries", "chempedak", "hazelnut", "honeyberries", "dragon fruit", "durian"
        };
        private static readonly string[] Articles = { "the", "a", "one", "some", "any", };
        private static readonly string[] Nouns =
        {
            "boy", "girl", "dog", "town", "car", "people", "history", "way", "art", "world", "information", "map",
            "two", "family", "government", "health", "system", "computer", "meat", "year", "thanks", "music",
            "person", "reading", "method", "data", "food", "understanding", "theory", "law", "bird", "literature",
            "problem", "software", "control", "knowledge", "power", "ability", "economics", "love", "internet",
            "television", "science", "library", "nature", "fact", "product", "idea", "temperature", "investment",
            "area", "society", "activity", "story", "industry", "media", "thing", "oven", "community", "definition"
        };
        private static readonly string[] Verbs =
        {
            "arose", "awoke", "was", "beat", "became", "began", "bent", "bet", "bit", "bled", "blew", "broke",
            "brought", "built", "burnt", "burst", "bought", "caught", "chose", "came", "cost", "cut", "dug", "did",
            "drew", "dreamed", "drank", "drove", "ate", "fell", "fed", "felt", "fought", "found", "flew", "forgot",
            "got", "gave", "went", "grew", "had", "heard", "hid", "hit", "held", "hurt", "kept", "knew", "led",
            "learnt", "left", "let", "lay", "lost", "made", "met", "paid", "put", "read", "rode", "rang", "ran",
            "said", "saw", "sold", "sent", "shot", "showed", "shut", "sang", "sat", "slept", "spoke", "spent",
            "stood", "stole", "swam", "took", "taught", "tore", "told", "thought", "threw", "understood", "wore",
            "won", "wrote",
        };

        private static readonly string[] Prepositions = { "to", "from", "over", "under", "on", };

        public static void Generate(long size, string name)
        {
            if (File.Exists(name))
                File.Delete(name);
            
            long writtenLines = 0;
            while (writtenLines < size)
            {
                var remainsLines = size - writtenLines;
                var nextWriteSize = remainsLines >= ChunkSize ? ChunkSize : remainsLines % ChunkSize;
                WriteToFile(nextWriteSize, name);
                writtenLines += nextWriteSize;
            }
        }

        private static void WriteToFile(long size, string name)
        {
            var generatedLines = new string[size];
            for (var i = 0; i < size; i++)
            {
                var stringPart = GenerateString(Rnd.Next(5, 30)); //line max 1024 chars a-zA-z
                generatedLines[i] = $"{GetLongRandom()}. {stringPart}";

                //~10% chance to go and add line with same stringPart
                if (Rnd.Next(0, 10) == 5 && i + 1 < size)
                {
                    generatedLines[i + 1] = $"{GetLongRandom()}. {stringPart}";
                    i++;
                }
            }
            File.AppendAllLines(name, generatedLines);
        }

        private static string GenerateString(int sentenceCount)
        {
            var sb = new StringBuilder();
            while (sentenceCount > 0)
            {
                sb.Append(GenerateSentence());
                if (sentenceCount != 1)
                    sb.Append(", ");
                sentenceCount--;
            }

            return sb.ToString();
        }

        private static string GenerateSentence() =>
                $"{NextWord(Fruits)} {NextWord(Verbs)} {NextWord(Prepositions)} {NextWord(Articles)} {NextWord(Nouns)}";

        private static string NextWord(string[] words) => words[Rnd.Next(0,words.Length)];

        private static long GetLongRandom()
        {
            long number1 = Rnd.Next(int.MinValue, int.MaxValue);
            long number2 = Rnd.Next(int.MinValue, int.MaxValue);
            return number1 + number2;
        }
    }
}


