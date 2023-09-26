using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static UnityEditor.Progress;

public class SaveLoad : MonoBehaviour
{
    [SerializeField] private Player player;

    private void Start()
    {
        LoadData();
    }

    [Serializable]
    public class DataInventory
    {
        public int ak;
        public int helmet;
        public int makarov;
    }

    [ContextMenu("Save")]
    public void SaveData()
    {
        List<Item> inventory = player.inventory;
        DataInventory data = new DataInventory();

        Item item = inventory.Find(i => i.name == "AK-74");
        data.ak = item == null ? 0 : item.count;

        item = inventory.Find(i => i.name == "Helmet");
        data.helmet = item == null ? 0 : item.count;

        item = inventory.Find(i => i.name == "Makarov");
        data.makarov = item == null ? 0 : item.count;

        File.WriteAllText(Application.streamingAssetsPath + "/JSON.json",
            JsonUtility.ToJson(data));
    }

    [ContextMenu("Load")]
    public void LoadData()
    {
        DataInventory data = JsonUtility.FromJson<DataInventory>(
            File.ReadAllText(Application.streamingAssetsPath + "/JSON.json"));
        player.inventory.Clear();
        for (int i = 0; i < data.ak; i++)
            player.TakeItem(0);
        for (int i = 0; i < data.helmet; i++)
            player.TakeItem(1);
        for (int i = 0; i < data.makarov; i++)
            player.TakeItem(2);
    }
}
