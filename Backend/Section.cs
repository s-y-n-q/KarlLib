using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;

namespace KarlLib.Backend
{
    public class Section
    {
        public string Name;
        public Action OnEnter;

        private List<UIElement> uiElements;
        private Dictionary<string, bool> toggles = new Dictionary<string, bool>();

        public Section(string text, Action action)
        {
            Name = text;
            OnEnter = action;
            uiElements = new List<UIElement>();
        }

        public void AddLabel(string text)
        {
            uiElements.Add(new UIElement(UIType.Label, text));
        }

        public void AddButton(string name, Action action)
        {
            uiElements.Add(new UIElement(UIType.Button, name, action));
        }

        public void AddToggle(string name, Action<bool> action)
        {
            if (!toggles.ContainsKey(name))
            {
                toggles.Add(name, false);
            }

            uiElements.Add(new UIElement(UIType.Toggle, name, action));
        }

        public void RenderUI()
        {
            foreach (var element in uiElements)
            {
                switch (element.Type)
                {
                    case UIType.Label:
                        GUILayout.Label("<b>" + element.LabelText + "</b>");
                        break;

                    case UIType.Button:
                        Load.button(element.Name, Load.buttoncolro, element.ButtonAction);
                        break;

                    case UIType.Toggle:
                        var currentState = toggles[element.Name];

                        Load.button(currentState ? $"<color=green>{element.Name}</color> [Enabled]" : $"<color=red>{element.Name}</color> [Disabled]",
                            Load.buttoncolro, () =>
                            {
                                currentState = !currentState;
                                toggles[element.Name] = currentState;

                                element.ToggleAction.Invoke(currentState);
                            });
                        break;
                }
            }
        }
    }
}
