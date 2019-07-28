using System;

namespace CosminIv.Games.Common
{
    public class IColor
    {
        //public Color() {
        //}

        //public Color(byte red, byte green, byte blue) {
        //    Red = red;
        //    Green = green;
        //    Blue = blue;
        //}

        //public byte Red { get; set; }
        //public byte Green { get; set; }
        //public byte Blue { get; set; }
    }

    public class ConsoleColor2 : IColor
    {
        public ConsoleColor2(ConsoleColor value) {
            Value = value;
        }

        public ConsoleColor Value { get; set; }
    }
}