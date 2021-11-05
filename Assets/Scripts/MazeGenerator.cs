using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Flags]
public enum WallState
{
    // 0000 --> No walls
    // 1111 --> Left,Rigth,Up,Down
    LEFT = 1,//0001
    RIGTH = 2,//0010
    UP = 4,//0100
    DOWN = 8,//1000


    VISITED = 128, // 1000 000
}


public struct Position
{
    public int X;
    public int Y;
}

public struct Neighbour
{
    public Position Position;
    public WallState SharedWall;
}
public static class  MazeGenerator 
{


    private static WallState GetOppositeWall(WallState wall)
    {
        switch (wall)
        {
            case WallState.RIGTH: return WallState.LEFT;
            case WallState.LEFT: return WallState.RIGTH;
            case WallState.UP: return WallState.DOWN;
            case WallState.DOWN: return WallState.UP;
            default: return WallState.LEFT;
        }

        
    }
    private static WallState[,] ApplyRecursiveBacktracker(WallState[,]maze,int width,int height)
    {

        //here we make changes
        var rnd = new System.Random(/*seed*/);
        var positionStack = new Stack<Position>();

        var position = new Position
        {
            X = rnd.Next(0, width),
            Y = rnd.Next(0, height),
        };

        maze[position.X, position.Y] |= WallState.VISITED;// 1000 1111 seting the inital cell as visited 
        positionStack.Push(position);

        while (positionStack.Count > 0)
        {
            var current = positionStack.Pop();
            var neighbours = GetUnvisitedNeighbours(current, maze, width, height);

            if (neighbours.Count > 0)
            {
                positionStack.Push(current);
                var randIndex = rnd.Next(0, neighbours.Count);
                var randomNeighbour = neighbours[randIndex];

                var nPosition = randomNeighbour.Position;
                maze[current.X, current.Y] &= ~randomNeighbour.SharedWall;
                maze[nPosition.X, nPosition.Y] &= ~GetOppositeWall(randomNeighbour.SharedWall);


                maze[nPosition.X, nPosition.Y] |= WallState.VISITED;

                positionStack.Push(nPosition);
            }
        }
        return maze;
    }
    private static List<Neighbour> GetUnvisitedNeighbours(Position p,WallState[,] maze, int width, int height)
    {
        var list = new List<Neighbour>();

        if (p.X > 0) //left
        {
            if (!maze[p.X -1,p.Y].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        X = p.X - 1,
                        Y = p.Y
                    },
                    SharedWall = WallState.LEFT
                });
            }
        }
        if (p.Y > 0) //Down
        {
            if (!maze[p.X, p.Y-1].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        X = p.X,
                        Y = p.Y-1
                    },
                    SharedWall = WallState.DOWN
                });
            }
        }
        if (p.Y < height -1) //Up
        {
            if (!maze[p.X, p.Y+1].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        X = p.X,
                        Y = p.Y +1 
                    },
                    SharedWall = WallState.UP
                });
            }
        }
        if (p.X < width -1) //Right
        {
            if (!maze[p.X + 1, p.Y].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        X = p.X + 1,
                        Y = p.Y
                    },
                    SharedWall = WallState.RIGTH
                });
            }
        }



        return list;
    }





  public static WallState[,]Generate(int width,int height)
    {
        WallState[,] maze = new WallState[width, height];
        WallState initial = WallState.RIGTH | WallState.LEFT | WallState.UP | WallState.DOWN;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                maze[i, j] = initial;
            }
        }


        return ApplyRecursiveBacktracker(maze, width, height);
    }
}
