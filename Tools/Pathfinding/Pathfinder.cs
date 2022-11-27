using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Framework.Tools.Pathfinding
{
    public interface IPathfinder
    {
        IReadOnlyList<IPathNode> FindPathNodes(int startX, int startY, int endX, int endY, bool includeDiagonals);

        IReadOnlyList<IPathNode> FindPathNodes(Vector2Int start, Vector2Int end, bool includeDiagonals);

        IReadOnlyList<Vector2Int> FindPath(int startX, int startY, int endX, int endY, bool includeDiagonals);

        IReadOnlyList<Vector2Int> FindPath(Vector2Int start, Vector2Int end, bool includeDiagonals);

        void SetIsWalkable(int x, int y, bool isWalkable);
    }

    public class Pathfinder : IPathfinder
    {
        private readonly IGrid2D<IPathNode> grid;
        private readonly IList<IPathNode> openNodes;
        private readonly HashSet<IPathNode> closedNodes;

        public Pathfinder(int width, int height)
        {
            grid = new Grid2D<IPathNode>(width, height, (x, y) => new PathNode(x, y));
            openNodes = new List<IPathNode>();
            closedNodes = new HashSet<IPathNode>();
        }

        public Pathfinder(Vector2Int size) : this(size.x, size.y)
        {
        }

        public IReadOnlyList<IPathNode> FindPathNodes(int startX, int startY, int endX, int endY, bool includeDiagonals)
        {
            if (!grid.IsWithinBounds(startX, startY) || !grid.IsWithinBounds(endX, endY)) return Array.Empty<IPathNode>();
            InitializeGrid(grid.Size);
            var startNode = grid[startX, startY];
            var endNode = grid[endX, endY];
            closedNodes.Clear();
            openNodes.Clear();
            openNodes.Add(startNode);
            startNode.GCost = 0;
            startNode.HCost = PathfinderStaticMethods.CalculateDistance(startNode, endNode, includeDiagonals);
            while (openNodes.Count > 0)
            {
                var currentNode = openNodes.Min();
                if (ReferenceEquals(currentNode, endNode)) return PathfinderStaticMethods.CalculatePath(currentNode);
                if (!openNodes.Remove(currentNode)) throw new PathNodeNotFoundException(currentNode);
                if (!closedNodes.Add(currentNode)) throw new PathNodeAlreadyExistsException(currentNode);
                foreach (var neighbourNode in grid.GetNeighbours(currentNode, includeDiagonals))
                {
                    if (closedNodes.Contains(neighbourNode)) continue;
                    if (!neighbourNode.IsWalkable && closedNodes.Add(neighbourNode)) continue;
                    var tentativeGCost = currentNode.GCost + PathfinderStaticMethods.CalculateDistance(currentNode, neighbourNode, includeDiagonals);
                    if (tentativeGCost < neighbourNode.GCost)
                    {
                        neighbourNode.Parent = currentNode;
                        neighbourNode.GCost = tentativeGCost;
                        neighbourNode.HCost = PathfinderStaticMethods.CalculateDistance(neighbourNode, endNode, includeDiagonals);
                        if (!openNodes.Contains(neighbourNode)) openNodes.Add(neighbourNode);
                    }
                }
            }

            return Array.Empty<IPathNode>();
        }

        public IReadOnlyList<IPathNode> FindPathNodes(Vector2Int start, Vector2Int end, bool includeDiagonals)
        {
            return FindPathNodes(start.x, start.y, end.x, end.y, includeDiagonals);
        }

        public IReadOnlyList<Vector2Int> FindPath(int startX, int startY, int endX, int endY, bool includeDiagonals)
        {
            var pathNodes = FindPathNodes(startX, startY, endX, endY, includeDiagonals);
            return pathNodes.Count == 0 ? Array.Empty<Vector2Int>() : pathNodes.Select(node => node.Coordinate).ToArray();
        }

        public IReadOnlyList<Vector2Int> FindPath(Vector2Int start, Vector2Int end, bool includeDiagonals)
        {
            return FindPath(start.x, start.y, end.x, end.y, includeDiagonals);
        }

        public void SetIsWalkable(int x, int y, bool isWalkable)
        {
            grid[x, y].SetIsWalkable(isWalkable);
        }

        private void InitializeGrid(Vector2Int size)
        {
            for (var x = 0; x < size.x; x++)
            for (var y = 0; y < size.y; y++)
            {
                var node = grid[x, y];
                node.GCost = float.PositiveInfinity;
                node.HCost = float.PositiveInfinity;
                node.Parent = null;
            }
        }

        private static class PathfinderStaticMethods
        {
            private const float Sqrt2 = 1.4142135f;

            public static float CalculateDistance(IPathNode currentNode, IPathNode endNode, bool includeDiagonals)
            {
                var xDistance = Mathf.Abs(currentNode.X - endNode.X);
                var yDistance = Mathf.Abs(currentNode.Y - endNode.Y);
                if (!includeDiagonals) return xDistance + yDistance;
                var remaining = Mathf.Abs(xDistance - yDistance);
                return Sqrt2 * Mathf.Min(xDistance, yDistance) + remaining;
            }

            public static IReadOnlyList<IPathNode> CalculatePath(IPathNode endNode)
            {
                var path = new LinkedList<IPathNode>();
                for (var currentNode = endNode; currentNode != null; currentNode = currentNode.Parent) path.AddFirst(currentNode);
                path.RemoveFirst();
                var pathArray = new IPathNode[path.Count];
                path.CopyTo(pathArray, 0);
                return pathArray;
            }
        }
    }
}