using System;
using System.Collections.Generic;
using UnityEngine;

namespace Smart.Types
{
    public class SpatialNodes<T>
        where T: class
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public SpatialNodes(Func<Vector3, T> onGetNode, float nodeSize, Func<Vector3, Vector3> roundCoords)
        {
            _onGetNode = onGetNode;
            _nodeSize = nodeSize;
            _halfNodeSize = nodeSize * 0.5f;
            _roundCoords = roundCoords;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        private readonly float _nodeSize;
        private readonly float _halfNodeSize;
        private readonly Func<Vector3, Vector3> _roundCoords;
        private readonly Func<Vector3, T> _onGetNode;
        private readonly Dictionary<Vector3, T> _nodes = new Dictionary<Vector3, T>();

        //--------------------------------------------------------------------------------------------------------------------------

        public void Clear()
        {
            _nodes.Clear();
        }

        public IEnumerable<T> Enum(Func<Vector3, bool> onFilter)
        {
            foreach (var p in _nodes)
                if (onFilter(p.Key))
                    yield return p.Value;
        }

        public IEnumerable<KeyValuePair<Vector3, T>> Nodes => _nodes;

        //--------------------------------------------------------------------------------------------------------------------------
        
        public T Get(Vector3 coord)
        {
            T node;
            return _nodes.TryGetValue(_roundCoords(coord), out node) ? node : null;
        }

        public IEnumerable<T> GetInSphere(Vector3 center, float radius)
        {
            for (var x = center.x - radius; x < center.x + radius + _halfNodeSize; x += _nodeSize)
                for (var y = center.y - radius; y < center.y + radius + _halfNodeSize; y += _nodeSize)
                    for (var z = center.z - radius; z < center.z + radius + _halfNodeSize; z += _nodeSize)
                    {
                        var pt = new Vector3(x, y, z);
                        if (Vector3.Distance(pt, center) <= radius)
                        {
                            var node = Get(pt);
                            if (node != null) yield return node;
                        }
                    }
        }

        public IEnumerable<T> GetInBox(Vector3 center, Vector3 extends)
        {
            for (var x = center.x - extends.x; x < center.x + extends.x + _halfNodeSize; x += _nodeSize)
                for (var y = center.y - extends.y; y < center.y + extends.y + _halfNodeSize; y += _nodeSize)
                    for (var z = center.z - extends.z; z < center.z + extends.z + _halfNodeSize; z += _nodeSize)
                    {
                        var node = Get(new Vector3(x, y, z));
                        if (node != null) yield return node;
                    }
        }

        //--------------------------------------------------------------------------------------------------------------------------        

        public void Gen(Vector3 coord)
        {
            var nodeCenter = _roundCoords(coord);
            if (_nodes.ContainsKey(nodeCenter)) return;
            _nodes.Add(nodeCenter, _onGetNode(coord));
        }

        public void GenInSphere(Vector3 center, float radius)
        {
            for (var x = center.x - radius; x < center.x + radius + _halfNodeSize; x += _nodeSize)
                for (var y = center.y - radius; y < center.y + radius + _halfNodeSize; y += _nodeSize)
                    for (var z = center.z - radius; z < center.z + radius + _halfNodeSize; z += _nodeSize)
                    {
                        var pt = new Vector3(x, y, z);
                        if (Vector3.Distance(pt, center) <= radius) Gen(pt);
                    }
        }

        public void GenInBox(Vector3 center, Vector3 extends)
        {
            for (var x = center.x - extends.x; x < center.x + extends.x + _halfNodeSize; x += _nodeSize)
                for (var y = center.y - extends.y; y < center.y + extends.y + _halfNodeSize; y += _nodeSize)
                    for (var z = center.z - extends.z; z < center.z + extends.z + _halfNodeSize; z += _nodeSize)
                        Gen(new Vector3(x, y, z));
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public T GetOrGen(Vector3 coord)
        {
            T node;
            var nodeCenter = _roundCoords(coord);
            if (!_nodes.TryGetValue(nodeCenter, out node))
                _nodes.Add(nodeCenter, node = _onGetNode(coord));
            return node;
        }

        public IEnumerable<T> GetOrGenInSphere(Vector3 center, float radius)
        {
            for (var x = center.x - radius; x < center.x + radius + _halfNodeSize; x += _nodeSize)
                for (var y = center.y - radius; y < center.y + radius + _halfNodeSize; y += _nodeSize)
                    for (var z = center.z - radius; z < center.z + radius + _halfNodeSize; z += _nodeSize)
                    {
                        var pt = new Vector3(x, y, z);
                        if (Vector3.Distance(pt, center) <= radius) yield return GetOrGen(pt);
                    }
        }

        public IEnumerable<T> GetOrGenInBox(Vector3 center, Vector3 extends)
        {
            for (var x = center.x - extends.x; x < center.x + extends.x + _halfNodeSize; x += _nodeSize)
                for (var y = center.y - extends.y; y < center.y + extends.y + _halfNodeSize; y += _nodeSize)
                    for (var z = center.z - extends.z; z < center.z + extends.z + _halfNodeSize; z += _nodeSize)
                        yield return GetOrGen(new Vector3(x, y, z));
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
