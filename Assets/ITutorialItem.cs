using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ITutorialItem 
{
    public bool HasShownTutorial { get; set; }

    public void ShowTutorialPopUp();
    public void ItemAction();
    public void TutorialAction();
}
