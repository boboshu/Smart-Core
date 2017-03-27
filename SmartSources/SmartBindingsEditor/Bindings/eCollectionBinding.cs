using System.Collections.Generic;
using Smart.Custom;
using Smart.Editors;
using UnityEditor;
using UnityEngine;

namespace Smart.Bindings
{
    [CustomEditor(typeof(CollectionBinding))]
    public class eCollectionBinding : eEditor<CollectionBinding>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawProperty(eGUI.crimsonLt, serializedObject.FindProperty(nameof(_target.updatesPerSecond)));
            eBinding.Draw(serializedObject, _target, eBinding.BuildFieldsAndProperties<IEnumerable<Object>>);
            eCustomEditors.DrawObjectProperty(serializedObject.FindProperty(nameof(_target.itemPrefab)), null, true, true);
        }
    }
}
