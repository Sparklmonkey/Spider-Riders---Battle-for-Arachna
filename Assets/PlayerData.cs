using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class PlayerData
//{
//    public static PlayerData currentPlayer = new PlayerData();

//    public List<string> inventoryItems = new List<string>();


//    public void AddItemToInventory(string itemToAdd)
//    {
//        inventoryItems.Add(itemToAdd);
//    }

//    public bool PlayerHasItem(string itemToCheck)
//    {
//        return inventoryItems.Contains(itemToCheck);
//    }

//}

public class TestPlayer<T> : ScriptableObject where T : ScriptableObject
{
    private static PlayerData _instance;
    public static PlayerData GetInstance()
    {
        if (_instance == null)
        {
            _instance = Resources.Load("TestPlayer") as PlayerData;
        }
        return _instance;
    }

    
    public static void AddItemToInventory(string itemToAdd)
    {
        GetInstance().itemInventory.Add(itemToAdd);
    }

    public static bool PlayerHasItem(string itemToCheck)
    {
        return GetInstance().itemInventory.Contains(itemToCheck);
    }
}