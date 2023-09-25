using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public byte Count;
}
