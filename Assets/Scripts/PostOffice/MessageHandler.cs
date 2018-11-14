using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageHandler{

    private static MessageHandler instance;
    public static MessageHandler Instance
    {
        get
        {
            if (instance == null)
                instance = new MessageHandler();
            return instance;
        }
    }
    private MessageHandler()
    {
        //Debug.Log("Singleton - MessageHandler created");
    }

    public void Handle(GameObject go, Message.MESSAGE_TYPE message)
    {
        if (!go)
            return;

        switch (message)
        {
            case Message.MESSAGE_TYPE.ADDHPPOT:
                go.GetComponent<CPlayer>().AddItem("HP_POTION");
                break;
            case Message.MESSAGE_TYPE.USEHPPOT:
                go.GetComponent<CPlayer>().UseItem("HP_POTION");
                break;
        }
    }
}
