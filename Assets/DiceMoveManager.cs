using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiceMoveManager : MonoBehaviour, IDragHandler, IDropHandler
{
    private Vector3 originalPosition;
    private bool shouldMoveBack;
    public void OnDrag(PointerEventData eventData)
    {
        shouldMoveBack = false;
        transform.position = eventData.position;
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Check If over card
        shouldMoveBack = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.GetComponent<RectTransform>().anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMoveBack)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, Time.deltaTime * 20f);

            if(transform.position == originalPosition)
            {
                shouldMoveBack = false;
            }
        }
    }
}
