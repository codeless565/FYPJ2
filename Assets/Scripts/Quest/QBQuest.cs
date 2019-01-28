using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBQuest {
    private static QBQuest instance;
    private QBQuest()
    {
        QuestList = new List<QuestBase>();
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

    public List<QuestBase> QuestList;



    public QuestBase RandomizeQuest()
    {
        QuestBase newquest = new QuestBase();

        QuestType randomType = (QuestType)Random.Range((int)QuestType.NONE + 1, (int)QuestType.TOTAL);

        if (randomType == QuestType.SLAY)
        {
            QuestTarget randomTarget = (QuestTarget)Random.Range((int)QuestTarget.NONE + 1, (int)QuestTarget.TOTAL);



            newquest = new QuestBase(randomType, randomTarget, Random.Range(1, 4) * 5, Random.Range(1, 10) * 10, RewardType.NOTES);
        }
        else if (randomType == QuestType.REACH)
        {
            
            int nextlevel = CTDungeon.Instance.currentFloor;

            if (CTDungeon.Instance.currentFloor == -1)
                nextlevel = 0;

            if (nextlevel % 10 == 0)
                nextlevel++;

            while (nextlevel % 10 != 0)
            {
                    nextlevel++;
            }

            newquest = new QuestBase(QuestType.REACH, nextlevel, Random.Range(1, 5) * 5, RewardType.GEMS);
        }

        return newquest;
    }

}
