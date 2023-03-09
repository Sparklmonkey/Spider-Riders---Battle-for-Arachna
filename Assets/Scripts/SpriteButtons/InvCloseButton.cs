using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvCloseButton : MonoBehaviour
{
    void OnMouseDown()
    {
        MissionInventoryManager.Instance.CloseInventory(true);
    }
}
