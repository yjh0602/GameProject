using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeeleAttack : MonoBehaviour
{   
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy")) // ���̶��
        {
            ShakeCamera.Instance.OnshakeCamera(0.1f, 0.3f);           
        }   
    }
}
