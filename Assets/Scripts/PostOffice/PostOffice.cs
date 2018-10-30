using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostOffice {
    private Dictionary<string, GameObject> m_addressBook;

    private static PostOffice instance;
    public static PostOffice Instance
    {
        get
        {
            if (instance == null)
                instance = new PostOffice();
            return instance;
        }
    }
    private PostOffice()
    {
        m_addressBook = new Dictionary<string, GameObject>();
    }

    public void Register(string addressname, GameObject go)
    {
        if (go == null)
            return;
        if (m_addressBook.ContainsKey(addressname))
            return;
        m_addressBook.Add(addressname, go);
    }

    public bool Send(string receiveraddress, Message.MESSAGE_TYPE message)
    {
        if (!m_addressBook.ContainsKey(receiveraddress))
            return false;
        else
        {
            GameObject temp;
            m_addressBook.TryGetValue(receiveraddress, out temp);
            Handle(temp, message);
        }
        

        return false;
    }

    public void Handle(GameObject go, Message.MESSAGE_TYPE message)
    {
        if (!go)
            return;

        switch(message)
        {
            case Message.MESSAGE_TYPE.HELLOWORLD:
                Debug.Log("helloWorld");
                break;
            case Message.MESSAGE_TYPE.OHYEA:
                Debug.Log("ohyea");
                break;
        }
    }
}
