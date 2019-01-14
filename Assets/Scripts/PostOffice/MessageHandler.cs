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
            

            AchievementSystem.Instance.UpdateAchievementProperty(GetStringFromMessage(message.EncodedMessage,MESSAGE_DATA.ACHIEVEMENTNAME.ToString()),
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
