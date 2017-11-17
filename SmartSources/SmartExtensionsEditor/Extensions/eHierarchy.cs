using Smart.Editors;
using Smart.Types;
using UnityEditor;
using UnityEngine;

namespace Smart.Extensions
{
    [InitializeOnLoad]
    public static class eHierarchy
    {
        //--------------------------------------------------------------------------------------------------------------------------

        static eHierarchy()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnDrawHierarchyItem;
        }

        private static GUIStyle _polyCountGreenLabel;
        private static GUIStyle _polyCountRedLabel;
        private static GUIStyle _polyCountYellowLabel;
        private static GUIStyle _polyCountOrangeLabel;
        
        private static GUIStyle _countLabel;
        private static GUIStyle _expandButton;
        private static GUIStyle _toggleButton;
        private static GUIStyle _layerLabel;

        //--------------------------------------------------------------------------------------------------------------------------

        private static void OnDrawHierarchyItem(int instanceID, Rect selectionRect)
        {
            if (!eSettings.HierarchyExtension) return;
            var offset = eSettings.HierarchyOffsetCheckboxes ? 16 : 0;
            
            // 1. get game object and draw check box
            var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (go == null) return;

            var drawButton = false;
            {
                var textOffset = 0;

                // Draw child count text
                if (go.transform.childCount > 0)
                {
                    var chldCount = go.transform.GetTotalChildCount();
                    var txt = chldCount > 1000 ? chldCount / 1000 + "k" : chldCount.ToString();
                    if (_countLabel == null) _countLabel = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleRight };
                    GUI.Label(new Rect(selectionRect.xMax - 16 - 32 - offset, selectionRect.y, 32f, 16f), txt, _countLabel);
                    textOffset = (int)GUI.skin.label.CalcSize(new GUIContent(txt)).x;
                    drawButton = true;
                }

                // Draw total polygons count
                if (eSettings.HierarchyShowPolyCount)
                {
                    var mi = _getTransformMeshInfoCache[go.transform];
                    if (mi.trianglesCount > 0)
                    {
                        var txt = mi.trianglesCount > 1000000 ? (mi.trianglesCount / 1000000).ToString() + 'm' : (mi.trianglesCount > 1000 ? (mi.trianglesCount / 1000).ToString() + 'k' : mi.trianglesCount.ToString());
                        if (mi.dynamicBatching) txt = txt + '*';
                                                
                        GUIStyle stl;
                        if (mi.trianglesCount > 100000)
                        {
                            stl = _polyCountRedLabel ?? (_polyCountRedLabel = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleRight, fontSize = 8, normal = { textColor = Color.red } });
                        }
                        else
                        {
                            if (mi.trianglesCount > 10000)
                            {
                                stl = _polyCountOrangeLabel ?? (_polyCountOrangeLabel = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleRight, fontSize = 8, normal = { textColor = Color.Lerp(Color.red, Color.yellow, 0.5f) } });
                            }
                            else
                            {
                                if (mi.trianglesCount > 1000)
                                {
                                    stl = _polyCountYellowLabel ?? (_polyCountYellowLabel = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleRight, fontSize = 8, normal = { textColor = Color.yellow } });
                                }
                                else
                                {
                                    stl = _polyCountGreenLabel ?? (_polyCountGreenLabel = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleRight, fontSize = 8, normal = { textColor = Color.green } });
                                }
                            }
                        }
                        GUI.Label(new Rect(selectionRect.xMax - 16 - 32 - offset - textOffset, selectionRect.y, 32f, 16f), txt, stl);
                        textOffset += (int)stl.CalcSize(new GUIContent(txt)).x;
                    }
                }

                // Draw layer and tag text
                if (eSettings.HierarchyShowLayer || eSettings.HierarchyShowTag)
                {
                    var layer = go.layer;
                    if (_layerLabel == null) _layerLabel = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleRight, clipping = TextClipping.Clip, fontSize = 6, normal = { textColor = eGUI.gray } };
                    var txt = layer > 0 && eSettings.HierarchyShowLayer ? LayerMask.LayerToName(layer) : "";
                    try
                    {
                        if (go.tag != "Untagged" && eSettings.HierarchyShowTag) txt = txt == "" ? go.tag : txt + '\n' + go.tag;
                    }
                    catch { }
                    if (txt != "")
                    {
                        var textWidth = _layerLabel.CalcSize(new GUIContent(txt)).x;
                        GUI.Label(new Rect(selectionRect.xMax - 16 - textWidth - offset - textOffset, selectionRect.y, textWidth, 16f), txt, _layerLabel);
                    }
                }
            }
            
            // Draw checkbox
            if (_toggleButton == null) _toggleButton = new GUIStyle("OL Toggle");
            if (go.isStatic) { eGUI.BeginColors();  GUI.backgroundColor = new Color(0, 1, 1, 0.5f); }
            var act = GUI.Toggle(new Rect(selectionRect.xMax - 16 - offset, selectionRect.y, 16f, 16f), go.activeSelf, new GUIContent(), _toggleButton);
            if (act != go.activeSelf) go.SetActive(act);
            if (go.isStatic) eGUI.EndColors();

            // Draw expand button
            if (_expandButton == null)
                _expandButton = new GUIStyle(GUI.skin.button) { normal = GUI.skin.button.active };
            if (drawButton) GUI.Button(new Rect(selectionRect.x - 16, selectionRect.y, 16f, 16f), "", _expandButton);

            // Collect icons for all components
            var hh = new Texture2D[eHierarchyIcons.IconHandler.PRIORITIES_COUNT];
            foreach (var cmp in go.GetComponents<Component>())
            {
                if (cmp == null) // Missing component
                {
                    GUI.DrawTexture(new Rect(selectionRect.x - 16, selectionRect.y, 16f, 16f), eIcons.Get("icons/console.warnicon.sml.png"));
                    return;
                }
                var h = eHierarchyIcons.GetIconHandler(cmp);
                if (h != null && h.priority < eHierarchyIcons.Priority.Never && hh[(int)h.priority] == null) hh[(int)h.priority] = h.Get(cmp);
            }

            // Draw icon with highest priority
            var i = eHierarchyIcons.IconHandler.PRIORITIES_COUNT;
            while (i-- > 0)
            {
                if (hh[i] == null) continue;
                GUI.DrawTexture(new Rect(selectionRect.x - 16, selectionRect.y, 16f, 16f), hh[i]);
                return;
            }
            GUI.DrawTexture(new Rect(selectionRect.x - 16, selectionRect.y, 16f, 16f), eIcons.Get("icons/processed/gameobject icon.asset"));
        }

        //--------------------------------------------------------------------------------------------------------------------------

        private struct MeshInfo
        {
            public int trianglesCount;
            public bool dynamicBatching;

            public MeshInfo(int trianglesCount, bool dynamicBatching)
            {
                this.trianglesCount = trianglesCount;
                this.dynamicBatching = dynamicBatching;
            }
        }

        private static readonly QueryCache<Transform, MeshInfo> _getTransformMeshInfoCache = new QueryCache<Transform, MeshInfo>(tr => { var mi = new MeshInfo(0, true); GetTotalMeshInfo(tr, ref mi); return mi; }, 5);
        private static readonly QueryCache<Mesh, MeshInfo> _getMeshInfoCache = new QueryCache<Mesh, MeshInfo>(m => new MeshInfo(m.triangles.Length / 3, m.vertexCount < 900), 10);

        private static void GetTotalMeshInfo(Transform tr, ref MeshInfo mi)
        {
            var cmp = tr.GetComponent<MeshFilter>();
            if (cmp != null && cmp.sharedMesh != null)
            {
                var xmi = _getMeshInfoCache[cmp.sharedMesh];
                mi.trianglesCount += xmi.trianglesCount;
                mi.dynamicBatching &= xmi.dynamicBatching;
            }

            for (var i = 0; i < tr.childCount; i++)
                GetTotalMeshInfo(tr.GetChild(i), ref mi);
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}