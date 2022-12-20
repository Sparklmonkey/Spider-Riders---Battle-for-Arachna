using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RopeItem : MonoBehaviour, ITutorialItem
{
    public TutorialPopUp tutorialPopUp;
   
    private bool hasShownTutorial;

    public bool HasShownTutorial { get => hasShownTutorial; set => hasShownTutorial = value; }

    public void ItemAction()
    {
        TestPlayer<PlayerData>.AddItemToInventory("Rope");
    }

    public void ShowTutorialPopUp()
    {
        tutorialPopUp.SetupTutorial("To pick something up, you must go to the square where the object is.", TutorialAction);
        //HasShownTutorial = true;
    }

    public void TutorialAction()
    {
        HasShownTutorial = true;
    }
}