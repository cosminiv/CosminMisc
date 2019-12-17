using System;

namespace CosminIv.Games.Engine.Common.Color
{
    public class ConsoleColor2 : IColor
    {
        public ConsoleColor2(ConsoleColor value) {
            Value = value;
        }

        public ConsoleColor Value { get; }

        public object Clone() {
            return new ConsoleColor2(this.Value);
        }

        public override bool Equals(object obj) {
            return (obj is ConsoleColor2 && 
                (obj as ConsoleColor2).Value == this.Value);
        }

        public override int GetHashCode() {
            return Value.GetHashCode();
        }
    }
}