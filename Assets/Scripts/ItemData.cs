using UnityEngine;

public class ItemData : MonoBehaviour
{
    public enum ItemType { Head, Body, Legs }
    public ItemType type;

    public Sprite equipmentSprite;
}