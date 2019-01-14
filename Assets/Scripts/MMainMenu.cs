﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MMainMenu : MonoBehaviour {

    public GameObject MainMenuPanel;
    public GameObject OptionsPanel;

    public Slider BGMSlider;
    public Slider FXSlider;
    public Toggle BGMToggle;
    public Toggle FXToggle;

    // Use this for initialization
    void Start () {
        MainMenuPanel.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        MAudio.Instance.BGMSource.volume = BGMSlider.value;
        MAudio.Instance.FXSource.volume = FXSlider.value;

        MAudio.Instance.BGMSource.mute = BGMToggle.isOn;
        MAudio.Instance.FXSource.mute = FXToggle.isOn;
    }

    public void StartGame_Click()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void ContinueGame_Click()
    {
        print("Not done");
    }
    public void MusicalAttack_Click()
    {
        print("Not done");
    }
    public void Highscore_Click()
    {
        print("Not done");
    }
    public void Quit_Click()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    #region Options
    public void Options_Click()
    {
        OptionsPanel.SetActive(true);
    }
    public void Back_Click()
    {
        OptionsPanel.SetActive(false);
    }
    public void Apply_Click()
    {
        print("Not done");
    } 
    #endregion
}
