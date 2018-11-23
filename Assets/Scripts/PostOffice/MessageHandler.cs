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
        }
    }
}
