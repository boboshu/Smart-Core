using Smart.Editors;
using Smart.Utils;
using UnityEditor;
using UnityEngine;

namespace Smart.Extensions
{
    public class eSettingsDialog : EditorWindow
    {
        [MenuItem("Tools/Smart/Smart Settings", false, 4010)]
        private static void MainMenuItem()
        {
            Execute();
        }

        private void OnGUI()
        {
            eGUI.RememberColors();
            GUILayout.BeginVertical(new GUIStyle { normal = { background = eIcons.Get("icons/statemachineeditor.background.png") } });
            {
                GUILayout.BeginVertical();
                {
                    eGUI.LabelBold("Core");
                    CoreSettings.DrawGizmos = EditorGUILayout.Toggle("Draw Gizmos", CoreSettings.DrawGizmos);
                    eSettings.AABBGizmo = EditorGUILayout.Toggle("Draw AABB Gizmo", eSettings.AABBGizmo);
                    
                    eGUI.EmptySpace();
                    eGUI.LabelBold("Transform");
                    eSettings.TransformToolsButtons = EditorGUILayout.Toggle("Tools Buttons", eSettings.TransformToolsButtons);
                    eSettings.TransformExtendedTools = EditorGUILayout.Toggle("Extended Tools", eSettings.TransformExtendedTools);
                    eSettings.TransformAutoCollapse = EditorGUILayout.IntPopup("Auto Collapse", eSettings.TransformAutoCollapse, new[] {"Never", "After 15 Seconds", "After 1 Minute", "After 5 Minutes"}, new[] {-1, 15, 60, 300});

                    eGUI.EmptySpace();
                    eGUI.LabelBold("Hierarchy");
                    eSettings.HierarchyExtension = EditorGUILayout.Toggle("Enable Extension", eSettings.HierarchyExtension);
                    eSettings.HierarchyOffsetCheckboxes = EditorGUILayout.Toggle("Offset Checkboxes", eSettings.HierarchyOffsetCheckboxes);
                    eSettings.HierarchyShowPolyCount = EditorGUILayout.Toggle("Show Poligons Count", eSettings.HierarchyShowPolyCount);
                    eSettings.HierarchyShowLayer = EditorGUILayout.Toggle("Show Layer", eSettings.HierarchyShowLayer);
                    eSettings.HierarchyShowTag = EditorGUILayout.Toggle("Show Tag", eSettings.HierarchyShowTag);

                    eGUI.EmptySpace();
                    eGUI.LabelBold("Events");
                    eSettings.EventsExtension = EditorGUILayout.Toggle("Enable Extension", eSettings.EventsExtension);
                }
                GUILayout.EndVertical();
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndVertical();
            eGUI.ResetColors();
        }
     
        public static void Execute()
        {
            const int WIDTH = 275;
            const int HEIGHT = 300;

            var wnd = GetWindow<eSettingsDialog>(true, "Smart Core Settings");
            wnd.minSize = new Vector2(WIDTH, HEIGHT);
            wnd.position = new Rect(400, 300, WIDTH, HEIGHT);
            wnd.Show();
        }
   }
}