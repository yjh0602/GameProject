using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Golem : MonoBehaviour
{ 

    public enum Type 
    {
       RED,
       BLUE,
       BOSS
    }
    public Type golemType;

    public int maxHealth;
    public int curHealth;
    public Transform target;
    public GameObject damageEffect;
    public GameObject[] fairy; // 요정
    public PlayerKeyDown arrowTypeChange; // 요정에 따라 화살 타입 추가
    public TalkManager storyManager; // 스토리 ui

    bool isChase;
    bool isAttack;
    bool isHit;
    bool isDie;

    NavMeshAgent navi;
    BoxCollider boxCollider;
    Animator animator;

    [SerializeField]
    private string hitGolem = "hitGolem";
       

    void Awake()
    {        
        navi = GetComponent<NavMeshAgent>();
        boxCollider = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
       
        navi.enabled = false;

    }

    void Update()
    {
        Targerting();
        if (navi.enabled)
        {
            navi.SetDestination(target.position);
            navi.isStopped = !isChase;
            if(!isAttack)
            {
                SoundManager.instance.PlayBg(2);
                ChaseStart();
            }
            if(isAttack)
            {
                ChaseEnd();
            }
        }     
    }
    void Targerting()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 30f, 1<< LayerMask.NameToLayer("Player"));

        if (colliders.Length > 0)
        {
            navi.enabled = true;
        }
        else if (colliders.Length == 0)
        {
            navi.enabled = false;
            animator.SetBool("isWalk", false);
        }
        AttackTargerting();
    }
    void AttackTargerting()
    {
        float targetRadius = 1.5f;
        float targetRange = 3f;

        RaycastHit[] rayHits =
          Physics.SphereCastAll(transform.position,
                                targetRadius,
                                transform.forward, targetRange,
                                LayerMask.GetMask("Player"));
        if (rayHits.Length > 0 && !isAttack)
        {
            Attack();
        }
    }
    void Attack()
    {
        isChase = false;
        isAttack = true;
        int randomAction = Random.Range(0, 2);
        switch(randomAction)
        {
            case 0:
                animator.SetBool("LeftAttack", true);
                break;
            case 1:
                animator.SetBool("RightAttack", true);
                break;
        }
                    
    }
    public void AttackEnd()
    {
        isChase = true;
        isAttack = false;
        isHit = false;
        animator.SetBool("LeftAttack", false);
        animator.SetBool("RightAttack", false);      
    }
    void ChaseStart()
    {
        isChase = true;
        animator.SetBool("isWalk", true); 
    }
    void ChaseEnd()
    {
        isChase = false;
        animator.SetBool("isWalk", false);
    }
   

    private void OnTriggerEnter(Collider other)
    {
        if (isDie) return;

        switch(golemType)
        {
            case Type.RED:               
            case Type.BLUE:
                if (other.tag == "Player_Attack")
                {
                    if (!isHit)
                    {
                        isHit = true;
                        animator.SetTrigger("doHit");
                        StartCoroutine(DamageEffect());
                        curHealth -= 10; // 하트를 지울예정
                        SoundManager.instance.PlaySE(hitGolem);
                    }
                    if (curHealth <= 0)
                    {                       
                        EnemyDead();
                    }
                }
                if (other.tag == "Arrow_Attack")
                {
                    if (curHealth <= 0)
                    {
                        EnemyDead();
                    }
                    isHit = true;                   
                    curHealth -= 5;
                    Destroy(other.gameObject);
                }
                if (other.tag == "Fire_Attack" || other.tag == "Ice_Attack")
                {
                    if (curHealth <= 0)
                    {
                        EnemyDead();
                    }
                    isHit = true;
                    curHealth -= 10;
                    Destroy(other.gameObject);
                }
                if (other.tag == "Parrying")
                {
                    animator.SetTrigger("doStun");
                }
                break;

            case Type.BOSS:
                if (other.tag == "Player_Attack")
                {
                    if (!isHit)
                    {
                        isHit = true;                       
                        StartCoroutine(DamageEffect());
                        curHealth -= 5; // 
                    }
                    if (curHealth <= 0)
                    {                       
                        EnemyDead();
                    }
                }
                if(other.tag == "Arrow_Attack")
                {
                    if (curHealth <= 0)
                    {
                        EnemyDead();
                    }
                    isHit = true;               
                    curHealth -= 5;
                    Destroy(other.gameObject, 0.5f);
                }
                if(other.tag == "Fire_Attack" || other.tag == "Ice_Attack")
                {
                    if (curHealth <= 0)
                    {
                        EnemyDead();
                    }
                    isHit = true;
                    curHealth -= 20;
                    Destroy(other.gameObject, 0.5f);
                }
                if (other.tag == "Parrying")
                {
                    animator.SetTrigger("doStun");
                }
                break;               
        }      
    }
    IEnumerator DamageEffect()
    {
        damageEffect.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        damageEffect.SetActive(false);
    }
    void EnemyDead()
    {
        isDie = true;
        isChase = false;
        isAttack = false;
        navi.enabled = false;
        boxCollider.enabled = false;
        animator.SetTrigger("doDie");       

        switch (golemType)
        {
            case Type.RED:
                fairy[0].SetActive(true);
                arrowTypeChange.fire_FairyOn = true;
                storyManager.Story(0);
                SoundManager.instance.PlayBg(1);
                break;
            case Type.BLUE:
                fairy[1].SetActive(true);
                arrowTypeChange.ice_FairyOn = true;
                arrowTypeChange.fire_FairyOn = false;
                storyManager.Story(1);
                SoundManager.instance.PlayBg(1);
                break;
            case Type.BOSS:
                storyManager.Story(2);
                SoundManager.instance.PlayBg(3);
                Cursor.lockState = CursorLockMode.None;
                break;
        }
    }

    public void EndGame()
    {        
        Application.Quit();
    }
}
