using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Vector3 m_target;
    public float speed;
    public PlayerKeyDown arrow;

    public GameObject[] arrowType;
    public GameObject[] HitEffect;

    private void Update()
    {
        float step = speed * Time.deltaTime;
        if(m_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_target, step);
        }
    }
    public void SetTarget(Vector3 target)
    {
        m_target = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Enemy")
        {
            if(arrow.arrowNumber == 0)
            {
                StartCoroutine(ArrowHit(0));              
            }
            if(arrow.arrowNumber == 1)
            {
                StartCoroutine(ArrowHit(1));               
            }
            if(arrow.arrowNumber == 2)
            {
                StartCoroutine(ArrowHit(2));               
            }
        }
        else
            Destroy(gameObject, 0.5f);
    }
    IEnumerator ArrowHit(int num)
    {
        Time.timeScale = 0.5f;
        HitEffect[num].SetActive(true);
        yield return new WaitForSeconds(0.2f);
        HitEffect[num].SetActive(false);
        Destroy(gameObject);
        Time.timeScale = 1f;
    }
}
