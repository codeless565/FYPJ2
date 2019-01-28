using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MMainMenu : MonoBehaviour {

    public GameObject MainMenuPanel;
    public GameObject OptionsPanel;

    // Use this for initialization
    void Start () {
        MainMenuPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update () {
    }

    public void StartGame_Click()
    {
        SceneManager.LoadScene("GameScene");
        MAudio.Instance.PlayBGM(AudioDatabase.Instance.getBGMAudio("townscene"));// 2 is game scene
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

}
