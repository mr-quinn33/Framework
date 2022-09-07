using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Framework.Tools.ScriptableObjects.Nested
{
    public interface IParentSO<T> where T : ScriptableObject, IChildSO<T>
    {
        IList<T> Children { get; }

        TChild GetChild<TChild>() where TChild : T;
    }

    public abstract class ParentSO<T> : ScriptableObject, IParentSO<T> where T : ScriptableObject, IChildSO<T>
    {
        public IList<T> Children { get; } = new List<T>();

        public TChild GetChild<TChild>() where TChild : T
        {
            return Children.FirstOrDefault(child => child.GetType() == typeof(TChild)) as TChild;
        }

#if UNITY_EDITOR
        protected void CreateChild<TChild>() where TChild : T
        {
            var child = CreateInstance<TChild>();
            child.name = typeof(TChild).Name;
            child.Initialize(this);
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
                if (child is TChild && Children.Remove(child)) Undo.DestroyObjectImmediate(child);
            }

            AssetDatabase.SaveAssets();
        }

        private void OnValidate()
        {
            var assets = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(this));
            Children.Clear();
            foreach (var asset in assets)
                if (asset is T childSO)
                    Children.Add(childSO);
        }
#endif
    }
}