using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    static public MainMenu instance;
    //public GameObject main;
    public GameObject MainPanel;
    public GameObject Option;
    public bool isOption;   

    public GameObject KeyGuide;
    public GameObject soundGuide;


    private void Awake() // ╫л╠шео х╜
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    private void Start()
    {      
        SoundManager.instance.PlayBg(0);      
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnClicOption();        
        } 
    }
    public void OnClicNewGame()
    {
        SceneManager.LoadScene("Loding");
        MainPanel.SetActive(false);
    }
    public void OnClicLoad()
    {      
        
    }
    public void OnClicOption()
    {
        KeyGuide.SetActive(true);
        soundGuide.SetActive(false);
        if (isOption)
        {          
            Option.SetActive(false);
            isOption = false;           
        }
        else
        {          
            Option.SetActive(true);
            isOption = true;           
        }
    }
    public void OnClicKeyGuide()
    {
        KeyGuide.SetActive(true);
        soundGuide.SetActive(false);
    }
    public void OnClicSoundGuide()
    {
        KeyGuide.SetActive(false);
        soundGuide.SetActive(true);
    }
    public void OnClicQuit()
    {
        Application.Quit();
    }
    public void onMenu()
    {
        Option.SetActive(false);
        isOption = false;
        SceneManager.LoadScene("Title");
        SoundManager.instance.PlayBg(0);
        MainPanel.SetActive(true);

    }
}
