using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CTabMover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IEndDragHandler
{
    GameObject parentTab;
    Vector2 m_displacementVec;

    // Use this for initialization
    void Start () {
        GetComponent<Image>().color = Color.grey;
        parentTab = transform.parent.gameObject;
        m_displacementVec = new Vector2();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            if (Input.GetMouseButtonDown(0))
                m_displacementVec = (Vector2)Input.mousePosition - (Vector2)parentTab.transform.position;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = Color.green;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = Color.grey;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_displacementVec = (Vector2)Input.mousePosition - (Vector2)parentTab.transform.position;
        Debug.Log("clicked Tab: displaceVec = " + m_displacementVec);
    }

    public void OnDrag(PointerEventData eventData)
    {
        parentTab.transform.position = (Vector2)Input.mousePosition - m_displacementVec;
        Debug.Log("dragging");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
}
