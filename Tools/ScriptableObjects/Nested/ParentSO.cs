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

        public bool Remove(T child, string childAssetAddress)
        {
            return Children.Remove(child) && childAssetAddressList.Remove(childAssetAddress);
        }

        private void MaskSureChildren()
        {
            if (Children.Count > 0 && Children.All(child => child != null)) return;
#if ADDRESSABLES
            Children.Clear();
            foreach (var childAssetAddress in childAssetAddressList) Children.Add(Addressables.LoadAssetAsync<T>(childAssetAddress).WaitForCompletion());
#endif
        }

#if UNITY_EDITOR
        protected void CreateChild<TChild>() where TChild : T
        {
            var assetAddress = AssetDatabase.GetAssetPath(this);
            var child = CreateInstance<TChild>();
            child.name = typeof(TChild).Name;
            child.Initialize(this, assetAddress);
            Children.Add(child);

            AssetDatabase.AddObjectToAsset(child, this);
            AssetDatabase.SaveAssets();

            var childAssetAddress = assetAddress + $"[{child.name}]";
            childAssetAddressList.Add(childAssetAddress);

            EditorUtility.SetDirty(child);
            EditorUtility.SetDirty(this);
        }

        protected void DestroyChildren<TChild>() where TChild : T
        {
            var assetAddress = AssetDatabase.GetAssetPath(this);
            for (var i = Children.Count - 1; i >= 0; i--)
            {
                var child = Children[i];
                var childAssetAddress = assetAddress + $"[{child.name}]";
                if (child is TChild && Remove(child, childAssetAddress)) Undo.DestroyObjectImmediate(child);
            }

            AssetDatabase.SaveAssets();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            MaskSureChildren();
        }
#endif

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