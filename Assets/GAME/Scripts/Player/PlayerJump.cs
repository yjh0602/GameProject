using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpPower;
    public int jumpCount;
    public int maxJumpCount;
    public Controller playerController;

    public Rigidbody playerRigid;
    Animator playerAnim;
    PlayerKeyDown playerAttack;
     
    public bool isJumping;

    private string PlayerJumpSound = "PlayerJump";

    void Start()
    {
        playerAttack = GetComponent<PlayerKeyDown>();
        playerRigid = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();       
    }

    // Update is called once per frame
    void Update()
    {
        if(!(playerAttack.isAttack))
        {
            Jump();       
        }                   
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (jumpCount > 0))
        {          
            jumpCount -= 1; // 점프시 마이너스
            isJumping = true;

            if (isJumping && jumpCount == 1)
            {
                playerRigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                playerAnim.SetBool("isJump", true);
                SoundManager.instance.PlaySE(PlayerJumpSound);
            }                       
            else if(jumpCount == 0)
            {
                playerRigid.AddForce(Vector3.up * jumpPower * 2f , ForceMode.Impulse);
                playerAnim.SetBool("isDoubleJump", true);
                SoundManager.instance.PlaySE(PlayerJumpSound);
            }
        }
        //VelocityChange 질량을 무시하고, 리지드바디(rigidbody)에 속도 변화를 짧은 순간에 적용할 경우에 사용.
    }

    #region 점프 충돌
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {         
            isJumping = false;
            playerAnim.SetBool("isJump", false);
            playerAnim.SetBool("isDoubleJump", false);
            jumpCount = maxJumpCount; // reset           
        }
    }
    #endregion

    public void MoveFalse()
    {
        playerController.isMove = false;
        playerController.isDodge = false;
    }
    public void MoveTrue()
    {
        playerController.isMove = true;
        playerController.isDodge = true;
    }
}
