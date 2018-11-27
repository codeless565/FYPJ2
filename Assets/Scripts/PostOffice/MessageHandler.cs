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

    public void Handle(GameObject _receiver, Message message)
    {
        if (!_receiver)
            return;

        switch (message.m_MessageType)
        {
            case MESSAGE_TYPE.ADDEXP:
                {
                    if (message.m_Sender.GetComponent<IEnemy>() != null)
                        _receiver.GetComponent<CPlayer>().AddEXP(message.m_Sender.GetComponent<IEnemy>().GetStats().Level * (int)message.m_Sender.GetComponent<IEnemy>().GetStats().EXP);
                }
                break;
        }
    }
    
}
