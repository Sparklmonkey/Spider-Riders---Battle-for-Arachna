using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DrawPredefinedNumberOfCards", menuName = "ScriptableObjects/CardEffects/DrawPredefinedNumberOfCards", order = 1)]
public class DrawPredefinedNumberOfCards : CardEffect
{
    public override void Invoke(CardEffectContext cardEffectContext)
    {
        DrawCards(cardEffectContext.thisCardInstance, cardEffectContext.thisCardInstance.CardDefinition.CardsToDraw);
    }
}
