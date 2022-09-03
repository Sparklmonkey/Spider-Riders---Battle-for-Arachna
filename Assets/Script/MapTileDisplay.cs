using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MapTileDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField]
    private Image tileDisplay;
    [SerializeField]
    private MovementManager movementManager;

    public string itemName;
    public string npcName;
    public GameObject itemImagePrefab;
    private GameObject itemImage;
    public Vector2 tilePosition;
    public bool isWalkable = false;
    public bool isPlayerSpawn;
    public bool isTransition;
    public string transitionDestination;
    public MapTileDisplay previousTile;
    public int G, H, X, Y;
    public int F { get { return H + G; } }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isWalkable)
        {
            tileDisplay.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tileDisplay.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MinValue);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isWalkable)
        {
            if(itemName != "")
            {
                ITutorialItem tutorialItem = transform.GetComponentInChildren<ITutorialItem>();
                if (!tutorialItem.HasShownTutorial)
                {
                    tutorialItem.ShowTutorialPopUp();
                    return;
                }
            }
            movementManager.StartPathFinding(this);
        }
    }

    public void PickUpItem()
    {
        transform.GetComponentInChildren<ITutorialItem>().ItemAction();
        itemName = "";
        Destroy(itemImage);
    }

    private void Awake()
    {
        tileDisplay.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MinValue);
        tilePosition = new Vector2(transform.position.x, transform.position.y);

        X = int.Parse(name.Split('-')[0]);
        Y = int.Parse(name.Split('-')[1]);
        if(itemName != "")
        {
            itemImage = transform.GetChild(0).gameObject;
            Sprite itemSprite = Resources.Load<Sprite>($"Sprites/Items/{itemName}");

            itemImage.GetComponent<Image>().sprite = itemSprite;

            itemImage.GetComponent<Image>().color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
        }
    }
}
