using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Smart.Editors;
using UnityEditor;
using UnityEngine;

namespace Smart.Bindings
{
    public static class eBinding
    {
        public delegate void TypeItemsBuilder(Binding binding, Object source, ref int indx, List<string> items, List<string> names, List<Object> sources);

        public static void Draw(SerializedObject serializedObject, Binding binding, TypeItemsBuilder typeItemsBuilder)
        {
            eGUI.BeginColors();
            eGUI.SetColor(eGUI.azureLt);
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(binding.useBindingRoot)));

            if (!binding.useBindingRoot)
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(binding.sourceObject)), new GUIContent("Source"));

            if (binding.sourceObject == null)
            {
                if (binding.useBindingRoot) eGUI.TextEditor(ref binding.sourceMember, "Member");
                else DrawSourceSelectorOfGameObject(binding, binding.gameObject, typeItemsBuilder); // use this
            }
            else
            {
                if (binding.useBindingRoot) DrawSourceSelectorAsIs(binding, binding.sourceObject, typeItemsBuilder);
                else // use flexible selection to allow to select any component in given GameObject
                {
                    var cmp = binding.sourceObject as Component;
                    if (cmp) DrawSourceSelectorOfGameObject(binding, cmp.gameObject, typeItemsBuilder);
                    else
                    {
                        var go = binding.sourceObject as GameObject;
                        if (go) DrawSourceSelectorOfGameObject(binding, go, typeItemsBuilder);
                        else DrawSourceSelectorAsIs(binding, binding.sourceObject, typeItemsBuilder);
                    }
                }
            }
            eGUI.EndColors();
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private static void DrawSourceSelectorAsIs(Binding binding, Object sourceObject, TypeItemsBuilder typeItemsBuilder)
        {
            var items = new List<string>();
            var names = new List<string>();
            var sources = new List<Object>();
            var indx = -1;

            typeItemsBuilder(binding, sourceObject, ref indx, items, names, sources);

            var newIndx = EditorGUILayout.Popup("Member", indx, items.ToArray());
            if (newIndx == indx) return;
            binding.sourceMember = names[newIndx];
            binding.sourceObject = sources[newIndx];
        }

        private static void DrawSourceSelectorOfGameObject(Binding binding, GameObject go, TypeItemsBuilder typeItemsBuilder)
        {
            var items = new List<string>();
            var names = new List<string>();
            var sources = new List<Object>();
            var indx = -1;

            typeItemsBuilder(binding, go, ref indx, items, names, sources);
            foreach (var cmp in go.GetComponents<Component>())
                typeItemsBuilder(binding, cmp, ref indx, items, names, sources);

            var newIndx = EditorGUILayout.Popup("Member", indx, items.ToArray());
            if (newIndx == indx) return;
            binding.sourceMember = names[newIndx];
            binding.sourceObject = sources[newIndx];
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public static void BuildFieldsAndProperties<T>(Binding binding, Object source, ref int indx, List<string> items, List<string> names, List<Object> sources)
        {
            var st = source.GetType();
            var vt = typeof(T);

            foreach (var f in st.GetFields(BindingFlags.Instance | BindingFlags.Public).Where(f => vt.IsAssignableFrom(f.FieldType)))
            {
                if (indx < 0 && f.Name == binding.sourceMember && source == binding.sourceObject) indx = items.Count;
                items.Add(st.Name + "." + f.Name);
                names.Add(f.Name);
                sources.Add(source);
            }

            foreach (var p in st.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => vt.IsAssignableFrom(p.PropertyType) && !p.GetIndexParameters().Any()))
            {
                if (indx < 0 && p.Name == binding.sourceMember && source == binding.sourceObject) indx = items.Count;
                items.Add(st.Name + "." + p.Name);
                names.Add(p.Name);
                sources.Add(source);
            }
        }

        public static void BuildMethodsVoid(Binding binding, Object source, ref int indx, List<string> items, List<string> names, List<Object> sources)
        {
            var st = source.GetType();

            foreach (var m in st.GetMethods(BindingFlags.Instance | BindingFlags.Public).Where(x => x.ReturnType == typeof(void)))
            {
                var pp = m.GetParameters();
                if (pp.Count() != 0) continue;
                if (indx < 0 && m.Name == binding.sourceMember && source == binding.sourceObject) indx = items.Count;
                items.Add(st.Name + "." + m.Name);
                names.Add(m.Name);
                sources.Add(source);
            }
        }

        public static void BuildMethods<T>(Binding binding, Object source, ref int indx, List<string> items, List<string> names, List<Object> sources)
        {
            var st = source.GetType();
            var pt = typeof(T);

            foreach (var m in st.GetMethods(BindingFlags.Instance | BindingFlags.Public).Where(x => x.ReturnType == typeof(void)))
            {
                var pp = m.GetParameters();
                if (pp.Count() != 1 || pp.First().ParameterType != pt) continue;
                if (indx < 0 && m.Name == binding.sourceMember && source == binding.sourceObject) indx = items.Count;
                items.Add(st.Name + "." + m.Name);
                names.Add(m.Name);
                sources.Add(source);
            }
        }
    }
}
