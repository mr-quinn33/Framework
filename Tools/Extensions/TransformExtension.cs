using System.Collections.Generic;
using UnityEngine;

namespace Framework.Tools.Extensions
{
    public static class TransformExtension
    {
        public static Transform FindInChildren(this Transform root, string name)
        {
            var queue = new Queue<Transform>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                var transform = queue.Dequeue();
                if (transform.name == name) return transform;
                foreach (Transform child in transform) queue.Enqueue(child);
            }

            return null;
        }
    }
}