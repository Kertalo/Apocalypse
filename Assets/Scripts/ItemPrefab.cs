using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemPrefab : MonoBehaviour
{
    public new string name;
    public TextMeshProUGUI count;

    public void Click()
    {
        transform.parent.parent.GetComponent<Inventory>().ChangeDeleteIndex(name);
    }
}
