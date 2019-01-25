using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DungeonNotice : MonoBehaviour {

    [SerializeField]
    GameObject NoticePrefab;

	// Use this for initialization
	void Start () {
        if (NoticePrefab == null)
        {
            NoticePrefab = gameObject.transform.GetChild(0).gameObject;
        }
        NoticePrefab.SetActive(false);
    }

    public void OpenNoticeforAbandon()
    {
        NoticePrefab.SetActive(true);
    }

    public void CloseNotice()
    {
        NoticePrefab.SetActive(false);
    }

    public void AbandonProgress()
    {
        SceneManager.LoadScene("TownScene");
    }
}
