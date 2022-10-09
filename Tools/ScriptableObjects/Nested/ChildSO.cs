using System.Collections;
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
    public interface IChildSO
    {
#if ADDRESSABLES
        IEnumerator MakeSureParentAsync();
#endif
    }

    public interface IChildSO<T> : IChildSO where T : ScriptableObject, IChildSO<T>
    {
        void Initialize(IParentSO<T> parentSO, string parentAssetAddress);
    }

    public abstract class ChildSO : ScriptableObject, IChildSO<ChildSO>
    {
#if ODIN_INSPECTOR
        [ReadOnly]
        [FoldoutGroup("Parent Asset Address", Expanded = false, Order = 0)]
#endif
        [SerializeField]
        private string parentAssetAddress;

        private IParentSO<ChildSO> parentSO;

        private IParentSO<ChildSO> ParentSO
        {
            get
            {
                if (parentSO != null) return parentSO;
#if ADDRESSABLES
                parentSO = Addressables.LoadAssetAsync<IParentSO<ChildSO>>(parentAssetAddress).WaitForCompletion();
#endif
                return parentSO;
            }
        }

        void IChildSO<ChildSO>.Initialize(IParentSO<ChildSO> parentSO, string parentAssetAddress)
        {
            this.parentSO = parentSO;
            this.parentAssetAddress = parentAssetAddress;
        }

#if ADDRESSABLES
        public IEnumerator MakeSureParentAsync()
        {
            if (parentSO != null) yield break;
            var handle = Addressables.LoadAssetAsync<IParentSO<ChildSO>>(parentAssetAddress);
            yield return handle;
            parentSO = handle.Result;
            Addressables.Release(handle);
        }
#endif

        protected TParent GetParent<TParent>() where TParent : class, IParentSO<ChildSO>
        {
            return ParentSO as TParent;
        }

#if UNITY_EDITOR
        protected TParent GetParentEditor<TParent>() where TParent : class, IParentSO<ChildSO>
        {
            return parentSO as TParent;
        }

        protected TChild GetChildEditor<TChild>() where TChild : ChildSO
        {
            return parentSO?.GetChildEditor<TChild>();
        }

        protected IEnumerable<ChildSO> GetChildrenEditor()
        {
            return parentSO == null ? Enumerable.Empty<ChildSO>() : parentSO.Children;
        }
#endif

#if UNITY_EDITOR
        [ContextMenu(nameof(Destroy))]
        private void Destroy()
        {
            var assetAddress = parentAssetAddress + $"[{GetType().Name}]";
            if (ParentSO.Remove(this, assetAddress))
            {
                Undo.DestroyObjectImmediate(this);
                AssetDatabase.SaveAssets();
            }
        }

#if DEBUG && ODIN_INSPECTOR
        [OnInspectorInit]
        private void LogParentSO()
        {
            Debug.LogFormat("{0}: {1}", nameof(LogParentSO), ParentSO);
        }
#endif
#endif
    }
}