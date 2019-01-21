using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CInventorySlots : MonoBehaviour {

    public int InventorySlots = 3;

    [SerializeField]
    GameObject MovingParent;

    GameObject[] m_Itemslots;

    public void Init(Dictionary<string, CItemSlot> _Items)
    {
        m_Itemslots = new GameObject[InventorySlots];
        for (int i = 0; i < InventorySlots; i++)
        {
            m_Itemslots[i] = gameObject.transform.GetChild(i).transform.GetChild(0).gameObject;
            switch (i)
            {
                case 0:
                    m_Itemslots[i].GetComponentInChildren<CSlotInfo>().Init(_Items[CItemDatabase.Instance.HPRation.GetComponent<IItem>().ItemKey], MovingParent);
                    break;
                case 1:
                    m_Itemslots[i].GetComponentInChildren<CSlotInfo>().Init(_Items[CItemDatabase.Instance.HPPotion.GetComponent<IItem>().ItemKey], MovingParent);
                    break;
                case 2:
                    m_Itemslots[i].GetComponentInChildren<CSlotInfo>().Init(_Items[CItemDatabase.Instance.HPElixir.GetComponent<IItem>().ItemKey], MovingParent);
                    break;
                case 3:
                    m_Itemslots[i].GetComponentInChildren<CSlotInfo>().Init(_Items[CItemDatabase.Instance.SPPotion.GetComponent<IItem>().ItemKey], MovingParent);
                    break;
                case 4:
                    m_Itemslots[i].GetComponentInChildren<CSlotInfo>().Init(_Items[CItemDatabase.Instance.SPElixir.GetComponent<IItem>().ItemKey], MovingParent);
                    break;
                case 5:
                    m_Itemslots[i].GetComponentInChildren<CSlotInfo>().Init(_Items[CItemDatabase.Instance.ReviveTix.GetComponent<IItem>().ItemKey], MovingParent);
                    break;
            }
        }
    }



}
