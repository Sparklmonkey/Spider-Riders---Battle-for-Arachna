using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRollTest : MonoBehaviour
{
    [SerializeField]
    private GameObject dicePrefab;

    public List<bool> completedDieRolls = new List<bool>();
    public List<DiceRollAnimation> diceRollAnimations = new List<DiceRollAnimation>();
    private bool _canRollDice = true;

    public void RollDice(int diceAmount)
    {
        if (!_canRollDice) return;
        _canRollDice = false;
        completedDieRolls.Clear();
        for (int i = 0; i < diceRollAnimations.Count; i++)
            if (diceRollAnimations[i] != null)
                Destroy(diceRollAnimations[i].gameObject);
        diceRollAnimations.Clear();
        StartCoroutine(DiceRollAnim(diceAmount));
    }

    IEnumerator DiceRollAnim(int diceAmount)
    {
        int diceCount = TestPlayer<PlayerData>.GetAttack();

        for (int i = 0; i < diceAmount; i++)
        {
            GameObject dice = Instantiate(dicePrefab, transform);
            int result = dice.GetComponent<DiceRollAnimation>().RollDice(this);
            diceRollAnimations.Add(dice.GetComponent<DiceRollAnimation>());
            yield return new WaitForSeconds(0.05f);
        }

        while (completedDieRolls.Count < diceAmount)
        {
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.1f);
        foreach (var item in diceRollAnimations)
        {
            item.ClearIfWhite();
        }
        _canRollDice = true;
    }
}
