using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private float takeDistance;
    [SerializeField] private Item[] items;
    public GameObject player; // игрок
    public int item;

    private void Start()
    {
        item = Random.Range(0, items.Length);
        GetComponent<SpriteRenderer>().sprite = player.GetComponent<Player>().allItems[item].icon;
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < takeDistance)
        {
            player.GetComponent<Player>().TakeItem(item);
            Destroy(gameObject);
        }
    }
}
