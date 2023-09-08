using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController 
{

    public MovementController(float speed, OverlayTile initialTile)
    {
        _pathFinder = new PathFinder();
        _currentPath = new List<OverlayTile>();
        _speed = speed;
        _currentTile = initialTile;
    }

    private float _speed = 5f;
    private PathFinder _pathFinder;
    private List<OverlayTile> _currentPath;
    private OverlayTile _currentTile;

    public event Action<Vector2, float> OnCharacterMove;

    public void SetCurrentTile(OverlayTile tile)
    {
        _currentTile = tile;
        _currentPath.Clear();
    }

    public void StartMovingPlayer(OverlayTile destination)
    {
        _currentPath = _pathFinder.FindPath(_currentTile, destination);
    }

    public void ArrivedAtTile()
    {
        _currentTile = _currentPath[0];
        _currentPath.RemoveAt(0);
    }

    public void MoveAlongPath()
    {
        if (_currentPath.Count == 0) { return; }
        var step = _speed * Time.deltaTime;
        var destination = new Vector2(_currentPath[0].transform.position.x, _currentPath[0].transform.position.y - 0.12f);
        OnCharacterMove?.Invoke(destination, step);
    }
}