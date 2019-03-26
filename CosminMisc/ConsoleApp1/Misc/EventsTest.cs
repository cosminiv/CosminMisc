using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Misc
{
    public class EventsTest
    {
        public delegate void SetZeroHandler (object sender, SetZeroEventArgs args);
        public event SetZeroHandler OnSetZero;

        public int Number 
        {
            set {
                if (value == 0 && OnSetZero != null)
                    OnSetZero(this, new SetZeroEventArgs { Text = "zero set!"});
            }
        }
    }

    public class SetZeroEventArgs
    {
        public string Text {
            get; set;
        }
    }
}
