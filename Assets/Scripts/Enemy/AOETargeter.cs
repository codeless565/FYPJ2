using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOETargeter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.GetComponent<IEntity>() != null)
        {
            GetComponentInParent<IAreaOfEffect>().TargetList.Add(_other.gameObject);
            Debug.Log("Added " + _other.name + " to TargetList");
        }
    }

    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.GetComponent<IEntity>() != null)
        {
            GetComponentInParent<IAreaOfEffect>().TargetList.Remove(_other.gameObject);
            Debug.Log("Removed " + _other.name + " to TargetList");
        }
    }
}
