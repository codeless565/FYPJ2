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
        //Debug.Log("Singleton - Post Office created");
    }

    public void Register(string addressname, GameObject go)
    {
        if (go == null)
            return;
        if (m_addressBook.ContainsKey(addressname))
        {
            m_addressBook[addressname] = go;
            return;
        }
        m_addressBook.Add(addressname, go);
    }

    public void Remove(string addressname, GameObject go)
    {
        if (go == null)
            return;
        if (!m_addressBook.ContainsKey(addressname))
            return;

        m_addressBook.Remove(addressname);
    }

    public bool Send(string receiveraddress, Message message)
    {
        if (!m_addressBook.ContainsKey(receiveraddress))
            return false;
        else
        {
            GameObject temp;
            m_addressBook.TryGetValue(receiveraddress, out temp);
            MessageHandler.Instance.Handle(temp, message);
        }
        

        return false;
    }

}
