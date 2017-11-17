using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Smart.Extensions
{
    public static class GameObjectExtensions
    {

        //--------------------------------------------------------------------------------------------------------------------------

        public static T GetComponentInParent<T>(this GameObject go, bool includeInactive) where T : Component
        {
            if (!includeInactive) return go.GetComponentInParent<T>();

            var tr = go.transform;
            while (tr != null)
            {
                var cmp = tr.GetComponent<T>();
                if (cmp) return cmp;
                tr = tr.parent;
            }

            return null;
        }

        public static T GetComponentInParent<T>(this Component goCmp, bool includeInactive) where T : Component
        {
            if (!includeInactive) return goCmp.GetComponentInParent<T>();

            var tr = goCmp.transform;
            while (tr != null)
            {
                var cmp = tr.GetComponent<T>();
                if (cmp) return cmp;
                tr = tr.parent;
            }

            return null;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static GameObject GetChild(this GameObject go, string name)
        {
            for (var i = go.transform.childCount - 1; i >= 0; i--)
            {
                var child = go.transform.GetChild(i);
                if (child.name == name)
                    return child.gameObject;
            }
            return null;
        }

        public static GameObject AddChild(this GameObject go, string name)
        {
            var chld = new GameObject(name);
            chld.Reparent(go);
            return chld;
        }

        public static GameObject GetOrAddChild(this GameObject go, string name)
        {
            return GetChild(go, name) ?? AddChild(go, name);
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static void DestroyChildren(this GameObject go, Func<Transform, bool> condition, bool immediate = true)
        {
            for (var i = go.transform.childCount - 1; i >= 0; i--)
            {
                var child = go.transform.GetChild(i);
                if (condition(child))
                {
                    if (immediate) Object.DestroyImmediate(child.gameObject);
                    else Object.Destroy(child.gameObject);
                }
            }
        }

        public static void DestroyChildren(this Transform cmp, Func<Transform, bool> condition, bool immediate = true)
        {
            for (var i = cmp.childCount - 1; i >= 0; i--)
            {
                var child = cmp.GetChild(i);
                if (condition(child))
                {
                    if (immediate) Object.DestroyImmediate(child.gameObject);
                    else Object.Destroy(child.gameObject);
                }
            }
        }

        public static void DestroyChildren(this Component cmp, Func<Transform, bool> condition, bool immediate = true)
        {
            for (var i = cmp.transform.childCount - 1; i >= 0; i--)
            {
                var child = cmp.transform.GetChild(i);
                if (condition(child))
                {
                    if (immediate) Object.DestroyImmediate(child.gameObject);
                    else Object.Destroy(child.gameObject);
                }
            }
        }

        //------------------------------------------------------------------------------------------------------------------

        public static void DestroyChildren(this GameObject go, bool immediate = true)
        {
            for (var i = go.transform.childCount - 1; i >= 0; i--)
            {
                var child = go.transform.GetChild(i);
                if (immediate) Object.DestroyImmediate(child.gameObject);
                else Object.Destroy(child.gameObject);
            }
        }

        public static void DestroyChildren(this Transform cmp, bool immediate = true)
        {
            for (var i = cmp.childCount - 1; i >= 0; i--)
            {
                var child = cmp.GetChild(i);
                if (immediate) Object.DestroyImmediate(child.gameObject);
                else Object.Destroy(child.gameObject);
            }
        }

        public static void DestroyChildren(this Component cmp, bool immediate = true)
        {
            for (var i = cmp.transform.childCount - 1; i >= 0; i--)
            {
                var child = cmp.transform.GetChild(i);
                if (immediate) Object.DestroyImmediate(child.gameObject);
                else Object.Destroy(child.gameObject);
            }
        }

        //------------------------------------------------------------------------------------------------------------------

        public static IEnumerable<Transform> GetParents(this Component cmp)
        {
            var tr = cmp.transform.parent;
            while (tr != null)
            {
                yield return tr;
                tr = tr.parent;
            }                
        }

        public static IEnumerable<Transform> GetParents(this GameObject go)
        {
            var tr = go.transform.parent;
            while (tr != null)
            {
                yield return tr;
                tr = tr.parent;
            }                
        }

        //------------------------------------------------------------------------------------------------------------------

        public static IEnumerable<Transform> GetChildren(this Component cmp)
        {
            var tr = cmp.transform;
            for (var i = 0; i < tr.childCount; i++)
                yield return tr.GetChild(i);
        }

        public static IEnumerable<Transform> GetChildren(this GameObject go)
        {
            var tr = go.transform;
            for (var i = 0; i < tr.childCount; i++)
                yield return tr.GetChild(i);
        }

        //------------------------------------------------------------------------------------------------------------------

        public static IEnumerable<GameObject> GetAllChildren(this GameObject go)
        {
            var tr = go.transform;
            for (var i = 0; i < tr.childCount; i++)
            {
                var c = tr.GetChild(i);
                var cgo = c.gameObject;

                yield return cgo;
                foreach (var cc in GetAllChildren(cgo))
                    yield return cc;
            }
        }

        public static IEnumerable<GameObject> GetAllChildrenDescending(this GameObject go)
        {
            var tr = go.transform;
            for (var i = 0; i < tr.childCount; i++)
            {
                var c = tr.GetChild(i);
                var cgo = c.gameObject;
               
                foreach (var cc in GetAllChildrenDescending(cgo))
                    yield return cc;
                yield return cgo;
            }
        }

        public static IEnumerable<GameObject> GetAllChildrenAndThis(this GameObject go)
        {
            yield return go;
            foreach (var cc in GetAllChildren(go))
                yield return cc;
        }

        public static IEnumerable<GameObject> GetAllChildrenAndThisDescending(this GameObject go)
        {
            foreach (var cc in GetAllChildrenDescending(go))
                yield return cc;
            yield return go;
        }

        public static IEnumerable<T> GetFirstDepthChildrenComponents<T>(this GameObject go)
        {
            foreach (var component in go.GetComponents<T>())
            {
                yield return component;
            }

            var tr = go.transform;
            for (var i = 0; i < tr.childCount; i++)
            {
                var c = tr.GetChild(i);
                foreach (var component in c.GetComponents<T>())
                {
                    yield return component;
                }
                
            }
        }

        //------------------------------------------------------------------------------------------------------------------

        public static void EnumAllChildren(this GameObject go, Action<GameObject> onChild)
        {
            var tr = go.transform;
            for (var i = 0; i < tr.childCount; i++)
            {
                var c = tr.GetChild(i);
                onChild(c.gameObject);
                EnumAllChildren(c.gameObject, onChild);
            }
        }

        public static void EnumAllChildrenAndThis(this GameObject go, Action<GameObject> onChild)
        {
            onChild(go);
            EnumAllChildren(go, onChild);
        }

        //------------------------------------------------------------------------------------------------------------------

        public static int GetTotalChildCount(this GameObject go)
        {
            var tr = go.transform;
            var cnt = tr.childCount;
            for (var i = 0; i < tr.childCount; i++)
                cnt += GetTotalChildCount(tr.GetChild(i));
            return cnt;
        }

        public static int GetTotalChildCount(this Transform tr)
        {
            var cnt = tr.childCount;
            for (var i = 0; i < tr.childCount; i++)
                cnt += GetTotalChildCount(tr.GetChild(i));
            return cnt;
        }

        //------------------------------------------------------------------------------------------------------------------

        public static string GetFullName(this Component cmp)
        {
            var path = '/' + cmp.name;
            var root = cmp.transform.parent;
            while (root)
            {
                path = '/' + root.name + path;
                root = root.parent;
            }
            return path;
        }

        public static string GetFullName(this GameObject go)
        {
            var path = '/' + go.name;
            var root = go.transform.parent;
            while (root)
            {
                path = '/' + root.name + path;
                root = root.parent;
            }
            return path;
        }

        //------------------------------------------------------------------------------------------------------------------

        public static void ApplyLayerRecursively(this Transform root, int layer)
        {
            root.gameObject.layer = layer;
            foreach (Transform child in root)
                ApplyLayerRecursively(child, layer);
        }

        public static void ApplyLayerRecursively(this GameObject root, int layer)
        {
            root.gameObject.layer = layer;
            foreach (Transform child in root.transform)
                ApplyLayerRecursively(child, layer);
        }

        //------------------------------------------------------------------------------------------------------------------

        public static void ApplyTagRecursively(this Transform root, string tag)
        {
            root.gameObject.tag = tag;
            foreach (Transform child in root)
                ApplyTagRecursively(child, tag);
        }

        public static void ApplyTagRecursively(this GameObject root, string tag)
        {
            root.gameObject.tag = tag;
            foreach (Transform child in root.transform)
                ApplyTagRecursively(child, tag);
        }

        //------------------------------------------------------------------------------------------------------------------

        public static Vector4 CalcBoundingSphere(this Component cmp)
        {
            return CalcBoundingSphere(cmp.gameObject);
        }

        public static Vector4 CalcBoundingSphere(this GameObject go)
        {
            var bb = CalcBounds(go.transform, false, "Gizmo");
            return bb.HasValue
                ? VectorExtensions.V4(bb.Value.center, bb.Value.extents.magnitude)
                : VectorExtensions.V4(go.transform.position, 0.1f);
        }

        //------------------------------------------------------------------------------------------------------------------

        public static Bounds? CalcBounds(this GameObject go, bool byColliders, string ignoreTag = null)
        {
            return CalcBounds(go.transform, byColliders, ignoreTag);
        }

        public static Bounds? CalcBounds(this Component cmp, bool byColliders, string ignoreTag = null)
        {
            return CalcBounds(cmp.transform, byColliders, ignoreTag);
        }

        public static Bounds? CalcBounds(this Transform tr, bool byColliders, string ignoreTag = null)
        {
            if (!string.IsNullOrEmpty(ignoreTag) && tr.CompareTag(ignoreTag)) return null;

            var bnds = new Bounds();
            var empty = true;

            if (byColliders)
            {
                foreach (var c in tr.GetComponents<Collider>().Where(c => c.enabled))
                {
                    if (empty)
                    {
                        bnds = c.bounds;
                        empty = false;
                    }
                    else bnds.Encapsulate(c.bounds);
                }
            }
            else
            {
                var r = tr.GetComponent<Renderer>();
                if (r != null && r.enabled)
                {
                    bnds = r.bounds;
                    empty = false;
                }
            }

            for (var i = 0; i < tr.childCount; i++)
            {
                var child = tr.GetChild(i);
                if (!child.gameObject.activeSelf) continue;

                var cb = CalcBounds(child, byColliders, ignoreTag);
                if (cb == null) continue;

                if (empty)
                {
                    bnds = cb.Value;
                    empty = false;
                }
                else bnds.Encapsulate(cb.Value);
            }

            return empty ? null : (Bounds?)bnds;
        }

        //------------------------------------------------------------------------------------------------------------------
    }
}
