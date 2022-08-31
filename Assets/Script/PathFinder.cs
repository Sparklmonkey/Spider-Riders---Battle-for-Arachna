using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder
{
    public MapTileDisplay[,] map;
    public List<MapTileDisplay> FindPath(MapTileDisplay startTile, MapTileDisplay endTile)
    {
        List<MapTileDisplay> openList = new List<MapTileDisplay>();
        List<MapTileDisplay> closedList = new List<MapTileDisplay>();

        openList.Add(startTile);

        while(openList.Count > 0)
        {
            MapTileDisplay currentTile = openList.OrderBy(x => x.F).First();

            openList.Remove(currentTile);

            closedList.Add(currentTile);

            if (currentTile == endTile)
            {
                return GetFinishedList(startTile, endTile);
            }

            List<MapTileDisplay> neighbourTiles = GetNeighbourTiles(currentTile);

            foreach (var tile in neighbourTiles)
            {
                if(!tile.isWalkable || closedList.Contains(tile))
                {
                    continue;
                }

                tile.previousTile = currentTile;
                tile.G = GetManhattanDistance(startTile, tile);
                tile.H = GetManhattanDistance(endTile, tile);

                if (!openList.Contains(tile))
                {
                    openList.Add(tile);
                }
            }
        }
        return new List<MapTileDisplay>();
    }

    private List<MapTileDisplay> GetFinishedList(MapTileDisplay startTile, MapTileDisplay endTile)
    {
        List<MapTileDisplay> finishedList = new List<MapTileDisplay>();
        MapTileDisplay currentTile = endTile;

        while(currentTile != startTile)
        {
            finishedList.Add(currentTile);
            currentTile = currentTile.previousTile;
        }
        finishedList.Reverse();
        return finishedList;
    }

    private int GetManhattanDistance(MapTileDisplay startTile, MapTileDisplay neihbour)
    {
        return Mathf.Abs(startTile.X - neihbour.X) + Mathf.Abs(startTile.Y - neihbour.Y);
    }

    private List<MapTileDisplay> GetNeighbourTiles(MapTileDisplay currentTile)
    {

        List<MapTileDisplay> neighbourTiles = new List<MapTileDisplay>();


        //Above
        if(map[currentTile.X,currentTile.Y + 1] != null)
        {
            neighbourTiles.Add(map[currentTile.X, currentTile.Y + 1]);
        }
        //Bottom
        if (map[currentTile.X, currentTile.Y - 1] != null)
        {
            neighbourTiles.Add(map[currentTile.X, currentTile.Y - 1]);
        }
        //Left
        if (map[currentTile.X - 1, currentTile.Y] != null)
        {
            neighbourTiles.Add(map[currentTile.X - 1, currentTile.Y]);
        }
        //Right
        if (map[currentTile.X + 1, currentTile.Y] != null)
        {
            neighbourTiles.Add(map[currentTile.X + 1, currentTile.Y]);
        }

        return neighbourTiles;
    }
}
