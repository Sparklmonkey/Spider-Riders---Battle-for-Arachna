using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionEndInteratable : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        TestPlayer<PlayerData>.FinishMission();
        MapSceneManager.Instance.SetupMapScene(1);
        MapManager.Instance.SetupNextMission();
    }
}
