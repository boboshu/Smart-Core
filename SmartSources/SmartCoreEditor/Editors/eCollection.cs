using System;

namespace Smart.Editors
{
    [Flags]
    public enum eCollection
    {
        Delete = 1,
        Reorder = 2,
        SingleLine = 4,
        GotoRef = 8,
        DestroyRef = 16,
        VerticalTools = 32,
        Delete_SingleLine = Delete | SingleLine,
        Delete_SingleLine_Reorder = Delete | SingleLine | Reorder        
    }

    public static class eCollectionExtensions
    {
        public static bool HasDestroyRef(this eCollection options)
        {
            return (options & eCollection.DestroyRef) != 0;
        }

        public static bool HasReorder(this eCollection options)
        {
            return (options & eCollection.Reorder) != 0;
        }

        public static bool HasSingleLine(this eCollection options)
        {
            return (options & eCollection.SingleLine) != 0;
        }

        public static bool HasVerticalTools(this eCollection options)
        {
            return (options & eCollection.VerticalTools) != 0;
        }

        public static bool HasDelete(this eCollection options)
        {
            return (options & eCollection.Delete) != 0;
        }

        public static bool HasGotoRef(this eCollection options)
        {
            return (options & eCollection.GotoRef) != 0;
        }
    }

}
