using System;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace Smart.Types
{
    [Serializable] public class UnityEventColor : UnityEvent<Color> { }

    [Serializable] public class UnityEventString : UnityEvent<string> { }

    [Serializable] public class UnityEventFloat : UnityEvent<float> { }

    [Serializable] public class UnityEventInt : UnityEvent<int> { }

    [Serializable] public class UnityEventBoolean : UnityEvent<bool> { }

    [Serializable] public class UnityEventSprite : UnityEvent<Sprite> { }

    [Serializable] public class UnityEventAudioClip : UnityEvent<AudioClip> { }

    [Serializable] public class UnityEventObject : UnityEvent<Object> { }

    [Serializable] public class UnityEventGameObject : UnityEvent<GameObject> { }

    [Serializable] public class UnityEventVector2 : UnityEvent<Vector2> { }
    
    [Serializable] public class UnityEventVector3 : UnityEvent<Vector3> { }
    
    [Serializable] public class UnityEventTexture : UnityEvent<Texture2D> { }

    [Serializable] public class UnityEventMaterial : UnityEvent<Material> { }
}