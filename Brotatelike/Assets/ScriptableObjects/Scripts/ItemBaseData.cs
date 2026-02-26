using UnityEngine;

[CreateAssetMenu(fileName = "ItemBaseData", menuName = "ItemBaseData")]
public abstract class ItemBaseData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public string itemText;
    public int itemPrice;
}
