using System.Collections;
using NUnit.Framework;
using UnityEngine;

namespace Framework.Tools.Pathfinding.Editor.Tests
{
    public class PathfinderFindPathTests
    {
        [TestCaseSource(typeof(FindPathWhenStartOrEndAreOutOfBoundsReturnsEmptyPathTestCaseSource))]
        public void FindPath_WhenStartOrEndAreOutOfBounds_ReturnsEmptyPath(IPathfinder pathfinder, Vector2Int start, Vector2Int end)
        {
            var pathIncludeDiagonals = pathfinder.FindPath(start, end, true);
            var pathExcludeDiagonals = pathfinder.FindPath(start, end, false);

            Assert.That(pathIncludeDiagonals, Is.Empty);
            Assert.That(pathExcludeDiagonals, Is.Empty);
        }

        [TestCaseSource(typeof(FindPathWhenStartAndEndAreSameReturnsEmptyPathTestCaseSource))]
        public void FindPath_WhenStartAndEndAreSame_ReturnsEmptyPath(IPathfinder pathfinder, Vector2Int start, Vector2Int end)
        {
            var pathIncludeDiagonals = pathfinder.FindPath(start, end, true);
            var pathExcludeDiagonals = pathfinder.FindPath(start, end, false);

            Assert.That(pathIncludeDiagonals, Is.Empty);
            Assert.That(pathExcludeDiagonals, Is.Empty);
        }

        [TestCaseSource(typeof(FindPathNotDiagonalPathReturnsCorrectPathTestCaseSource))]
        public void FindPath_NotDiagonalPath_ReturnsCorrectPath(IPathfinder pathfinder, Vector2Int start, Vector2Int end, Vector2Int[] expectedPath)
        {
            var path = pathfinder.FindPath(start, end, false);

            Assert.That(path, Is.EqualTo(expectedPath));
        }

        [TestCaseSource(typeof(FindPathDiagonalPathReturnsCorrectPathTestCaseSource))]
        public void FindPath_DiagonalPath_ReturnsCorrectPath(IPathfinder pathfinder, Vector2Int start, Vector2Int end, Vector2Int[] expectedPath)
        {
            var path = pathfinder.FindPath(start, end, true);

            Assert.That(path, Is.EqualTo(expectedPath));
        }

        private class FindPathWhenStartOrEndAreOutOfBoundsReturnsEmptyPathTestCaseSource : IEnumerable
        {
            private readonly IPathfinder pathfinder = new Pathfinder(10, 10);

            public IEnumerator GetEnumerator()
            {
                yield return new object[] {pathfinder, new Vector2Int(-1, 0), new Vector2Int(0, 0)};
                yield return new object[] {pathfinder, new Vector2Int(9, 9), new Vector2Int(9, 10)};
            }
        }

        private class FindPathWhenStartAndEndAreSameReturnsEmptyPathTestCaseSource : IEnumerable
        {
            private readonly IPathfinder pathfinder = new Pathfinder(10, 10);

            public IEnumerator GetEnumerator()
            {
                yield return new object[] {pathfinder, new Vector2Int(0, 0), new Vector2Int(0, 0)};
                yield return new object[] {pathfinder, new Vector2Int(9, 9), new Vector2Int(9, 9)};
            }
        }

        private class FindPathNotDiagonalPathReturnsCorrectPathTestCaseSource : IEnumerable
        {
            private readonly IPathfinder pathfinder = new Pathfinder(5, 5);

