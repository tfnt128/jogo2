using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 0)]
public class Item: ScriptableObject
{
    public int ID;
    public string name;
    public Sprite icon;

    public int maxStack;
}