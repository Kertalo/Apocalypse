using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Item> allItems = new List<Item>();
    public List<Item> inventory = new List<Item>();
    [SerializeField] private float speed;
    [SerializeField] private byte health;
    [SerializeField] private byte shotDamage;
    [SerializeField] private byte shotDistance;
    [SerializeField] private byte bulletsCount;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Transform healthPanel;
    [SerializeField] private new Transform camera;

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
        if (closestMutantDistance > shotDistance)
            return;

        mutants[closestMutantIndex].GetComponent<Mutant>().Damage(shotDamage);

        bulletsCount--;
    }

    public void TakeItem(int item)
    {
        if (inventory.Contains(allItems[item]))
            inventory[inventory.IndexOf(allItems[item])].count++;
        else
        {
            allItems[item].count = 1;
            inventory.Add(allItems[item]);
        }
    }

    private void Update()
    {
        transform.position += new Vector3(joystick.Horizontal * speed, joystick.Vertical * speed);
        camera.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
