using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CSlotInfo : MonoBehaviour, IDragHandler, IEndDragHandler
{
    CItemSlot m_itemSlot;
    public ItemSlotType m_slotType;

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
        GetComponent<Image>().sprite = m_itemSlot.itemInfo.ItemSprite;
        GetComponent<Image>().color = Color.white;
        transform.GetChild(0).GetComponent<Text>().text = m_itemSlot.quantity.ToString();
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

    private void ResetSlot()
    {
        GetComponent<Image>().sprite = null;
        GetComponent<Image>().color = Color.gray;
        transform.GetChild(0).GetComponent<Text>().text = "";
        m_itemSlot = null;
    }


    public ItemSlotType SlotType
    { get { return m_slotType; } }

    /********************
     * Drag Drop Control
     ********************/
    #region DragDrop
    private bool isSwapping;
    private bool isReplacing;
    private GameObject otherSlot;

    public void OnDrag(PointerEventData eventData)
    {
        if (m_itemSlot == null)
            return;
        transform.position = Input.mousePosition;
        //Change rendering layer to forward
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isSwapping)
        {
            CItemSlot oldSlot = m_itemSlot;
            m_itemSlot = otherSlot.GetComponent<CSlotInfo>().m_itemSlot;
            if (m_itemSlot == null)
                ResetSlot();
            otherSlot.GetComponent<CSlotInfo>().m_itemSlot = oldSlot;
            isSwapping = false;
            Debug.Log("Swapped");
        }
        else if (isReplacing)
        {
            otherSlot.GetComponent<CSlotInfo>().m_itemSlot = m_itemSlot;
            isReplacing = false;
            Debug.Log("Replaced");
        }
        else
        {
            if (m_slotType == ItemSlotType.ItemBar)
                if ((transform.localPosition - Vector3.zero).magnitude >= 50)
                {
                    GetComponent<Image>().sprite = null;
                    GetComponent<Image>().color = Color.gray;
                    transform.GetChild(0).GetComponent<Text>().text = "";
                    m_itemSlot = null;
                    Debug.Log("item removed from hotbar");
                }
        }

        //Always return back to origin
        transform.localPosition = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.gameObject.GetComponent<CSlotInfo>())
        {
            switch (m_slotType)
            {
                case ItemSlotType.ItemBar:
                    if (_other.gameObject.GetComponent<CSlotInfo>().SlotType != ItemSlotType.ItemBar)
                        return;
                    isSwapping = true;
                    otherSlot = _other.gameObject;
                    Debug.Log("Swap detected");
                    return;
                case ItemSlotType.InventoryUse:
                    switch (_other.gameObject.GetComponent<CSlotInfo>().SlotType)
                    {
                        case ItemSlotType.ItemBar:
                            isReplacing = true;
                            otherSlot = _other.gameObject;
                            Debug.Log("Replace detected - InventoryUse to ItemBar");
                            break;
                        case ItemSlotType.InventoryUse:
                            isSwapping = true;
                            otherSlot = _other.gameObject;
                            Debug.Log("Swap detected - InventoryUse to InventoryUse");
                            break;
                    }
                    return;
                case ItemSlotType.InventoryEquip:
                    if (_other.gameObject.GetComponent<CSlotInfo>().SlotType != ItemSlotType.InventoryEquip)
                        return;
                    isSwapping = true;
                    otherSlot = _other.gameObject;
                    Debug.Log("Swap detected - InventoryEquip to InventoryEquip");
                    return;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isSwapping = false;
        isReplacing = false;
        otherSlot = null;
    }

    #endregion
}

[SerializeField]
public enum ItemSlotType
{
    ItemBar, InventoryUse, InventoryEquip, Smith, 
}