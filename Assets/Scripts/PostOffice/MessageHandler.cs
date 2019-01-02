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
        //switch (message.m_MessageType)
        //{
        //    case MESSAGE_TYPE.ADDEXP:
        //        {
        //            if (message.m_Sender.GetComponent<IEnemy>() != null)
        //                _receiver.GetComponent<PlayerUIScript>().AddEXP(_receiver.GetComponent<CPlayer>().GetStats().EXP,message.m_Sender.GetComponent<IEnemy>().GetStats().Level * (int)message.m_Sender.GetComponent<IEnemy>().GetStats().EXP);
        //        }
        //        break;
        //    case MESSAGE_TYPE.ADDPROP:
        //        {
        //            _receiver.GetComponent<CPlayer>().m_QuestSystem.UpdateQuestProperty(NoiseSlayer.m_questName, KillNoiseProp.m_propertyname, 1);
        //        }
        //        break;
        //}
        if (GetStringFromMessage(message.EncodedMessage, "MessageType") == MESSAGE_TYPE.ADDEXP.ToString())
        {
            float amount = 0.0f;
            float.TryParse(GetStringFromMessage(message.EncodedMessage, MESSAGE_DATA.AMOUNT.ToString()), out amount);

                _receiver.GetComponent<PlayerUIScript>().AddEXP(_receiver.GetComponent<CPlayer>().GetStats().EXP, amount);

        }

        if (GetStringFromMessage(message.EncodedMessage,"MessageType") == MESSAGE_TYPE.ADDPROP.ToString())
        {

            float amount = 0.0f;
            float.TryParse(GetStringFromMessage(message.EncodedMessage, MESSAGE_DATA.AMOUNT.ToString()), out amount);
            

            _receiver.GetComponent<CPlayer>().m_QuestSystem.UpdateQuestProperty(GetStringFromMessage(message.EncodedMessage,MESSAGE_DATA.QUESTNAME.ToString()),
                GetStringFromMessage(message.EncodedMessage, MESSAGE_DATA.PROPERTYNAME.ToString()),
                amount);

        }
    }
    string GetStringFromMessage(string _message, string wanteddata)
    {
        if (_message.Contains(wanteddata))
        {
            string[] newmsg = _message.Split(new char[] { ',' });

            foreach (string st in newmsg)
            {
                if (st.Contains(wanteddata))
                    return st.Split(new char[] { '=' })[1].ToString();
            }
        }

        return "";
    }
}
