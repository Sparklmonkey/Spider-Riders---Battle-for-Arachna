using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollAnimation : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> baseRollList, yellowResult, greenResult, redResult, whiteResult, blueResult, shineList, startList, dissapearList;
    [SerializeField]
    private Image diceImage, shineImage;

    private int result;
    private DiceRollTest manager;
    public void ClearIfWhite()
    {
        if(result > 3)
        {
            StartCoroutine(ClearDie());
        }
    }

    public int RollDice(DiceRollTest manager)
    {
        this.manager = manager;
        int diceResult = Random.Range(0, 6);
        StartCoroutine(SpinDice(diceResult));
        StartCoroutine(ShowShine());
        return diceResult;
    }

    IEnumerator SpinDice(int result)
    {
        this.result = result;
        List<Sprite> diceRoll = new List<Sprite>(startList);
        diceRoll.AddRange(baseRollList);
        switch (result)
        {
            case 0:
                diceRoll.AddRange(yellowResult);
                break;
            case 1:
                diceRoll.AddRange(greenResult);
                break;
            case 2:
                diceRoll.AddRange(blueResult);
                break;
            case 3:
                diceRoll.AddRange(redResult);
                break;
            default:
                diceRoll.AddRange(whiteResult);
                break;
        }
        for (int i = 0; i < diceRoll.Count; i++)
        {
            diceImage.sprite = diceRoll[i];
            yield return new WaitForSeconds(0.03f);
        }

        manager.completedDieRolls.Add(true);
    }

    IEnumerator ClearDie()
    {
        for (int i = 0; i < dissapearList.Count; i++)
        {
            diceImage.sprite = dissapearList[i];
            yield return new WaitForSeconds(0.03f);
        }
    }

    IEnumerator ShowShine()
    {
        for (int i = 0; i < shineList.Count; i++)
        {
            shineImage.sprite = shineList[i];
            yield return new WaitForSeconds(0.03f);
        }
        shineImage.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MinValue);
    }
}
