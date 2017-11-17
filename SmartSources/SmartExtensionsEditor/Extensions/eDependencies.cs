using System.Linq;
using Smart.Editors;
using Smart.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;

namespace Smart.Extensions
{
    public class eDependencies : EditorWindow
    {
        private Object _obj;
        private Object[] _dependencies;
        private Vector2 _scrollPos;
        private byte _pageIndex;

        private IOrderedEnumerable<Texture> _enumTextures;
        private IOrderedEnumerable<Material> _enumMaterials;
        private IOrderedEnumerable<Mesh> _enumMeshes;
        private IOrderedEnumerable<Shader> _enumShaders;
        private IOrderedEnumerable<MonoBehaviour> _enumScripts;
        private IOrderedEnumerable<GameObject> _enumGameObjects;
        private IOrderedEnumerable<Object> _enumAnimations;
        private IOrderedEnumerable<AudioClip> _enumAudio;
        private IOrderedEnumerable<Object> _enumOther;

        private readonly string[] _names = new string[9];
        private static eDependencies _window;

        [MenuItem("Tools/Smart/Dependencies #&D", false, 4006)]
        public static void MainMenuItem()
        {
            if (_window) _window.Close();
            else
            {
                _window = GetWindow<eDependencies>(false, "Dependencies");
                _window.minSize = new Vector2(300, 300);
                _window.titleContent.text = "Dependencies";
                _window.Show();
            }
        }

        void OnInspectorUpdate()
        {
            Repaint();
        }

        void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            try
            {
                var oldObj = _obj;
                eGUI.EmptySpace();
                EditorGUILayout.BeginHorizontal();
                {
                    eGUI.LabelBold(_obj == null ? "Drop object here" : "Dependencies source");
                    _obj = EditorGUILayout.ObjectField("", _obj, typeof(Object), true);
                    if (_obj != null && eGUI.Button("Clear", eGUI.redLt, 50)) _obj = null;
                }
                EditorGUILayout.EndHorizontal();
                eGUI.LineDelimiter();

                if (_obj == null) return;
                UpdateData(oldObj);
                if (_dependencies.Length < 50) DrawSimpleView();
                else DrawTabbedView();
            }
            finally
            {
                EditorGUILayout.EndVertical();
            }
        }

        private void UpdateData(Object oldObj)
        {
            if (oldObj != _obj || _dependencies == null || _enumTextures == null || _enumMaterials == null || _enumMeshes == null || _enumShaders == null
                || _enumScripts == null || _enumGameObjects == null || _enumAnimations == null || _enumOther == null || _enumAudio == null)
            {
                EditorUtility.DisplayProgressBar("Please wait", "Collecting dependencies", 0);
                _dependencies = EditorUtility.CollectDependencies(new[] {_obj});
                _enumTextures = _dependencies.OfType<Texture>().OrderByDescending(t => t.width);
                _enumMaterials = _dependencies.OfType<Material>().OrderBy(x => x.name);
                _enumMeshes = _dependencies.OfType<Mesh>().OrderBy(x => x.name);
                _enumShaders = _dependencies.OfType<Shader>().OrderBy(x => x.name);
                _enumScripts = _dependencies.OfType<MonoBehaviour>().OrderBy(x => x.name);
                _enumGameObjects = _dependencies.OfType<GameObject>().OrderBy(x => x.name);
                _enumAnimations = _dependencies.Where(dep => dep is Avatar || dep is AnimationClip || dep is Animation).OrderBy(x => x.name);
                _enumAudio = _dependencies.OfType<AudioClip>().OrderBy(x => x.name);
                _enumOther = _dependencies.Where(dep => !(dep is Texture || dep is Material || dep is Mesh || dep is MeshRenderer || dep is Shader ||
                                                          dep is MeshFilter || dep is GameObject || dep is Transform || dep is MonoBehaviour || dep is Collider || dep is AnimationClip ||
                                                          dep is Animator || dep is Animation || dep is Avatar || dep is AudioClip)).OrderBy(x => x.name);

                EditorUtility.DisplayProgressBar("Please wait", "Preparing data", 0.5f);
                _names[0] = "Textures [" + _enumTextures.Count() + "] " + Size.GetMemSize(_enumTextures.Sum(t => Profiler.GetRuntimeMemorySize(t)));
                _names[1] = "Materials [" + _enumMaterials.Count() + "] " + Size.GetMemSize(_enumMaterials.Sum(x => Profiler.GetRuntimeMemorySize(x)));
                _names[2] = "Meshes [" + _enumMeshes.Count() + "] " + Size.GetMemSize(_enumMeshes.Sum(x => Profiler.GetRuntimeMemorySize(x)));
                _names[3] = "Shaders [" + _enumShaders.Count() + "] " + Size.GetMemSize(_enumShaders.Sum(x => Profiler.GetRuntimeMemorySize(x)));
                _names[4] = "Scripts [" + _enumScripts.Count() + "] " + Size.GetMemSize(_enumScripts.Sum(x => Profiler.GetRuntimeMemorySize(x)));
                _names[5] = "GameObjects [" + _enumGameObjects.Count() + "] " + Size.GetMemSize(_enumGameObjects.Sum(x => Profiler.GetRuntimeMemorySize(x)));
                _names[6] = "Animations [" + _enumAnimations.Count() + "] " + Size.GetMemSize(_enumAnimations.Sum(x => Profiler.GetRuntimeMemorySize(x)));
                _names[7] = "Audio [" + _enumAudio.Count() + "] " + Size.GetMemSize(_enumAudio.Sum(x => Profiler.GetRuntimeMemorySize(x)));
                _names[8] = "Other [" + _enumOther.Count() + "] " + Size.GetMemSize(_enumOther.Sum(x => Profiler.GetRuntimeMemorySize(x)));

                EditorUtility.ClearProgressBar();
            }
        }

