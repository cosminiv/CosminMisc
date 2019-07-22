namespace Games.Common
{
    public class Color
    {
        public Color(byte red, byte green, byte blue) {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
    }
}