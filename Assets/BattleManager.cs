using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleManager : MonoBehaviour
{
    enum BattleAction { None, RollDice, Attack }
    BattleAction chosenBattleAction = BattleAction.None;
    BattleState state = BattleState.START;
    //singletons are supposedly not good/nice but since we don't plan to scale this a ton, and 
    //for the most part it's singleplayer, I'm going to use it.
    public static BattleManager Instance;
    [SerializeField]
    private Button rollDiceButton;
    [SerializeField]
    private Button attackButton;
    [SerializeField]
    private Canvas canvas;
    public DiceRollTest diceRoll;

    BattleEntity player;
    BattleEntity enemy;
    GameObject currentAttack;
    GameObject currentDefense;
    public TextMeshProUGUI playerAttackText;
    public TextMeshProUGUI playerDefenseText;
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI enemyAttackText;
    public TextMeshProUGUI enemyDefenseText;
    public TextMeshProUGUI enemyHealthText;
    public Sprite yellowCardBack;
    public Sprite blueCardBack;
    public Sprite greenCardBack;
    public GameObject playerAttack;
    public Transform playerAttackPosition;
    public GameObject playerDefense;
    public Transform playerDefensePosition;
    public GameObject enemyAttack;
    public Transform enemyAttackPosition;
    public GameObject enemyDefense;
    public Transform enemyDefensePosition;
    public GameObject playerHealth;
    public MapTileDisplay currentTile;
    List<Card> currentHand = new List<Card>();

    public GameObject cardDisplay;
    public Transform cardContent;
    public GameObject battleScreen;
    // Start is called before the first frame update
    void Start()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (state == BattleState.PLAYERTURN)
        {
            //set buttons to active
            rollDiceButton.interactable = true;
            rollDiceButton.gameObject.SetActive(true);
            attackButton.interactable = true;
            attackButton.gameObject.SetActive(true);
            playerAttackText.text = player.attack.ToString();
            playerDefenseText.text = player.defense.ToString();
            playerHealthText.text = player.health.ToString();
            enemyAttackText.text = enemy.attack.ToString();
            enemyDefenseText.text = enemy.defense.ToString();
            enemyHealthText.text = enemy.health.ToString();
        }
        else
        {
            rollDiceButton.interactable = false;
            attackButton.interactable = false;
        }
    }
    public void EnterBattle(BattleEntity player, BattleEntity enemy, MapTileDisplay tile)
    {
        this.player = player;
        playerAttackText.text = player.attack.ToString();
        playerDefenseText.text = player.defense.ToString();
        playerHealthText.text = player.health.ToString();
        enemyAttackText.text = enemy.attack.ToString();
        enemyDefenseText.text = enemy.defense.ToString();
        enemyHealthText.text = enemy.health.ToString();
        //instantiate 7 cards randomly
        for (int i = 0; i < 7; i++)
        {
            int rand = UnityEngine.Random.Range(0, player.playerData.cardInventory.Count);
            GameObject go = Instantiate(cardDisplay, cardContent);
            CardDisplay cd = go.GetComponent<CardDisplay>();
            cd.cardName.text = player.playerData.cardInventory[rand].name;
            cd.cardPrice.text = player.playerData.cardInventory[rand].buyCost.ToString();
            cd.cardDesc.text = player.playerData.cardInventory[rand].description;
            cd.cardImage.sprite = player.playerData.cardInventory[rand].image;
            if (player.playerData.cardInventory[rand].statModifyList.Count > 0)
                switch (player.playerData.cardInventory[rand].statModifyList[0].statType)
                {
                    case StatType.Power:
                        cd.cardBack.sprite = yellowCardBack;
                        go.tag = "YellowCard";
                        break;
                    case StatType.Defense:
                        cd.cardBack.sprite = greenCardBack;
                        go.tag = "GreenCard";
                        break;
                    case StatType.Health:
                        cd.cardBack.sprite = blueCardBack;
                        go.tag = "BlueCard";
                        break;
                    default:
                        break;
                }
            currentHand.Add(player.playerData.cardInventory[rand]);
        }
        this.enemy = enemy;
        this.state = BattleState.PLAYERTURN;
        this.currentTile = tile;
    }
    public void AddCard(int cardIndex)
    {
        for (int i = 0; i < currentHand[cardIndex].statModifyList.Count; i++)
        {
            if (currentHand[cardIndex].statModifyList[i].statType == StatType.Power)
                player.attack += currentHand[cardIndex].statModifyList[i].amount;
            if (currentHand[cardIndex].statModifyList[i].statType == StatType.Defense)
                player.defense += currentHand[cardIndex].statModifyList[i].amount;
            if (currentHand[cardIndex].statModifyList[i].statType == StatType.Health)
                player.health += currentHand[cardIndex].statModifyList[i].amount;
        }
    }
    //for when the dice roll is red, just add 1
    public void AddAttack(int cardIndex)
    {
        player.attack += 1;
    }
    //for the player-button event
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }
    //for the player-button event
    public void OnRollDiceButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        diceRoll.RollDice(player.attack);
    }
    IEnumerator PlayerAttack()
    {
        //bring the health over
        yield return PlayerAttackUIAnimation();
        yield return EnemyDefenseAnimation();
        //play the characer hit animation and deal damage
        enemy.health -= player.attack - enemy.defense;
        bool isDead = enemy.health <= 0;

        yield return new WaitForSeconds(1f);
        Destroy(currentAttack);
        Destroy(currentDefense);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        //bring the health over
        yield return EnemyAttackUIAnimation();
        yield return PlayerDefenseAnimation();
        //play the characer hit animation and deal damage
        player.health -= enemy.attack - player.defense;

        bool isDead = player.health == 0;

        yield return new WaitForSeconds(1f);
        Destroy(currentAttack);
        Destroy(currentDefense);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
        }

    }
    IEnumerator PlayerAttackUIAnimation()
    {
        currentAttack = Instantiate(playerAttack, playerAttack.transform.position, Quaternion.identity, canvas.transform);
        while (Vector3.Distance(currentAttack.transform.position, playerAttackPosition.position) > 1f)
        {
            yield return null;
            currentAttack.transform.position = Vector3.Lerp(currentAttack.transform.position, playerAttackPosition.position, Time.deltaTime * 4);
        }
        yield return null;
    }
    IEnumerator PlayerDefenseAnimation()
    {
        currentDefense = Instantiate(playerDefense, playerDefense.transform.position, Quaternion.identity, canvas.transform);
        while (Vector3.Distance(currentDefense.transform.position, enemyDefensePosition.position) > 1f)
        {
            yield return null;
            currentDefense.transform.position = Vector3.Lerp(currentDefense.transform.position, enemyDefensePosition.position, Time.deltaTime * 4);
        }
        yield return null;
    }
    IEnumerator EnemyAttackUIAnimation()
    {
        currentAttack = Instantiate(enemyAttack, enemyAttack.transform.position, Quaternion.identity, canvas.transform);
        while (Vector3.Distance(currentAttack.transform.position, playerAttackPosition.position) > 1f)
        {
            yield return null;
            currentAttack.transform.position = Vector3.Lerp(currentAttack.transform.position, playerAttackPosition.position, Time.deltaTime * 4);
        }
        yield return null;
    }
    IEnumerator EnemyDefenseAnimation()
    {
        currentDefense = Instantiate(enemyDefense, enemyDefense.transform.position, Quaternion.identity, canvas.transform);
        while (Vector3.Distance(currentDefense.transform.position, enemyDefensePosition.position) > 1f)
        {
            yield return null;
            currentDefense.transform.position = Vector3.Lerp(currentDefense.transform.position, enemyDefensePosition.position, Time.deltaTime * 4);
        }
        yield return null;
    }
    //exit battle and give the reward
    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            Destroy(currentTile.GetComponent<BattleEntity>());
            currentTile.npcName = "";
            battleScreen.SetActive(false);
        }
        else if (state == BattleState.LOST)
        {
            battleScreen.SetActive(false);
        }
    }
}
