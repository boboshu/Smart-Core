using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Smart.Editors
{
    [InitializeOnLoad]
    public static class eHierarchyIcons
    {
        public enum Priority
        {
            Lowest = 0,
            Lower = 1,
            Normal = 2,
            Higher = 3,
            Highest = 4,
            Never = 999
        }

        public abstract class IconHandler
        {
            public const int PRIORITIES_COUNT = 5;
            public Priority priority;

            public abstract Texture2D Get(object obj = null);
        }

        public class IconHandlerGeneric<T> : IconHandler
            where T : class
        {
            public Func<T, Texture2D> onGet;

            public override Texture2D Get(object obj = null)
            {
                return onGet(obj as T);
            }
        }

        public class IconHandlerStatic : IconHandler
        {
            public Func<Texture2D> onGet;

            public override Texture2D Get(object obj = null)
            {
                return onGet();
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public static void ScriptPriority<T>(Priority priority)
            where T : MonoBehaviour
        {
            _scriptPriorities[typeof(T)] = priority;
        }

        public static void Reg<T>(string icon, Priority priority = Priority.Normal)
            where T : class
        {
            var type = typeof(T);
            if (_handlers.ContainsKey(type)) _handlers.Remove(type);
            _handlers.Add(type, new IconHandlerGeneric<T> { priority = priority, onGet = _ => eIcons.Get(icon) });
        }

        public static void Reg<T>(Func<T, Texture2D> onGet, Priority priority = Priority.Normal)
            where T : class
        {
            var type = typeof(T);
            if (_handlers.ContainsKey(type)) _handlers.Remove(type);
            _handlers.Add(type, new IconHandlerGeneric<T> { priority = priority, onGet = onGet });
        }

        public static IconHandler GetIconHandler(object obj)
        {
            return obj == null ? null : GetIconHandler(obj.GetType());
        }

        public static IconHandler GetIconHandler<T>() where T : MonoBehaviour
        {
            return GetIconHandler(typeof(T));
        }

        public static IconHandler GetIconHandler(Type type)
        {
            IconHandler h;
            if (_handlers.TryGetValue(type, out h)) return h;

            if (typeof(MonoBehaviour).IsAssignableFrom(type)) // try get script icon
            {
                var tex = eIcons.GetForScript(type.Name);
                if (tex == null)
                {
                    _handlers.Add(type, null);
                    return null;
                }

                Priority priority;
                if (!_scriptPriorities.TryGetValue(type, out priority)) priority = Priority.Normal; // use preset priority
                h = new IconHandlerStatic { priority = priority, onGet = () => tex };
                _handlers.Add(type, h);
                return h;
            }
            return null;
        }

        private static readonly Dictionary<Type, IconHandler> _handlers = new Dictionary<Type, IconHandler>();
        private static readonly Dictionary<Type, Priority> _scriptPriorities = new Dictionary<Type, Priority>();
    }
}