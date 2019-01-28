using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMenu : MonoBehaviour
{
    [SerializeField]
    GameObject SettingMenu;

    // Use this for initialization
    void Start()
    {
        SettingMenu.SetActive(false);
    }

    public void ToggleExitMenu()
    {
        SettingMenu.SetActive(!SettingMenu.activeSelf);
    }
    public void OpenExitMenu()
    {
        SettingMenu.SetActive(true);
    }
    public void CloseExitMenu()
    {
        SettingMenu.SetActive(false);
    }

    public void ExitToTitle()
    {
        CProgression.Instance.UpdatePlayerSave(GameObject.FindGameObjectWithTag("Player").GetComponent<CPlayer>());
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        CProgression.Instance.UpdatePlayerSave(GameObject.FindGameObjectWithTag("Player").GetComponent<CPlayer>());
        Application.Quit();
    }
}
