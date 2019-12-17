using System;

namespace CosminIv.Games.Common.Color
{
    class ConsoleColorFactory
    {
        IColor[] Colors = MakePrototypeColors();
        Random Random = new Random();

        public IColor MakeRandomColor() {
            int colorIndex = Random.Next(Colors.Length);
            IColor color = Colors[colorIndex];
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
