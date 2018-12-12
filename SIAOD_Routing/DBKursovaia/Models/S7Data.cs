namespace DBKursovaia.Models
{
    public enum S7Type
    {
        Float,
        Double,
        Int32,
        UInt32,
        Int16,
        UInt16,
        Int8,
        UInt8,
        String,
        Bool,
    }
    public enum BytesSequence
    {
        LowByteForward,
        LowWordForward,
        LowByteAndWordForward,
        Normal
    }
    public class S7Data
    {
        public int DBNumber { get; set; }
        public int Offset { get; set; }
        public S7Type S7Type { get; set; }
        public BytesSequence BytesSequence { get; set; }
        public int? BitPosition { get; set; }
    }
}