using System;
using System.Collections.Generic;
using UnityEngine;

namespace Smart.Types
{
    public abstract class SpatialNode
    {
        public ushort x;
        public ushort y;
        public ushort z;

        public Vector3 Center => WorldCoordinates(x, y, z);

        public abstract void Setup();
        public abstract void Clear();

        public static Vector3 WorldCoordinates(int x, int y, int z)
        {
            return new Vector3(x - 32767, y - 32767, z - 32767);
        }
    }

    public class SpatialNodes<T> where T : SpatialNode, new()
    {
        //--------------------------------------------------------------------------------------------------------------------------

        private static readonly Stack<T> _nodesPool = new Stack<T>();

        //--------------------------------------------------------------------------------------------------------------------------

        private class Layer0 // 4 x 4 x 4 (00 00 00 00 00 11)
        {
            public readonly T[] nodes = new T[64];

            public void Gen(ushort x, ushort y, ushort z)
            {
                var indx = (x & 3) | ((y & 3) << 2) | ((z & 3) << 4);
                if (nodes[indx] != null) return;
                var node = _nodesPool.Count > 0 ? _nodesPool.Pop() : new T();
                node.x = x; node.y = y; node.z = z;
                node.Setup();
                nodes[indx] = node;
            }

            public T GetOrGen(ushort x, ushort y, ushort z)
            {
                var indx = (x & 3) | ((y & 3) << 2) | ((z & 3) << 4);
                var node = nodes[indx];
                if (node != null) return node;
                node = _nodesPool.Count > 0 ? _nodesPool.Pop() : new T();
                node.x = x; node.y = y; node.z = z;
                node.Setup();
                nodes[indx] = node;
                return node;
            }