            public IEnumerator GetEnumerator()
            {
                yield return new object[]
                {
                    pathfinder, new Vector2Int(0, 0), new Vector2Int(4, 0), new[]
                    {
                        new Vector2Int(1, 0), new Vector2Int(2, 0), new Vector2Int(3, 0), new Vector2Int(4, 0)
                    }
                };
                yield return new object[]
                {
                    pathfinder, new Vector2Int(0, 0), new Vector2Int(0, 4), new[]
                    {
                        new Vector2Int(0, 1), new Vector2Int(0, 2), new Vector2Int(0, 3), new Vector2Int(0, 4)
                    }
                };
                yield return new object[]
                {
                    pathfinder, new Vector2Int(4, 4), new Vector2Int(4, 0), new[]
                    {
                        new Vector2Int(4, 3), new Vector2Int(4, 2), new Vector2Int(4, 1), new Vector2Int(4, 0)
                    }
                };
                yield return new object[]
                {
                    pathfinder, new Vector2Int(4, 4), new Vector2Int(0, 4), new[]
                    {
                        new Vector2Int(3, 4), new Vector2Int(2, 4), new Vector2Int(1, 4), new Vector2Int(0, 4)
                    }
                };

                yield return new object[]
                {
                    pathfinder, new Vector2Int(0, 0), new Vector2Int(4, 4), new[]
                    {
                        new Vector2Int(1, 0), new Vector2Int(2, 0), new Vector2Int(3, 0), new Vector2Int(4, 0),
                        new Vector2Int(4, 1), new Vector2Int(4, 2), new Vector2Int(4, 3), new Vector2Int(4, 4)
                    }
                };
                yield return new object[]
                {
                    pathfinder, new Vector2Int(4, 0), new Vector2Int(0, 4), new[]
                    {
                        new Vector2Int(4, 1), new Vector2Int(4, 2), new Vector2Int(4, 3), new Vector2Int(4, 4),
                        new Vector2Int(3, 4), new Vector2Int(2, 4), new Vector2Int(1, 4), new Vector2Int(0, 4)
                    }
                };
                yield return new object[]
                {
                    pathfinder, new Vector2Int(4, 4), new Vector2Int(0, 0), new[]
                    {
                        new Vector2Int(3, 4), new Vector2Int(2, 4), new Vector2Int(1, 4), new Vector2Int(0, 4),
                        new Vector2Int(0, 3), new Vector2Int(0, 2), new Vector2Int(0, 1), new Vector2Int(0, 0)
                    }
                };
                yield return new object[]
                {
                    pathfinder, new Vector2Int(0, 4), new Vector2Int(4, 0), new[]
                    {
                        new Vector2Int(1, 4), new Vector2Int(2, 4), new Vector2Int(3, 4), new Vector2Int(4, 4),
                        new Vector2Int(4, 3), new Vector2Int(4, 2), new Vector2Int(4, 1), new Vector2Int(4, 0)
                    }
                };
            }
        }

        private class FindPathDiagonalPathReturnsCorrectPathTestCaseSource : IEnumerable
        {
            private readonly IPathfinder pathfinder = new Pathfinder(5, 5);

            public IEnumerator GetEnumerator()
            {
                yield return new object[]
                {
                    pathfinder, new Vector2Int(0, 0), new Vector2Int(4, 4), new[]
                    {
                        new Vector2Int(1, 1), new Vector2Int(2, 2), new Vector2Int(3, 3), new Vector2Int(4, 4)
                    }
                };
                yield return new object[]
                {
                    pathfinder, new Vector2Int(0, 0), new Vector2Int(3, 4), new[]
                    {
                        new Vector2Int(1, 1), new Vector2Int(2, 2), new Vector2Int(3, 3), new Vector2Int(3, 4)
                    }
                };
                yield return new object[]
                {
                    pathfinder, new Vector2Int(0, 0), new Vector2Int(4, 3), new[]
                    {
                        new Vector2Int(1, 1), new Vector2Int(2, 2), new Vector2Int(3, 3), new Vector2Int(4, 3)
                    }
                };
                yield return new object[]
                {
                    pathfinder, new Vector2Int(0, 0), new Vector2Int(2, 4), new[]
                    {
                        new Vector2Int(0, 1), new Vector2Int(0, 2), new Vector2Int(1, 3), new Vector2Int(2, 4)
                    }
                };
                yield return new object[]
                {
                    pathfinder, new Vector2Int(0, 0), new Vector2Int(4, 2), new[]
                    {
                        new Vector2Int(1, 0), new Vector2Int(2, 0), new Vector2Int(3, 1), new Vector2Int(4, 2)
                    }
                };
                yield return new object[]
                {
                    pathfinder, new Vector2Int(0, 0), new Vector2Int(1, 4), new[]
                    {
                        new Vector2Int(0, 1), new Vector2Int(0, 2), new Vector2Int(1, 3), new Vector2Int(1, 4)
                    }
                };
                yield return new object[]
                {
                    pathfinder, new Vector2Int(0, 0), new Vector2Int(4, 1), new[]
                    {
                        new Vector2Int(1, 0), new Vector2Int(2, 0), new Vector2Int(3, 1), new Vector2Int(4, 1)
                    }
                };
            }
        }
    }
}