using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public int currentHp;
    public int maxHp;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public Image fadeOut;
    [SerializeField] private AnimationCurve fadeCurve; // 페이드효과 곡선 조절 

    [SerializeField]
    [Range(0.01f, 10f)] private float fadeTime;




    private void Awake()
    {
        StartCoroutine(Fade(1, 0,"Start"));
    }
    private void Update()
    {
        if(currentHp == 0)
        {
            StartCoroutine(Fade(1, 0 , "Die"));
        }

        if(currentHp > maxHp)
        {
            currentHp = maxHp;
        }

        for (int i = 0; i< hearts.Length; i++)
        {
            if (i < currentHp)
            {
                hearts[i].sprite = fullHeart;
            }
            else
                hearts[i].sprite = emptyHeart;
        }
    }

    IEnumerator Fade(float start, float end , string fadeOutMotion)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while(percent <1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = fadeOut.color;
            if(fadeOutMotion == "Start")
            {
                color.a = Mathf.Lerp(start, end, percent);
            }
            else if(fadeOutMotion == "Die")
            {
               color.a = Mathf.Lerp(start, end, fadeCurve.Evaluate(percent));
            }          
            fadeOut.color = color;

            yield return null;
        }
    }

}
