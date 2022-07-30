using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{

    public HealthUI playerHp;

    public Animator playerAnimator;  
    public Smash_Attack_Slow  HitStop;
    BoxCollider hitBox;  
    public GameObject[] Effect;

    bool isDie;
    bool isParrying;

   
    private string Defense = "Defense";   
    private string Parrying = "Parrying";
    private string PlayerHit = "PlayerHit";
    private string PlayerDie = "PlayerDie";




    private void Start()
    {
        hitBox = GetComponent<BoxCollider>();
    }
    private void Update()
    {
        if (playerHp.currentHp == 0)
        {
            hitBox.enabled = false;
            playerAnimator.Play("PlayerDie");
            isDie = true;                   
        }
        if(playerHp.currentHp == 10)
        {
            hitBox.enabled = true;
        }      
    }

    private void OnTriggerEnter(Collider other)
    {      
        if(other.tag == "Enemy_Attack")
        {
            if(gameObject.tag == "Player_HitBox")
            {
                SoundManager.instance.PlaySE(PlayerHit);
                playerHp.currentHp -= 1;
                playerAnimator.Play("PlayerHit");               
            }
            else if (gameObject.tag == "Defense")
            {
               StartCoroutine(DefenseEffect());
               SoundManager.instance.PlaySE(Defense);
            }
            else if (gameObject.tag == "Parrying" && !isParrying)
            {
                StartCoroutine(HitBoxON());
                StartCoroutine(DefenseEffect());
                playerAnimator.Play("Parrying");
                HitStop.StopTime();
                SoundManager.instance.PlaySE(Parrying);
            }
        }
        if (isDie)
        {
            SoundManager.instance.PlaySE(PlayerDie);
            isDie = false;
        }
    }   
    IEnumerator HitBoxON()
    {
        isParrying = true;
        hitBox.enabled = false;
        playerHp.currentHp += 1;
        yield return new WaitForSeconds(2.5f);
        hitBox.enabled = true;
        isParrying = false;
    }

    IEnumerator DefenseEffect()
    {
        Effect[0].SetActive(true);
        yield return new WaitForSeconds(1f);
        Effect[0].SetActive(false);
    }
}
