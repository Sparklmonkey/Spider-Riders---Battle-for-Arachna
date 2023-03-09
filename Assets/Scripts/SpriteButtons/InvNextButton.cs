using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvNextButton : MonoBehaviour
{
    void OnMouseDown()
    {
        MissionInventoryManager.Instance.ChangePage(true);
    }
}
