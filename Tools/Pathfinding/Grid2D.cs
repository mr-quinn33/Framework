using System;
using System.Collections.Generic;
using Framework.Interfaces;
using UnityEngine;

namespace Framework.Tools.Pathfinding
{
    public interface IReadOnlyGrid2D<T>
    {
        Vector2Int Size { get; }

        T this[int x, int y] { get; }

        bool IsWithinBounds(int x, int y);

        bool IsWithinBounds(Vector2Int coordinate);

        bool Contains(T t);

        bool Contains(T t, out int x, out int y);

        bool Contains(T t, out Vector2Int position);

        IReadOnlyList<T> GetNeighbours(int x, int y, bool includeDiagonals);

        IReadOnlyList<T> GetNeighbours(T t, bool includeDiagonals);
    }

    public interface IGrid2D<T> : IReadOnlyGrid2D<T>
    {
        new T this[int x, int y] { get; set; }

        T this[Vector2Int coordinate] { get; set; }

        IUnregisterHandler RegisterOnValueChanged(Action<IReadOnlyGrid2D<T>, int, int, bool> action);

        void UnregisterOnValueChanged(Action<IReadOnlyGrid2D<T>, int, int, bool> action);

        bool Remove(T t);

        void Clear();
    }

    public class Grid2D<T> : IGrid2D<T>
    {
        private readonly T[,] array;
        private readonly int height;
        private readonly int width;

        public Grid2D(int width, int height)
        {
            this.width = width;
            this.height = height;
            array = new T[width, height];
        }

        public Grid2D(int width, int height, Func<int, int, T> func) : this(width, height)
        {
            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
                array[x, y] = func(x, y);
        }

        public Grid2D(Vector2Int size) : this(size.x, size.y)
        {
        }

        public T this[int x, int y]
        {
            get => IsWithinBounds(x, y) ? array[x, y] : default;
            set
            {
                if (!IsWithinBounds(x, y)) return;
                if (value == null && array[x, y] == null) return;
                if (value != null && value.Equals(array[x, y])) return;
                var isGridContainsValue = Contains(value);
                array[x, y] = value;
                OnValueChanged?.Invoke(this, x, y, isGridContainsValue);
            }
        }

        public T this[Vector2Int coordinate]
        {
            get => this[coordinate.x, coordinate.y];
            set => this[coordinate.x, coordinate.y] = value;
        }

        public bool Remove(T t)
        {
            if (!Contains(t, out var x, out var y)) return false;
            array[x, y] = default;
            return true;
        }

        public void Clear()
        {
            Array.Clear(array, 0, array.Length);
        }

        public IUnregisterHandler RegisterOnValueChanged(Action<IReadOnlyGrid2D<T>, int, int, bool> action)
        {
            OnValueChanged += action;
            return new Grid2DOnValueChangedUnregisterHandler(this, action);
        }

        public void UnregisterOnValueChanged(Action<IReadOnlyGrid2D<T>, int, int, bool> action)
        {
            OnValueChanged -= action;
        }

        public bool Contains(T t)
        {
            return Contains(t, out _, out _);
        }

        public bool Contains(T t, out int x, out int y)
        {
            for (x = 0; x < width; x++)
            for (y = 0; y < height; y++)
            {
                if (array[x, y] == null && t == null) return true;
                if (array[x, y] != null && array[x, y].Equals(t)) return true;
            }

            x = -1;
            y = -1;
            return false;
        }

        public bool Contains(T t, out Vector2Int position)
        {
            var result = Contains(t, out var x, out var y);
            position = new Vector2Int(x, y);
            return result;
        }

        public IReadOnlyList<T> GetNeighbours(int x, int y, bool includeDiagonals)
        {
            var result = new List<T>(8);
            if (IsWithinBounds(x - 1, y)) result.Add(array[x - 1, y]);
            if (IsWithinBounds(x + 1, y)) result.Add(array[x + 1, y]);
            if (IsWithinBounds(x, y - 1)) result.Add(array[x, y - 1]);
            if (IsWithinBounds(x, y + 1)) result.Add(array[x, y + 1]);
            if (!includeDiagonals) return result;
            if (IsWithinBounds(x - 1, y - 1)) result.Add(array[x - 1, y - 1]);
            if (IsWithinBounds(x + 1, y - 1)) result.Add(array[x + 1, y - 1]);
            if (IsWithinBounds(x - 1, y + 1)) result.Add(array[x - 1, y + 1]);
            if (IsWithinBounds(x + 1, y + 1)) result.Add(array[x + 1, y + 1]);
            return result;
        }

        public IReadOnlyList<T> GetNeighbours(T t, bool includeDiagonals)
        {
            return !Contains(t, out var x, out var y) ? null : GetNeighbours(x, y, includeDiagonals);
        }

        public Vector2Int Size => new(width, height);

        public bool IsWithinBounds(int x, int y)
        {
            return 0 <= x && x < width && 0 <= y && y < height;
        }

        public bool IsWithinBounds(Vector2Int coordinate)
        {
            return IsWithinBounds(coordinate.x, coordinate.y);
        }

        private event Action<IReadOnlyGrid2D<T>, int, int, bool> OnValueChanged;

        private class Grid2DOnValueChangedUnregisterHandler : IUnregisterHandler
        {
            private Action<IReadOnlyGrid2D<T>, int, int, bool> action;
            private Grid2D<T> grid2D;

            public Grid2DOnValueChangedUnregisterHandler(Grid2D<T> grid2D, Action<IReadOnlyGrid2D<T>, int, int, bool> action)
            {
                this.action = action;
                this.grid2D = grid2D;
            }

            public void Unregister()
            {
                grid2D.UnregisterOnValueChanged(action);
                grid2D = null;
                action = null;
            }
        }
    }
}