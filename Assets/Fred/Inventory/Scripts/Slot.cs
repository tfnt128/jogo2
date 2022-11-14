using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int ID;
    public InventoryManager manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<InventoryManager>();
    }

    public void Select(Sprite selectSprite)
    {
        gameObject.GetComponent<Image>().sprite = selectSprite;
    }

    public void InstantiateMenu(GameObject itemMenu)
    {
        Instantiate(itemMenu);
    }

    public void Deselect(Sprite deselectSprite)
    {
        gameObject.GetComponent<Image>().sprite = deselectSprite;
    }

    public void SetID()
    {
        manager.currentSlot = ID;
        manager.PickupDropInventory();
    }
}
