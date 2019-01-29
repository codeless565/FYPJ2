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

    // Use this for initialization
    void Start () {
        if (GameObject.FindGameObjectsWithTag("AudioManager").Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        if (!PlayerPrefs.HasKey("BGM_VOL") || !PlayerPrefs.HasKey("FX_VOL"))
        {
            BGMSource.volume = 1f;
            FXSource.volume = 1f;
        }
        else
        {
            BGMSource.volume = PlayerPrefs.GetFloat("BGM_VOL");
            FXSource.volume = PlayerPrefs.GetFloat("FX_VOL");
        }

        if(PlayerPrefs.GetInt("BGM_MUTE") == 1)
            BGMSource.mute = true;
        if (PlayerPrefs.GetInt("FX_MUTE") == 1)
            FXSource.mute = true;

        instance = this;
        PlayBGM(AudioDatabase.Instance.getBGMAudio("mainmenu"));

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
