using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float damageTime; // 비활성화 되기까지 시간    

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
