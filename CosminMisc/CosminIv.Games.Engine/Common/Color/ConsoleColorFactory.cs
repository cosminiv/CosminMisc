using System;

namespace CosminIv.Games.Engine.Common.Color
{
    class ConsoleColorFactory
    {
        readonly IColor[] _colors = MakePrototypeColors();
        readonly Random _random = new Random();

        public IColor MakeRandomColor() {
            int colorIndex = _random.Next(_colors.Length);
            IColor color = _colors[colorIndex];
            return color;
        }

        private static IColor[] MakePrototypeColors() {
            return new[] {
                new ConsoleColor2(ConsoleColor.Blue),
                new ConsoleColor2(ConsoleColor.Cyan),
                new ConsoleColor2(ConsoleColor.Gray),
                new ConsoleColor2(ConsoleColor.Green),
                new ConsoleColor2(ConsoleColor.Magenta),
                new ConsoleColor2(ConsoleColor.Red),
                new ConsoleColor2(ConsoleColor.White),
                new ConsoleColor2(ConsoleColor.Yellow),
            };
        }
    }
}
