using Smart.Extensions;
using UnityEngine;

namespace Smart.Helpers
{
    public class TemplateHelper : MonoBehaviour
    {
        public GameObject template;
        public GameObject[] redirectCommandsTo = new GameObject[0];
        public bool syncExistingComponents = false;

        public enum Mode { InstantiateGameObject, CopyAllComponents, CopyEnabledComponents }
        public Mode mode;

        void Awake()
        {
            if (template)
            {
                if (mode == Mode.InstantiateGameObject) InstantiateChildGameObject();
                else CopyComponents(mode == Mode.CopyEnabledComponents);
            }
            Destroy(this);
        }

        private void CopyComponents(bool onlyEnabled)
        {
            var cmdIndx = 0;
            var isActive = gameObject.activeSelf;
            if (isActive) gameObject.SetActive(false);

            foreach (var cmp in template.GetComponents<Component>())
            {
                if (cmp is Transform) continue;

                if (onlyEnabled)
                {
                    var beh = cmp as Behaviour;
                    if (beh && !beh.enabled) continue;
                }

                // already has component of this type
                var existing = GetComponent(cmp.GetType());
                if (existing != null)
                {
                    if (syncExistingComponents) existing.SyncComponent(cmp);
                    continue; 
                }

                var copy = gameObject.CopyComponent(cmp);

                if (redirectCommandsTo.Length > 0)
                {
                    var redirectable = copy as RedirectableHelper;
                    if (redirectable)
                    {
                        redirectable.redirect = redirectCommandsTo[cmdIndx++];
                        cmdIndx %= redirectCommandsTo.Length;
                    }                    
                }
            }

            if (isActive) gameObject.SetActive(true);
        }

        private void InstantiateChildGameObject()
        {
            var inst = template.CreateInstance(gameObject);
            if (redirectCommandsTo.Length == 0) return;

            var cmdIndx = 0;
            foreach (var redirectable in inst.GetComponentsInChildren<RedirectableHelper>(true))
            {
                redirectable.redirect = redirectCommandsTo[cmdIndx++];
                cmdIndx %= redirectCommandsTo.Length;
            }
        }
    }
}