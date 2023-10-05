using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    public bool debugMode = false;
    [Header("Characters")]
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    //public List<GameObject> enemyPrefabs;

    public Unit playerUnit;
    public Unit enemyUnit;

    public GameObject enemyGO;
    
    public CharacterSelection players;
    [FormerlySerializedAs("playersGO")] public List<GameObject> clones = new List<GameObject>();

    [Header("BattleHUD")]
    public Transform[] playerBattleStations;
    public Transform enemyBattleStation;
    //public Transform[] enemyBattleStations;
    
    public TMP_Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    [Header("Battle States")]
    public BattleState state;

    public bool hasActed = false;
    public bool isHealing = false;

    public string scene;
    
    [FormerlySerializedAs("waterEffect")]
    [Header("Battle Animations")]
    [SerializeField] private GameObject particleEffect;
    
    private Animator enemyAnimator;
    public Animator playerAnimator;

    [Header("Events")] 
    public UnityEvent _OnBattleEnd;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        
        if (!debugMode)
        { 
            players.characters = GameManager.instance.battleTeam; //uncomment when testing info transfer   
        }
        
        //check that battle characters from game manager match team in battle system
        foreach (GameObject character in players.characters)
        {
            Debug.Log("character is: " + character.GetComponent<Unit>().type);
        }
        
        foreach (GameObject character in GameManager.instance.battleTeam)
        {
            Debug.Log("battle character is: " + character.GetComponent<Unit>().type);
        }
        
        //start the battle
        StartCoroutine(SetUpBattle());
    }

    void Update()
    {

    }

    //player and enemy spawn into battle
    IEnumerator SetUpBattle() 
    {       
        GameObject playerGO = playerPrefab;
        //GameObject playerGo = playerPrefab;
        playerUnit = playerGO.GetComponent<Unit>();

        for(int i=0; i < players.characters.Count; i++)
        {
            Debug.Log("instantiate team member" + players.characters[i].GetComponent<Unit>().type);
            GameObject playerClone = Instantiate(players.characters[i], playerBattleStations[i]);
            clones.Add(playerClone);
        }

        players.characters = clones;

        enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();
        enemyAnimator = enemyGO.GetComponent<Animator>();
        particleEffect = enemyGO.transform.GetChild(0).gameObject;

        playerHUD.characters = players.characters;
        enemyHUD.characters = new List<GameObject> {enemyPrefab};

        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches.";

        playerHUD.SetHUD();
        playerHUD.SetButtons(playerUnit);
        enemyHUD.SetHUD();

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    //handles player's attack
    IEnumerator PlayerAttack(bool isSpecial)
    {
        //determine amount of damage
        //var rnd = new System.Random();
        //int rndDamage = rnd.Next(-10, 10);

        //Debug.Log(rndDamage);

        playerAnimator = FindAnimator();
        
        int damage;
        if (isSpecial)
        {
            if (playerUnit.type == enemyUnit.type)
            {
                damage = playerUnit.specialDamage;
                Debug.Log("special attack! very effective");
                dialogueText.text = playerUnit.unitName + " attempted to fix " + enemyUnit.unitName +
                                    ". " + " It is super successful!";
            }
            else
            {
                damage = 1;
                Debug.Log("not very effective");
                dialogueText.text = playerUnit.unitName + " attempted to fix " + enemyUnit.unitName +
                                    ". " + " It is not very effective!";
            }
        }
        else
        {
            damage = playerUnit.damage;
            Debug.Log("normal");
            dialogueText.text = playerUnit.unitName + " attempted to fix " + enemyUnit.unitName +
                                ". " + " It is somewhat successful!";
        }

        //Damage enemy
        bool isDead = enemyUnit.TakeDamage(damage);
        enemyHUD.SetHP(enemyUnit.currentHP, enemyUnit.index);

        Debug.Log("player attack animation start");
        playerAnimator.SetBool("IsAttacking", true);

        yield return new WaitForSeconds(2f);
        
        playerAnimator.SetBool("IsAttacking", false);
        Debug.Log("player attack animation end");

        //Check if the enemy is dead
        //Change state based on what happened
        if(isDead)
        {
            //End the battle
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            //enemy turn            
            state = BattleState.ENEMYTURN;
            enemyAnimator.SetBool("IsAttacking", true);
            particleEffect.SetActive(true);
            StartCoroutine(EnemyTurn());
        }     

    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";
        
        Debug.Log("enemy's turn and it has attacked");

        yield return new WaitForSeconds(1f);
        
        AttackAPlayer();

        bool allDead = players.AllDead();

        yield return new WaitForSeconds(3f);
        
        enemyAnimator.SetBool("IsAttacking", false);
        particleEffect.SetActive(false);

        if(allDead)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.PLAYERTURN;            
            PlayerTurn();
        }
    }

    void AttackAPlayer()
    {
        var rnd = new System.Random();
        int extraDamage = 0;
        int i = rnd.Next(0, players.characters.Count - 1);
        Unit tempPlayer = players.characters[i].GetComponent<Unit>();
        dialogueText.text = enemyUnit.unitName + " attacks " + tempPlayer.unitName + "!";
        if(tempPlayer.weakness == enemyUnit.type) 
        {
            Debug.Log("weakened");
            extraDamage = 5;
        }
        tempPlayer.TakeDamage(enemyUnit.damage + extraDamage);
        playerHUD.SetHP(tempPlayer.currentHP, tempPlayer.index);
    }

    IEnumerator EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
            StaticData.isBattleWon = true;
        }
        else if(state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
            StaticData.isBattleWon = false;
        }

        //UpdateTeamInformation();
        if (debugMode)                                                      // SCOTT EDIT - so the list remains to be pulled from in the Progress Scene & cleared there
        {
            StaticData.team.Clear(); //reset the team list
        }
        
        yield return new WaitForSeconds(2f);

        _OnBattleEnd?.Invoke(); // Invoke Event                             // SCOTT EDIT - So that functions can be called for integrating to two scene
        SceneManager.LoadScene(scene);
    }

    void UpdateTeamInformation()
    {
        bool condition = StaticData.team.Count == players.characters.Count;
        if (condition)
        {
            for (int i = 0; i < players.characters.Count; i++)
            {
                StaticData.team[i]._currentEnergy = players.characters[i].GetComponent<Unit>().currentHP;
                StaticData.team[i]._numDeaths = players.characters[i].GetComponent<Unit>().numDeaths;
                StaticData.team[i].moraleCount++;
                if (StaticData.team[i].moraleCount >= 2)
                {
                    StaticData.team[i].isExhausted = true;
                }
            }
        }
    }

    void PlayerTurn()
    {
        hasActed = false;
        dialogueText.text = "Choose an action for your team";
    }

    public IEnumerator PlayerHeal()
    {
        playerAnimator = FindAnimator();

        dialogueText.text = "You feel renewed strength!";
        playerAnimator.SetBool("IsHealing", true);

        yield return new WaitForSeconds(2f);
        
        playerAnimator.SetBool("IsHealing", false);
        
        //begin enemy turn
        state = BattleState.ENEMYTURN;
        enemyAnimator.SetBool("IsAttacking", true);
        particleEffect.SetActive(true);
        StartCoroutine(EnemyTurn());
    }

    private Animator FindAnimator()
    {
        for (int i=0; i < clones.Count; i++)
        {
            if (i == players.selectedCharacter)
            {
                return clones[i].GetComponent<Animator>();
            }
        }

        return null;
    }

    //execute player attack
    public void OnAttackButton()
    {
        if(state != BattleState.PLAYERTURN || hasActed)
            return;
   
        //if player dead, choose another player
        if(playerUnit.currentHP == 0)
        {
            dialogueText.text = "The " + playerUnit.unitName +" exhausted. Choose another role";
            return;
        }
        //Change button colour 
        hasActed = !hasActed;   
        StartCoroutine(PlayerAttack(false));
    }

    //execute healing action
    public void OnHealButton()
    {
        if(state != BattleState.PLAYERTURN || hasActed)
            return;

        //if player is dead choose another player
        if(playerUnit.currentHP == 0)
        {
            dialogueText.text = "The " + playerUnit.unitName + " exhausted. Choose another role";
            return;
        }
        //change button colour 
        hasActed = true;  
        isHealing = true;
        dialogueText.text = "Choose a team member to heal";       
        //StartCoroutine(PlayerHeal());
    }

    //execute player special attack
    public void OnSpecialAttackButton()
    {
        if(state != BattleState.PLAYERTURN || hasActed)
            return;
   
        //if player dead, choose another player
        if(playerUnit.currentHP == 0)
        {
            dialogueText.text = "The " + playerUnit.unitName + " exhausted. Choose another role";
            return;
        }
        //Change button colour 
        hasActed = !hasActed;   
        StartCoroutine(PlayerAttack(true));
    }
}
