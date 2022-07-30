using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    private static ShakeCamera instance;
    public  static ShakeCamera Instance => instance;

    private float shakeTime; // ī�޶� ��鸲 �ð�
    private float shakeIntensity; // ī�޶� ����

    public Controller cameraController;
    
    public ShakeCamera()
    {
        //�ڱ� �ڽ��� ������ static ���� ������ �����ؼ� �ܺ������� �����ϰ� �Ѵ�.
        instance = this;      
    }
   
    public void OnshakeCamera(float shakeTime = 1.0f, float shakeIntensity = 0.1f) // ����Ʈ�� ����
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
            //ó����ġ���� �������ȿ��� ī�޶� �̵�!
            transform.position = startPosition + Random.insideUnitSphere * shakeIntensity;

            shakeTime -= Time.deltaTime; // �ð��� ���� �����ش�

            yield return null;
        }
        transform.position = startPosition;

        cameraController.isCameraShake = false;
    }
}
