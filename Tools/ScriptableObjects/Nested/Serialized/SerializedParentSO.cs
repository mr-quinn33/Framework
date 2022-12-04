#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
using Sirenix.Serialization;
#endif
using UnityEngine;

namespace Framework.Tools.ScriptableObjects.Nested.Serialized
{
#if ODIN_INSPECTOR
    [ShowOdinSerializedPropertiesInInspector]
    public abstract class SerializedParentSO : ParentSO<ChildSO>, ISerializationCallbackReceiver
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
#endif
}