using Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInitialItem : MonoBehaviour
{
    [SerializeField] InventoryController inventoryController;

    private void Start()
    {
        Inventory.Model.InventoryItem a = inventoryController.initialItems[0];


        print(PlayerAttributes.NumOfFood);
        a.quantity = PlayerAttributes.NumOfFood;
        inventoryController.initialItems[0] = a;
    }
}