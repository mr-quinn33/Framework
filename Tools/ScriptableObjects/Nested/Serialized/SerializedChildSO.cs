#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Framework.Tools.ScriptableObjects.Nested.Serialized
{
    [ShowOdinSerializedPropertiesInInspector]
    public abstract class SerializedChildSO : ChildSO, ISerializationCallbackReceiver
    {
        [SerializeField] [HideInInspector] private SerializationData serializationData;

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            UnitySerializationUtility.DeserializeUnityObject(this, ref serializationData);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            UnitySerializationUtility.SerializeUnityObject(this, ref serializationData);
        }
    }
}
#endif