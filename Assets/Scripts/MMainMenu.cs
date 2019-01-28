using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MMainMenu : MonoBehaviour {

    [SerializeField]
    GameObject MainMenuPanel;
    [SerializeField]
    GameObject OptionsPanel;
    [SerializeField]
    GameObject WarningReset;

    // Use this for initialization
    void Start () {
        MainMenuPanel.SetActive(true);
        WarningReset.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
    }

    public void StartGame_Click()
    {
    }
    public void StartGame_OpenRestartProgressConfirmation()
    {
        WarningReset.SetActive(true);
    }
    public void StartGame_RestartYes()
    {
        WarningReset.SetActive(false);
        SceneManager.LoadScene("TownScene");
        CProgression.Instance.ResetSave();
        MAudio.Instance.PlayBGM(AudioDatabase.Instance.getBGMAudio("townscene"));// 2 is game scene
    }
    public void StartGame_RestartNo()
    {
        WarningReset.SetActive(false);
    }

    public void ContinueGame_Click()
    {
        SceneManager.LoadScene("TownScene");
        MAudio.Instance.PlayBGM(AudioDatabase.Instance.getBGMAudio("townscene"));// 2 is game scene
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
