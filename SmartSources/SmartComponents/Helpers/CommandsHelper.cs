using Smart.Extensions;
using Smart.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Commands Helper")]
    public class CommandsHelper : RedirectableHelper
    {
        //-------------------------------------------------------------------------------
        // Debugger
        //-------------------------------------------------------------------------------

        public void Log_Value(bool value)
        {
            Debug.Log("Value: " + value);
        }

        public void Log_Value(float value)
        {
            Debug.Log("Value: " + value);
        }

        public void Log_Value(int value)
        {
            Debug.Log("Value: " + value);
        }

        public void Log_Text(string text)
        {
            Debug.Log(text);
        }

        public void Log_Warning(string text)
        {
            Debug.LogWarning(text);
        }

        public void Log_Error(string text)
        {
            Debug.LogError(text);
        }

        //-------------------------------------------------------------------------------
        // Application
        //-------------------------------------------------------------------------------

        public void App_LoadLevelAdditiveAsync(string sceneName)
        {
            if (string.IsNullOrEmpty(sceneName)) return;
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        public void App_LoadLevelAdditive(string sceneName)
        {
            if (string.IsNullOrEmpty(sceneName)) return;
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }

        public void App_LoadLevelAsync(string sceneName)
        {
            if (string.IsNullOrEmpty(sceneName)) return;
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }

        public void App_LoadLevel(string sceneName)
        {
            if (string.IsNullOrEmpty(sceneName)) return;
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

        //-------------------------------------------------------------------------------

        public void App_Quit()
        {
            Application.Quit();
        }

        //-------------------------------------------------------------------------------
        // Canvas Manager
        //-------------------------------------------------------------------------------

        public void Cnvs_Open(Canvas canvas)
        {
            CanvasManager.instance.Open(canvas);
        }

        public void Cnvs_OpenPopup(Canvas canvas)
        {
            CanvasManager.instance.OpenPopup(canvas);
        }

        public void Cnvs_ClosePopup(Canvas canvas)
        {
            CanvasManager.instance.ClosePopup(canvas);
        }

        //-------------------------------------------------------------------------------
        // GameObject
        //-------------------------------------------------------------------------------

        public void GO_Destroy(Component target)
        {
            Destroy(target);
        }

        public void GO_Destroy(GameObject target)
        {
            Destroy(target);
        }

        public void GO_Destroy()
        {
            Destroy(TargetGameObject);
        }

        public void GO_InstantiateHere(GameObject prefab)
        {
            Instantiate(prefab).Reparent(TargetGameObject);
        }

        public void GO_InstantiateToRoot(GameObject prefab)
        {
            Instantiate(prefab);
        }

        public void GO_ReparentTo(GameObject newParent)
        {
            TargetGameObject.Reparent(newParent);
        }

        public void GO_ReparentHere(GameObject child)
        {
            child.Reparent(TargetGameObject);
        }

        public void GO_MoveTo(GameObject target)
        {
            TargetGameObject.transform.position = target.transform.position;
        }

        public void GO_MoveHere(GameObject target)
        {
            target.transform.position = TargetGameObject.transform.position;
        }

        public void GO_CopyRotationFrom(Transform rotationSource)
        {
            TargetGameObject.transform.rotation = transform.localRotation;
        }

        //-------------------------------------------------------------------------------

        public void GO_RotateX(float angle)
        {
            TargetGameObject.transform.Rotate(angle, 0, 0);
        }

        public void GO_RotateY(float angle)
        {
            TargetGameObject.transform.Rotate(0, angle, 0);
        }

        public void GO_RotateZ(float angle)
        {
            TargetGameObject.transform.Rotate(0, 0, angle);
        }

        //-------------------------------------------------------------------------------

        public void GO_SetRotationX(float angle)
        {
            TargetGameObject.transform.localRotation = Quaternion.Euler(angle, 0, 0);
        }

        public void GO_SetRotationY(float angle)
        {
            TargetGameObject.transform.localRotation = Quaternion.Euler(0, angle, 0);
        }

        public void GO_SetRotationZ(float angle)
        {
            TargetGameObject.transform.localRotation = Quaternion.Euler(0, 0, angle);
        }

        //-------------------------------------------------------------------------------
        // Animation
        //-------------------------------------------------------------------------------

        public void Anim_Stop()
        {
            TargetGameObject.GetComponent<Animation>()?.Stop();
        }

        public void Anim_Play()
        {
            TargetGameObject.GetComponent<Animation>()?.Play();
        }

        public void Anim_Play(string animationName)
        {
            TargetGameObject.GetComponent<Animation>()?.Play(animationName);
        }

        //-------------------------------------------------------------------------------
        // Sound
        //-------------------------------------------------------------------------------

        public void Sound_Play(AudioClip clip)
        {
            TargetGameObject.GetOrAddComponent<AudioSource>().PlayOneShot(clip);
        }

        //-------------------------------------------------------------------------------
        // Scene
        //-------------------------------------------------------------------------------

        public void Scn_SetFogDensity(float density)
        {
            RenderSettings.fogDensity = density;
        }

        //-------------------------------------------------------------------------------
        // Light
        //-------------------------------------------------------------------------------

        public void Lght_SetShadowType(int type)
        {
            var cmp = TargetGameObject.GetComponent<Light>();
            if (!cmp) return;

            switch (type)
            {
                case 0: cmp.shadows = LightShadows.None; break;
                case 1: cmp.shadows = LightShadows.Hard; break;
                case 2: cmp.shadows = LightShadows.Soft; break;
            }
        }

        //-------------------------------------------------------------------------------
        // Sound
        //-------------------------------------------------------------------------------

        public void Snd_SetRolloffModeLogarithmic(bool isLogarithmic)
        {
            var cmp = TargetGameObject.GetComponent<AudioSource>();
            if (cmp) cmp.rolloffMode = isLogarithmic ? AudioRolloffMode.Logarithmic : AudioRolloffMode.Linear;
        }

        //-------------------------------------------------------------------------------
        // Component
        //-------------------------------------------------------------------------------

        public void Cmp_ToggleEnabled(MonoBehaviour cmp)
        {
            cmp.enabled = !cmp.enabled;
        }

        public void Cmp_Enable(MonoBehaviour cmp)
        {
            cmp.enabled = true;
        }

        public void Cmp_Disable(MonoBehaviour cmp)
        {
            cmp.enabled = false;
        }

        public void Cmp_CopyFromReference(MonoBehaviour reference)
        {
            if (reference == null)
            {
                Debug.LogError("[" + name + ".Cmp_CopyFromReferenceDeep]: Reference is null");
                return;
            }

            TargetGameObject.CopyComponent(reference);
        }

        public void Cmp_RemoveAsReference(MonoBehaviour reference)
        {
            if (reference == null)
            {
                Debug.LogError("[" + name + ".Cmp_RemoveAsReference]: Reference is null");
                return;
            }

            var cmp = TargetGameObject.GetComponent(reference.GetType());
            if (cmp) Destroy(cmp);
        }

        //-------------------------------------------------------------------------------
        // Mesh Renderer
        //-------------------------------------------------------------------------------

        public void MR_SetMaterial_0(Material material)
        {
            MR_SetMaterial(0, material);
        }

        public void MR_SetMaterial_1(Material material)
        {
            MR_SetMaterial(1, material);
        }

        public void MR_SetMaterial_2(Material material)
        {
            MR_SetMaterial(2, material);
        }

        public void MR_SetMaterial_3(Material material)
        {
            MR_SetMaterial(3, material);
        }

        public void MR_SetMaterial_4(Material material)
        {
            MR_SetMaterial(4, material);
        }

        public void MR_SetMaterial_5(Material material)
        {
            MR_SetMaterial(5, material);
        }

        private void MR_SetMaterial(int index, Material material)
        {
            var cmp = TargetGameObject.GetComponent<MeshRenderer>();
            if (cmp == null) return;

            var materials = cmp.materials;
            if (index >= materials.Length) return;

            materials[index] = material;
            cmp.materials = materials;
        }

        //-------------------------------------------------------------------------------

        public void MR_SetMaterial_InChilds_0(Material material)
        {
            MR_SetMaterial_InChilds(0, material);
        }

        public void MR_SetMaterial_InChilds_1(Material material)
        {
            MR_SetMaterial_InChilds(1, material);
        }

        public void MR_SetMaterial_InChilds_2(Material material)
        {
            MR_SetMaterial_InChilds(2, material);
        }

        public void MR_SetMaterial_InChilds_3(Material material)
        {
            MR_SetMaterial_InChilds(3, material);
        }

        public void MR_SetMaterial_InChilds_4(Material material)
        {
            MR_SetMaterial_InChilds(4, material);
        }

        public void MR_SetMaterial_InChilds_5(Material material)
        {
            MR_SetMaterial_InChilds(5, material);
        }

        private void MR_SetMaterial_InChilds(int index, Material material)
        {
            foreach (var cmp in TargetGameObject.GetComponentsInChildren<MeshRenderer>(true))
            {
                var materials = cmp.materials;
                if (index >= materials.Length) continue;

                materials[index] = material;
                cmp.materials = materials;
            }
        }

        //-------------------------------------------------------------------------------
    }
}
