using System.Collections.Generic;
using Smart.Editors;
using UnityEditor;
using UnityEngine;

namespace Smart.Bindings
{
    [CustomEditor(typeof(BindingRoot))]
    public class eBindingRoot : eEditor<BindingRoot>
    {
        private static void GetTypeItems(BindingRoot bindingRoot, Object source, ref int indx, List<string> items, List<Object> sources)
        {
            if (indx < 0 && source == bindingRoot.sourceObject) indx = items.Count;
            items.Add(source.GetType().Name);
            sources.Add(source);
        }

        private static void DrawSourceSelectorOfGameObject(BindingRoot bindingRoot, GameObject go)
        {
            var items = new List<string>();
            var sources = new List<Object>();
            var indx = -1;

            GetTypeItems(bindingRoot, go, ref indx, items, sources);
            foreach (var cmp in go.GetComponents<Component>())
                GetTypeItems(bindingRoot, cmp, ref indx, items, sources);    

            var newIndx = EditorGUILayout.Popup("Source Object Type", indx, items.ToArray());
            if (newIndx == indx) return;
            bindingRoot.sourceObject = sources[newIndx];
        }

        protected override void DrawComponent()
        {
            eGUI.BeginColors();
            eGUI.SetColor(eGUI.azureLt);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.sourceObject)));
            if (_target.sourceObject)
            {
                var go = _target.sourceObject as GameObject;
                if (go) DrawSourceSelectorOfGameObject(_target, go);
                else
                {
                    var cmp = _target.sourceObject as Component;
                    if (cmp) DrawSourceSelectorOfGameObject(_target, cmp.gameObject);
                }
            }
            eGUI.EndColors();
        }
    }
}
