using System;
using System.Collections;
using System.Collections.Generic;
using DigitalRuby.SoundManagerNamespace;
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
    public bool isDebuggingToConsole = false;
    
    [Header("Characters")]
    public GameObject enemyPrefab;
    private GameObject playerPrefab;

    public List<GameObject> enemies;
    
    public Unit playerUnit;
    public Unit enemyUnit;

    public GameObject enemyGO;
    
    public CharacterSelection players;
    [FormerlySerializedAs("playersGO")] public List<GameObject> clones = new List<GameObject>();
    
    //Enemy and audio indices
    private int LEAKY_PIPE = 0;
    private int LIVE_WIRE = 1;
    private int CINDER_BLOCK = 2;
    private int SCAFFOLDING = 3;
    private int ENEMY_ATTACK = 0;

    private int FAILED_ATTACK = 4;
    private int PLAYER_ATTACK = 5;
    private int PLAYER_DAMAGE = 6;
    private int PLAYER_HEAL = 7;
    private int PLAYER_SLEEP = 8;
    private int SUCCESS_ATTACK = 9;

    [Header("Audio")]
    public BattleAudioManager BAM;

    [Header("BattleHUD")]
    public Transform[] battlePos;
    public GameObject bridge;
    
    public Transform[] playerBattleStations;
    public Transform enemyBattleStation;
    
    public TMP_Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    [Header("Battle States")]
    public BattleState state;

    public bool hasActed = false;
    public bool isHealing = false;
    private int attackIndex = 0;

    public string scene;
    
    [FormerlySerializedAs("waterEffect")]
    [Header("Battle Animations")]
    [SerializeField] private GameObject particleEffect;
    
    private Animator enemyAnimator;
    public Animator playerAnimator;

    [Header("Events")] 
    public UnityEvent _OnBattleEnd;

    public bool fleeing = false;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        
        if (!debugMode)
        { 
            players.characters = GameManager.instance.battleTeam; 
            SetBattleState();
        }

        //Set battle position, roles and enemies
        playerPrefab = players.characters[0];
        SetUpCharacters();
        
        //start the battle
        StartCoroutine(SetUpBattle());
    }

    void SetBattleState()
    {
        if (StaticData.enemyType == EnemyTypes.error)
        {
            Debug.LogError("error");
        }
        else if (StaticData.enemyType == EnemyTypes.Water)
        {
            enemyPrefab = enemies[LEAKY_PIPE];
            ENEMY_ATTACK = LEAKY_PIPE;
        }
        else if (StaticData.enemyType == EnemyTypes.Electric)
        {
            enemyPrefab = enemies[LIVE_WIRE];
            ENEMY_ATTACK = LIVE_WIRE;
        }
        else if (StaticData.enemyType == EnemyTypes.Practical)
        {
            enemyPrefab = enemies[CINDER_BLOCK];
            ENEMY_ATTACK = CINDER_BLOCK;
        }
        else if (StaticData.enemyType == EnemyTypes.Planning)
        {
            enemyPrefab = enemies[SCAFFOLDING];
            ENEMY_ATTACK = SCAFFOLDING;
        }
        
        
        if (bridge != null)
        {
            bridge.transform.position = battlePos[StaticData.battlePosition].position;
        }
    }

    void SetUpCharacters()
    {
        GameObject playerGO = playerPrefab;
        playerUnit = playerGO.GetComponent<Unit>();

        for(int i=0; i < players.characters.Count; i++)
        {
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
    }

    //player and enemy spawn into battle
    IEnumerator SetUpBattle() 
    {       
        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    //handles player's attack
    IEnumerator PlayerAttack(bool isSpecial)
    {
        playerAnimator = FindAnimator();
        
        int damage;
        string newText;
        int playdmg = 0;
        if (isSpecial)
        {
            if (playerUnit.type == enemyUnit.type)
            {
                damage = playerUnit.specialDamage;
                if(isDebuggingToConsole) Debug.Log("special attack! very effective");
                dialogueText.text = playerUnit.unitName + " attempted to fix " + enemyUnit.unitName +
                                    ". " + " It is super successful!";
                BAM.PlaySound(SUCCESS_ATTACK);
            }
            else
            {
                // - Team Member - 
                damage = 1;
                if (isDebuggingToConsole) Debug.Log("not very effective");
                dialogueText.text = playerUnit._teamMemberName + " attempted to fix " + enemyUnit._teamMemberName +
                                    ". " + " It is not very effective!";
                BAM.PlaySound(FAILED_ATTACK);
            }

            playdmg = 10;           // Energy drain from Team Member
            newText = "Wow that was tiring!";
            BAM.PlaySound(PLAYER_HEAL);
            Debug.Log("tiring sound played");
        }
        else
        {
            damage = playerUnit.damage;
            if (isDebuggingToConsole) Debug.Log("normal");
            dialogueText.text = playerUnit.unitName + " attempted to fix " + enemyUnit.unitName +
                                ". " + " It is somewhat successful!";
            BAM.PlaySound(PLAYER_ATTACK);
            newText = dialogueText.text;
        }

        //energy depleted
        if (isDebuggingToConsole) Debug.Log("take damage");
        playerUnit.TakeDamage(playdmg);
        playerHUD.SetHP(playerUnit.currentHP, playerUnit.index);

        //Damage enemy
        bool isDead = enemyUnit.TakeDamage(damage);
        enemyHUD.SetHP(enemyUnit.currentHP, enemyUnit.index);
        
        //attack animation called
        playerAnimator.SetBool("IsAttacking", true);

        yield return new WaitForSeconds(2f);
        
        playerAnimator.SetBool("IsAttacking", false);
        
        dialogueText.text = newText;

        yield return new WaitForSeconds(2f);

        //Check if the enemy is dead
        //Change state based on what happened
        if(isDead)
        {
            //End the battle
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else if (fleeing)
        {
            //End the battle
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else
        {
            //enemy turn            
            state = BattleState.ENEMYTURN;
            
            //enemy animation called
            enemyAnimator.SetBool("IsAttacking", true);
            particleEffect.GetComponent<ActivateParticleEffect>().activate = true;
            
            StartCoroutine(EnemyTurn());
        }     

    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = "It's " + enemyUnit.unitName + "'s turn!";

        if (isDebuggingToConsole) Debug.Log("enemy's turn and it has attacked");
        BAM.PlaySound(ENEMY_ATTACK);

        yield return new WaitForSeconds(2f);
        
        AttackAPlayer();

        bool allDead = players.AllDead();
        
        BAM.PlaySound(PLAYER_DAMAGE);

        yield return new WaitForSeconds(3f);
        
        enemyAnimator.SetBool("IsAttacking", false); //animation turned off
        particleEffect.GetComponent<ActivateParticleEffect>().activate = false;

        if(allDead || fleeing)
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
        //decide which player to attack
        var rnd = new System.Random();
        int j = rnd.Next(0, 1);
        Unit tempPlayer;
        int i;
        if (j < 0.5)
        {
            tempPlayer = players.characters[attackIndex].GetComponent<Unit>();
            attackIndex++;
            if (attackIndex == players.characters.Count)
            {
                attackIndex = 0;
            }

            i = attackIndex;
            Debug.Log("choosen");
        }
        else
        {
            var rnd2 = new System.Random();
            i = rnd2.Next(0, players.characters.Count - 1);
            tempPlayer = players.characters[i].GetComponent<Unit>();
            Debug.Log("random");
        }
        
        dialogueText.text = enemyUnit.unitName + " hurts " + tempPlayer.unitName + "!";
        
        int extraDamage = 0;
        if(tempPlayer.weakness == enemyUnit.type) 
        {
            if (isDebuggingToConsole) Debug.Log("weakened");
            extraDamage = 5;
        }
        tempPlayer.TakeDamage(enemyUnit.damage + extraDamage);
        playerHUD.SetHP(tempPlayer.currentHP, i);
        if (isDebuggingToConsole) Debug.Log("attacked index: " + i);
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

        if (debugMode)                                                      // SCOTT EDIT - so the list remains to be pulled from in the Progress Scene & cleared there
        {
            StaticData.team.Clear(); //reset the team list
        }
        
        _OnBattleEnd?.Invoke(); // Invoke Event                             // SCOTT EDIT - So that functions can be called for integrating to two scene

        yield return new WaitForSeconds(5f);                                // SCOTT EDIT - changed to 5 seconds to give a change for the player to read the Success / Lose Info   
        SceneManager.LoadScene(scene);
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
        
        BAM.PlaySound(SUCCESS_ATTACK);
        
        //healing animation called
        playerAnimator.SetBool("IsHealing", true);

        yield return new WaitForSeconds(2f);
        
        playerAnimator.SetBool("IsHealing", false);

        DepleteMorale();

        dialogueText.text = "Woah that was tiring";
        yield return new WaitForSeconds(2f);
        
        //begin enemy turn
        state = BattleState.ENEMYTURN;
        enemyAnimator.SetBool("IsAttacking", true);
        particleEffect.GetComponent<ActivateParticleEffect>().activate = true;
        StartCoroutine(EnemyTurn());
    }

    private void DepleteMorale()
    {
        for (int i = 0; i < StaticData.team.Count; i++)
        {
            TeamMemberTransfer_Data role = StaticData.team[i];
            if (playerUnit.type.ToString() == role._classType.ToString())
            {
                role.moraleCount += 5;
                if (isDebuggingToConsole) Debug.Log("morale: " + role.moraleCount);
            }
        }
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
            BAM.PlaySound(PLAYER_HEAL);
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
            BAM.PlaySound(PLAYER_HEAL);
            return;
        }
        //change button colour 
        hasActed = true;  
        isHealing = true;
        dialogueText.text = "Choose a team member to heal";    
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
            BAM.PlaySound(PLAYER_HEAL);
            return;
        }
        //Change button colour 
        hasActed = !hasActed;   
        StartCoroutine(PlayerAttack(true));
    }

    public void fn_OverrideDebugMode() => debugMode = false;
    public void fn_LoadNextScene() => SceneManager.LoadScene(scene);

}
