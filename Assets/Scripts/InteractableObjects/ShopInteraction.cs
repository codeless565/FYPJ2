using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteraction : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            if (ShopGenerator.Instance != null)
                ShopGenerator.Instance.OpenShopeMenu();
        }
    }


    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            if (ShopGenerator.Instance != null)
                ShopGenerator.Instance.CloseShopMenu();
        }
    }
}
