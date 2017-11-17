using UnityEditor;
using UnityEngine;

namespace Smart.Extensions
{
    public static class eAcnhorsAroundObject
    {
        [MenuItem("GameObject/UI/Smart Anchors Around Object")]
        private static void Execute()
        {
            var go = Selection.activeGameObject;
            if (go && go.GetComponent<RectTransform>())
            {
                var r = go.GetComponent<RectTransform>();
                var p = go.transform.parent.GetComponent<RectTransform>();

                var offsetMin = r.offsetMin;
                var offsetMax = r.offsetMax;
                var _anchorMin = r.anchorMin;
                var _anchorMax = r.anchorMax;

                var parent_width = p.rect.width;
                var parent_height = p.rect.height;

                r.anchorMin = new Vector2(_anchorMin.x + offsetMin.x / parent_width, _anchorMin.y + offsetMin.y / parent_height);
                r.anchorMax = new Vector2(_anchorMax.x + offsetMax.x / parent_width, _anchorMax.y + offsetMax.y / parent_height);

                r.offsetMin = new Vector2(0, 0);
                r.offsetMax = new Vector2(0, 0);
                r.pivot = new Vector2(0.5f, 0.5f);

                Undo.RegisterCompleteObjectUndo(go, "Anchors Around Object");
            }
        }
    }
}