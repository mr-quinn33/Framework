using System;
using System.Collections.Generic;
#if UNITY_BURST
using Unity.Burst;
#endif
using Unity.Jobs;
#if UNITY_COLLECTIONS
using Unity.Collections;
#endif
#if UNITY_MATHEMATICS
using Unity.Mathematics;
#endif
using UnityEngine;

namespace Framework.Tools.Pathfinding.DOTS
{
    public interface IPathfinder : Framework.Tools.Pathfinding.IPathfinder
    {
#if UNITY_COLLECTIONS && UNITY_MATHEMATICS
        NativeList<int2> FindPath(ref NativeArray<PathNode> pathNodes, int2 size, int2 start, int2 end, bool includeDiagonals);
#endif
    }

    public sealed class Pathfinder : IPathfinder
    {
        private readonly PathNode[] pathNodeArray;
        private readonly int width;
        private readonly int height;

        public Pathfinder(int width, int height)
        {
            this.width = width;
            this.height = height;
            var size = width * height;
            Span<PathNode> pathNodeSpan = stackalloc PathNode[size];
            for (var i = 0; i < size; i++) pathNodeSpan[i] = new PathNode {isWalkable = true};
            pathNodeArray = pathNodeSpan.ToArray();
        }

        public Pathfinder(Vector2Int size) : this(size.x, size.y)
        {
        }

#if UNITY_COLLECTIONS && UNITY_MATHEMATICS
        public NativeList<int2> FindPath(ref NativeArray<PathNode> pathNodes, int2 size, int2 start, int2 end, bool includeDiagonals)
        {
            if (!PathfinderStaticMethods.IsPositionInsideGrid(start, size) || !PathfinderStaticMethods.IsPositionInsideGrid(end, size)) return new NativeList<int2>(Allocator.Temp);
            PathfinderStaticMethods.InitializeNodes(ref pathNodes, size);
            ReadOnlySpan<int2> neighborOffsetArrayExcludeDiagonals = stackalloc int2[]
            {
                new int2(1, 0),
                new int2(0, 1),
                new int2(-1, 0),
                new int2(0, -1)
            };
            ReadOnlySpan<int2> neighborOffsetArrayIncludeDiagonals = stackalloc int2[]
            {
                new int2(1, 1),
                new int2(-1, 1),
                new int2(-1, -1),
                new int2(1, -1),
                new int2(1, 0),
                new int2(0, 1),
                new int2(-1, 0),
                new int2(0, -1)
            };
            var openList = new NativeList<int>(Allocator.Temp);
            var closedList = new NativeList<int>(Allocator.Temp);
            var startIndex = PathfinderStaticMethods.GetIndex(start, size);
            var endIndex = PathfinderStaticMethods.GetIndex(end, size);
            var startNode = pathNodes[startIndex];
            startNode.gCost = 0;
            startNode.hCost = PathfinderStaticMethods.GetDistance(start, end, includeDiagonals);
            pathNodes[startIndex] = startNode;
            openList.Add(startIndex);
            while (openList.Length > 0)
            {
                var currentNodeIndex = PathfinderStaticMethods.GetMinFCostPathNodeIndex(ref pathNodes, ref openList);
                var currentNode = pathNodes[currentNodeIndex];
                if (currentNodeIndex == endIndex) break;
                for (var i = 0; i < openList.Length; i++)
                {
                    if (openList[i] == currentNodeIndex)
                    {
                        openList.RemoveAtSwapBack(i);
                        break;
                    }
                }

                closedList.Add(currentNodeIndex);
                foreach (var neighborOffset in includeDiagonals ? neighborOffsetArrayIncludeDiagonals : neighborOffsetArrayExcludeDiagonals)
                {
                    var neighbor = new int2(currentNode.Position + neighborOffset);
                    if (!PathfinderStaticMethods.IsPositionInsideGrid(neighbor, size)) continue;
                    var neighborIndex = PathfinderStaticMethods.GetIndex(neighbor, size);
                    if (closedList.Contains(neighborIndex)) continue;
                    var neighborNode = pathNodes[neighborIndex];
                    if (!neighborNode.isWalkable) continue;
                    var tentativeGCost = currentNode.gCost + PathfinderStaticMethods.GetDistance(currentNode.Position, neighbor, includeDiagonals);
                    if (tentativeGCost < neighborNode.gCost)
                    {
                        neighborNode.previousIndex = currentNodeIndex;
                        neighborNode.gCost = tentativeGCost;
                        neighborNode.hCost = PathfinderStaticMethods.GetDistance(neighbor, end, includeDiagonals);
                        pathNodes[neighborIndex] = neighborNode;
                        if (!openList.Contains(neighborIndex)) openList.Add(neighborIndex);
                    }
                }
            }

            openList.Dispose();
            closedList.Dispose();
            var endNode = pathNodes[endIndex];
            var path = PathfinderStaticMethods.CalculatePath(ref pathNodes, endNode, Allocator.Temp);
            return path;
        }
#endif

