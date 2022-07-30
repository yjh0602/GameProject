using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShoot : MonoBehaviour
{
    [SerializeField] Camera charactorCamera;   
    [SerializeField] Animator PlayerAnimator;
    [SerializeField] GameObject ArrowPrefab;   
    [SerializeField] Transform ArrowSpawnPosition;
    [SerializeField] GameObject HandArrow;
    [SerializeField] PlayerKeyDown PlayerBowStatus;

    [SerializeField] string BowShoot = "BowShoot";

    RaycastHit hit;
    float range = 1000f;  

    private void Update()
    {      

        if (!(PlayerBowStatus.meeleWeapon) && PlayerBowStatus.isAim)
        { 
            Ray ray = charactorCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitResult;
            if (Physics.Raycast(ray, out hitResult))
            {
                Vector3 mouseDir = new Vector3(hitResult.point.x, transform.position.y, hitResult.point.z) - transform.position;
                PlayerAnimator.transform.forward = mouseDir;
            }                   
        }
    }

    void Shoot()
    {
        SoundManager.instance.PlaySE(BowShoot);
        PlayerBowStatus.HandArrow.SetActive(false);

        Ray ray = charactorCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, range))
        {
            GameObject ArrowInstatiate = GameObject.Instantiate(ArrowPrefab, 
                            ArrowSpawnPosition.transform.position, 
                            ArrowSpawnPosition.transform.rotation);
            Vector3 mouseDir = new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position;
            PlayerAnimator.transform.forward = mouseDir;

            ArrowInstatiate.GetComponent<Arrow>().SetTarget(hit.point);
            Destroy(ArrowInstatiate, 3f);
        }

    }
}
