using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_ADDRESSABLES
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
    public interface IParentSO
    {
#if UNITY_ADDRESSABLES
        IEnumerator MaskSureChildrenAsync(Addressables.MergeMode mergeMode = Addressables.MergeMode.Union);
#endif
    }

    public interface IParentSO<T> : IParentSO where T : ScriptableObject, IChildSO<T>
    {
        IList<T> Children { get; }

        bool Remove(T child, string childAssetAddress);

#if UNITY_EDITOR
        TChild GetChildEditor<TChild>() where TChild : T;
#endif
    }

    public abstract class ParentSO<T> : ScriptableObject, IParentSO<T> where T : ScriptableObject, IChildSO<T>
    {
#if ODIN_INSPECTOR
        [ReadOnly]
#endif
        [SerializeField]
        private List<string> childAssetAddressList;

        private bool IsChildrenValid => Children.Count == childAssetAddressList.Count && Children.All(child => child != null);

        public IList<T> Children { get; } = new List<T>();

        public bool Remove(T child, string childAssetAddress)
        {
            return Children.Remove(child) && childAssetAddressList.Remove(childAssetAddress);
        }

#if UNITY_EDITOR
        public TChild GetChildEditor<TChild>() where TChild : T
        {
            MaskSureChildrenEditor();
            return Children.FirstOrDefault(child => child is TChild) as TChild;
        }
#endif

#if UNITY_ADDRESSABLES
        public IEnumerator MaskSureChildrenAsync(Addressables.MergeMode mergeMode = Addressables.MergeMode.Union)
        {
            if (childAssetAddressList.Count == 0 || IsChildrenValid) yield break;
            Children.Clear();
            var handle = Addressables.LoadAssetsAsync<T>(childAssetAddressList, t =>
            {
                if (Children.Contains(t)) return;
                Children.Add(t);
            }, mergeMode);
            yield return handle;
            foreach (var child in Children) yield return child.MakeSureParentAsync();
            Addressables.Release(handle);
        }
#endif

        protected TChild GetChild<TChild>() where TChild : T
        {
            return Children.FirstOrDefault(child => child is TChild) as TChild;
        }

        private void MaskSureChildrenEditor()
        {
#if UNITY_EDITOR && UNITY_ADDRESSABLES
            if (childAssetAddressList == null || childAssetAddressList.Count == 0 || IsChildrenValid) return;
            Children.Clear();
            foreach (var handle in childAssetAddressList.Select(Addressables.LoadAssetAsync<T>))
            {
                Children.Add(handle.WaitForCompletion());
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
            var childAssetAddress = $"{assetAddress}[{childName}]";
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
            for (var i = Children.Count - 1; i >= 0; i--)
            {
                var child = Children[i];
                var childAssetAddress = childAssetAddressList[i];
                if (child is TChild && Remove(child, childAssetAddress)) Undo.DestroyObjectImmediate(child);
            }

            AssetDatabase.SaveAssets();
        }

#if DEBUG && ODIN_INSPECTOR
        [OnInspectorInit]
        private void LogChildSO()
        {
            MaskSureChildrenEditor();
            foreach (var childSO in Children) Debug.LogFormat("{0}: {1}", nameof(LogChildSO), childSO);
        }
#endif
#endif
    }
}