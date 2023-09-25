using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private Item[] items;
    public GameObject player; // игрок
    public Item item;

    private void Start()
    {
        item = items[Random.Range(0, items.Length)];
        GetComponent<SpriteRenderer>().sprite = item.Icon;
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 0.5f)
        {
            player.GetComponent<Player>().TakeItem(item);
            Destroy(gameObject);
        }
    }
}
