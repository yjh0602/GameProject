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
            StoryText.text = "���� ������ ������ ���� ��� ȭ���� ��� �����մϴ�.";
        }
        if(name == 1)
        {
            StartCoroutine(StoryInfo());
            StoryText.text = "������ ������ ������ ���� ȭ���� ��� �����մϴ�.";
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
                TalkText.text = "KING : �񷽿� ������ ���ɵ��� ������ ������ ���� �ְԳ�";
                question[0].SetActive(false);
            }
            if (name == "Man")
            {
                TalkText.text = "���� : ���� ������ ũ����Ż�� Ȱ�� ���߸� ���ο� ���� �����ٰ� �ϴ���...";
                question[1].SetActive(false);
            }
            if (name == "Woman")
            {
                TalkText.text = "���� : ���� �� ������ �ִ� ������ ��Ȱ�� ����� �ִٰ� �մϴ�.";
                question[2].SetActive(false);
            }
            if (name == "Soldier_1")
            {
                TalkText.text = "���� 1 : �и��� �����ϸ� ü���� ȸ���� �ȴٰ� �մϴ�. ";
                question[3].SetActive(false);
            }
            if(name == "Soldier_2")
            {
                TalkText.text = "���� 2 : �� ���� ���� ���ظ� ���Դ°� ����.. ";
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
