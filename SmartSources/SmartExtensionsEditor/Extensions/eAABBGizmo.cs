using Smart.Extensions;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class eAABBGizmo
{
    static eAABBGizmo()
    {
        SceneView.onSceneGUIDelegate += OnSceneGUI;
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        if (!eSettings.AABBGizmo) return;
        if (Event.current.type != EventType.Repaint) return;

        var go = Selection.activeGameObject;
        if (go && PrefabUtility.GetPrefabType(go) != PrefabType.Prefab)
            DrawAABB(go.transform);
    }

    private static Material _material;

    private static void DrawAABB(Transform tr)
    {
        var bb = tr.CalcBounds(false) ?? new Bounds { min = tr.position - tr.localScale * 0.5f, max = tr.position + tr.localScale * 0.5f };

        var min = bb.min;
        var max = bb.max;
        var center = bb.center;

        var minX = -1000 + center.x;
        var maxX = 1000 + center.x;
        var minY = -1000 + center.y;
        var maxY = 1000 + center.y;
        var minZ = -1000 + center.z;
        var maxZ = 1000 + center.z;

        if (_material == null) _material = new Material(Shader.Find("Hidden/GizmoShader"));
        _material.SetPass(1);

        GL.Begin(GL.LINES);
        GL.Color(new Color(1, 0.5f, 0.5f, 0.3f)); // Light red
        GL.Vertex(new Vector3(minX, min.y, min.z)); GL.Vertex(new Vector3(maxX, min.y, min.z));
        GL.Vertex(new Vector3(minX, min.y, max.z)); GL.Vertex(new Vector3(maxX, min.y, max.z));
        GL.Vertex(new Vector3(minX, max.y, min.z)); GL.Vertex(new Vector3(maxX, max.y, min.z));
        GL.Vertex(new Vector3(minX, max.y, max.z)); GL.Vertex(new Vector3(maxX, max.y, max.z));

        GL.Color(new Color(0.5f, 1, 0.5f, 0.3f)); // Light green
        GL.Vertex(new Vector3(max.x, minY, min.z)); GL.Vertex(new Vector3(max.x, maxY, min.z));
        GL.Vertex(new Vector3(min.x, minY, min.z)); GL.Vertex(new Vector3(min.x, maxY, min.z));
        GL.Vertex(new Vector3(max.x, minY, max.z)); GL.Vertex(new Vector3(max.x, maxY, max.z));
        GL.Vertex(new Vector3(min.x, minY, max.z)); GL.Vertex(new Vector3(min.x, maxY, max.z));

        GL.Color(new Color(0.5f, 0.5f, 1, 0.3f)); // Light blue
        GL.Vertex(new Vector3(max.x, min.y, minZ)); GL.Vertex(new Vector3(max.x, min.y, maxZ));
        GL.Vertex(new Vector3(min.x, min.y, minZ)); GL.Vertex(new Vector3(min.x, min.y, maxZ));
        GL.Vertex(new Vector3(max.x, max.y, minZ)); GL.Vertex(new Vector3(max.x, max.y, maxZ));
        GL.Vertex(new Vector3(min.x, max.y, minZ)); GL.Vertex(new Vector3(min.x, max.y, maxZ));
        GL.End();
    }
}