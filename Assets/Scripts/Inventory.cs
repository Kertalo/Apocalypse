using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Button deleteButton;
    [SerializeField] private RectTransform inventoryPanel;
    [SerializeField] private List<GameObject> itemsOnPanel;

    [SerializeField] private int deleteIndex;

    public void CloseInventory()
    {
        deleteButton.interactable = false;
        gameObject.SetActive(false);
    }

    public void DeleteItem()
    {
        player.items.Remove(player.items[deleteIndex]);
        deleteButton.interactable = false;
        OpenInventory();
    }

    public void ChangeDeleteIndex(string name)
    {
        for (int i = 0; i < player.items.Count; i++)
            if (player.items[i].Name == name)
                deleteIndex = i;
        deleteButton.interactable = true;
    }

    public void OpenInventory()
    {
        foreach (var item in itemsOnPanel)
            Destroy(item);
        itemsOnPanel.Clear();
        foreach (var item in player.items)
        {
            GameObject Item = Instantiate(itemPrefab, inventoryPanel.transform);
            Item.GetComponent<Image>().sprite = item.Icon;
            Item.GetComponent<ItemPrefab>().name = item.Name;
            if (item.Count > 1)
                Item.GetComponent<ItemPrefab>().count.text = item.Count.ToString();
            itemsOnPanel.Add(Item);
        }
    }
}