        private void DrawSimpleView()
        {
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
            foreach (var dep in _enumOther)
                EditorGUILayout.ObjectField(dep.GetType().Name, dep, dep.GetType(), true);
            EditorGUILayout.EndScrollView();
        }

        private void DrawTabbedView()
        {
            var xCount = (int) (position.width / 200);
            eGUI.PagesGrid(ref _pageIndex, eGUI.violet, xCount, _names);

            eGUI.LineDelimiter();
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
            switch (_pageIndex)
            {
                case 0:
                    foreach (var dep in _enumTextures)
                    {
                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.ObjectField("", dep, dep.GetType(), true, GUILayout.Width(48), GUILayout.Height(48), GUILayout.ExpandWidth(false));
                            GUILayout.Label(dep.name + "\n[" + dep.width + " x " + dep.height + "]");
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    break;

                case 1:
                    foreach (var dep in _enumMaterials)
                        EditorGUILayout.ObjectField(dep.GetType().Name, dep, dep.GetType(), true);
                    break;

                case 2:
                    foreach (var dep in _enumMeshes)
                        EditorGUILayout.ObjectField(dep.GetType().Name, dep, dep.GetType(), true);
                    break;

                case 3:
                    foreach (var dep in _enumShaders)
                        EditorGUILayout.ObjectField(dep.GetType().Name, dep, dep.GetType(), true);
                    break;

                case 4:
                    foreach (var dep in _enumScripts)
                        EditorGUILayout.ObjectField(dep.GetType().Name, dep, dep.GetType(), true);
                    break;

                case 5:
                    foreach (var dep in _enumGameObjects)
                        EditorGUILayout.ObjectField(dep.GetType().Name, dep, dep.GetType(), true);
                    break;

                case 6:
                    foreach (var dep in _enumAnimations)
                    {
                        EditorGUILayout.ObjectField(dep.GetType().Name, dep, dep.GetType(), true);
                    }
                    break;

                case 7:
                    foreach (var dep in _enumAudio)
                        EditorGUILayout.ObjectField(dep.GetType().Name, dep, dep.GetType(), true);
                    break;

                case 8:
                    foreach (var dep in _enumOther)
                        EditorGUILayout.ObjectField(dep.GetType().Name, dep, dep.GetType(), true);
                    break;
            }
            EditorGUILayout.EndScrollView();
        }
    }
}