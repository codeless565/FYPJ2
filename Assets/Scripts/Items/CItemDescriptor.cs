using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CItemDescriptor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    bool isCurrency = false;
    [SerializeField]
    string CurrencyDescription;

    public GameObject DescriptorBox;
    GameObject m_DescriptorOBJ;

	void Start ()
    {
        if (!isCurrency)
            if (GetComponent<CSlotInfo>() == null)
                Destroy(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isCurrency)
        {
            m_DescriptorOBJ = Instantiate(DescriptorBox, new Vector3(transform.position.x - DescriptorBox.GetComponent<RectTransform>().rect.width / 2 - gameObject.GetComponent<RectTransform>().rect.width / 2, transform.position.y, 0), Quaternion.identity);
            m_DescriptorOBJ.transform.SetParent(gameObject.transform);
            m_DescriptorOBJ.GetComponentInChildren<Text>().text = GetComponent<CSlotInfo>().ItemDescription;
        }
        else
        {
            m_DescriptorOBJ = Instantiate(DescriptorBox, new Vector3(transform.position.x + DescriptorBox.GetComponent<RectTransform>().rect.width / 2 + gameObject.GetComponent<RectTransform>().rect.width / 2, transform.position.y, 0), Quaternion.identity);
            m_DescriptorOBJ.transform.SetParent(gameObject.transform);
            m_DescriptorOBJ.GetComponentInChildren<Text>().text = CurrencyDescription;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (m_DescriptorOBJ != null)
            Destroy(m_DescriptorOBJ);
    }
}
