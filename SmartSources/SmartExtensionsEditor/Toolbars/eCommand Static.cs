using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Smart.Toolbars
{
    public partial class eCommand
    {
        public static Dictionary<string, Type> ComponentsDictionary
        {
            get
            {
                if (_componentsDictionary == null)
                {
                    var baseType = typeof(Component);
                    var unityEngine = typeof(Component).Assembly.GetTypes();
                    var unityEngineUI = typeof(Button).Assembly.GetTypes();
                    var unityEngineNet = typeof(NetworkBehaviour).Assembly.GetTypes();
                    var allTypes = unityEngine.Concat(unityEngineUI).Concat(unityEngineNet);
                    _componentsDictionary = allTypes.Where(t => t != baseType && baseType.IsAssignableFrom(t)).ToDictionary(t => t.Name, t => t);
                }
                return _componentsDictionary;
            }
        }
        public static Dictionary<string, Type> _componentsDictionary;

        //------------------------------------------------------------------------------------------------------------------------------------------------------

        public static string[] ComponentNames
        {
            get
            {
                return _componentsArray ?? (_componentsArray = ComponentsDictionary.Keys.OrderBy(x => x).ToArray());
            }
        }
        private static string[] _componentsArray;
    }
}