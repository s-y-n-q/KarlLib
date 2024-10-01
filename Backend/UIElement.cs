using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlLib.Backend
{
    public enum UIType
    {
        Label,
        Button,
        Toggle
    }

    public struct UIElement
    {
        public UIType Type;
        public string Name;
        public string LabelText;
        public Action ButtonAction;
        public Action<bool> ToggleAction;

        public UIElement(UIType type, string labelText)
        {
            Type = type;
            Name = null;
            LabelText = labelText;
            ButtonAction = null;
            ToggleAction = null;
        }

        public UIElement(UIType type, string name, Action buttonAction)
        {
            Type = type;
            Name = name;
            LabelText = null;
            ButtonAction = buttonAction;
            ToggleAction = null;
        }

        public UIElement(UIType type, string name, Action<bool> toggleAction)
        {
            Type = type;
            Name = name;
            LabelText = null;
            ButtonAction = null;
            ToggleAction = toggleAction;
        }
    }
}
