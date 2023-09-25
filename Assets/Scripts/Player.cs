using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[Serializable]
public class DataInventory
{
    public int helmet;
    public int ak;
    public int makarov;
}

public class Player : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    [SerializeField] private float speed;
    [SerializeField] private byte health;
    [SerializeField] private byte shotDamage;
    [SerializeField] private byte bulletsCount;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Transform healthPanel;

    public void Damage(byte damage)
    {
        if (health - damage <= 0)
            Destroy(gameObject);
        health -= damage;
        healthPanel.localScale = new Vector3((float)health / 100, 1, 1);
    }

    public void Shot()
    {
        GameObject[] mutants = GameObject.FindGameObjectsWithTag("Mutant");
        if (mutants.Length == 0 || bulletsCount == 0)
            return;

        int closestMutantIndex = 0;
        float closestMutantDistance = Vector3.Distance(mutants[0].transform.position, transform.position);
        for (int i = 1; i < mutants.Length; i++)
        {
            float distance = Vector3.Distance(mutants[i].transform.position, transform.position);
            if (distance < closestMutantDistance)
            {
                closestMutantDistance = distance;
                closestMutantIndex = i;
            }
        }
        if (closestMutantDistance > 4)
            return;

        mutants[closestMutantIndex].GetComponent<Mutant>().Damage(shotDamage);

        bulletsCount--;
    }

    public void TakeItem(Item item)
    {
        if (items.Contains(item))
            items[items.IndexOf(item)].Count++;
        else
        {
            item.Count = 1;
            items.Add(item);
        }
    }

    private void Update()
    {
        transform.position += new Vector3(joystick.Horizontal * speed, joystick.Vertical * speed);
    }

    public void SaveData()
    {
        DataInventory data = new DataInventory();

        Item item = items.Find(i => i.name == "Helmet");
        data.helmet = item == null ? 0 : item.Count;

        item = items.Find(i => i.name == "AK-74");
        data.ak = item == null ? 0 : item.Count;

        item = items.Find(i => i.name == "Makarov");
        data.makarov = item == null ? 0 : item.Count;
    }

    public void LoadData()
    {

    }
}
