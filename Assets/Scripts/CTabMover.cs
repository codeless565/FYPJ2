using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CTabMover : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler
{
    float m_TabWidth;
    float m_TabHeight;

    GameObject m_parentTab;
    Vector2 m_displacementVec;

    // Use this for initialization
    void Start () {
        m_parentTab = transform.parent.gameObject;
        m_TabWidth = m_parentTab.GetComponent<RectTransform>().rect.width * Screen.width/1920;
        m_TabHeight = m_parentTab.GetComponent<RectTransform>().rect.height * Screen.height/1080;

        m_displacementVec = new Vector2();

        Debug.Log("TAB SCREEN: " + Screen.width + ", " + Screen.height);
        Debug.Log("TAB RECT: " + m_TabWidth + ", " + m_TabHeight);

    }

    // Update is called once per frame
    void Update ()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            if (Input.GetMouseButtonDown(0))
                m_displacementVec = (Vector2)Input.mousePosition - (Vector2)m_parentTab.transform.position;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_displacementVec = (Vector2)Input.mousePosition - (Vector2)m_parentTab.transform.position;
        Debug.Log("clicked Tab: displaceVec = " + m_displacementVec);
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_parentTab.transform.position = (Vector2)Input.mousePosition - m_displacementVec;
        Debug.Log("dragging");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_TabWidth = m_parentTab.GetComponent<RectTransform>().rect.width * Screen.width / 1920;
        m_TabHeight = m_parentTab.GetComponent<RectTransform>().rect.height * Screen.height / 1080;

        if (m_parentTab.transform.position.x < m_TabWidth * 0.5f)
            m_parentTab.transform.position = new Vector3(m_TabWidth * 0.5f, m_parentTab.transform.position.y, 0);
        if (m_parentTab.transform.position.x > Screen.width - m_TabWidth * 0.5f)
            m_parentTab.transform.position = new Vector3(Screen.width - m_TabWidth * 0.5f, m_parentTab.transform.position.y, 0);

        if (m_parentTab.transform.position.y < m_TabHeight * 0.5f)
            m_parentTab.transform.position = new Vector3(m_parentTab.transform.position.x, m_TabHeight * 0.5f, 0);
        if (m_parentTab.transform.position.y > Screen.height - m_TabHeight * 0.5f)
            m_parentTab.transform.position = new Vector3(m_parentTab.transform.position.x, Screen.height - m_TabHeight * 0.5f, 0);
    }
}
