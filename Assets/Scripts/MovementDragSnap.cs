using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementDragSnapState
{
    Stationary,
    Dragging,
    MovingToPosition,
}

public class MovementDragSnap
{
    public Transform TransformToMove { get; private set; }
    public MovementDragSnapState MovementState { get; private set; }
    public bool IsStationary => MovementState == MovementDragSnapState.Stationary;
    public bool IsDragging => MovementState == MovementDragSnapState.Dragging;
    public bool IsMovingToPosition => MovementState == MovementDragSnapState.MovingToPosition;
    public Vector3 DragPosition { get; set; }


    public MovementDragSnap(Transform thisCardTransform)
    {
        TransformToMove = thisCardTransform;
        MovementState = MovementDragSnapState.Stationary;
        DragPosition = Vector3.zero;
    }

    public MovementDragSnap TeleportToPosition(Vector3 newPosition)
    {
        if (MovementState != MovementDragSnapState.Stationary) return this;
        TransformToMove.transform.position = newPosition;
        return this;
    }
    public IEnumerator MoveToDestination(Vector3 newPosition, float movementDuration, bool stopDragging = false)
    {
        if (IsMovingToPosition || !(IsDragging && stopDragging)) yield break;
        MovementState = MovementDragSnapState.MovingToPosition;

        Vector3 startPosition = TransformToMove.position;
        float movementElapsedTime = 0f;
        float movementProgress = movementDuration > 0f ? 0f : 1f;

        while (movementProgress < 1f)
        {
            yield return null;
            if (!IsMovingToPosition) yield break;

            movementElapsedTime += Time.deltaTime;
            movementProgress = movementElapsedTime / movementDuration;
            TransformToMove.position = Vector3.Lerp(startPosition, newPosition, movementProgress);
        }
        MovementState = MovementDragSnapState.Stationary;
    }
    public IEnumerator Drag()
    {
        if (!IsStationary) yield break;
        MovementState = MovementDragSnapState.Dragging;
        while (IsDragging)
        {
            TransformToMove.position = DragPosition;
            yield return null;
        }
    }
    /*public void StartDragging()
    {
        if (!IsStationary) return;
        MovementState = MovementDragSnapState.Dragging;
    }
    public void UpdateDragging(Vector3 currentDragPosition)
    {
        if (!IsDragging) return;
        TransformToMove.position = DragPosition = currentDragPosition;
    }*/
    public void StopDragging()
    {
        if (!IsDragging) return;
        MovementState = MovementDragSnapState.Stationary;
    }
}
