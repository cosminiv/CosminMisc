using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu._Common
{
    /// <summary>
    /// Represents a combination of numbers, for which the order is not important.
    /// </summary>
    public class Combination
    {
        /// <summary>
        /// Intended only for reading
        /// </summary>
        public List<long> Items;

        public Combination(long item) {
            Items = new List<long> { item };
        }

        public Combination(IEnumerable<long> items) {
            Items = new List<long>(items);
        }

        public Combination(IEnumerable<long> items, long item) {
            Items = new List<long>(items);
            Items.Add(item);
            // Sort, to normalize for lists with another order.
            Items.Sort();
        }

        public override string ToString() {
            return "(" + string.Join(", ", Items) + ")";
        }

        public override int GetHashCode() {
            unchecked {
                int hash = 19;
                foreach (var item in Items) {
                    hash = hash * 31 + item.GetHashCode();
                }
                return hash;
            }
        }

        public override bool Equals(object obj) {
            return Equals(obj as Combination);
        }

        public bool Equals(Combination obj) {
            return obj != null && obj.GetHashCode() == this.GetHashCode();
        }
    }
}
