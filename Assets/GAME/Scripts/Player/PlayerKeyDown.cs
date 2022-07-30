using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerKeyDown : MonoBehaviour
{
    public Animator playerAnim;
    public Controller playerController;
    public GameObject hitBoxChange;
    public int comboStep;

    bool comboPossible;
    bool inputSmash;

    public bool meeleWeapon;
    public bool isAim;
    public bool isAttack;

    [SerializeField] bool isMeele;
    [SerializeField] bool isRange;
    
    PlayerJump PlayerJump;
    

    //��ĵ npc
    public TalkManager talkManager;
    public Text TalkText;
    
    //ȭ�� ����
    [SerializeField] GameObject zoomCamera;
    [SerializeField] GameObject zoomTarget;
    [SerializeField] GameObject mainTarget;
    public float cameraSpeed;

    //�տ� �޸� ����
    public GameObject HandArrow;                     
    [SerializeField] GameObject Bow;                
    [SerializeField] GameObject MasterSword;        
    [SerializeField] GameObject Shield;

    // � �޸� ���� ����
    public GameObject[] unequip;

    //ȭ�� ����Ʈ
    public GameObject[] ArrowEffect;
    public Arrow arrow;
    public int arrowNumber;

    //���콺 Ŀ��
    [SerializeField] GameObject CrossHair;
    public bool isLock = true;

    // ������ �ֳ����� Ȯ��    
    public bool fire_FairyOn;
    public bool ice_FairyOn;

    // arrow ui
    public Image[] arrowUi;

    //�ʿ��� �����̸�
    [SerializeField]
    private string Attack;
    [SerializeField]
    private string S_Attack;
    [SerializeField]
    private string BowPull = "BowPull";
   

    private void Start()
    {
        CrossHair.SetActive(false);        
        zoomCamera.SetActive(false);       
        PlayerJump = GetComponent<PlayerJump>();
        Cursor.lockState = CursorLockMode.Locked;

    }
    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape))
       {
           if(isLock)
           {
               Cursor.lockState = CursorLockMode.None;
               isLock = false;
           }
           else
           {
               Cursor.lockState = CursorLockMode.Locked;
               isLock = true;             
           }                              
       }
                           
        if (!PlayerJump.isJumping && isLock)
        {         
            PlayerInput();           
        }
        talking();
    }
    void talking()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f, 1 << LayerMask.NameToLayer("Npc"));

        if(colliders.Length == 1 ) // npc == layer.7
        {
            TalkText.enabled = true;
            if (Input.GetKeyDown(KeyCode.F))
            {
                talkManager.Talk(colliders[0].gameObject.name);
                TalkText.enabled = false;
            }            
        }
        if (colliders.Length == 0)
        {
            talkManager.talkPanel.SetActive(false);
            TalkText.enabled = false;
        }
    }
    
    void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !isMeele)
        {
            playerAnim.Play("Meele_Unequip");
            MeeleSwap();           
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !isRange)
        {
            playerAnim.Play("Range_Unequip");
            RangeSwap();         
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(fire_FairyOn)
            {
                arrowNumber += 1;
                if (arrowNumber > 1)
                {
                    arrowNumber = 0;
                }
            }
            if(ice_FairyOn)
            {
                arrowNumber += 1;
                if (arrowNumber > 2)
                {
                    arrowNumber = 0;
                }
            }
            ArrowType(arrowNumber);

            #region arrowUi
            if (arrowNumber == 0)
            {
                arrowUi[0].enabled = true;
                arrowUi[1].enabled = false;
                arrowUi[2].enabled = false;

            }
            else if(arrowNumber == 1)
            {
                arrowUi[0].enabled = false;
                arrowUi[1].enabled = true;
                arrowUi[2].enabled = false;
            }
            else if(arrowNumber == 2)
            {
                arrowUi[0].enabled = false;
                arrowUi[1].enabled = false;
                arrowUi[2].enabled = true;
            }
            #endregion
        }

        if (meeleWeapon)
        {
            MeleeWeapon();
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                playerAnim.Play("Defence");               
                playerController.isMove = false;
            }
        }
        else
        {
            RangedWeapon();
        }
    }

    void MeleeWeapon()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NormalAttack();
            playerController.isMove = false;
            isAttack = true;
        }
        if (Input.GetMouseButtonDown(1) && !(playerController.isMove))
        {
            SmashAttack();
            playerController.isMove = false;
            isAttack = true;
        }
    }
    void RangedWeapon()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            SoundManager.instance.PlaySE(BowPull);
        }
        if (Input.GetButton("Fire1"))
        {
            playerAnim.SetBool("isAim", true);          
            HandArrow.SetActive(true);
            playerController.isMove = false;         
            isAim = true;
            isAttack = true;
            CameraZoomIn();
            CrossHair.SetActive(true);
            ArrowEffect[0].SetActive(true);
            playerController.camStatus = 2;                       
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            playerAnim.SetBool("isShoot", true);
            playerAnim.SetBool("isAim", false);           
            playerController.isMove = true;
            isAim = false;
            Invoke("ArrowAttackOff", 0.2f);            
            CrossHair.SetActive(false);
            ArrowEffect[0].SetActive(false);
        }
        else
        {
            CameraZoomOut();
            playerAnim.SetBool("isShoot", false);
        }
    }

    public void ArrowType(int arrowNum)
    {      
      switch(arrowNum)
        {
            case 0:
                arrow.arrowType[0].SetActive(true);
                arrow.arrowType[1].SetActive(false);
                arrow.arrowType[2].SetActive(false);
                break;
            case 1:
                arrow.arrowType[1].SetActive(true);
                arrow.arrowType[0].SetActive(false);
                arrow.arrowType[2].SetActive(false);
                break;
            case 2:
                arrow.arrowType[2].SetActive(true);
                arrow.arrowType[0].SetActive(false);
                arrow.arrowType[1].SetActive(false);
                break;            
        }
    }
    private void ChangeTag(string Tag) // �и��� ���潺�� ���� �±� ����
    {
        hitBoxChange.tag = Tag;
    }

    public void ComboPossible() // ���� �޺� üũ
    {
        comboPossible = true;
    }
    void CameraZoomIn()
    {
        zoomCamera.SetActive(true);
        zoomCamera.transform.position = Vector3.Lerp(zoomCamera.transform.position,
                                                     zoomTarget.transform.position,
                                                     cameraSpeed * 2.5f);
    }
    void CameraZoomOut() // �� �ƿ��� ������ õõ�� 
    {      
        zoomCamera.transform.position = Vector3.Lerp(zoomCamera.transform.position,
                                                     mainTarget.transform.position,
                                                     cameraSpeed * 3f);       
    }
    void ArrowAttackOff()
    {
        playerController.camStatus = 1; // Ȱ �þ� �ٽ� �ʱ�ȭ
    }
    public void BowCameraOFF()
    {
        zoomCamera.SetActive(false);
    }

    void NormalAttack()
    {
        
        if (comboStep == 0)
        {   
            playerAnim.Play("Combo1");
            SoundManager.instance.PlaySE(Attack);
            comboStep = 1;
        }       
        if (comboStep != 0) // �޺��� 1�� �߰�
        { 
            if (comboPossible)
            {
                comboPossible = false; // ������ �Է� ����
                comboStep += 1;               
                if (comboStep == 2)
                {
                    SoundManager.instance.PlaySE(Attack);
                }
                if (comboStep == 3)
                {
                    SoundManager.instance.PlaySE(Attack);
                }               
            }            
        }        
    }
    void SmashAttack()
    {        
        if (comboPossible)
        {
            comboPossible = false;
            inputSmash = true;
        }
    }
    public void ComboAttack()
    {
        if (!inputSmash)
        {        
            if (comboStep == 2)
            {
                playerAnim.Play("Combo2");                
            }              
            if (comboStep == 3)
            {
                playerAnim.Play("Combo3");               
            }
               
        }
        if (inputSmash)
        {
            SoundManager.instance.PlaySE(S_Attack);
            if (comboStep == 1)
                playerAnim.Play("SmashCombo1");
            if (comboStep == 2)
                playerAnim.Play("SmashCombo2");
            if (comboStep == 3)
                playerAnim.Play("SmashCombo3");
        }
    }

    public void ResetCombo()
    {
        comboPossible = false;
        inputSmash = false;
        comboStep = 0;
        playerController.isMove = true;
        isAttack = false;
    }


    #region weapon Swap
    void MeeleSwap()
    {
        meeleWeapon = true;
        unequip[0].SetActive(false);
        unequip[1].SetActive(false);
        unequip[2].SetActive(true);
        playerController.isMove = false;

        isMeele = true;
        isRange = false;
    }
    void RangeSwap()
    {
        meeleWeapon = false;
        unequip[0].SetActive(true);
        unequip[1].SetActive(true);
        unequip[2].SetActive(false);       
        playerController.isMove = false;

        isMeele = false;
        isRange = true;
    }
    public void MeeleUnequip()
    {
        Bow.SetActive(false);
        HandArrow.SetActive(false);
        MasterSword.SetActive(true);
        Shield.SetActive(true);
    }
    public void RangeUnequip()
    {
        Bow.SetActive(true);
        MasterSword.SetActive(false);
        Shield.SetActive(false);
    }
    #endregion
}
