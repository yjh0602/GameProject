using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TalkManager : MonoBehaviour
{
    public GameObject talkPanel;
    public GameObject storyPanel;
    public GameObject endPanel;   

    public Text TalkText;
    public Text StoryText;    
    public bool Talking;

    [SerializeField] GameObject[] question;

    private string TalkSound = "Talk";
    private void Start()
    {
        SoundManager.instance.PlayBg(1);
    }

    public void Story(int name)
    {
        SoundManager.instance.PlaySE(TalkSound);
        if (name == 0)
        {
            StartCoroutine(StoryInfo());
            StoryText.text = "불의 정령을 구출해 불이 깃든 화살을 사용 가능합니다.";
        }
        if(name == 1)
        {
            StartCoroutine(StoryInfo());
            StoryText.text = "얼음의 정령을 구출해 얼음 화살을 사용 가능합니다.";
        }
        if(name == 2)
        {
            endPanel.SetActive(true);          
        }     
    }
    public void Talk(string name)
    {
        if(Talking)
        {
            Talking = false;           
        }
        else
        {
            SoundManager.instance.PlaySE(TalkSound);
            Talking = true;            
            if (name == "King")
            {
                TalkText.text = "KING : 골렘에 잡혀간 정령들을 구출해 세상을 구해 주게나";
                question[0].SetActive(false);
            }
            if (name == "Man")
            {
                TalkText.text = "남자 : 용사님 빛나는 크리스탈에 활을 맞추면 새로운 길이 열린다고 하던데...";
                question[1].SetActive(false);
            }
            if (name == "Woman")
            {
                TalkText.text = "여자 : 용사님 별 문양이 있는 돌에는 부활의 기운이 있다고 합니다.";
                question[2].SetActive(false);
            }
            if (name == "Soldier_1")
            {
                TalkText.text = "병사 1 : 패링에 성공하면 체력이 회복이 된다고 합니다. ";
                question[3].SetActive(false);
            }
            if(name == "Soldier_2")
            {
                TalkText.text = "병사 2 : 저 골렘은 전혀 피해를 안입는거 같아.. ";
                question[4].SetActive(false);
            }
        }
        talkPanel.SetActive(Talking);      
    }

    IEnumerator StoryInfo()
    {
        storyPanel.SetActive(true);
        yield return new WaitForSeconds(5f);
        storyPanel.SetActive(false);
    }
}
