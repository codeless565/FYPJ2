using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CSlotInfo : MonoBehaviour, IDragHandler, IEndDragHandler
{
    bool moving;
    CItemSlot m_itemSlot;
    public ItemSlotType m_slotType;

    Text QuantText;

    //Impt info for drag
    float m_OriginalPosX;
    float m_OriginalPosY;
    float m_OriginalWidth;
    float m_OriginalHeight;
    GameObject m_OriginalParent;
    GameObject m_MovingParent;

    public void Init(CItemSlot _item, GameObject _movingParent = null )
    {
        moving = false;

        m_itemSlot = _item;
        GetComponent<Image>().sprite = m_itemSlot.itemInfo.ItemSprite;

        if (transform.GetComponentInParent<Text>() == null)
        {
            if (transform.GetComponentInChildren<Text>() == null)
                return;
            else
                QuantText = transform.GetComponentInChildren<Text>();
        }
        else
            QuantText = transform.GetComponentInParent<Text>();

        QuantText.text = "X " + m_itemSlot.quantity.ToString();

        m_OriginalParent = transform.parent.gameObject;
        m_OriginalPosX = GetComponent<RectTransform>().localPosition.x;
        m_OriginalPosY = GetComponent<RectTransform>().localPosition.y;
        m_OriginalWidth = GetComponent<RectTransform>().rect.width;
        m_OriginalHeight = GetComponent<RectTransform>().rect.height;

        m_MovingParent = _movingParent;
    }

    public void Update()
    {
        if (m_itemSlot != null && !moving)
        {
            if (m_itemSlot.quantity <= 0)
                GetComponent<Image>().color = Color.grey;
            else
                GetComponent<Image>().color = Color.white;
            QuantText.text = "X " + m_itemSlot.quantity.ToString();
        }
    }

    public ItemSlotType SlotType
    { get { return m_slotType; } }

    public string ItemDescription
    {
        get { return m_itemSlot.itemInfo.Description; }
    }

    /********************
     * Drag Drop Control
     ********************/
    #region DragDrop

    public void OnDrag(PointerEventData eventData)
    {
        if (SlotType != ItemSlotType.InventoryUse)
            return;

        if (m_itemSlot == null)
            return;

        moving = true;
        transform.SetParent(m_MovingParent.transform);
        transform.position = Input.mousePosition; 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (SlotType != ItemSlotType.InventoryUse)
            return;

        moving = false;
        //Always return back to origin
        transform.SetParent(m_OriginalParent.transform);
        transform.localPosition = new Vector3(m_OriginalPosX, m_OriginalPosY, 0);
        GetComponent<RectTransform>().sizeDelta = new Vector2(m_OriginalWidth, m_OriginalHeight);

        Debug.Log("w: " + GetComponent<RectTransform>().rect.width + "act w: " + m_OriginalWidth + " h: " + GetComponent<RectTransform>().rect.height + "act h: " + m_OriginalHeight);
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (SlotType != ItemSlotType.InventoryUse)
            return;

        if (_other.gameObject.GetComponent<CSlotInfo>())
        {
            switch (m_slotType)
            {
                case ItemSlotType.InventoryUse:
                    switch (_other.gameObject.GetComponent<CSlotInfo>().SlotType)
                    {
                        case ItemSlotType.Shop:
                            //sell the item
                            break;
                    }
                    return;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
    }

    #endregion
}

[SerializeField]
public enum ItemSlotType
{
    None, ItemBar, InventoryUse, Shop 
}