            public T Get(ushort x, ushort y, ushort z)
            {
                var indx = (x & 3) | ((y & 3) << 2) | ((z & 3) << 4);
                return nodes[indx];
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------

        private class Layer1 // 16 x 16 x 16 (00 00 00 00 11 00)
        {
            public readonly Layer0[] layers = new Layer0[64];

            public void Gen(ushort x, ushort y, ushort z)
            {
                var indx = (x & 12) >> 2 | (y & 12) | ((z & 12) << 2);
                (layers[indx] ?? (layers[indx] = new Layer0())).Gen(x, y, z);
            }

            public T GetOrGen(ushort x, ushort y, ushort z)
            {
                var indx = (x & 12) >> 2 | (y & 12) | ((z & 12) << 2);
                return (layers[indx] ?? (layers[indx] = new Layer0())).GetOrGen(x, y, z);
            }

            public T Get(ushort x, ushort y, ushort z)
            {
                var indx = (x & 12) >> 2 | (y & 12) | ((z & 12) << 2);
                return layers[indx]?.Get(x, y, z);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------

        private class Layer2 // 64 x 64 x 64 (00 00 00 11 00 00)
        {
            public readonly Layer1[] layers = new Layer1[64];

            public void Gen(ushort x, ushort y, ushort z)
            {
                var indx = (x & 48) >> 4 | (y & 48) >> 2 | (z & 48);
                (layers[indx] ?? (layers[indx] = new Layer1())).Gen(x, y, z);
            }

            public T GetOrGen(ushort x, ushort y, ushort z)
            {
                var indx = (x & 48) >> 4 | (y & 48) >> 2 | (z & 48);
                return (layers[indx] ?? (layers[indx] = new Layer1())).GetOrGen(x, y, z);
            }

            public T Get(ushort x, ushort y, ushort z)
            {
                var indx = (x & 48) >> 4 | (y & 48) >> 2 | (z & 48);
                return layers[indx]?.Get(x, y, z);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------

        private class Layer3 // 256 x 256 x 256 (00 00 11 00 00 00)
        {
            public readonly Layer2[] layers = new Layer2[64];

            public void Gen(ushort x, ushort y, ushort z)
            {
                var indx = (x & 192) >> 6 | (y & 192) >> 4 | (z & 192) >> 2;
                (layers[indx] ?? (layers[indx] = new Layer2())).Gen(x, y, z);
            }

            public T GetOrGen(ushort x, ushort y, ushort z)
            {
                var indx = (x & 192) >> 6 | (y & 192) >> 4 | (z & 192) >> 2;
                return (layers[indx] ?? (layers[indx] = new Layer2())).GetOrGen(x, y, z);
            }

            public T Get(ushort x, ushort y, ushort z)
            {
                var indx = (x & 192) >> 6 | (y & 192) >> 4 | (z & 192) >> 2;
                return layers[indx]?.Get(x, y, z);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------

        private class Layer4 // 1024 x 1024 x 1024 (00 11 00 00 00 00)
        {
            public readonly Layer3[] layers = new Layer3[64];

            public void Gen(ushort x, ushort y, ushort z)
            {
                var indx = (x & 768) >> 8 | (y & 768) >> 6 | (z & 768) >> 4;
                (layers[indx] ?? (layers[indx] = new Layer3())).Gen(x, y, z);
            }

            public T GetOrGen(ushort x, ushort y, ushort z)
            {
                var indx = (x & 768) >> 8 | (y & 768) >> 6 | (z & 768) >> 4;
                return (layers[indx] ?? (layers[indx] = new Layer3())).GetOrGen(x, y, z);
            }

            public T Get(ushort x, ushort y, ushort z)
            {
                var indx = (x & 768) >> 8 | (y & 768) >> 6 | (z & 768) >> 4;
                return layers[indx]?.Get(x, y, z);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------

        private class Layer5 // 4096 x 4096 x 4096 (11 00 00 00 00 00)
        {
            public readonly Layer4[] layers = new Layer4[64];

            public void Gen(ushort x, ushort y, ushort z)
            {
                var indx = (x & 3072) >> 10 | (y & 3072) >> 8 | (z & 3072) >> 6;
                (layers[indx] ?? (layers[indx] = new Layer4())).Gen(x, y, z);
            }

            public T GetOrGen(ushort x, ushort y, ushort z)
            {
                var indx = (x & 3072) >> 10 | (y & 3072) >> 8 | (z & 3072) >> 6;
                return (layers[indx] ?? (layers[indx] = new Layer4())).GetOrGen(x, y, z);
            }

            public T Get(ushort x, ushort y, ushort z)
            {
                var indx = (x & 3072) >> 10 | (y & 3072) >> 8 | (z & 3072) >> 6;
                return layers[indx]?.Get(x, y, z);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------

        private readonly Layer5 _root = new Layer5();
        private readonly List<T> _temp = new List<T>(512);

        //--------------------------------------------------------------------------------------------------------------------------

        public void Clear()
        {
            for (var i5 = 0; i5 < 64; i5++)
            {
                var layer4 = _root.layers[i5];
                if (layer4 == null) continue;

                for (var i4 = 0; i4 < 64; i4++)
                {
                    var layer3 = layer4.layers[i4];
                    if (layer3 == null) continue;

                    for (var i3 = 0; i3 < 64; i3++)
                    {
                        var layer2 = layer3.layers[i3];
                        if (layer2 == null) continue;

                        for (var i2 = 0; i2 < 64; i2++)
                        {
                            var layer1 = layer2.layers[i2];
                            if (layer1 == null) continue;

                            for (var i0 = 0; i0 < 64; i0++)
                            {
                                var layer0 = layer1.layers[i0];
                                if (layer0 == null) continue;

                                for (var n = 0; n < 64; n++)
                                {
                                    var node = layer0.nodes[n];
                                    if (node == null) continue;

                                    layer0.nodes[n] = null;
                                    _nodesPool.Push(node);
                                    node.Clear();
                                }
                            }
                        }
                    }
                }
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public List<T> Enum()
        {
            _temp.Clear();
            for (var i5 = 0; i5 < 64; i5++)
            {
                var layer4 = _root.layers[i5];
                if (layer4 == null) continue;

                for (var i4 = 0; i4 < 64; i4++)
                {
                    var layer3 = layer4.layers[i4];
                    if (layer3 == null) continue;

                    for (var i3 = 0; i3 < 64; i3++)
                    {
                        var layer2 = layer3.layers[i3];
                        if (layer2 == null) continue;

                        for (var i2 = 0; i2 < 64; i2++)
                        {
                            var layer1 = layer2.layers[i2];
                            if (layer1 == null) continue;

                            for (var i0 = 0; i0 < 64; i0++)
                            {
                                var layer0 = layer1.layers[i0];
                                if (layer0 == null) continue;

                                for (var n = 0; n < 64; n++)
                                {
                                    var node = layer0.nodes[n];
                                    if (node != null) _temp.Add(node);
                                }
                            }
                        }
                    }
                }
            }
            return _temp;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public T Get(Vector3 coord)
        {
            var x = (ushort)(coord.x + 32767.5f);
            var y = (ushort)(coord.y + 32767.5f);
            var z = (ushort)(coord.z + 32767.5f);
            return _root.Get(x, y, z);
        }

        public T GetOrGen(Vector3 coord)
        {
            var x = (ushort)(coord.x + 32767.5f);
            var y = (ushort)(coord.y + 32767.5f);
            var z = (ushort)(coord.z + 32767.5f);
            return _root.GetOrGen(x, y, z);
        }

        public void Gen(Vector3 coord)
        {
            var x = (ushort)(coord.x + 32767.5f);
            var y = (ushort)(coord.y + 32767.5f);
            var z = (ushort)(coord.z + 32767.5f);
            _root.Gen(x, y, z);
        }

        //--------------------------------------------------------------------------------------------------------------------------

        private const float SQRT_2 = 1.4142135623f;

        public void EnumInSphere(Vector3 center, float radius, Action<ushort, ushort, ushort> action)
        {            
            var cxf = center.x + 32767.5f;
            var cyf = center.y + 32767.5f;
            var czf = center.z + 32767.5f;
            var c = new Vector3(cxf, cyf, czf);

            var sqrR = radius + SQRT_2;
            sqrR = sqrR * sqrR;

            var xf = (ushort)(cxf - radius);
            var xt = (ushort)(cxf + radius);
            var yf = (ushort)(cyf - radius);
            var yt = (ushort)(cyf + radius);
            var zf = (ushort)(czf - radius);
            var zt = (ushort)(czf + radius);

            for (var x = xf; x <= xt; x++)
                for (var y = yf; y <= yt; y++)
                    for (var z = zf; z <= zt; z++)
                        if ((c - new Vector3(x, y, z)).sqrMagnitude < sqrR)
                            action(x, y, z);
        }

        public List<T> GetInSphere(Vector3 center, float radius)
        {
            _temp.Clear();
            EnumInSphere(center, radius, (x, y, z) => { var n = _root.Get(x, y, z); if (n != null) _temp.Add(n); });
            return _temp;
        }

        public List<T> GetOrGenInSphere(Vector3 center, float radius)
        {
            _temp.Clear();
            EnumInSphere(center, radius, (x, y, z) => _temp.Add(_root.GetOrGen(x, y, z)));
            return _temp;
        }

        public void GenInSphere(Vector3 center, float radius)
        {
            EnumInSphere(center, radius, (x, y, z) => _root.Gen(x, y, z));
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public void EnumInBox(Vector3 center, Vector3 extends, Action<ushort, ushort, ushort> action)
        {
            var cxf = center.x + 32767.5f;
            var cyf = center.y + 32767.5f;
            var czf = center.z + 32767.5f;

            var xf = (ushort)(cxf - extends.x);
            var xt = (ushort)(cxf + extends.x);
            var yf = (ushort)(cyf - extends.y);
            var yt = (ushort)(cyf + extends.y);
            var zf = (ushort)(czf - extends.z);
            var zt = (ushort)(czf + extends.z);

            for (var x = xf; x <= xt; x++)
                for (var y = yf; y <= yt; y++)
                    for (var z = zf; z <= zt; z++)
                        action(x, y, z);
        }

        public List<T> GetInBox(Vector3 center, Vector3 extends)
        {
            _temp.Clear();
            EnumInBox(center, extends, (x, y, z) => { var n = _root.Get(x, y, z); if (n != null) _temp.Add(n); });
            return _temp;
        }

        public List<T> GetOrGenInBox(Vector3 center, Vector3 extends)
        {
            _temp.Clear();
            EnumInBox(center, extends, (x, y, z) => _temp.Add(_root.GetOrGen(x, y, z)));
            return _temp;
        }

        public void GenInBox(Vector3 center, Vector3 extends)
        {
            EnumInBox(center, extends, (x, y, z) => _root.Gen(x, y, z));
        }

        //--------------------------------------------------------------------------------------------------------------------------
        
        public void EnumInBoxWhere(Vector3 center, Vector3 extends, Action<ushort, ushort, ushort> action, Func<Vector3, bool> condition)
        {
            var cxf = center.x + 32767.5f;
            var cyf = center.y + 32767.5f;
            var czf = center.z + 32767.5f;

            var xf = (ushort)(cxf - extends.x);
            var xt = (ushort)(cxf + extends.x);
            var yf = (ushort)(cyf - extends.y);
            var yt = (ushort)(cyf + extends.y);
            var zf = (ushort)(czf - extends.z);
            var zt = (ushort)(czf + extends.z);

            for (var x = xf; x <= xt; x++)
                for (var y = yf; y <= yt; y++)
                    for (var z = zf; z <= zt; z++)
                        if (condition(SpatialNode.WorldCoordinates(x, y, z))) action(x, y, z);
        }

        public List<T> GetInBox(Vector3 center, Vector3 extends, Func<Vector3, bool> condition)
        {
            _temp.Clear();
            EnumInBoxWhere(center, extends, (x, y, z) => { var n = _root.Get(x, y, z); if (n != null) _temp.Add(n); }, condition);
            return _temp;
        }

        public List<T> GetOrGenInBox(Vector3 center, Vector3 extends, Func<Vector3, bool> condition)
        {
            _temp.Clear();
            EnumInBoxWhere(center, extends, (x, y, z) => _temp.Add(_root.GetOrGen(x, y, z)), condition);
            return _temp;
        }

        public void GenInBox(Vector3 center, Vector3 extends, Func<Vector3, bool> condition)
        {
            EnumInBoxWhere(center, extends, (x, y, z) => _root.Gen(x, y, z), condition);
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
