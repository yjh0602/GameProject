using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    private static ShakeCamera instance;
    public  static ShakeCamera Instance => instance;

    private float shakeTime; // 카메라 흔들림 시간
    private float shakeIntensity; // 카메라 세기

    public Controller cameraController;
    
    public ShakeCamera()
    {
        //자기 자신의 정보를 static 형태 변수에 저장해서 외부접근을 가능하게 한다.
        instance = this;      
    }
   
    public void OnshakeCamera(float shakeTime = 1.0f, float shakeIntensity = 0.1f) // 디폴트값 설정
    {
        this.shakeTime = shakeTime;
        this.shakeIntensity = shakeIntensity;

        StopCoroutine("ShakeByPosition");
        StartCoroutine("ShakeByPosition");
    }

    private IEnumerator ShakeByPosition()
    {
        cameraController.isCameraShake = true;

        Vector3 startPosition = transform.position;

        while (shakeTime > 0.0f)
        {
            //처음위치에서 구범위안에서 카메라 이동!
            transform.position = startPosition + Random.insideUnitSphere * shakeIntensity;

            shakeTime -= Time.deltaTime; // 시간을 감소 시켜준다

            yield return null;
        }
        transform.position = startPosition;

        cameraController.isCameraShake = false;
    }
}
