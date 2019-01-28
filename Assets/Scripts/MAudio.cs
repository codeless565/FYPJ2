using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MAudio : MonoBehaviour {

    private static MAudio instance;
    private MAudio() { }
    public static MAudio Instance
    {
        get
        {
            if (instance == null)
                instance = new MAudio();
            return instance;
        }
    }

    public AudioSource BGMSource;
    public AudioSource FXSource;

    public List<AudioClip> BGMClipList;
    public List<AudioClip> FXClipList;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this.gameObject);
        BGMSource.volume= PlayerPrefs.GetFloat("BGM_VOL");
        FXSource.volume = PlayerPrefs.GetFloat("FX_VOL");

        if(PlayerPrefs.GetInt("BGM_MUTE") == 1)
            BGMSource.mute = true;
        if (PlayerPrefs.GetInt("FX_MUTE") == 1)
            FXSource.mute = true;

        instance = this;

        PlayBGM(BGMClipList[0]);
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void PlayBGM(AudioClip _clip)
    {
        if (!_clip)
            return;

        BGMSource.Stop();
        BGMSource.clip = _clip;
        BGMSource.Play();
    }

    public void PlayFX(AudioClip _clip)
    {
        if (!_clip)
            return;

        FXSource.Stop();
        FXSource.clip = _clip;
        FXSource.Play();
    }

    
}
