using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash_Attack_Slow : MonoBehaviour
{
    bool stop; // 중복 실행 막기 위한
    public float stopTime; // 타격시 멈춤
    public float slowTime; // 슬로우
     
    public Transform shakeCamera; // 카메라 흔들림
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
