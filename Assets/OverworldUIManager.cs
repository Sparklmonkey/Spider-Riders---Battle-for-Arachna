using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldUIManager : MonoBehaviour
{
    private static OverworldUIManager _instance;
    public static OverworldUIManager Instance
    {
        get { return _instance; }
    }

    [SerializeField]
    private MissionInventoryManager inventoryManager;


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
}
