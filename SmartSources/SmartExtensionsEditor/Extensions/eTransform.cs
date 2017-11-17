using System.Linq;
using Smart.Editors;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Smart.Extensions
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Transform))]
    public class eTransform : Editor
    {
        //----------------------------------------------------------------------------------------------------------------------------------------

        private static eTransform transform;

        private SerializedProperty mPos;
        private SerializedProperty mRot;
        private SerializedProperty mScl;

        void OnEnable()
        {
            transform = this;
            mPos = serializedObject.FindProperty("m_LocalPosition");
            mRot = serializedObject.FindProperty("m_LocalRotation");
            mScl = serializedObject.FindProperty("m_LocalScale");
        }

        void OnDestroy()
        {
            transform = null;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------

        public override void OnInspectorGUI()
        {
            if (eSettings.TransformAutoCollapse > 0 && EditorApplication.timeSinceStartup > _forceExpandTime)
            {
                var tr = serializedObject.targetObject as Transform;
                var tr_p = tr.localPosition;
                var tr_r = tr.localRotation;
                var tr_s = tr.localScale;
                if (tr_p.x == 0 && tr_p.y == 0 && tr_p.z == 0 && tr_r.x == 0 && tr_r.y == 0 && tr_r.z == 0 && tr_s.x == 1 && tr_s.y == 1 && tr_s.z == 1)
                {
                    OnInspectorGUI_Collapsed();
                    return;
                }
            }

            eGUI.RememberColors();

            serializedObject.Update();
            EditorGUIUtility.labelWidth = 15f;

            DrawPosition();
            DrawRotation();
            DrawScale();
            if (eSettings.TransformToolsButtons) DrawTools();
            if (eSettings.TransformExtendedTools) DrawExtendedTools();

            serializedObject.ApplyModifiedProperties();
            eGUI.ResetColors();
        }

        //----------------------------------------------------------------------------------------------------------------------------------------

        private void DrawPosition()
        {
            GUILayout.BeginHorizontal();
            {
                var pos = (serializedObject.targetObject as Transform).position;

                eGUI.SetColor(pos == Vector3.zero ? Color.white : Color.gray);
                GUI.skin.button.padding = new RectOffset(0, 0, 0, 0);
                var tex = eIcons.Get(pos == Vector3.zero ? "d_MoveTool" : "d_MoveTool On");
                var reset = GUILayout.Button(new GUIContent(tex, "Reset Position"), GUILayout.Width(32f), GUILayout.Height(18f));
                GUI.skin.button.padding = new RectOffset(6, 6, 2, 3);

                eGUI.SetColor(eGUI.redLt);
                EditorGUILayout.PropertyField(mPos.FindPropertyRelative("x"));

                eGUI.SetColor(eGUI.greenLt);
                EditorGUILayout.PropertyField(mPos.FindPropertyRelative("y"));

                eGUI.SetColor(eGUI.blueLt);
                EditorGUILayout.PropertyField(mPos.FindPropertyRelative("z"));

                eGUI.ResetColors();
                if (reset)
                {
                    Undo.RecordObject(transform, "Reset Psition " + transform.name);
                    mPos.vector3Value = Vector3.zero;
                }
            }
            GUILayout.EndHorizontal();
        }

        private void DrawScale()
        {
            GUILayout.BeginHorizontal();
            {
                var scl = (serializedObject.targetObject as Transform).localScale;

                eGUI.SetColor(scl == Vector3.one ? Color.white : Color.gray);
                GUI.skin.button.padding = new RectOffset(0, 0, 0, 0);
                var tex = eIcons.Get(scl == Vector3.one ? "d_ScaleTool" : "d_ScaleTool On");
                var reset = GUILayout.Button(new GUIContent(tex, "Reset Scale"), GUILayout.Width(32f), GUILayout.Height(18f));
                GUI.skin.button.padding = new RectOffset(6, 6, 2, 3);

                eGUI.SetColor(eGUI.redLt);
                EditorGUILayout.PropertyField(mScl.FindPropertyRelative("x"));

                eGUI.SetColor(eGUI.greenLt);
                EditorGUILayout.PropertyField(mScl.FindPropertyRelative("y"));

                eGUI.SetColor(eGUI.blueLt);
                EditorGUILayout.PropertyField(mScl.FindPropertyRelative("z"));

                eGUI.ResetColors();
                if (reset)
                {
                    Undo.RecordObject(transform, "Reset Scale " + transform.name);
                    mScl.vector3Value = Vector3.one;
                }
            }
            GUILayout.EndHorizontal();
        }

        private void DrawRotation()
        {
            GUILayout.BeginHorizontal();
            {
                var rot = (serializedObject.targetObject as Transform).rotation.eulerAngles;

                eGUI.SetColor(rot == Vector3.zero ? Color.white : Color.gray);
                GUI.skin.button.padding = new RectOffset(0, 0, 0, 0);
                var tex = eIcons.Get(rot == Vector3.zero ? "d_RotateTool" : "d_RotateTool On");
                var reset = GUILayout.Button(new GUIContent(tex, "Reset Rotation"), GUILayout.Width(32f), GUILayout.Height(18f));
                GUI.skin.button.padding = new RectOffset(6, 6, 2, 3);

                var visible = (serializedObject.targetObject as Transform).localEulerAngles;
                var changed = CheckDifference(mRot);
                var altered = Axes.None;

                var opt = GUILayout.MinWidth(30f);

                eGUI.SetColor(eGUI.redDk);
                if (FloatField("X", ref visible.x, (changed & Axes.X) != 0, opt)) altered |= Axes.X;

                eGUI.SetColor(eGUI.greenDk);
                if (FloatField("Y", ref visible.y, (changed & Axes.Y) != 0, opt)) altered |= Axes.Y;

                eGUI.SetColor(eGUI.blueDk);
                if (FloatField("Z", ref visible.z, (changed & Axes.Z) != 0, opt)) altered |= Axes.Z;

                eGUI.ResetColors();

                if (reset)
                {
                    Undo.RecordObject(transform, "Reset Rotation " + transform.name);
                    mRot.quaternionValue = Quaternion.identity;
                }
                else if (altered != Axes.None)
                {
                    foreach (var obj in serializedObject.targetObjects)
                    {
                        var t = obj as Transform;
                        var v = t.localEulerAngles;

                        if ((altered & Axes.X) != 0) v.x = visible.x;
                        if ((altered & Axes.Y) != 0) v.y = visible.y;
                        if ((altered & Axes.Z) != 0) v.z = visible.z;

                        t.localEulerAngles = v;
                    }
                }
            }
            GUILayout.EndHorizontal();
        }

        //----------------------------------------------------------------------------------------------------------------------------------------

        private enum Axes { None = 0, X = 1, Y = 2, Z = 4, All = 7 }

        private static Axes CheckDifference(Transform t, Vector3 original)
        {
            var next = t.localEulerAngles;

            var axes = Axes.None;

            if (Differs(next.x, original.x)) axes |= Axes.X;
            if (Differs(next.y, original.y)) axes |= Axes.Y;
            if (Differs(next.z, original.z)) axes |= Axes.Z;

            return axes;
        }

        private Axes CheckDifference(SerializedProperty property)
        {
            var axes = Axes.None;

            if (property.hasMultipleDifferentValues)
            {
                var original = property.quaternionValue.eulerAngles;

                foreach (var obj in serializedObject.targetObjects)
                {
                    axes |= CheckDifference(obj as Transform, original);
                    if (axes == Axes.All) break;
                }
            }
            return axes;
        }

        private static bool FloatField(string name, ref float value, bool hidden, GUILayoutOption opt)
        {
            var newValue = value;
            GUI.changed = false;

            if (!hidden) newValue = EditorGUILayout.FloatField(name, newValue, opt);
            else float.TryParse(EditorGUILayout.TextField(name, "--", opt), out newValue);

            if (GUI.changed && Differs(newValue, value))
            {
                value = newValue;
                return true;
            }
            return false;
        }

        private static bool Differs(float a, float b)
        {
            return Mathf.Abs(a - b) > 0.0001f;
        }

        private void DrawTools()
        {
            var t = (Transform) target;
            GUILayout.BeginHorizontal();

            eGUI.SetColor(eGUI.grayLt);
            GUI.skin.button.padding = new RectOffset(0, 0, 0, 0);
            if (GUILayout.Button(new GUIContent(eIcons.Get("icons/processed/boxcollider icon.asset"), "Creates Bounding Box collider"), GUILayout.Width(32), GUILayout.Height(16)))
            {
                var b = t.CalcBounds(_byColliders);

                if (b != null)
                {
                    if (t.rotation != Quaternion.identity || t.localToWorldMatrix.m00 != 1 || t.localToWorldMatrix.m11 != 1 || t.localToWorldMatrix.m22 != 1) // create collider on nested object to do not confuse user
                    {
                        var go = new GameObject("Collider");
                        go.transform.localPosition = b.Value.center;
                        go.transform.localScale = b.Value.size;
                        go.Reparent(t.gameObject, ReparentingMode.KeepWorld);
                        var bc = go.AddComponent<BoxCollider>();
                        bc.center = Vector3.zero;
                        bc.size = Vector3.one;
                        Undo.RegisterCreatedObjectUndo(go, "Collider");
                    }
                    else
                    {
                        var bc = t.gameObject.AddComponent<BoxCollider>();
                        bc.size = b.Value.size;
                        bc.center = b.Value.center - t.position;
                        Undo.RegisterCreatedObjectUndo(bc, "Collider");
                    }
                }
            }
            GUI.skin.button.padding = new RectOffset(6, 6, 2, 3);

            eGUI.SetColor(Color.gray);
            _byColliders = GUILayout.Toggle(_byColliders, new GUIContent("C", "Use Colliders to detect BB\nOtherwise visible geometry will be used"), GUILayout.ExpandWidth(false));

            eGUI.SetColor(eGUI.redLt);
            _alignAffectX = GUILayout.Toggle(_alignAffectX, new GUIContent("X", "Do not change X value"), GUILayout.ExpandWidth(false));

            eGUI.SetColor(eGUI.greenLt);
            _alignAffectY = GUILayout.Toggle(_alignAffectY, new GUIContent("Y", "Do not change Y value"), GUILayout.ExpandWidth(false));

            eGUI.SetColor(eGUI.blueLt);
            _alignAffectZ = GUILayout.Toggle(_alignAffectZ, new GUIContent("Z", "Do not change Z value"), GUILayout.ExpandWidth(false));

            eGUI.SetColor(eGUI.grayLt);
            GUI.skin.button.padding = new RectOffset(0, 0, 0, 0);
            if (GUILayout.Button(new GUIContent("Align", eIcons.Get("profiler.physics"), "Smart ALIGN two selected objects by their Bounding Boxes"), GUILayout.Height(16)))
            {
                var objs = Selection.objects.OfType<GameObject>().ToList();
                if (objs.Count >= 2)
                {
                    var objDst = Selection.activeGameObject;
                    objs.Remove(objDst);
                    while (objs.Count > 0)
                    {
                        var objTrg = objs.OrderBy(x => Vector3.Distance(x.transform.position, objDst.transform.position)).First(); // Find closest object
                        DoAlignment(objTrg, objDst);
                        objDst = objTrg;
                        objs.Remove(objDst);
                    }
                }
            }
            GUI.skin.button.padding = new RectOffset(6, 6, 2, 3);

            eGUI.ResetColors();
            GUILayout.EndHorizontal();
        }

        private static void DoAlignment(GameObject target, GameObject destination)
        {
            if (destination == null || target == null) return;
            Undo.RecordObject(target.transform, "Move " + target.name);

            var b0 = destination.transform.CalcBounds(_byColliders);
            var b1 = target.transform.CalcBounds(_byColliders);

            if (b0 == null) b0 = new Bounds(destination.transform.position, Vector3.zero);
            if (b1 == null) b1 = new Bounds(target.transform.position, Vector3.zero);

            // cache
            var p1 = target.transform.position;
            var c1 = b1.Value.center;
            var c0 = b0.Value.center;

            // precalc
            var s = (b1.Value.size + b0.Value.size) * 0.5f;
            var hs = s * 0.75f;
            var d = c1 - c0;
            var pd = p1 - c1; // diff between object pos and colliders center

            // snap
            hs.x = Mathf.Max(hs.x, 0.45f);
            hs.y = Mathf.Max(hs.y, 0.45f);
            hs.z = Mathf.Max(hs.z, 0.45f);

            // align
            var nx = (Mathf.Abs(d.x) <= hs.x ? c0.x : (d.x > 0 ? c0.x + s.x : c0.x - s.x)) + pd.x;
            var ny = (Mathf.Abs(d.y) <= hs.y ? c0.y : (d.y > 0 ? c0.y + s.y : c0.y - s.y)) + pd.y;
            var nz = (Mathf.Abs(d.z) <= hs.z ? c0.z : (d.z > 0 ? c0.z + s.z : c0.z - s.z)) + pd.z;

            // apply
            target.transform.position = new Vector3(_alignAffectX ? nx : p1.x, _alignAffectY ? ny : p1.y, _alignAffectZ ? nz : p1.z);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------

        private static bool _byColliders;
        private static bool _alignAffectX = true;
        private static bool _alignAffectY = true;
        private static bool _alignAffectZ = true;

        private static double _forceExpandTime;
        private static GUIStyle _collapsedStyle;

        public void OnInspectorGUI_Collapsed()
        {
            GUI.backgroundColor = eGUI.grayLt;
            if (_collapsedStyle == null)
                _collapsedStyle = new GUIStyle(GUI.skin.textField)
                {
                    normal = { textColor = Color.gray },
                    padding = new RectOffset(1, 1, 1, 1),
                    overflow = new RectOffset(),
                    margin = new RectOffset(1, 1, 1, 1),
                    alignment = TextAnchor.MiddleCenter,
                    stretchWidth = true,
                    fontStyle = FontStyle.Bold
                };

            if (GUILayout.Button("Collapsed", _collapsedStyle))
                _forceExpandTime = EditorApplication.timeSinceStartup + eSettings.TransformAutoCollapse; // seconds of collapsing not allowed
        }

        //----------------------------------------------------------------------------------------------------------------------------------------

        private void DrawExtendedTools()
        {
            var tr = serializedObject.targetObject as Transform;
            if (tr == null) return;

            if (eGUI.Button("Apply Transform to Colliders", eGUI.cyanLt))
            {
                ApplyTransformToColliders(tr);
            }

            if (eGUI.Button("Clean up empty GameObjects", eGUI.redLt))
            {
                Transform empty_tr;
                while ((empty_tr = GetEmptyChild(tr.gameObject)) != null)
                    DestroyImmediate(empty_tr.gameObject);
            }
        }

        private static void ApplyTransformToColliders(Transform tr)
        {
            foreach (var go in tr.gameObject.GetAllChildrenAndThisDescending())
            {
                if (!go.activeSelf) continue;
                if (go.transform.localRotation != Quaternion.identity) continue;

                var pos = go.transform.localPosition;
                go.transform.localPosition = Vector3.zero;

                var size = go.transform.localScale;
                go.transform.localScale = Vector3.one;

                foreach (var bc in go.GetFirstDepthChildrenComponents<BoxCollider>())
                {
                    if (bc.transform != go.transform && bc.transform.rotation == go.transform.rotation)
                    {
                        bc.center += Vector3.Scale(bc.transform.localPosition, bc.transform.localScale);
                        bc.size = Vector3.Scale(bc.size, bc.transform.localScale);
                    }

                    bc.size = Vector3.Scale(bc.size, size);
                    bc.center = Vector3.Scale(bc.center, size);
                    bc.center += pos;

                    if (bc.transform != go.transform && bc.transform.rotation == go.transform.rotation)
                    {
                        ComponentUtility.CopyComponent(bc);
                        ComponentUtility.PasteComponentAsNew(go);
                        DestroyImmediate(bc);
                    }
                }

                foreach (var cc in go.GetFirstDepthChildrenComponents<CapsuleCollider>())
                {
                    if (cc.transform != go.transform && cc.transform.rotation == go.transform.rotation)
                    {
                        cc.center += Vector3.Scale(cc.transform.localPosition, cc.transform.localScale);
                        var lcs = cc.transform.localScale;
                        cc.height *= lcs[cc.direction];
                        cc.radius *=
                            cc.direction == 0
                                ? Mathf.Max(lcs.y, lcs.z)
                                : cc.direction == 1
                                    ? Mathf.Max(lcs.x, lcs.z)
                                    : Mathf.Max(lcs.x, lcs.y);
                    }

                    cc.center = Vector3.Scale(cc.center, size);
                    cc.center += pos;
                    cc.height *= size[cc.direction];
                    cc.radius *=
                        cc.direction == 0
                            ? Mathf.Max(size.y, size.z)
                            : cc.direction == 1
                                ? Mathf.Max(size.x, size.z)
                                : Mathf.Max(size.x, size.y);


                    if (cc.transform != go.transform && cc.transform.rotation == go.transform.rotation)
                    {
                        ComponentUtility.CopyComponent(cc);
                        ComponentUtility.PasteComponentAsNew(go);
                        DestroyImmediate(cc);
                    }
                }

                foreach (var sc in go.GetFirstDepthChildrenComponents<SphereCollider>())
                {
                    if (sc.transform != go.transform && sc.transform.rotation == go.transform.rotation)
                    {
                        sc.center += Vector3.Scale(sc.transform.localPosition, sc.transform.localScale);
                        var lcs = sc.transform.localScale;
                        sc.radius *= Mathf.Max(lcs.x, lcs.y, lcs.z);
                    }

                    sc.center = Vector3.Scale(sc.center, size);
                    sc.center += pos;
                    sc.radius *= Mathf.Max(size.x, size.y, size.z);

                    if (sc.transform != go.transform && sc.transform.rotation == go.transform.rotation)
                    {
                        ComponentUtility.CopyComponent(sc);
                        ComponentUtility.PasteComponentAsNew(go);
                        DestroyImmediate(sc);
                    }
                }
            }
        }

        private static Transform GetEmptyChild(GameObject go)
        {
            return go.GetComponentsInChildren<Transform>(true).FirstOrDefault(tr => tr.GetTotalChildCount() + 1 == tr.GetComponentsInChildren<Component>(true).Length); // contains only transform components
        }

        //----------------------------------------------------------------------------------------------------------------------------------------
    }
}