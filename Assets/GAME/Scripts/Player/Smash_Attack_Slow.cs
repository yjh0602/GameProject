using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash_Attack_Slow : MonoBehaviour
{
    bool stop; // �ߺ� ���� ���� ����
    public float stopTime; // Ÿ�ݽ� ����
    public float slowTime; // ���ο�
     
    public Transform shakeCamera; // ī�޶� ��鸲
    public Vector3 shake;


    public void StopTime()
    {
        if(!stop)
        {
            stop = true;            
            Time.timeScale = 0;

            StartCoroutine("ReturnTime");
        }
    }

    IEnumerator ReturnTime()
    {
        shakeCamera.localPosition = shake;
        yield return new WaitForSecondsRealtime(stopTime);
        Time.timeScale = 0.02f;
        

        yield return new WaitForSecondsRealtime(slowTime);

        Time.timeScale = 1;

        shakeCamera.localPosition = new Vector3(-0.23f, 0.64f, -3.48f);
        stop = false;
    }
}
