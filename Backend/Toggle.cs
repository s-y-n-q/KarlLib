using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlLib.Backend
{
    public class Toggle
    {
        public string Name;
        public Action Callback;
        public bool StartValue;

        public Toggle(string text, Action callback, bool startvalue)
        {
            Name = text;
            Callback = callback;
            StartValue = startvalue;
        }
    }
}
