using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiceMoveManager : MonoBehaviour, IDragHandler, IDropHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 originalPosition;
    private bool shouldMoveBack;
    Transform startParent;
    Transform canvas;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startParent = transform.parent;
        if (!GetComponent<CanvasGroup>())
            gameObject.AddComponent<CanvasGroup>();
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        canvas = transform.root;
        transform.parent = canvas;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == canvas)
        {
            transform.parent = startParent;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        shouldMoveBack = false;
        transform.position = eventData.position;
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Check If over card
        DiceMoveManager dcm = eventData.pointerDrag.GetComponent<DiceMoveManager>();
        if (dcm)
        {
            if (eventData.pointerDrag != this)
            {
                DiceRollAnimation diceRollAnimation = dcm.GetComponent<DiceRollAnimation>();
                if (diceRollAnimation)
                {
                    //check if it's the right colour
                    if (diceRollAnimation.result == 0 && gameObject.CompareTag("YellowCard") ||
                        diceRollAnimation.result == 1 && gameObject.CompareTag("GreenCard") ||
                        diceRollAnimation.result == 2 && gameObject.CompareTag("BlueCard"))
                    {
                        BattleManager.Instance.AddCard(transform.GetSiblingIndex());
                        Destroy(dcm.gameObject);
                    }
                }
            }
        }
        if (!gameObject.CompareTag("YellowCard") &&
           !gameObject.CompareTag("GreenCard") &&
           !gameObject.CompareTag("BlueCard"))
            shouldMoveBack = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("GetOriginalPosition", 0.01f);
    }
    void GetOriginalPosition()
    {
        originalPosition = transform.GetComponent<RectTransform>().anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMoveBack)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, originalPosition, Time.deltaTime * 400f);

            if (transform.localPosition == originalPosition)
            {
                shouldMoveBack = false;
            }
        }
    }

}
