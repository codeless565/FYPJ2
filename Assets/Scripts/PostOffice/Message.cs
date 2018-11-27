using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message {
    public MESSAGE_TYPE m_MessageType;
    public GameObject m_Sender;

    public Message(MESSAGE_TYPE _messagetype)
    {
        m_MessageType = _messagetype;
        m_Sender = null;
    }
    public Message(MESSAGE_TYPE _messagetype, GameObject _sender)
    {
        m_MessageType = _messagetype;
        m_Sender = _sender;
    }
}

public enum MESSAGE_TYPE
{
    NONE,
    ADDHPPOT,
    USEHPPOT,
    ADDEXP
}
