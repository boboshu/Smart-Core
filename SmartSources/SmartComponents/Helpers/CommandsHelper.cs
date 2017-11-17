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

        public void GO_DestroyAllChildren()
        {
            TargetGameObject.DestroyChildren(false);
        }

        public void GO_DestroyAllChildrenImmediate()
        {
            TargetGameObject.DestroyChildren();
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

        public void GO_SetPositionX(float value)
        {
            var pos = TargetGameObject.transform.localPosition;
            TargetGameObject.transform.localPosition = new Vector3(value, pos.y, pos.z);
        }

        public void GO_SetPositionY(float value)
        {
            var pos = TargetGameObject.transform.localPosition;
            TargetGameObject.transform.localPosition = new Vector3(pos.x, value, pos.z);
        }

        public void GO_SetPositionZ(float value)
        {
            var pos = TargetGameObject.transform.localPosition;
            TargetGameObject.transform.localPosition = new Vector3(pos.x, pos.y, value);
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

        public void GO_SetScaleX(float value)
        {
            var scale = TargetGameObject.transform.localScale;
            TargetGameObject.transform.localScale = new Vector3(value, scale.y, scale.z);
        }

        public void GO_SetScaleY(float value)
        {
            var scale = TargetGameObject.transform.localScale;
            TargetGameObject.transform.localScale = new Vector3(scale.x, value, scale.z);
        }

        public void GO_SetScaleZ(float value)
        {
            var scale = TargetGameObject.transform.localScale;
            TargetGameObject.transform.localScale = new Vector3(scale.x, scale.y, value);
        }

        //-------------------------------------------------------------------------------

        public void GO_SetScaleXY(float value)
        {
            var scale = TargetGameObject.transform.localScale;
            TargetGameObject.transform.localScale = new Vector3(value, value, scale.z);
        }

        public void GO_SetScaleXZ(float value)
        {
            var scale = TargetGameObject.transform.localScale;
            TargetGameObject.transform.localScale = new Vector3(value, scale.y, value);
        }

        public void GO_SetScaleYZ(float value)
        {
            var scale = TargetGameObject.transform.localScale;
            TargetGameObject.transform.localScale = new Vector3(scale.x, value, value);
        }

        public void GO_SetScaleXYZ(float value)
        {
            TargetGameObject.transform.localScale = new Vector3(value, value, value);
        }

        //-------------------------------------------------------------------------------

        public void GO_SetNotActive(bool value)
        {
            TargetGameObject.SetActive(!value);
        }

        //-------------------------------------------------------------------------------
        // Animation
        //-------------------------------------------------------------------------------

        public void Anim_Stop()
        {
            Enum<Animation>(a => a.Stop());
        }

        public void Anim_Play()
        {
            Enum<Animation>(a => a.Play());
        }

        public void Anim_Play(string animationName)
        {
            Enum<Animation>(a => a.Play(animationName));
        }

        //-------------------------------------------------------------------------------
        // Sound
        //-------------------------------------------------------------------------------

        public void Sound_Play(AudioClip clip)
        {

            Enum<AudioSource>(a => a.PlayOneShot(clip));
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
            Enum<Light>(lght =>
            {
                switch (type)
                {
                    case 0: lght.shadows = LightShadows.None; break;
                    case 1: lght.shadows = LightShadows.Hard; break;
                    case 2: lght.shadows = LightShadows.Soft; break;
                }
            });
        }

        //-------------------------------------------------------------------------------
        // Sound
        //-------------------------------------------------------------------------------

        public void Snd_SetRolloffModeLogarithmic(bool isLogarithmic)
        {
            Enum<AudioSource>(a => a.rolloffMode = isLogarithmic ? AudioRolloffMode.Logarithmic : AudioRolloffMode.Linear);
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

        public void MR_SetMaterial_6(Material material)
        {
            MR_SetMaterial(6, material);
        }

        public void MR_SetMaterial_7(Material material)
        {
            MR_SetMaterial(7, material);
        }

        private void MR_SetMaterial(int index, Material material)
        {
            Enum<MeshRenderer>(mr =>
            {
                var materials = mr.materials;
                if (index >= materials.Length) return;
                materials[index] = material;
                mr.materials = materials;
            });
        }

        //-------------------------------------------------------------------------------
    }
}
