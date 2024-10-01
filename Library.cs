using KarlLib.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KarlLib
{
    public class Library
    {
        public static Section AddSection(string name, Action onEnter)
        {
            Section section = new Section(name, onEnter);
            Load.sections.Add(section);
            return section;
        }
    }
}
