﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestBoard : MonoBehaviour {
    public Canvas QuestBoardCanvas;
    //public Text QuestText;
    public Button btnPrefab;
    public List<Button> btnlist;
    float startPos = -175;

    private void Start()
    {
        //QBQuest.Instance.UpdateQuest(QBQuest.Instance.firstQuest,QuestType.REACH,QuestTarget.NONE);
        btnlist = new List<Button>();

        for(int i = 0;i<3;++i)
        {
            QuestBase newquest = QBQuest.Instance.RandomizeQuest();
            QBQuest.Instance.QuestList.Add(newquest);

            Button newbtn = Instantiate(btnPrefab, QuestBoardCanvas.transform);
            newbtn.GetComponent<RectTransform>().anchoredPosition = new Vector3(startPos, 0, 0);
            btnlist.Add(newbtn);

            btnlist[i].onClick.RemoveAllListeners();
            btnlist[i].onClick.AddListener(delegate { AddQuestToPlayer(newquest); });

            startPos += 175;
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
            //if (QBQuest.Instance.currQuest.QuestComplete)
            //{
            //    // reward quest
            //    QBQuest.Instance.RandomizeCurrQuest();
            //}
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        QuestBoardCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
    }

    public void UpdateQuestBoardUI()
    {
        for (int i = 0;i<QBQuest.Instance.QuestList.Count;++i)
        {            
                btnlist[i].GetComponentInChildren<Text>().text = QBQuest.Instance.QuestList[i].QuestString;
        }
    }
    public void AddQuestToPlayer(QuestBase _quest)
    {

        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<CPlayer>().AddNewQuest(_quest))
        return;
        else
        
        // change the image
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().GetComponentInChildren<Text>().text = "";
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
