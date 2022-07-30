using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private string foodSound = "FoodSound";
    [SerializeField] GameObject Effects;
    SphereCollider sphereCollider;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }
    public enum Type
    {
        FOOD,
        FRUIT     
    }
    public Type FoodType;

    HealthUI playerHp;

    private void Awake()
    {
        playerHp = FindObjectOfType<HealthUI>();
    }
    private void OnTriggerEnter(Collider other)
    {
        switch(FoodType)
        {
            case Type.FOOD:
                if (other.tag == "Player")
                {                   
                    SoundManager.instance.PlaySE(foodSound);
                    playerHp.currentHp += 2;                  
                    StartCoroutine(FoodEffect());                   
                }
                break;
            case Type.FRUIT:
                if (other.tag == "Player")
                {
                    SoundManager.instance.PlaySE(foodSound);
                    playerHp.currentHp += 1;                  
                    StartCoroutine(FoodEffect());
                    
                }
                break;
        }       
    }
    IEnumerator FoodEffect()
    {
        Effects.SetActive(true);
        sphereCollider.enabled = false;
        yield return new WaitForSeconds(2f);
        Effects.SetActive(false);
        Destroy(gameObject);
    }
}
