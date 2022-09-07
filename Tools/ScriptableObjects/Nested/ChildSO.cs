using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Framework.Tools.ScriptableObjects.Nested
{
    public interface IChildSO<T> where T : ScriptableObject, IChildSO<T>
    {
        void Initialize(IParentSO<T> parentSO);
    }

    public abstract class ChildSO : ScriptableObject, IChildSO<ChildSO>
    {
        private IParentSO<ChildSO> parentSO;

        void IChildSO<ChildSO>.Initialize(IParentSO<ChildSO> parentSO)
        {
            this.parentSO = parentSO;
        }

        protected TParent GetParent<TParent>() where TParent : class, IParentSO<ChildSO>
        {
            return parentSO as TParent;
        }

        protected TChild GetChild<TChild>() where TChild : ChildSO
        {
            return parentSO.GetChild<TChild>();
        }

        protected IEnumerable<ChildSO> GetChildren()
        {
            return parentSO == null ? Enumerable.Empty<ChildSO>() : parentSO.Children;
        }

#if UNITY_EDITOR
        [ContextMenu(nameof(Destroy))]
        private void Destroy()
        {
            if (parentSO.Children.Remove(this))
            {
                Undo.DestroyObjectImmediate(this);
                AssetDatabase.SaveAssets();
            }
        }
#endif
    }
}