        public IReadOnlyList<Vector2Int> FindPath(int startX, int startY, int endX, int endY, bool includeDiagonals)
        {
#if UNITY_MATHEMATICS && UNITY_COLLECTIONS
            var pathNodes = new NativeArray<PathNode>(pathNodeArray, Allocator.Temp);
            var path = FindPath(ref pathNodes, new int2(width, height), new int2(startX, startY), new int2(endX, endY), includeDiagonals);
            var result = new Vector2Int[path.Length];
            for (var i = 0; i < path.Length; i++) result[i] = new Vector2Int(path[i].x, path[i].y);
            pathNodes.Dispose();
            path.Dispose();
            return result;
#else
            return Array.Empty<Vector2Int>();
#endif
        }

        public IReadOnlyList<Vector2Int> FindPath(Vector2Int start, Vector2Int end, bool includeDiagonals)
        {
            return FindPath(start.x, start.y, end.x, end.y, includeDiagonals);
        }

        public void SetIsWalkable(int x, int y, bool isWalkable)
        {
            var index = PathfinderStaticMethods.GetIndex(x, y, height);
            var pathNode = pathNodeArray[index];
            pathNode.isWalkable = isWalkable;
            pathNodeArray[index] = pathNode;
        }

        public void SetIsWalkable(Vector2Int position, bool isWalkable)
        {
            SetIsWalkable(position.x, position.y, isWalkable);
        }

#if UNITY_BURST
        [BurstCompile]
#endif
        private struct FindPathJob : IJob
        {
#if UNITY_MATHEMATICS && UNITY_COLLECTIONS
            public bool includeDiagonals;
            public int2 startPosition;
            public int2 endPosition;
            public int2 gridSize;
            public NativeArray<PathNode> nodes;
            public NativeArray<int2> path;
#endif

            public void Execute()
            {
#if UNITY_MATHEMATICS && UNITY_COLLECTIONS
                if (!PathfinderStaticMethods.IsPositionInsideGrid(startPosition, gridSize) || !PathfinderStaticMethods.IsPositionInsideGrid(endPosition, gridSize))
                {
                    path = new NativeArray<int2>(0, Allocator.Temp);
                    return;
                }

                PathfinderStaticMethods.InitializeNodes(ref nodes, gridSize);
                ReadOnlySpan<int2> neighborOffsetArrayExcludeDiagonals = stackalloc int2[]
                {
                    new int2(1, 0),
                    new int2(0, 1),
                    new int2(-1, 0),
                    new int2(0, -1)
                };
                ReadOnlySpan<int2> neighborOffsetArrayIncludeDiagonals = stackalloc int2[]
                {
                    new int2(1, 1),
                    new int2(-1, 1),
                    new int2(-1, -1),
                    new int2(1, -1),
                    new int2(1, 0),
                    new int2(0, 1),
                    new int2(-1, 0),
                    new int2(0, -1)
                };
                var openList = new NativeList<int>(Allocator.Temp);
                var closedList = new NativeList<int>(Allocator.Temp);
                var startIndex = PathfinderStaticMethods.GetIndex(startPosition, gridSize);
                var endIndex = PathfinderStaticMethods.GetIndex(endPosition, gridSize);
                var startNode = nodes[startIndex];
                startNode.gCost = 0;
                startNode.hCost = PathfinderStaticMethods.GetDistance(startPosition, endPosition, includeDiagonals);
                nodes[startIndex] = startNode;
                openList.Add(startIndex);
                while (openList.Length > 0)
                {
                    var currentNodeIndex = PathfinderStaticMethods.GetMinFCostPathNodeIndex(ref nodes, ref openList);
                    var currentNode = nodes[currentNodeIndex];
                    if (currentNodeIndex == endIndex) break;
                    for (var i = 0; i < openList.Length; i++)
                    {
                        if (openList[i] == currentNodeIndex)
                        {
                            openList.RemoveAtSwapBack(i);
                            break;
                        }
                    }

                    closedList.Add(currentNodeIndex);
                    foreach (var neighborOffset in includeDiagonals ? neighborOffsetArrayIncludeDiagonals : neighborOffsetArrayExcludeDiagonals)
                    {
                        var neighbor = new int2(currentNode.Position + neighborOffset);
                        if (!PathfinderStaticMethods.IsPositionInsideGrid(neighbor, gridSize)) continue;
                        var neighborIndex = PathfinderStaticMethods.GetIndex(neighbor, gridSize);
                        if (closedList.Contains(neighborIndex)) continue;
                        var neighborNode = nodes[neighborIndex];
                        if (!neighborNode.isWalkable) continue;
                        var tentativeGCost = currentNode.gCost + PathfinderStaticMethods.GetDistance(currentNode.Position, neighbor, includeDiagonals);
                        if (tentativeGCost < neighborNode.gCost)
                        {
                            neighborNode.previousIndex = currentNodeIndex;
                            neighborNode.gCost = tentativeGCost;
                            neighborNode.hCost = PathfinderStaticMethods.GetDistance(neighbor, endPosition, includeDiagonals);
                            nodes[neighborIndex] = neighborNode;
                            if (!openList.Contains(neighborIndex)) openList.Add(neighborIndex);
                        }
                    }
                }

                openList.Dispose();
                closedList.Dispose();
                var endNode = nodes[endIndex];
                path = PathfinderStaticMethods.CalculatePath(ref nodes, endNode, Allocator.TempJob);
#endif
            }
        }

