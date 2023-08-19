using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleParticipant
{
    public IHandOfCardsManager HandOfCards { get; }
    public StatModifiers StatModifiers { get; set; }
    public int MaxHealth { get; }
    public int CurrentHealth { get; set; }
    public void AddCardsToDraw(int quantity);
}
