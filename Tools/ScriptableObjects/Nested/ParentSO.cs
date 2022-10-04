using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if ADDRESSABLES
using UnityEngine.AddressableAssets;
#endif
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Framework.Tools.ScriptableObjects.Nested
{
    public interface IParentSO<T> where T : ScriptableObject, IChildSO<T>
    {
        IList<T> Children { get; }

        TChild GetChild<TChild>() where TChild : T;

#if UNITY_EDITOR
        TChild GetChildEditor<TChild>() where TChild : T;
#endif

        bool Remove(T child, string childAssetAddress);
    }

    public abstract class ParentSO<T> : ScriptableObject, IParentSO<T> where T : ScriptableObject, IChildSO<T>
    {
#if ODIN_INSPECTOR
        [ReadOnly]
#endif
        [SerializeField]
        private List<string> childAssetAddressList;

        public IList<T> Children { get; } = new List<T>();

        public TChild GetChild<TChild>() where TChild : T
        {
            MaskSureChildren();
            return Children.FirstOrDefault(child => child.GetType() == typeof(TChild)) as TChild;
        }

#if UNITY_EDITOR
        public TChild GetChildEditor<TChild>() where TChild : T
        {
            return Children.FirstOrDefault(child => child.GetType() == typeof(TChild)) as TChild;
        }
#endif

        public bool Remove(T child, string childAssetAddress)
        {
            return Children.Remove(child) && childAssetAddressList.Remove(childAssetAddress);
        }

        private void MaskSureChildren()
        {
#if ADDRESSABLES
            if (Children.Count > 0 && Children.All(child => child != null)) return;
            Children.Clear();
            foreach (var address in childAssetAddressList)
            {
                var handle = Addressables.LoadAssetAsync<T>(address);
                var asset = handle.WaitForCompletion();
                if (!Children.Contains(asset)) Children.Add(asset);
                Addressables.Release(handle);
            }
#endif
        }

#if UNITY_EDITOR
        protected void CreateChild<TChild>() where TChild : T
        {
            var assetAddress = AssetDatabase.GetAssetPath(this);
            var child = CreateInstance<TChild>();
            var childName = typeof(TChild).Name;
            var childAssetAddress = assetAddress + $"[{childName}]";
            child.name = childName;
            child.Initialize(this, assetAddress);
            childAssetAddressList.Add(childAssetAddress);
            Children.Add(child);

            AssetDatabase.AddObjectToAsset(child, this);
            AssetDatabase.SaveAssets();

            EditorUtility.SetDirty(child);
            EditorUtility.SetDirty(this);
        }

        protected void DestroyChildren<TChild>() where TChild : T
        {
            var assetAddress = AssetDatabase.GetAssetPath(this);
            for (var i = Children.Count - 1; i >= 0; i--)
            {
                var child = Children[i];
                var childAssetAddress = assetAddress + $"[{child.GetType().Name}]";
                if (child is TChild && Remove(child, childAssetAddress)) Undo.DestroyObjectImmediate(child);
            }

            AssetDatabase.SaveAssets();
        }

#if DEBUG && ODIN_INSPECTOR
        [OnInspectorInit]
        private void LogChildSO()
        {
            foreach (var childSO in Children) Debug.LogFormat("{0}: {1}", nameof(LogChildSO), childSO);
        }
#endif
#endif
    }
}