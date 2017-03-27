using System.Collections.Generic;
using System.Linq;
using Smart.Extensions;
using UnityEngine;

namespace Smart.Bindings
{
    [AddComponentMenu("Smart/Bindings/Collection Binding")]
    public class CollectionBinding : Binding<IEnumerable<Object>>
    {
        public BindingRoot itemPrefab;
        private readonly Dictionary<Object, BindingRoot> _itemsPresentors = new Dictionary<Object, BindingRoot>();
        private static readonly Object[] _nullCollection = new Object[0];

        protected override void Apply(IEnumerable<Object> value)
        {
            if (!Application.isPlaying) return; // do not update collections from editor

            if (itemPrefab == null) return; // Skip if there is no prefab
            var itemsSources = (value == null) ? _nullCollection : value.ToArray(); // use special empty collection if value is not specified

            // Detach all existing presentors
            foreach (var presentor in _itemsPresentors.Values)
                presentor.Reparent((GameObject)null);

            // Add items and their presentors (reusing old if possible)
            foreach (var itemSource in itemsSources)
            {
                BindingRoot presentor;
                if (_itemsPresentors.TryGetValue(itemSource, out presentor)) presentor.Reparent(this);
                else
                {
                    var inst = PrefabExtensions.CreateInstance(itemPrefab, this);
                    _itemsPresentors.Add(itemSource, inst);
                    inst.sourceObject = itemSource;
                }
            }

            // Remove unused items and their presentors
            foreach (var unusedItemAndPresentor in _itemsPresentors.Where(p => !itemsSources.Contains(p.Key)).ToArray())
            {
                _itemsPresentors.Remove(unusedItemAndPresentor.Key);
                Destroy(unusedItemAndPresentor.Value.gameObject);
            }
        }

        protected override bool IsEquals(IEnumerable<Object> v1, IEnumerable<Object> v2)
        {
            if (v1 == null || v2 == null) return (v1 == v2);
            if (v1.Count() != v2.Count()) return false;

            try
            {
                return v1.SequenceEqual(v2);
            }
            catch
            {
                return true;
            }
        }
    }
}
