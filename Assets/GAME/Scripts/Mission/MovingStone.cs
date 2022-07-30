using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStone : MonoBehaviour
{

    public GameObject stone;
    public GameObject target;
    public GameObject stoneMoveCamera;
    public float stoneSpeed;
    public ShakeCamera shakeCamera;

    Vector3 destination;
    public bool isStoneMove;
    BoxCollider boxCollider;

    [SerializeField]
    private string movingStone;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        destination = target.transform.position;
        isStoneMove = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Arrow_Attack") || other.CompareTag("Fire_Attack") || other.CompareTag("Ice_Attack"))
        {
            SoundManager.instance.PlaySE(movingStone);
            Debug.Log("Ãæµ¹");
            isStoneMove = true;
            stoneMoveCamera.SetActive(true);
            shakeCamera.OnshakeCamera(4f, 0.3f);
            Invoke("StoneMoveCameraStop", 6f);
            boxCollider.enabled = false;
        }
    }

    private void Update()
    {
        if(isStoneMove)
        {
            MoveStone();
        }      
    }

    void MoveStone()
    {
        stone.transform.position = Vector3.MoveTowards(stone.transform.position, destination, stoneSpeed);
    }
    void StoneMoveCameraStop()
    {
        stoneMoveCamera.SetActive(false);
    }
}

