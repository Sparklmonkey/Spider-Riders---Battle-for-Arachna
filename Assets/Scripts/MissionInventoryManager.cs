using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MissionInventoryManager : MonoBehaviour
{
    private static MissionInventoryManager _instance;
    public static MissionInventoryManager Instance
    {
        get { return _instance; }
    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public bool IsInventoryOpen;
    private Animator _animator;
    private int _maxPages;
    private int _currentPage;
    private List<string> _inventoryList;
    [SerializeField]
    private GameObject _inventoryContainer, _inventoryItemPrefab;
    [SerializeField]
    private List<Transform> _inventoryItemContainers;
    [SerializeField]
    private TextMeshProUGUI _inventoryTitle, _pageLbl;

    public void OpenInventory()
    {
        _animator.SetTrigger("Open");
        IsInventoryOpen = true;
    }

    public void CloseInventory(bool isFromButton)
    {
        foreach (Transform inventoryContainer in _inventoryItemContainers)
        {
            if (inventoryContainer.childCount == 0) { continue; }
            Destroy(inventoryContainer.GetChild(0).gameObject);
        }

        _inventoryContainer.SetActive(false);
        _animator.SetTrigger("Close");
        IsInventoryOpen = !isFromButton;
    }
    public void RenderInventory()
    {
        _inventoryList = TestPlayer<PlayerData>.Inventory;
        _currentPage = 0;
        _maxPages = Mathf.CeilToInt(_inventoryList.Count / 8f);

        _pageLbl.text = $"{_currentPage}";
        _inventoryContainer.SetActive(true);
        for (int i = 0; i < 8; i++)
        {
            if (i >= _inventoryList.Count) { break; }
            var inventoryItem = Instantiate(_inventoryItemPrefab, _inventoryItemContainers[i]).GetComponent<InventoryItem>();
            inventoryItem.SetupInventoryItem(_inventoryList[i]);
        }
    }

    public void ChangePage(bool isNext)
    {
        if(_maxPages == 1) { return; }
        if (isNext)
        {
            _currentPage = _currentPage == _maxPages ? 0 : _currentPage + 1;
        }
        else
        {
            _currentPage = _currentPage == 0 ? _maxPages : _currentPage - 1;
        }

        _pageLbl.text = $"{_currentPage}";
        _inventoryContainer.SetActive(true);
        int loopStart = (_currentPage - 1) * 8;
        for (int i = loopStart; i < (loopStart + 8); i++)
        {
            if (i >= (_inventoryList.Count - 1)) { break; }
            var inventoryItem = Instantiate(_inventoryItemPrefab, _inventoryItemContainers[i]).GetComponent<InventoryItem>();
            inventoryItem.SetupInventoryItem(_inventoryList[i]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private InventoryItem _currentItem;
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    var mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

        //    RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition2D, Vector2.zero);
        //    if (hits.Length > 0)
        //    {
        //        InventoryItem inventoryItem = hits.OrderByDescending(i => i.collider.transform.position.z).First().transform.GetComponent<InventoryItem>();

        //        if (inventoryItem == null) { return; }
        //        if (inventoryItem == _currentItem) { return; }
        //        if (_currentItem != null) { _currentItem.EndDrag(); }
        //        inventoryItem.StartDrag();
        //        _currentItem = inventoryItem;
        //    }
        //}

        //if (Input.GetMouseButtonUp(0))
        //{
        //    if (_currentItem == null) { return; }
        //    _currentItem.EndDrag();
        //    _currentItem = null;
        //}

    }
}
