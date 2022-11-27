using System;
#if UNITY_BURST
using Unity.Burst;
#endif
using Unity.Jobs;
#if UNITY_COLLECTIONS
using Unity.Collections;
#endif
#if UNITY_MATHEMATICS
using Unity.Mathematics;
using UnityEngine;
#endif

namespace Framework.Tools.Pathfinding.DOTS
{
    public interface IPathfinder
    {
#if UNITY_COLLECTIONS && UNITY_MATHEMATICS
        void FindPath(ref NativeArray<PathNode> nodes, int2 size, int2 start, int2 end);
#endif
    }

    public class Pathfinder : IPathfinder
    {
#if UNITY_COLLECTIONS && UNITY_MATHEMATICS
        public void FindPath(ref NativeArray<PathNode> nodes, int2 size, int2 start, int2 end)
        {
            PathfinderStaticMethods.InitializeNodes(ref nodes, size);
            ReadOnlySpan<int2> neighborOffsetArray = stackalloc int2[]
            {
                new int2(0, 1),
                new int2(0, -1),
                new int2(-1, 0),
                new int2(1, 0),
                new int2(1, 1),
                new int2(-1, 1),
                new int2(-1, -1),
                new int2(1, -1)
            };
            var openList = new NativeList<int>(Allocator.Temp);
            var closedList = new NativeList<int>(Allocator.Temp);
            var startIndex = PathfinderStaticMethods.GetIndex(start, size);
            var endIndex = PathfinderStaticMethods.GetIndex(end, size);
            var startNode = nodes[startIndex];
            startNode.gCost = 0;
            startNode.hCost = PathfinderStaticMethods.GetDistance(start, end);
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
                foreach (var neighborOffset in neighborOffsetArray)
                {
                    var neighbor = new int2(currentNode.Position + neighborOffset);
                    if (!PathfinderStaticMethods.IsPositionInsideGrid(neighbor, size)) continue;
                    var neighborIndex = PathfinderStaticMethods.GetIndex(neighbor, size);
                    if (closedList.Contains(neighborIndex)) continue;
                    var neighborNode = nodes[neighborIndex];
                    if (!neighborNode.isWalkable) continue;
                    var tentativeGCost = currentNode.gCost + PathfinderStaticMethods.GetDistance(currentNode.Position, neighbor);
                    if (tentativeGCost < neighborNode.gCost)
                    {
                        neighborNode.previousIndex = currentNodeIndex;
                        neighborNode.gCost = tentativeGCost;
                        neighborNode.hCost = PathfinderStaticMethods.GetDistance(neighbor, end);
                        nodes[neighborIndex] = neighborNode;
                        if (!openList.Contains(neighborIndex)) openList.Add(neighborIndex);
                    }
                }
            }

            var endNode = nodes[endIndex];
            var path = PathfinderStaticMethods.CalculatePath(ref nodes, endNode);
#if DEBUG
            Debug.LogFormat("Path: {0}", string.Join(" -> ", path));
#endif
            path.Dispose();
            openList.Dispose();
            closedList.Dispose();
        }
#endif

#if UNITY_BURST
        [BurstCompile]
#endif
        private struct FindPathJob : IJob
        {
            public int2 startPosition;
            public int2 endPosition;
            public int2 gridSize;
            public NativeArray<PathNode> nodes;

            public void Execute()
            {
                PathfinderStaticMethods.InitializeNodes(ref nodes, gridSize);
                ReadOnlySpan<int2> neighborOffsetArray = stackalloc int2[]
                {
                    new int2(0, 1),
                    new int2(0, -1),
                    new int2(-1, 0),
                    new int2(1, 0),
                    new int2(1, 1),
                    new int2(-1, 1),
                    new int2(-1, -1),
                    new int2(1, -1)
                };
                var openList = new NativeList<int>(Allocator.Temp);
                var closedList = new NativeList<int>(Allocator.Temp);
                var startIndex = PathfinderStaticMethods.GetIndex(startPosition, gridSize);
                var endIndex = PathfinderStaticMethods.GetIndex(endPosition, gridSize);
                var startNode = nodes[startIndex];
                startNode.gCost = 0;
                startNode.hCost = PathfinderStaticMethods.GetDistance(startPosition, endPosition);
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
                    foreach (var neighborOffset in neighborOffsetArray)
                    {
                        var neighbor = new int2(currentNode.Position + neighborOffset);
                        if (!PathfinderStaticMethods.IsPositionInsideGrid(neighbor, gridSize)) continue;
                        var neighborIndex = PathfinderStaticMethods.GetIndex(neighbor, gridSize);
                        if (closedList.Contains(neighborIndex)) continue;
                        var neighborNode = nodes[neighborIndex];
                        if (!neighborNode.isWalkable) continue;
                        var tentativeGCost = currentNode.gCost + PathfinderStaticMethods.GetDistance(currentNode.Position, neighbor);
                        if (tentativeGCost < neighborNode.gCost)
                        {
                            neighborNode.previousIndex = currentNodeIndex;
                            neighborNode.gCost = tentativeGCost;
                            neighborNode.hCost = PathfinderStaticMethods.GetDistance(neighbor, endPosition);
                            nodes[neighborIndex] = neighborNode;
                            if (!openList.Contains(neighborIndex)) openList.Add(neighborIndex);
                        }
                    }
                }

                var endNode = nodes[endIndex];
                var path = PathfinderStaticMethods.CalculatePath(ref nodes, endNode);
                path.Dispose();
                openList.Dispose();
                closedList.Dispose();
            }
        }

        private static class PathfinderStaticMethods
        {
            private const float Sqrt2 = 1.4142135f;

#if UNITY_MATHEMATICS
            public static int GetIndex(int2 position, int2 size)
            {
                return GetIndex(position.x, position.y, size.y);
            }

            public static float GetDistance(int2 start, int2 end)
            {
                return GetDistance(start.x, start.y, end.x, end.y);
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
            public static NativeList<int2> CalculatePath(ref NativeArray<PathNode> pathNodes, PathNode endNode)
            {
                var path = new NativeList<int2>(Allocator.Temp);
                if (endNode.previousIndex == -1) return path;
                var reversePath = new NativeList<int2>(Allocator.Temp);
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
            private static float GetDistance(int startX, int startY, int endX, int endY)
            {
                var xDistance = math.abs(startX - endX);
                var yDistance = math.abs(startY - endY);
                var remaining = math.abs(xDistance - yDistance);
                return Sqrt2 * math.min(xDistance, yDistance) + remaining;
            }
#endif

            private static int GetIndex(int x, int y, int height)
            {
                return x * height + y;
            }
        }
    }
}