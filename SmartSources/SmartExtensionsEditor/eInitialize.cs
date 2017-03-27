using Smart.Editors;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Smart
{
    [InitializeOnLoad]
    public static class eInitialize
    {
        static eInitialize()
        {
            eHierarchyIcons.Reg<GameObject>("icons/processed/gameobject icon.asset", eHierarchyIcons.Priority.Never);
            eHierarchyIcons.Reg<Transform>("icons/processed/transform icon.asset", eHierarchyIcons.Priority.Never);
            eHierarchyIcons.Reg<RectTransform>("icons/processed/d_recttransform icon.asset", eHierarchyIcons.Priority.Never);
            eHierarchyIcons.Reg<CanvasRenderer>("icons/processed/d_canvasrenderer icon.asset", eHierarchyIcons.Priority.Never);

            eHierarchyIcons.Reg<Terrain>("icons/processed/terrain icon.asset", eHierarchyIcons.Priority.Higher);
            eHierarchyIcons.Reg<Tree>("icons/d_tree_icon_frond.png", eHierarchyIcons.Priority.Higher);
            eHierarchyIcons.Reg<Camera>("icons/processed/camera icon.asset", eHierarchyIcons.Priority.Higher);

            eHierarchyIcons.Reg<OcclusionArea>("icons/processed/occlusionarea icon.asset", eHierarchyIcons.Priority.Lower);
            eHierarchyIcons.Reg<OcclusionPortal>("icons/processed/occlusionportal icon.asset", eHierarchyIcons.Priority.Lower);
            eHierarchyIcons.Reg<OffMeshLink>("icons/processed/offmeshlink icon.asset", eHierarchyIcons.Priority.Lower);

            eHierarchyIcons.Reg<AudioReverbZone>("icons/processed/audioreverbzone icon.asset", eHierarchyIcons.Priority.Lower);
            eHierarchyIcons.Reg<AudioSource>("icons/processed/audiosource icon.asset", eHierarchyIcons.Priority.Lower);

            eHierarchyIcons.Reg<Sprite>("icons/processed/sprite icon.asset", eHierarchyIcons.Priority.Lower);
            eHierarchyIcons.Reg<Rigidbody>("icons/processed/rigidbody icon.asset", eHierarchyIcons.Priority.Lower);
            eHierarchyIcons.Reg<ParticleSystem>("icons/processed/d_particlesystem icon.asset", eHierarchyIcons.Priority.Lower);
            eHierarchyIcons.Reg<NetworkView>("icons/processed/networkview icon.asset", eHierarchyIcons.Priority.Lower);
            eHierarchyIcons.Reg<Projector>("icons/processed/projector icon.asset", eHierarchyIcons.Priority.Lower);

            eHierarchyIcons.Reg<EventSystem>("icons/processed/unityengine/eventsystems/d_eventsystem icon.asset", eHierarchyIcons.Priority.Higher);
            eHierarchyIcons.Reg<Canvas>("icons/processed/d_canvas icon.asset", eHierarchyIcons.Priority.Higher);

            eHierarchyIcons.Reg<RawImage>("icons/processed/unityengine/ui/rawimage icon.asset", eHierarchyIcons.Priority.Lower);
            eHierarchyIcons.Reg<Image>("icons/processed/unityengine/ui/image icon.asset", eHierarchyIcons.Priority.Lowest);
            eHierarchyIcons.Reg<Button>("icons/processed/unityengine/ui/button icon.asset", eHierarchyIcons.Priority.Lower);
            eHierarchyIcons.Reg<Slider>("icons/processed/unityengine/ui/slider icon.asset", eHierarchyIcons.Priority.Lower);
            eHierarchyIcons.Reg<Text>("icons/processed/unityengine/ui/text icon.asset", eHierarchyIcons.Priority.Lowest);
            eHierarchyIcons.Reg<InputField>("icons/processed/unityengine/ui/inputfield icon.asset", eHierarchyIcons.Priority.Lower);
            eHierarchyIcons.Reg<Toggle>("icons/processed/unityengine/ui/toggle icon.asset", eHierarchyIcons.Priority.Lower);
            eHierarchyIcons.Reg<Mask>("icons/processed/unityengine/ui/mask icon.asset", eHierarchyIcons.Priority.Lower);
            eHierarchyIcons.Reg<Dropdown>("icons/processed/unityengine/ui/dropdown icon.asset", eHierarchyIcons.Priority.Lower);

            eHierarchyIcons.Reg<HorizontalLayoutGroup>("icons/processed/d_horizontallayoutgroup icon.asset", eHierarchyIcons.Priority.Lowest);
            eHierarchyIcons.Reg<VerticalLayoutGroup>("icons/processed/d_verticallayoutgroup icon.asset", eHierarchyIcons.Priority.Lowest);
            eHierarchyIcons.Reg<GridLayoutGroup>("icons/processed/d_gridlayoutgroup icon.asset", eHierarchyIcons.Priority.Lowest);
            eHierarchyIcons.Reg<LayoutElement>("icons/processed/unityengine/ui/d_layoutelement icon.asset", eHierarchyIcons.Priority.Lowest);

            eHierarchyIcons.Reg<Scrollbar>("icons/processed/unityengine/ui/scrollbar icon.asset");
            eHierarchyIcons.Reg<ScrollRect>("icons/processed/unityengine/ui/scrollrect icon.asset");
            eHierarchyIcons.Reg<ToggleGroup>("icons/processed/unityengine/ui/togglegroup icon.asset");

            eHierarchyIcons.Reg<NetworkAnimator>("icons/processed/unityengine/networking/networkanimator icon.asset", eHierarchyIcons.Priority.Lower);
            eHierarchyIcons.Reg<NetworkIdentity>("icons/processed/unityengine/networking/networkidentity icon.asset", eHierarchyIcons.Priority.Lowest);
            eHierarchyIcons.Reg<NetworkLobbyManager>("icons/processed/unityengine/networking/networklobbymanager icon.asset", eHierarchyIcons.Priority.Lower);
            eHierarchyIcons.Reg<NetworkLobbyPlayer>("icons/processed/unityengine/networking/networklobbyplayer icon.asset", eHierarchyIcons.Priority.Lower);
            eHierarchyIcons.Reg<NetworkManager>("icons/processed/unityengine/networking/networkmanager icon.asset", eHierarchyIcons.Priority.Lower);
            eHierarchyIcons.Reg<NetworkManagerHUD>("icons/processed/unityengine/networking/networkmanagerhud icon.asset", eHierarchyIcons.Priority.Lower);
            eHierarchyIcons.Reg<NetworkProximityChecker>("icons/processed/unityengine/networking/networkproximitychecker icon.asset", eHierarchyIcons.Priority.Lower);
            eHierarchyIcons.Reg<NetworkStartPosition>("icons/processed/unityengine/networking/networkstartposition icon.asset", eHierarchyIcons.Priority.Lower);
            eHierarchyIcons.Reg<NetworkTransform>("icons/processed/unityengine/networking/networktransform icon.asset", eHierarchyIcons.Priority.Lower);
            eHierarchyIcons.Reg<NetworkTransformVisualizer>("icons/processed/unityengine/networking/networktransformvisualizer icon.asset", eHierarchyIcons.Priority.Lower);

            eHierarchyIcons.Reg<Light>(x =>
            {
                switch (x.type)
                {
                    case LightType.Point: return eIcons.Get("icons/processed/light icon.asset");
                    case LightType.Directional: return eIcons.Get("icons/processed/directionallight icon.asset");
                    case LightType.Spot: return eIcons.Get("icons/processed/spotlight icon.asset");
                    case LightType.Area: return eIcons.Get("icons/processed/arealight icon.asset");
                }
                return null;
            }, eHierarchyIcons.Priority.Lower);
        }
    }
}
