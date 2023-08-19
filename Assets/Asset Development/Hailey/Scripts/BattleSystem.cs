using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public Unit playerUnit;
    public Unit enemyUnit;

    public TMP_Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    public bool hasActed = false;
    public bool isHealing = false;

    public CharacterSelection players;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetUpBattle());
    }

    void Update()
    {

    }

    //player and enemy spawn into battle
    IEnumerator SetUpBattle() 
    {       
        GameObject playerGO = playerPrefab;
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        playerHUD.characters = players.characters;
        enemyHUD.characters = new GameObject[] {enemyPrefab};

        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches.";

        playerHUD.SetHUD();
        enemyHUD.SetHUD();

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack(bool isSpecial)
    {
        //determine amount of damage
        var rnd = new System.Random();
        int rndDamage = rnd.Next(-10, 10);

        Debug.Log(rndDamage);
        
        int damage;
        if(isSpecial)
            damage = playerUnit.specialDamage;
        else    
            damage = playerUnit.damage;


        //Damage enemy
        bool isDead = enemyUnit.TakeDamage(damage + rndDamage);
        enemyHUD.SetHP(enemyUnit.currentHP, enemyUnit.unitLevel);
        dialogueText.text = "The attack is successful";

        yield return new WaitForSeconds(2f);

        //Check if the enemy is dead
        //Change state based on what happened
        if(isDead)
        {
            //End the battle
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            //enemy turn            
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }     

    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        AttackAPlayer();

        bool allDead = players.AllDead();

        yield return new WaitForSeconds(1f);

        if(allDead)
        {
            state = BattleState.LOST;
            EndBattle();
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
        int i = rnd.Next(0, players.characters.Length - 1);
        Unit tempPlayer = players.characters[i].GetComponent<Unit>();
        tempPlayer.TakeDamage(enemyUnit.damage);
        playerHUD.SetHP(tempPlayer.currentHP, tempPlayer.unitLevel);
    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
        }
        else if(state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated. ";
        }
    }

    void PlayerTurn()
    {
        hasActed = false;
        dialogueText.text = "Choose an action for " + playerUnit.unitName;
    }

    public IEnumerator PlayerHeal()
    {
        //playerUnit.Heal(5);

        //playerHUD.SetHP(playerUnit.currentHP, playerUnit.unitLevel);
        dialogueText.text = "You feel renewed strength!";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    //execute player attack
    public void OnAttackButton()
    {
        if(state != BattleState.PLAYERTURN || hasActed)
            return;
   
        //if player dead, choose another player
        if(playerUnit.currentHP == 0)
        {
            dialogueText.text = "Team member is exhausted. Choose another";
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
            dialogueText.text = "Team member is exhausted. Choose another";
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
            dialogueText.text = "Team member is exhausted. Choose another";
            return;
        }
        //Change button colour 
        hasActed = !hasActed;   
        StartCoroutine(PlayerAttack(true));
    }
}
