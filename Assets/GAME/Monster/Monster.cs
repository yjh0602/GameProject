using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsPlayer;
    public Animator MonsterAnimator;

    //patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attack
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    bool isHit;
    bool isDie;
    public int maxHealth, curHealth;
    BoxCollider hitBox;
    Vector3 MonsterVector;
    Vector3 PlayerVector;

    //죽음 이펙트
    public GameObject DieEffect;
    //죽음 사운드    
    private string MonsterHit = "MonsterHit";
    private string MonsterDie = "MonsterDie";

    private void Awake()
    {
        hitBox = gameObject.GetComponent<BoxCollider>();
        player = GameObject.Find("Player_meliodas").transform;
        agent = GetComponent<NavMeshAgent>();
        DieEffect.SetActive(false);        
    }
    private void FixedUpdate()
    {
        MonsterVector = transform.position;
        PlayerVector = player.position - Vector3.forward;
    }

    private void Update()
    {       
        if(!isDie)
        {
            // 공격 범위 체크
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }
    }

    void Patroling()
    {
        if(!walkPointSet && !isDie)
        {
            MonsterAnimator.SetBool("isMove", false);
            MonsterAnimator.SetBool("isRun", false);
            SearchWalkPoint();
        }

        if(walkPointSet)
        {
            Vector3 distanceToWalkPoint = transform.position - walkPoint;
            // Walkpoint reached
            if (distanceToWalkPoint.magnitude <= 0.1f)
            {
                StartCoroutine(StopMove());
            }
        }    
    }

    IEnumerator StopMove()
    {
        MonsterAnimator.SetBool("isMove", false);
        MonsterAnimator.SetBool("isRun", false);
        yield return new WaitForSeconds(2f);
        walkPointSet = false;
    }
    void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);    
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        agent.SetDestination(walkPoint);
        MonsterAnimator.SetBool("isMove", true);
        walkPointSet = true;
    }

    void ChasePlayer()
    {
        MonsterAnimator.SetBool("isRun", true);
        agent.SetDestination(PlayerVector);     
    }
    void AttackPlayer()
    {              
        if(!alreadyAttacked)
        {
            transform.LookAt(player);
            MonsterAnimator.SetBool("isRun", false);
            MonsterAnimator.SetBool("isMove", false);
            MonsterAnimator.SetBool("isAttack", true);  
            alreadyAttacked = true;
            isDie = true;
        }
    } 

    public void AttackEnd()
    {       
        MonsterAnimator.SetBool("isAttack",false); 
        alreadyAttacked = false;
        isDie = false;
    }


    private void OnTriggerEnter(Collider other)
    {               
        if (other.tag == "Player_Attack")
        {
            if(curHealth <= 0 && !isHit)
            {               
                DestroyEnemy();
                return;
            }          
            if (curHealth > 50 && !isHit)
            {
                SoundManager.instance.PlaySE(MonsterHit);
                isHit = true;
                StartCoroutine(HitStop("doHit", 0.1f));
                curHealth -= 50;
                Debug.Log("일반");               
            }
            if (curHealth <= 50 && !isHit)
            {
                SoundManager.instance.PlaySE(MonsterHit);
                StartCoroutine(HitStop("doDown", 3f));
                curHealth -= 50;
            }
        }
        if(other.tag == "Arrow_Attack")
        {
            if (curHealth <= 0 && !isHit)
            {
                DestroyEnemy();
                Destroy(other.gameObject);
                return;
            }
            if (curHealth > 50 && !isHit)
            {
                SoundManager.instance.PlaySE(MonsterHit);
                isHit = true;
                StartCoroutine(HitStop("doHit", 0.1f));
                curHealth -= 25;
                Destroy(other.gameObject);

            }
            if (curHealth <= 50 && !isHit)
            {
                SoundManager.instance.PlaySE(MonsterHit);
                StartCoroutine(HitStop("doDown", 3f));
                curHealth -= 50;
                Destroy(other.gameObject);               
            }

        }
        if(other.tag == "Fire_Attack" || other.tag == "Ice_Attack")
        {
            if (curHealth <= 0 && !isHit)
            {
                DestroyEnemy();
                Destroy(other.gameObject);
                return;
            }
            if (curHealth > 0 && !isHit)
            {
                SoundManager.instance.PlaySE(MonsterHit);
                StartCoroutine(HitStop("doDown", 3f));
                curHealth -= 100;
                Destroy(other.gameObject);
                return;
            }           
        }

        if (other.tag == "Player")
        {
            transform.position = MonsterVector;
        }
    }

    private void DestroyEnemy()
    {
        SoundManager.instance.PlaySE(MonsterDie);
        isDie = true;
        MonsterAnimator.SetTrigger("doDie");       
        agent.enabled = false;
        hitBox.enabled = false;

        // 콜라이더 꺼주기
        Destroy(gameObject, 2f);
        DieEffect.SetActive(true);
    }

    IEnumerator HitStop(string name , float value)
    {
        MonsterAnimator.SetTrigger(name);
        
        isDie = true;
        isHit = true;
        hitBox.enabled = false;       
        agent.enabled = false;      

        yield return new WaitForSeconds(value);
      
        isHit = false;
        hitBox.enabled = true;
        agent.enabled = true;       
       
    }  
}
