using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvPrevButton : MonoBehaviour
{
    void OnMouseDown()
    {
        MissionInventoryManager.Instance.ChangePage(false);
    }
}
