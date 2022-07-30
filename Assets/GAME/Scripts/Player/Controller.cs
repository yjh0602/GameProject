using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Camera")]   
    public Transform camCentral; // 카메라 중심
    public Transform cam;             // 카메라
    public float cameraSpeed; // 카메라 이동속도
    public bool isCameraShake;
    float mouseX;
    float mouseY;
    float wheel; // 마우스 휠

    [Header("Player")]
    [SerializeField] Transform playerAxis;
    [SerializeField] Transform player;
    [SerializeField] float playerSpeed;
    [SerializeField] Vector3 movement;   

    public int camStatus;    

    public bool isMove;
    public bool isDodge;
        
    void Start()
    {       
        camStatus = 1;
        isMove = true;
        wheel = -2;
        mouseY = 1;
    }

    void Update()
    {
        if (isCameraShake) return;
        CamMove(camStatus);
        Zoom();       
        PlayerMove();      
        Dodge();
        
    }    
    void Zoom()
    {
        wheel += Input.GetAxis("Mouse ScrollWheel") * 2;
        if(wheel >= 2)
        { wheel = 2; }
        if(wheel <= -1)
        { wheel = -1; }

        cam.localPosition = new Vector3(0, 0, wheel);
    }

    public void CamMove(int camStatus)
    {
        if (camStatus == 1)
        {
            mouseX += Input.GetAxis("Mouse X");
            mouseY += Input.GetAxis("Mouse Y") * -1;

            if (mouseY > 5)
            { mouseY = 5; }
            if (mouseY < -1)
            { mouseY = -1; }
            camCentral.rotation = Quaternion.Euler(new Vector3(
            camCentral.rotation.x + mouseY,
            camCentral.rotation.y + mouseX, 0) * cameraSpeed);
        }
        else if(camStatus == 2)
        {           
            mouseX += Input.GetAxis("Mouse X");
            mouseY += Input.GetAxis("Mouse Y") * -1;

            if (mouseY > 5)
            { mouseY = 5; }
            if (mouseY < -3)
            { mouseY = -3; }
            camCentral.rotation = Quaternion.Euler(new Vector3(
            camCentral.rotation.x + mouseY,
            camCentral.rotation.y + mouseX, 0) * cameraSpeed);       
        }

    }

    void PlayerMove()
    {
        if(isMove)
        {
            movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            if (movement != Vector3.zero)
            {
                playerAxis.rotation = Quaternion.Euler(new Vector3(
                    0, camCentral.rotation.y + mouseX, 0) * cameraSpeed);

                playerAxis.Translate(movement * Time.deltaTime * playerSpeed);

                player.localRotation = Quaternion.Slerp(player.localRotation,
                    Quaternion.LookRotation(movement), 5 * Time.deltaTime);

                player.GetComponent<Animator>().SetBool("isWalk", true);
            }
            if (movement == Vector3.zero)
            {
                player.GetComponent<Animator>().SetBool("isWalk", false);
            }
            camCentral.position = new Vector3(player.position.x
            , player.position.y + 0.5f, player.position.z);
        }
    }
    void Talk()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            //talk.Talk()
        }
    }
    void Dodge()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && !isDodge)
        {
            //SoundManager.instance.PlaySE(PlayerJump);
            isDodge = true;           
            playerSpeed += 2;
            player.GetComponent<Animator>().SetBool("isDodge", true);

            StartCoroutine(DodgeOut());
        }
    }
    IEnumerator DodgeOut()
    { 
        yield return new WaitForSeconds(0.5f);
        isDodge = false;       
        playerSpeed -= 2;
        player.GetComponent<Animator>().SetBool("isDodge", false);
    } 
}
