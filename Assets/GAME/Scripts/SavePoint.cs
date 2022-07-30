using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    static List<SavePoint> savePoints = new List<SavePoint>();
    static int currentCheckPoint;
    bool isCheck = false;
    int checkPointNumber;
    public GameObject player;
    public GameObject Effect;
    public Vector3 vectorPoint; // 체크포인트 값 저장
    public HealthUI playerHealth; // 플레이어 체력
    public bool isDie;
    SphereCollider Scollider;

    private string SaveSound = "SaveSound";

    private void Start()
    {
        Scollider = GetComponent<SphereCollider>();
        checkPointNumber = savePoints.Count;
        savePoints.Add(this);       
    }
    void Update()
    {
        if (!isCheck)
            return;

        if(playerHealth.currentHp <= 0)
        {
            isDie = true;
        }    
        if(isDie)
        {
            StartCoroutine(ResetPosition());
        }
    }
    private void OnTriggerEnter(Collider other)
    {     
        if(gameObject.tag == "Die")
        {          
            player.transform.position = new Vector3(160, player.transform.position.y * 1.5f, 45);
        }      
        else if (other.tag == "Player")
        {
            SoundManager.instance.PlaySE(SaveSound);
            currentCheckPoint = checkPointNumber;
            vectorPoint = player.transform.position;
            Effect.SetActive(false);
            isCheck = true;
            Scollider.enabled = false;
        }     
    }

    IEnumerator ResetPosition()
    {
        yield return new WaitForSeconds(1.8f);
        isDie = false;
        player.transform.position = savePoints[currentCheckPoint].vectorPoint;
        playerHealth.currentHp = playerHealth.maxHp;
    }   
}
