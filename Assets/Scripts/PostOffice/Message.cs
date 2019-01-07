using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message {
    public string EncodedMessage;

    //public MESSAGE_TYPE m_MessageType;
    //public GameObject m_Sender;

    // for exp
    public Message(MESSAGE_TYPE _messagetype, float _amount)
    {
        EncodedMessage = "MessageType=" + _messagetype + "," + MESSAGE_DATA.AMOUNT.ToString() + "=" + _amount;

        Debug.Log("EncodedMessage: " + EncodedMessage);
    }

    // for Achievement
    public Message(MESSAGE_TYPE _messagetype,string Achievementname, string propname, float amount)
    {
        EncodedMessage = "MessageType=" + _messagetype + "," + MESSAGE_DATA.ACHIEVEMENTNAME.ToString() + "=" + Achievementname + "," + MESSAGE_DATA.PROPERTYNAME + "=" + propname + "," + MESSAGE_DATA.AMOUNT + "=" + amount;
        Debug.Log("EncodedMessage: " + EncodedMessage);
    }
}

public enum MESSAGE_TYPE
{
    NONE,
    ADDEXP,
    ADDPROP,
}

public enum MESSAGE_DATA
{
    ACHIEVEMENTNAME,
    PROPERTYNAME,
    AMOUNT
}
