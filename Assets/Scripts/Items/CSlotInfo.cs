using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CSlotInfo : MonoBehaviour, IDragHandler, IEndDragHandler
{
    CItemSlot m_itemSlot;

    public void Start()
    {
        GetComponent<Image>().sprite = null;
        GetComponent<Image>().color = Color.gray;
        transform.GetChild(0).GetComponent<Text>().text = "";
        m_itemSlot = null;
    }

    public void Update()
    {
        if (m_itemSlot != null)
        {
            if (m_itemSlot.quantity <= 0)
            {
                GetComponent<Image>().sprite = null;
                GetComponent<Image>().color = Color.gray;
                transform.GetChild(0).GetComponent<Text>().text = "";
                m_itemSlot = null;
                return;
            }
            GetComponent<Image>().sprite = m_itemSlot.itemInfo.ItemSprite;
            GetComponent<Image>().color = Color.white;
            transform.GetChild(0).GetComponent<Text>().text = m_itemSlot.quantity.ToString();
        }
    }

    public void SetItemSlot(CItemSlot _newItemInfo)
    {
        m_itemSlot = _newItemInfo;
    }

    public bool isEmpty()
    {
        if (m_itemSlot == null)
            return true;

        return false;
    }

    public bool isSameItem(CItemSlot _newItemInfo)
    {
        if (_newItemInfo.itemInfo.ItemKey == m_itemSlot.itemInfo.ItemKey)
            return true;

        return false;
    }

    /********************
     * Drag Drop Control
     ********************/
    #region DragDrop
    private bool isSwapping;
    private GameObject otherSlot;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isSwapping)
        {
            CItemSlot oldSlot = m_itemSlot;
            m_itemSlot = otherSlot.GetComponent<CSlotInfo>().m_itemSlot;
            otherSlot.GetComponent<CSlotInfo>().m_itemSlot = oldSlot;
            isSwapping = false;
            Debug.Log("Swapped");
        }
        else
        {
            if ((transform.localPosition - Vector3.zero).magnitude >= 50)
            {
                GetComponent<Image>().sprite = null;
                GetComponent<Image>().color = Color.gray;
                transform.GetChild(0).GetComponent<Text>().text = "";
                m_itemSlot = null;
                Debug.Log("item removed from hotbar");
            }
        }
        Debug.Log("moved - " + (transform.localPosition - Vector3.zero).magnitude);

        transform.localPosition = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.gameObject.GetComponent<CSlotInfo>())
        {
            isSwapping = true;
            otherSlot = _other.gameObject;
            Debug.Log("Swap detected");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isSwapping = false;
        otherSlot = null;
    }

    #endregion
}