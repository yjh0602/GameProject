using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float damageTime; // ��Ȱ��ȭ �Ǳ���� �ð�    

    private void OnEnable()
    {
        CancelInvoke();
        Invoke("Disable", damageTime); 
    }
    void Disable()
    {
        gameObject.SetActive(false);
    }
}
