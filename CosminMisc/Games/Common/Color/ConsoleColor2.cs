using System;

namespace CosminIv.Games.Common.Color
{
    public class ConsoleColor2 : IColor
    {
        public ConsoleColor2(ConsoleColor value) {
            Value = value;
        }

        public ConsoleColor Value { get; set; }

        public object Clone() {
            return new ConsoleColor2(this.Value);
        }
    }
}