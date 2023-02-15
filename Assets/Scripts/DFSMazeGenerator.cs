using System;
using System.Collections.Generic;

class Maze
{
    private int width;
    private int height;
    private Cell[,] cells;
    private Random random = new Random();

    public Maze(int width, int height)
    {
        this.width = width;
        this.height = height;

        cells = new Cell[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cells[x, y] = new Cell();
            }
        }

        DFS(0, 0);
    }

    private void DFS(int x, int y)
    {
        cells[x, y].Visited = true;

        List<Direction> directions = new List<Direction> { Direction.North, Direction.East, Direction.South, Direction.West };
        directions.Shuffle(random);

        foreach (Direction direction in directions)
        {
            int nx = x + direction.DeltaX;
            int ny = y + direction.DeltaY;
            if (nx >= 0 && nx < width && ny >= 0 && ny < height && !cells[nx, ny].Visited)
            {
                cells[x, y].Walls[(int)direction] = false;
                cells[nx, ny].Walls[(int)direction.Opposite] = false;
                DFS(nx, ny);
            }
        }
    }

    private enum Direction
    {
        North,
        East,
        South,
        West,
        Count
    }

    private class Cell
    {
        public bool[] Walls = new bool[(int)Direction.Count];
        public bool Visited;

        public Cell()
        {
            for (int i = 0; i < (int)Direction.Count; i++)
            {
                Walls[i] = true;
            }
        }
    }

    
}

static class Extensions
{
    public static void Shuffle<T>(this IList<T> list, Random random)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
