using UnityEngine;

namespace Smart.Extensions
{
    public enum ReparentingMode { KeepWorld, KeepLocal, ResetLocal }

    public static class ReparentingExtensions
    {
        //------------------------------------------------------------------------------------------------------------------------------

        public static void Reparent(this GameObject go, ReparentingMode mode = ReparentingMode.ResetLocal)
        {
            var trg = go.transform;            
            switch (mode)
            {
                case ReparentingMode.KeepWorld: trg.SetParent(null, true); break;
                case ReparentingMode.KeepLocal: trg.SetParent(null, false); break;
                case ReparentingMode.ResetLocal: trg.SetParent(null);
                    trg.localPosition = Vector3.zero;
                    trg.localRotation = Quaternion.identity;
                    trg.localScale = Vector3.one;
                    break;
            }
        }

        public static void Reparent(this Component go, ReparentingMode mode = ReparentingMode.ResetLocal)
        {
            var trg = go.transform;
            switch (mode)
            {
                case ReparentingMode.KeepWorld: trg.SetParent(null, true); break;
                case ReparentingMode.KeepLocal: trg.SetParent(null, false); break;
                case ReparentingMode.ResetLocal: trg.SetParent(null);
                    trg.localPosition = Vector3.zero;
                    trg.localRotation = Quaternion.identity;
                    trg.localScale = Vector3.one;
                    break;
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------

        public static void Reparent(this GameObject go, GameObject newParent, ReparentingMode mode = ReparentingMode.ResetLocal)
        {
            var trg = go.transform;
            var prnt = newParent == null ? null : newParent.transform;
            switch (mode)
            {
                case ReparentingMode.KeepWorld: trg.SetParent(prnt, true); break;
                case ReparentingMode.KeepLocal: trg.SetParent(prnt, false); break;
                case ReparentingMode.ResetLocal: trg.SetParent(prnt);
                    trg.localPosition = Vector3.zero;
                    trg.localRotation = Quaternion.identity;
                    trg.localScale = Vector3.one;
                    break;
            }
        }

        public static void Reparent(this Component go, Component newParent, ReparentingMode mode = ReparentingMode.ResetLocal)
        {
            var trg = go.transform;
            var prnt = newParent == null ? null : newParent.transform;
            switch (mode)
            {
                case ReparentingMode.KeepWorld: trg.SetParent(prnt, true); break;
                case ReparentingMode.KeepLocal: trg.SetParent(prnt, false); break;
                case ReparentingMode.ResetLocal: trg.SetParent(prnt);
                    trg.localPosition = Vector3.zero;
                    trg.localRotation = Quaternion.identity;
                    trg.localScale = Vector3.one;
                    break;
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------

        public static void Reparent(this GameObject go, Component newParent, ReparentingMode mode = ReparentingMode.ResetLocal)
        {
            var trg = go.transform;
            var prnt = newParent == null ? null : newParent.transform;
            switch (mode)
            {
                case ReparentingMode.KeepWorld: trg.SetParent(prnt, true); break;
                case ReparentingMode.KeepLocal: trg.SetParent(prnt, false); break;
                case ReparentingMode.ResetLocal: trg.SetParent(prnt);
                    trg.localPosition = Vector3.zero;
                    trg.localRotation = Quaternion.identity;
                    trg.localScale = Vector3.one;
                    break;
            }
        }

        public static void Reparent(this Component go, GameObject newParent, ReparentingMode mode = ReparentingMode.ResetLocal)
        {
            var trg = go.transform;
            var prnt = newParent == null ? null : newParent.transform;
            switch (mode)
            {
                case ReparentingMode.KeepWorld: trg.SetParent(prnt, true); break;
                case ReparentingMode.KeepLocal: trg.SetParent(prnt, false); break;
                case ReparentingMode.ResetLocal: trg.SetParent(prnt);
                    trg.localPosition = Vector3.zero;
                    trg.localRotation = Quaternion.identity;
                    trg.localScale = Vector3.one;
                    break;
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------

        public static void ReparentChilds(this GameObject source, Component newParent, ReparentingMode mode = ReparentingMode.ResetLocal)
        {
            var tr = source.transform;
            for (var i = 0; i < tr.childCount; i++)
                tr.GetChild(i).Reparent(newParent, mode);
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}