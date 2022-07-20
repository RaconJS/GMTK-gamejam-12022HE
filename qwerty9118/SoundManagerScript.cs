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
        //diceRoll,
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
    private static AudioSource audioSrc1;
    private static AudioSource audioSrc2;

    // Start is called before the first frame update
    void Start()
    {

        music = Resources.Load<AudioClip>("music");
        enemyWalk = Resources.Load<AudioClip>("enemyWalk");
        playerDamage = Resources.Load<AudioClip>("playerDamage");//not implemented
        playerJump = Resources.Load<AudioClip>("playerJump");
        playerWalk = Resources.Load<AudioClip>("playerWalk");
        bonusSound = Resources.Load<AudioClip>("bonusSound");//not implemented
        bowFire = Resources.Load<AudioClip>("bowFire");
        //diceRoll = Resources.Load<AudioClip>("diceRoll");
        diceRoll1 = Resources.Load<AudioClip>("diceRoll1");
        diceRoll2 = Resources.Load<AudioClip>("diceRoll2");
        diceRoll3 = Resources.Load<AudioClip>("diceRoll3");
        diceRoll4 = Resources.Load<AudioClip>("diceRoll4");
        diceRoll5 = Resources.Load<AudioClip>("diceRoll5");
        diceRoll6 = Resources.Load<AudioClip>("diceRoll6");
        enemyBite = Resources.Load<AudioClip>("enemyBite");//not implemented
        enemyGrunt = Resources.Load<AudioClip>("enemyGrunt");//not implemented
        pickUpItem = Resources.Load<AudioClip>("pickUpItem");
        swordAttack = Resources.Load<AudioClip>("swordAttack");

        audioSrc1 = gameObject.AddComponent<AudioSource>();
        audioSrc2 = gameObject.AddComponent<AudioSource>();
        audioSrc2.volume = 0.1f;

        StartCoroutine(loopMusic());
        StartCoroutine(loopPlayerWalk());
        StartCoroutine(loopEnemyWalk());

    }

    IEnumerator loopMusic()
    {
        new WaitForSeconds(3);
        while (true)
        {
            audioSrc2.PlayOneShot(music);
            yield return new WaitForSeconds(music.length - 0.5f);
        }
    }

    IEnumerator loopPlayerWalk()
    {
        while (true)
        {
            if (playerWalking)
            {
                playSound("playerWalk");
            }
            yield return new WaitForSeconds(playerWalk.length);
        }
    }

    IEnumerator loopEnemyWalk()
    {
        while (true)
        {
            if (enemyWalking)
            {
                playSound("enemyWalk");
            }
            yield return new WaitForSeconds(enemyWalk.length);
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
        playSound(clip, 0.5f);
    }

    public static void playSound(string clip, float volume)
    {

        audioSrc1.volume = volume;
        switch (clip)
        {
            case "enemyWalk":
                audioSrc1.PlayOneShot(enemyWalk);
                break;
            case "playerDamage":
                audioSrc1.PlayOneShot(playerDamage);
                break;
            case "playerJump":
                audioSrc1.PlayOneShot(playerJump);
                break;
            case "playerWalk":
                audioSrc1.PlayOneShot(playerWalk);
                break;
            case "bonusSound":
                audioSrc1.PlayOneShot(bonusSound);
                break;
            case "bowFire":
                audioSrc1.PlayOneShot(bowFire);
                break;
            //case "diceRoll":
            //    audioSrc1.PlayOneShot(diceRoll);
            //    break;
            case "diceRoll1":
                audioSrc1.PlayOneShot(diceRoll1);
                break;
            case "diceRoll2":
                audioSrc1.PlayOneShot(diceRoll2);
                break;
            case "diceRoll3":
                audioSrc1.PlayOneShot(diceRoll3);
                break;
            case "diceRoll4":
                audioSrc1.PlayOneShot(diceRoll4);
                break;
            case "diceRoll5":
                audioSrc1.PlayOneShot(diceRoll5);
                break;
            case "diceRoll6":
                audioSrc1.PlayOneShot(diceRoll6);
                break;
            case "enemyBite":
                audioSrc1.PlayOneShot(enemyBite);
                break;
            case "enemyGrunt":
                audioSrc1.PlayOneShot(enemyGrunt);
                break;
            case "pickUpItem":
                audioSrc1.PlayOneShot(pickUpItem);
                break;
            case "swordAttack":
                audioSrc1.PlayOneShot(swordAttack);
                break;
        }

    }
}
