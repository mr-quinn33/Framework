using System.Collections.Generic;
using UnityEngine;

namespace Framework.Tools.Pathfinding
{
    public interface IPathfinder
    {
        IReadOnlyList<IPathNode> FindPath(int startX, int startY, int endX, int endY);

        IReadOnlyList<IPathNode> FindPath(Vector2Int start, Vector2Int end);
    }

    public class Pathfinder : IPathfinder
    {
        private readonly IGrid2D<IPathNode> grid;
        private HashSet<IPathNode> closedNodes;
        private IList<IPathNode> openNodes;
        private SortedSet<IPathNode> sortedSet;

        public Pathfinder(int width, int height)
        {
            grid = new Grid2D<IPathNode>(width, height, (x, y) => new PathNode(x, y));
        }

        public Pathfinder(Vector2Int size) : this(size.x, size.y)
        {
        }

        public IReadOnlyList<IPathNode> FindPath(int startX, int startY, int endX, int endY)
        {
            var startNode = grid[startX, startY];
            openNodes = new List<IPathNode> {startNode};
            closedNodes = new HashSet<IPathNode>();
            sortedSet = new SortedSet<IPathNode>();
            InitializeGridNodes(grid.Size);
            startNode.GCost = 0;
            startNode.HCost = CalculateDistance(startNode, grid[endX, endY]);
            while (openNodes.Count > 0)
            {
                var currentNode = sortedSet.Min;
                if (ReferenceEquals(currentNode, grid[endX, endY])) return CalculatePath(currentNode);
                if (!openNodes.Remove(currentNode)) throw new PathNodeNotFoundException(currentNode);
                if (!sortedSet.Remove(currentNode)) throw new PathNodeNotFoundException(currentNode);
                closedNodes.Add(currentNode);
                foreach (var neighbourNode in grid.GetNeighbours(currentNode, true))
                {
                    if (closedNodes.Contains(neighbourNode)) continue;
                    if (!neighbourNode.IsWalkable)
                    {
                        closedNodes.Add(neighbourNode);
                        continue;
                    }

                    var tentativeGCost = currentNode.GCost + CalculateDistance(currentNode, neighbourNode);
                    if (tentativeGCost < neighbourNode.GCost)
                    {
                        neighbourNode.Parent = currentNode;
                        neighbourNode.GCost = tentativeGCost;
                        neighbourNode.HCost = CalculateDistance(neighbourNode, grid[endX, endY]);
                        if (sortedSet.Add(neighbourNode)) openNodes.Add(neighbourNode);
                    }
                }
            }

            return null;
        }

        public IReadOnlyList<IPathNode> FindPath(Vector2Int start, Vector2Int end)
        {
            return FindPath(start.x, start.y, end.x, end.y);
        }

        private void InitializeGridNodes(Vector2Int size)
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

        private static float CalculateDistance(IPathNode currentNode, IPathNode endNode)
        {
            var xDistance = Mathf.Abs(currentNode.X - endNode.X);
            var yDistance = Mathf.Abs(currentNode.Y - endNode.Y);
            var remaining = Mathf.Abs(xDistance - yDistance);
            return Mathf.Sqrt(2) * Mathf.Min(xDistance, yDistance) + remaining;
        }

        private static IReadOnlyList<IPathNode> CalculatePath(IPathNode endNode)
        {
            var path = new LinkedList<IPathNode>();
            var currentNode = endNode;
            while (currentNode != null)
            {
                path.AddFirst(currentNode);
                currentNode = currentNode.Parent;
            }

            var pathArray = new IPathNode[path.Count];
            path.CopyTo(pathArray, 0);
            return pathArray;
        }
    }
}