using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable] // 데이터 직렬화
public class Sound
{
    public string name; //곡의 이름
    public AudioClip clip; // 곡
}

public class SoundManager : MonoBehaviour
{
    static public SoundManager instance;

    #region singleton
    private void Awake() // 싱글턴 화
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    #endregion


    public AudioSource[] audioSourcesEffects;
    public AudioSource[] audioSourceBgm;

    public string[] playSoundName;

    public Sound[] effectSounds; // 이펙트 곡   

    private void Start()
    {
        playSoundName = new string[audioSourcesEffects.Length];
    }
    public void SetBgmVolume(float volume)
    {
        for (int j = 0; j < audioSourceBgm.Length; j++)
        {
            audioSourceBgm[j].volume = volume;
        }
    }
    public void SetEffectVolume(float volume)
    {       
        for (int i = 0; i < audioSourcesEffects.Length; i++)
        {
            audioSourcesEffects[i].volume = volume;
        }
    }

    public void PlaySE(string _name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if(_name == effectSounds[i].name)
            {
                for (int j = 0; j < audioSourcesEffects.Length; j++)
                {
                    if(!audioSourcesEffects[j].isPlaying)
                    {
                        playSoundName[j] = effectSounds[i].name;
                        audioSourcesEffects[j].clip = effectSounds[i].clip;
                        audioSourcesEffects[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 가용 audioSource가 사용중입니다.");
                return;
            }
        }
        Debug.Log(_name + "사운드가 soundManager에 등록되지 않았습니다.");
    }
     
    public void StopALLSE()
    {
        for (int i = 0; i < audioSourcesEffects.Length; i++)
        {
            audioSourcesEffects[i].Stop();
        }
    }

    public void StopSE(string _name)
    {
        for (int i = 0; i < audioSourcesEffects.Length; i++)
        {
            if(playSoundName[i] == _name)
            {
                audioSourcesEffects[i].Stop();
                return;
            }         
        }
        Debug.Log("재생 중인" + _name + "사운드가 없습니다.");
    }

    public void PlayBg(int num)
    {
        if (num == 0)
        {
            audioSourceBgm[0].enabled = true;
            audioSourceBgm[1].enabled = false;
            audioSourceBgm[2].enabled = false;
            audioSourceBgm[3].enabled = false;
        }
        if( num == 1)
        {           
            audioSourceBgm[0].enabled = false;
            audioSourceBgm[1].enabled = true;           
            audioSourceBgm[2].enabled = false;
            audioSourceBgm[3].enabled = false;
        }
        if(num == 2)
        {           
            audioSourceBgm[0].enabled = false;
            audioSourceBgm[1].enabled = false;           
            audioSourceBgm[2].enabled = true;
            audioSourceBgm[3].enabled = false;
        }
        if(num == 3)
        {
            audioSourceBgm[0].enabled = false;
            audioSourceBgm[1].enabled = false;
            audioSourceBgm[2].enabled = false;
            audioSourceBgm[3].enabled = true;
        }
       
    }

}
