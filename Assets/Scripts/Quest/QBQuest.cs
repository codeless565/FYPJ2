using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBQuest {
    private static QBQuest instance;
    private QBQuest()
    {
        RandomizeCurrQuest();
    }

    public static QBQuest Instance
    {
        get
        {
            if (instance == null)
                instance = new QBQuest();
            return instance;
        }
    }

    public QuestBase currQuest;

    public void RandomizeCurrQuest()
    {
        currQuest = RandomizeQuest();
    }
	
    public void UpdateQuest(QuestType _questtype, QuestTarget _questtarget)
    {
        // if slay
        if (_questtype == QuestType.SLAY)
        {
            if (_questtarget == currQuest.QuestTarget)
                currQuest.QuestAmount++;
            if (currQuest.QuestAmount >= currQuest.QuestCompleteAmount)
                currQuest.QuestComplete = true;
        }
        if(_questtype == QuestType.REACH)
        {
            if(CTDungeon.Instance.currentFloor == currQuest.QuestCompleteAmount)
                currQuest.QuestComplete = true;
        }
    }

    public QuestBase RandomizeQuest()
    {
        QuestBase newquest = new QuestBase();

        QuestType randomType = (QuestType)Random.Range((int)QuestType.NONE + 1, (int)QuestType.TOTAL);

        if (randomType == QuestType.SLAY)
        {
            QuestTarget randomTarget = (QuestTarget)Random.Range((int)QuestTarget.NONE + 1, (int)QuestTarget.TOTAL);

            newquest = new QuestBase(randomType, randomTarget, Random.Range(1, 4) * 5);
        }
        else if (randomType == QuestType.REACH)
        {
            bool found = false;
            int nextlevel = CTDungeon.Instance.currentFloor;
            while (!found)
            {
                if (nextlevel % 10 == 0)
                    found = true;
                else
                    nextlevel++;
            }

            newquest = new QuestBase(QuestType.REACH, nextlevel);
        }

        return newquest;
    }

}
