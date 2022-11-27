#if UNITY_MATHEMATICS
using Unity.Mathematics;
#endif

namespace Framework.Tools.Pathfinding.DOTS
{
    public struct PathNode
    {
        public bool isWalkable;

        public int index;
        public int previousIndex;
        public int x;
        public int y;

        public float gCost;
        public float hCost;

        public float FCost => gCost + hCost;

#if UNITY_MATHEMATICS
        public int2 Position => new(x, y);
#endif
    }
}