        private static class PathfinderStaticMethods
        {
            private const float Sqrt2 = 1.4142135f;

            public static int GetIndex(int x, int y, int height)
            {
                return x * height + y;
            }

#if UNITY_MATHEMATICS
            public static int GetIndex(int2 position, int2 size)
            {
                return GetIndex(position.x, position.y, size.y);
            }

            public static float GetDistance(int2 start, int2 end, bool includeDiagonals)
            {
                return GetDistance(start.x, start.y, end.x, end.y, includeDiagonals);
            }

#endif

#if UNITY_COLLECTIONS
            public static int GetMinFCostPathNodeIndex(ref NativeArray<PathNode> nodes, ref NativeList<int> openList)
            {
                var minFCost = float.PositiveInfinity;
                var minFCostIndex = -1;
                foreach (var index in openList)
                {
                    var node = nodes[index];
                    var fCost = node.FCost;
                    if (fCost < minFCost)
                    {
                        minFCost = fCost;
                        minFCostIndex = index;
                    }
                }

                return minFCostIndex;
            }
#endif

#if UNITY_MATHEMATICS
            public static bool IsPositionInsideGrid(int2 position, int2 size)
            {
                return -1 < position.x && position.x < size.x && -1 < position.y && position.y < size.y;
            }
#endif

#if UNITY_COLLECTIONS && UNITY_MATHEMATICS
            public static NativeList<int2> CalculatePath(ref NativeArray<PathNode> pathNodes, PathNode endNode, Allocator allocator)
            {
                var path = new NativeList<int2>(allocator);
                if (endNode.previousIndex == -1) return path;
                var reversePath = new NativeList<int2>(allocator);
                reversePath.Add(endNode.Position);
                var currentNode = endNode;
                while (currentNode.previousIndex != -1)
                {
                    currentNode = pathNodes[currentNode.previousIndex];
                    reversePath.Add(currentNode.Position);
                }

                for (var i = reversePath.Length - 2; i > -1; i--) path.Add(reversePath[i]);
                reversePath.Dispose();
                return path;
            }

            public static void InitializeNodes(ref NativeArray<PathNode> nodes, int2 size)
            {
                for (var x = 0; x < size.x; x++)
                for (var y = 0; y < size.y; y++)
                {
                    var index = GetIndex(x, y, size.y);
                    var pathNode = new PathNode
                    {
                        x = x,
                        y = y,
                        index = index,
                        previousIndex = -1,
                        gCost = float.PositiveInfinity,
                        hCost = float.PositiveInfinity,
                        isWalkable = nodes[index].isWalkable
                    };
                    nodes[index] = pathNode;
                }
            }
#endif

#if UNITY_MATHEMATICS
            private static float GetDistance(int startX, int startY, int endX, int endY, bool includeDiagonals)
            {
                var xDistance = math.abs(startX - endX);
                var yDistance = math.abs(startY - endY);
                if (!includeDiagonals) return xDistance + yDistance;
                var remaining = math.abs(xDistance - yDistance);
                return Sqrt2 * math.min(xDistance, yDistance) + remaining;
            }
#endif
        }
    }
}