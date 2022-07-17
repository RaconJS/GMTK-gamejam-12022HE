using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip
        enemyWalk,
        playerDamage,
        playerJump,
        playerWalk,
        bonusSound,
        bowFire,
        diceRoll,
        enemyBite,
        enemyGrunt,
        pickUpItem,
        swordAttack;
    private static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {

        enemyWalk = Resources.Load<AudioClip>("enemyWalk");
        playerDamage = Resources.Load<AudioClip>("playerDamage");
        playerJump = Resources.Load<AudioClip>("playerJump");
        playerWalk = Resources.Load<AudioClip>("playerWalk");
        bonusSound = Resources.Load<AudioClip>("bonusSound");
        bowFire = Resources.Load<AudioClip>("bowFire");
        diceRoll = Resources.Load<AudioClip>("diceRoll");
        enemyBite = Resources.Load<AudioClip>("enemyBite");
        enemyGrunt = Resources.Load<AudioClip>("enemyGrunt");
        pickUpItem = Resources.Load<AudioClip>("pickUpItem");
        swordAttack = Resources.Load<AudioClip>("swordAttack");

        audioSrc = GetComponent<AudioSource>();

    }

    public static void playSound(string clip)
    {

        switch (clip)
        {
            case "enemyWalk":
                audioSrc.PlayOneShot(enemyWalk);
                break;
            case "playerDamage":
                audioSrc.PlayOneShot(playerDamage);
                break;
            case "playerJump":
                audioSrc.PlayOneShot(playerJump);
                break;
            case "playerWalk":
                audioSrc.PlayOneShot(playerWalk);
                break;
            case "bonusSound":
                audioSrc.PlayOneShot(bonusSound);
                break;
            case "bowFire":
                audioSrc.PlayOneShot(bowFire);
                break;
            case "diceRoll":
                audioSrc.PlayOneShot(diceRoll);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
