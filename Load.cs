using BepInEx;
using KarlLib.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Collections.Specialized.BitVector32;
using Section = KarlLib.Backend.Section;

namespace KarlLib
{
    [BepInPlugin("synq.karlson.karlib", "KarlLib", "1.0.0")]
    public class Load : BaseUnityPlugin
    {
        static Texture2D mainTex;
        UnityEngine.Color background;
        UnityEngine.Color divider;
        public static UnityEngine.Color buttoncolro;
        static bool uiopen;

        public static List<Section> sections = new List<Section>();
        private Vector2 scrollPosition = Vector2.zero;

        private Stack<Section> sectionStack = new Stack<Section>();

        public void GoBack()
        {
            if (sectionStack.Count > 1)
            {
                sectionStack.Pop();
            }
        }


        void Awake()
        {
            Init();
        }

        bool testbool;

        void Init()
        {
            ColorUtility.TryParseHtmlString("#141414", out background);
            ColorUtility.TryParseHtmlString("#595959", out divider);
            ColorUtility.TryParseHtmlString("#252525", out buttoncolro);
        }

        void OnGUI()
        {
            mainTex = new Texture2D(2, 2);
            for (int i = 0; i < mainTex.width; i++)
            {
                for (int j = 0; j < mainTex.height; j++)
                {
                    mainTex.SetPixel(i, j, UnityEngine.Color.white);
                }
            }

            if (uiopen)
            {
                Vector3 mid = new Vector3(UnityEngine.Screen.width / 2, UnityEngine.Screen.height / 2, 0);

                Rect therect = new Rect(mid.x - 700 / 2, mid.y - 700 / 2, 700, 700);

                Draw(new Rect(therect), background, 6);
                GUI.Label(new Rect(630, 205, 150, 150), "<size=20>KarlLib</size>");
                GUI.Label(new Rect(695, 208, 150, 150), "<color=#414141><size=16>(insert)</size></color>");
                Draw(new Rect(610, 245, 700, 1), divider, 0);
                //Draw(new Rect(620, 250, 680, 630), UnityEngine.Color.white, 0);
                GUILayout.BeginArea(new Rect(620, 255, 680, 630));
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(700), GUILayout.Height(630));
                if (sectionStack.Count > 0)
                {
                    button("Back", buttoncolro, () => sectionStack.Pop());
                    if (sectionStack.Count == 0)
                    {
                        return;
                    }
                    sectionStack.Peek().RenderUI();
                }
                else
                {
                    foreach (var section in sections)
                    {
                        button(section.Name, buttoncolro, () => nextup(section));
                    }
                }
                GUILayout.EndScrollView();
                GUILayout.EndArea();
            }
        }

        void nextup(Section section)
        {
            section.OnEnter.Invoke();
            sectionStack.Push(section);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
            {
                Console.WriteLine("pressed insrt");
                uiopen = !uiopen;
                Console.WriteLine("ui is : " + uiopen);
                if (uiopen)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                if (!uiopen && SceneManager.GetActiveScene().name != "MainMenu")
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }

        public static void button(string name, UnityEngine.Color color, Action action)
        {
            GUI.backgroundColor = new Color32(0, 0, 0, 0);
            GUI.contentColor = UnityEngine.Color.white;
            if (GUILayout.Button("", GUILayout.Width(673f), GUILayout.Height(35f)))
            {
                action();
            }
            GUI.backgroundColor = UnityEngine.Color.white;
            Rect rect = GUILayoutUtility.GetLastRect();
            Draw(rect, color, 8f);
            GUI.backgroundColor = new Color32(0, 0, 0, 0);
            GUI.contentColor = UnityEngine.Color.white;
            if (GUI.Button(rect, "<b>" + name + "</b>"))
            {
                action();
            }
        }

        static void Draw(Rect size, UnityEngine.Color color, float r)
        {
            for (int i = 0; i < 5; i++)
            {
                GUI.DrawTexture(
                    size,
                    mainTex,
                    (ScaleMode)(0 + Type.EmptyTypes.Length),
                    Type.EmptyTypes.Length != 0,
                    0f,
                    color,
                    0f,
                    r
                );
            }
        }
    }
}
