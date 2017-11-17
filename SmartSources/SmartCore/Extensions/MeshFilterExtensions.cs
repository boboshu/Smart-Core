using System.Linq;
using UnityEngine;

namespace Smart.Extensions
{
    public static class MeshFilterExtensions
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public static bool MeshIsLODN(this MeshFilter mf)
        {
            var go = mf.gameObject;

            var lodGroup = go.GetComponentInParent<LODGroup>();
            if (lodGroup == null || lodGroup.lodCount < 2) return false;

            var renderersLOD0 = lodGroup.GetLODs()[0].renderers;
            if (renderersLOD0 == null || renderersLOD0.Length == 0) return false;

            return renderersLOD0.All(mr => mr != null && mr.gameObject != go); // all LOD0 meshes not this mesh
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}