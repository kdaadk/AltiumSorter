namespace AltiumSorter
{
    public class Line : IComparable<Line>
    {
        public const int SplitLength = 2;
        public long Number { get; set; }
        public string Text { get; set; }
        public int CompareTo(Line? other)
        {
            if (other == null)
                return 1;
            if (Text == other.Text)
                return Number.CompareTo(other.Number);
            return string.Compare(Text, other.Text, StringComparison.Ordinal);
        }

        public override string ToString() => $"{Number}. {Text}";
    }
}

