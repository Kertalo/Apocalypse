using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public new string name;
    public Sprite icon;
    public byte count;
}
