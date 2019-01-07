using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour {
    private static AchievementUI instance;
    private AchievementUI()
    {
    }
    public static AchievementUI Instance
    {
        get
        {
            if (instance == null)
                instance = new AchievementUI();
            return instance;
        }
    }


    public Canvas AchievementDisplay;
    public ScrollRect AchievementScrollView;

    public Button AchievementButtonPrefab;
    private List<Button> ButtonList;

	// Use this for initialization
	public void Start () {
        instance = this;
        ButtonList = new List<Button>();
	}

   public void AddNewButton(string _Achievementname)
    {
        //Button newbutton = Instantiate(AchievementButtonPrefab, AchievementScrollView.content.transform);

        //newbutton.name = _Achievementname;
        //newbutton.GetComponentInChildren<Text>().text = _Achievementname;

        //ButtonList.Add(newbutton);
    }

    public void ClearView()
    {
        foreach (Button bt in ButtonList)
            bt.gameObject.SetActive(false);
    }

    public void ViewAllButton()
    {
        ClearView();
        foreach(Button bt in ButtonList)
        {
            bt.gameObject.SetActive(true);
        }
    }
    public void ViewActiveButton()
    {
        ClearView();
        foreach (Button bt in ButtonList)
        {
            if (AchievementSystem.Instance.GetAchievementInfo(bt.name).AchievementActive)
                bt.gameObject.SetActive(true);
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}
