using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public GameObject subCamera;

    public float camSpeed;

    private void Update()
    {
       
        if(subCamera.transform.position == new Vector3(7,50,-60f))
        {
            subCamera.SetActive(false);
        }
        else
            subCamera.transform.position += Vector3.back * camSpeed;
    }

}
