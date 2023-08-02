using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder
{
    public List<OverlayTile> FindPath(OverlayTile startTile, OverlayTile endTile)
    {
        List<OverlayTile> openList = new ();
        List<OverlayTile> closedList = new ();

        openList.Add(startTile);

        while(openList.Count > 0)
        {
            OverlayTile currentTile = openList.OrderBy(x => x.F).First();

            openList.Remove(currentTile);

            closedList.Add(currentTile);

            if (currentTile == endTile)
            {
                return GetFinishedList(startTile, endTile);
            }

            List<OverlayTile> neighbourTiles = GetNeighbourTiles(currentTile);

            foreach (var tile in neighbourTiles)
            {
                if(tile.IsBlocked || closedList.Contains(tile))
                {
                    continue;
                }

                tile.Previous = currentTile;
                tile.G = GetManhattanDistance(startTile, tile);
                tile.H = GetManhattanDistance(endTile, tile);

                if (!openList.Contains(tile))
                {
                    openList.Add(tile);
                }
            }
        }
        return new List<OverlayTile>();
    }

    private List<OverlayTile> GetFinishedList(OverlayTile startTile, OverlayTile endTile)
    {
        List<OverlayTile> finishedList = new ();
        OverlayTile currentTile = endTile;

        while(currentTile != startTile)
        {
            finishedList.Add(currentTile);
            currentTile = currentTile.Previous;
        }
        finishedList.Reverse();
        return finishedList;
    }

    private int GetManhattanDistance(OverlayTile startTile, OverlayTile neihbour)
    {
        return Mathf.Abs(startTile.tileLocation.x - neihbour.tileLocation.x) + Mathf.Abs(startTile.tileLocation.y - neihbour.tileLocation.y);
    }

    private List<OverlayTile> GetNeighbourTiles(OverlayTile currentTile)
    {
        var map = MapManager.Instance.Map;
        List<OverlayTile> neighbourTiles = new List<OverlayTile>();


        //Above
        var locationToCheck = new Vector2Int(currentTile.tileLocation.x, currentTile.tileLocation.y + 1);
        if(map.ContainsKey(locationToCheck))
        {
            neighbourTiles.Add(map[locationToCheck]);
        }
        //Bottom
        locationToCheck = new Vector2Int(currentTile.tileLocation.x, currentTile.tileLocation.y - 1);
        if (map.ContainsKey(locationToCheck))
        {
            neighbourTiles.Add(map[locationToCheck]);
        }
        //Left
        locationToCheck = new Vector2Int(currentTile.tileLocation.x - 1, currentTile.tileLocation.y);
        if (map.ContainsKey(locationToCheck))
        {
            neighbourTiles.Add(map[locationToCheck]);
        }
        //Right
        locationToCheck = new Vector2Int(currentTile.tileLocation.x + 1, currentTile.tileLocation.y);
        if (map.ContainsKey(locationToCheck))
        {
            neighbourTiles.Add(map[locationToCheck]);
        }

        return neighbourTiles;
    }
}