using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.AppliedAlgorithms.PathFinding
{
    class GeometricPathTestCase
    {
        public List<string> Terrain;
        public Coords StartCoords;
        public Coords GoalCoords;

        public GeometricPathTestCase(string terrain, Coords startCoords, Coords goalCoords) {
            Terrain = ParseTerrain(terrain);
            StartCoords = startCoords;
            GoalCoords = goalCoords;
        }

        static List<string> ParseTerrain(string terrainStr) {
            string[] lines = terrainStr.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            return lines.Where(l => !String.IsNullOrWhiteSpace(l))
                .Select(l => l.Replace(" ", ""))
                .ToList();
        }
    }

    class Coords
    {
        public int Column;
        public int Row;

        public Coords(int row, int col) {
            Column = col;
            Row = row;
        }

        public override bool Equals(object obj) {
            Coords other = obj as Coords;
            return this.Column == other.Column &&
                this.Row == other.Row;
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = 656739706;
                hashCode = hashCode * -1521134295 + Column.GetHashCode();
                hashCode = hashCode * -1521134295 + Row.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString() {
            return $"({Row}, {Column})";
        }
    }
}
