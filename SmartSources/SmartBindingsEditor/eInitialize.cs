using Smart.Bindings;
using Smart.Bindings.Method;
using Smart.Bindings.Values;
using Smart.Editors;
using Smart.Managers;
using Smart.Setup;
using UnityEditor;

namespace Smart
{
    [InitializeOnLoad]
    public static class eInitialize
    {
        static eInitialize()
        {
            eSetup.InitializeDLLWithType<BindingsManager>("Bindings", "3c47746434abf0b46a53d9d23353a639", s =>
            {
                s.Reg<BindingsManager>("Managers/Bindings", eHierarchyIcons.Priority.Higher);

                s.Reg<BindingRoot>("Root", eHierarchyIcons.Priority.Normal);
                s.Reg<CollectionBinding>("Collection", eHierarchyIcons.Priority.Normal);

                s.Reg<BooleanBinding>("Values/Boolean", eHierarchyIcons.Priority.Lower);
                s.Reg<ColorBinding>("Values/Color", eHierarchyIcons.Priority.Lower);
                s.Reg<FloatBinding>("Values/Float", eHierarchyIcons.Priority.Lower);
                s.Reg<IntegerBinding>("Values/Integer", eHierarchyIcons.Priority.Lower);
                
                s.Reg<AudioBinding>("Values/Audio", eHierarchyIcons.Priority.Lower);
                s.Reg<SpriteBinding>("Values/Sprite", eHierarchyIcons.Priority.Lower);
                s.Reg<TextBinding>("Values/Text", eHierarchyIcons.Priority.Lower);
                
                s.Reg<BooleanMethodBinding>("Methods/Boolean", eHierarchyIcons.Priority.Lower);                
                s.Reg<FloatMethodBinding>("Methods/Float", eHierarchyIcons.Priority.Lower);
                s.Reg<IntegerMethodBinding>("Methods/Integer", eHierarchyIcons.Priority.Lower);
                s.Reg<ObjectMethodBinding>("Methods/Object", eHierarchyIcons.Priority.Lower);
                s.Reg<TextMethodBinding>("Methods/Text", eHierarchyIcons.Priority.Lower);
                s.Reg<VoidMethodBinding>("Methods/Void", eHierarchyIcons.Priority.Lower);
            }); 
        }
    }
}
