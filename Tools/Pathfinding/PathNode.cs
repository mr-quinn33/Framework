using System;
using UnityEngine;

namespace Framework.Tools.Pathfinding
{
    public interface IPathNode : IEquatable<IPathNode>, IComparable<IPathNode>
    {
        int X { get; }

        int Y { get; }

        Vector2Int Coordinate { get; }

        bool IsWalkable { get; }

        float GCost { get; set; }

        float HCost { get; set; }

        float FCost { get; }

        IPathNode Parent { get; set; }

        void SetIsWalkable(bool isWalkable);
    }

    public sealed class PathNode : IPathNode
    {
        public PathNode(int x, int y, bool isWalkable = true)
        {
            X = x;
            Y = y;
            IsWalkable = isWalkable;
        }

        public int X { get; }

        public int Y { get; }

        public Vector2Int Coordinate => new(X, Y);

        public bool IsWalkable { get; private set; }

        public float GCost { get; set; }

        public float HCost { get; set; }

        public float FCost => GCost + HCost;

        public IPathNode Parent { get; set; }

        public void SetIsWalkable(bool isWalkable)
        {
            IsWalkable = isWalkable;
        }

        public bool Equals(IPathNode other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;
            return X == other.X && Y == other.Y;
        }

        public int CompareTo(IPathNode other)
        {
            if (ReferenceEquals(null, other)) return 1;
            if (ReferenceEquals(this, other)) return 0;
            return GetType() != other.GetType() ? 1 : FCost.CompareTo(other.FCost);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            return Equals((IPathNode) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}