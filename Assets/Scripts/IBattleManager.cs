using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleManager
{
    public void PlayCardActivationSound();
    public void OpenCardDetailsPopup(Card cardDefinition);
}