namespace Models
{
    public class Vessel
    {
        public string Id { get; }
        public int X { get; }
        public int Y { get; }
        public int Range { get; }
        public char Type { get; }

        public Vessel(string id, int x, int y, int range, char type)
        {
            Id = id;
            X = x;
            Y = y;
            Range = range;
            Type = type;
        }
    }
}