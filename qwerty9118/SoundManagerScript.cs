using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip
        music,
        enemyWalk,
        playerDamage,
        playerJump,
        playerWalk,
        bonusSound,
        bowFire,
        diceRoll,
        diceRoll1,
        diceRoll2,
        diceRoll3,
        diceRoll4,
        diceRoll5,
        diceRoll6,
        enemyBite,
        enemyGrunt,
        pickUpItem,
        swordAttack;
    public static bool
        enemyWalking,
        playerWalking;
    private static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {

        music = Resources.Load<AudioClip>("music");
        enemyWalk = Resources.Load<AudioClip>("enemyWalk");
        playerDamage = Resources.Load<AudioClip>("playerDamage");
        playerJump = Resources.Load<AudioClip>("playerJump");
        playerWalk = Resources.Load<AudioClip>("playerWalk");
        bonusSound = Resources.Load<AudioClip>("bonusSound");
        bowFire = Resources.Load<AudioClip>("bowFire");
        diceRoll = Resources.Load<AudioClip>("diceRoll");
        diceRoll1 = Resources.Load<AudioClip>("diceRoll1");
        diceRoll2 = Resources.Load<AudioClip>("diceRoll2");
        diceRoll3 = Resources.Load<AudioClip>("diceRoll3");
        diceRoll4 = Resources.Load<AudioClip>("diceRoll4");
        diceRoll5 = Resources.Load<AudioClip>("diceRoll5");
        diceRoll6 = Resources.Load<AudioClip>("diceRoll6");
        enemyBite = Resources.Load<AudioClip>("enemyBite");
        enemyGrunt = Resources.Load<AudioClip>("enemyGrunt");
        pickUpItem = Resources.Load<AudioClip>("pickUpItem");
        swordAttack = Resources.Load<AudioClip>("swordAttack");

        audioSrc = GetComponent<AudioSource>();

        StartCoroutine(loopMusic());
        StartCoroutine(loopPlayerWalk());
        StartCoroutine(loopEnemyWalk());

    }

    IEnumerator loopMusic()
    {
        new WaitForSeconds(3);
        while (true)
        {
            //audioSrc.PlayOneShot(music);
            yield return new WaitForSeconds(music.length - 0.5f);
        }
    }

    IEnumerator loopPlayerWalk()
    {
        while (true)
        {
            if (playerWalking)
            {
                audioSrc.PlayOneShot(playerWalk);
                yield return new WaitForSeconds(playerWalk.length - 0.5f);
            }
        }
    }

    IEnumerator loopEnemyWalk()
    {
        while (true)
        {
            if (enemyWalking)
            {
                audioSrc.PlayOneShot(enemyWalk);
                yield return new WaitForSeconds(enemyWalk.length - 0.5f);
            }
        }
    }

    public static void setWalking(bool isPlayer, bool state)
    {
        if (isPlayer)//type is player
        {
            playerWalking = state;
        }
        else//type is enemy
        {
            enemyWalking = state;
        }
    }

    public static void playSound(string clip)
    {

        switch (clip)
        {
            //case "enemyWalk":
                //audioSrc.PlayOneShot(enemyWalk);
                //break;
            case "playerDamage":
                audioSrc.PlayOneShot(playerDamage);
                break;
            case "playerJump":
                audioSrc.PlayOneShot(playerJump);
                break;
            //case "playerWalk":
                //audioSrc.PlayOneShot(playerWalk);
                //break;
            case "bonusSound":
                audioSrc.PlayOneShot(bonusSound);
                break;
            case "bowFire":
                audioSrc.PlayOneShot(bowFire);
                break;
            case "diceRoll":
                audioSrc.PlayOneShot(diceRoll);
                break;
            case "diceRoll1":
                audioSrc.PlayOneShot(diceRoll1);
                break;
            case "diceRoll2":
                audioSrc.PlayOneShot(diceRoll2);
                break;
            case "diceRoll3":
                audioSrc.PlayOneShot(diceRoll3);
                break;
            case "diceRoll4":
                audioSrc.PlayOneShot(diceRoll4);
                break;
            case "diceRoll5":
                audioSrc.PlayOneShot(diceRoll5);
                break;
            case "diceRoll6":
                audioSrc.PlayOneShot(diceRoll6);
                break;
            case "enemyBite":
                audioSrc.PlayOneShot(enemyBite);
                break;
            case "enemyGrunt":
                audioSrc.PlayOneShot(enemyGrunt);
                break;
            case "pickUpItem":
                audioSrc.PlayOneShot(pickUpItem);
                break;
            case "swordAttack":
                audioSrc.PlayOneShot(swordAttack);
                break;
        }

    }
}
