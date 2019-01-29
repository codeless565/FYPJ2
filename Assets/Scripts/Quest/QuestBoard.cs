using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestBoard : MonoBehaviour {
    public Canvas QuestBoardCanvas;
    //public Text QuestText;
    public Button btnPrefab;
    public List<Button> btnlist;
    float startPos = -200;

    private void Start()
    {
        //QBQuest.Instance.UpdateQuest(QBQuest.Instance.firstQuest,QuestType.REACH,QuestTarget.NONE);
        QBQuest.Instance.QuestList.Clear();
        btnlist = new List<Button>();
            for (int i = 0; i < 3; ++i)
            {
                QuestBase newquest = QBQuest.Instance.RandomizeQuest();
                QBQuest.Instance.QuestList.Add(newquest);

                Button newbtn = Instantiate(btnPrefab, QuestBoardCanvas.transform);
                newbtn.GetComponent<RectTransform>().anchoredPosition = new Vector3(startPos, 0, 0);
                btnlist.Add(newbtn);

                btnlist[i].onClick.RemoveAllListeners();
                btnlist[i].onClick.AddListener(delegate { AddQuestToPlayer(newquest); });

                startPos += 200;
            }


        PostOffice.Instance.Send("Player", new Message(MESSAGE_TYPE.QUEST, QuestType.REACH.ToString(), QuestTarget.NONE.ToString()));
        UpdateQuestBoardUI();
        QuestBoardCanvas.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            QuestBoardCanvas.gameObject.SetActive(true);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        QuestBoardCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        Debug.Log(QBQuest.Instance.QuestList.Count);
    }

    public void UpdateQuestBoardUI()
    {
        for (int i = 0;i<3;++i)
        {            
            btnlist[i].transform.GetChild(0).GetComponent<Text>().text = QBQuest.Instance.QuestList[i].QuestString;
            //btnlist[i].transform.GetChild(1).GetComponent<Text>().text = QBQuest.Instance.QuestList[i].QuestReward + " " + QBQuest.Instance.QuestList[i].QuestRewardType;
        }
    }
    public void AddQuestToPlayer(QuestBase _quest)
    {

        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<CPlayer>().AddNewQuest(_quest))
        return;
        else
        
        // change the image
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().transform.GetChild(0).GetComponent<Text>().text = "";
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().transform.GetChild(1).GetComponent<Text>().text = "";
    }


    #region
    /*
     * Quest System
     * Quest Board will display 3 Quest available to accept everytime scene is start DONE
     * Players can accept UP TO 3 Quest DONE
     * Quest Accepting System DONE
     * 
     * Players Quest Display ---- ADD TO GAME SCENE
     * Quest Reward System
     * 
     */
    #endregion
}
