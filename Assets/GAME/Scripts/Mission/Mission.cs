using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    public GameObject mission;
    Vibration vibration;
    

    private void Start()
    {
        vibration = mission.GetComponent<Vibration>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Arrow_Attack")
        {
            Debug.Log("Ãæµ¹");
            vibration.enabled = true;                             
            Destroy(other.gameObject);         
        }      
    }    
}
