using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRollTest : MonoBehaviour
{
    [SerializeField]
    private GameObject dicePrefab;

    public List<bool> completedDieRolls = new List<bool>();
    private List<DiceRollAnimation> diceRollAnimations = new List<DiceRollAnimation>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DiceRollTestAnim());
    }

    IEnumerator DiceRollTestAnim()
    {
        int diceCount = TestPlayer<PlayerData>.GetPower();

        for (int i = 0; i < 20; i++)
        {
            GameObject dice = Instantiate(dicePrefab, transform);
            int result = dice.GetComponent<DiceRollAnimation>().RollDice(this);
            diceRollAnimations.Add(dice.GetComponent<DiceRollAnimation>());
            yield return new WaitForSeconds(0.05f);
        }

        while(completedDieRolls.Count < 20)
        {
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.1f);
        foreach (var item in diceRollAnimations)
        {
            item.ClearIfWhite();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
