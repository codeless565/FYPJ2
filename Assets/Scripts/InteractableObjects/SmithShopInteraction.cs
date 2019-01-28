using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmithShopInteraction : MonoBehaviour
{
    [SerializeField]
    GameObject WeaponSelector;

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.GetComponent<CPlayer>() != null)
            WeaponSelector.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.GetComponent<CPlayer>() != null)
            WeaponSelector.SetActive(false);
    }
}
