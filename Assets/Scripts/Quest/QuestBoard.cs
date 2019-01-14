using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestBoard : MonoBehaviour {
    public Canvas QuestBoardCanvas;
    public Text QuestText;

    private void Start()
    {
        QBQuest.Instance.UpdateQuest(QuestType.REACH,QuestTarget.NONE);
        QuestBoardCanvas.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            QuestBoardCanvas.gameObject.SetActive(true);
            if (QBQuest.Instance.currQuest.QuestComplete)
            {
                // reward quest
                QBQuest.Instance.RandomizeCurrQuest();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        QuestBoardCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (QBQuest.Instance.currQuest.QuestType == QuestType.SLAY)
        {
                QuestText.text = QBQuest.Instance.currQuest.QuestType + " " + QBQuest.Instance.currQuest.QuestTarget + ":" + QBQuest.Instance.currQuest.QuestAmount + "/" + QBQuest.Instance.currQuest.QuestCompleteAmount + "===INCOMPLETE";

        }
        else if (QBQuest.Instance.currQuest.QuestType == QuestType.REACH)
        {
            
                QuestText.text = QBQuest.Instance.currQuest.QuestType +  " LEVEL " + QBQuest.Instance.currQuest.QuestCompleteAmount + "===INCOMPLETE";

        }
    }

    #region
    /*
     * Quest System
     * Quest Board will display 3 Quest available to accept everytime scene is start
     * Players can accept UP TO 3 Quest 
     * 
     * Quest Accepting System
     * 
     * Quest Reward System
     * 
     */ 
    #endregion
}
