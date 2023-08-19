using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DrawPredefinedNumberOfCards", menuName = "ScriptableObjects/CardEffects/DrawPredefinedNumberOfCards", order = 1)]
public class DrawPredefinedNumberOfCards : CardEffect
{
    public override IEnumerator Invoke(CardEffectContext cardEffectContext)
    {
        yield return cardEffectContext.thisCardInstance.StartCoroutine(DrawCards(
            cardEffectContext.thisCardInstance,
            cardEffectContext.thisCardInstance.Reference.CardDefinition.CardsToDraw
        ));
    }
}
