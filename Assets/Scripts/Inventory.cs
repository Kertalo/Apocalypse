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
        player.inventory.Remove(player.inventory[deleteIndex]);
        deleteButton.interactable = false;
        OpenInventory();
    }

    public void ChangeDeleteIndex(string name)
    {
        for (int i = 0; i < player.inventory.Count; i++)
            if (player.inventory[i].name == name)
                deleteIndex = i;
        deleteButton.interactable = true;
    }

    public void OpenInventory()
    {
        foreach (var item in itemsOnPanel)
            Destroy(item);
        itemsOnPanel.Clear();
        foreach (var item in player.inventory)
        {
            GameObject Item = Instantiate(itemPrefab, inventoryPanel.transform);
            Item.GetComponent<Image>().sprite = item.icon;
            Item.GetComponent<ItemPrefab>().name = item.name;
            if (item.count > 1)
                Item.GetComponent<ItemPrefab>().count.text = item.count.ToString();
            itemsOnPanel.Add(Item);
        }
    }
}
