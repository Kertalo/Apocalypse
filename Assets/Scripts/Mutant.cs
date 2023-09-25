using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutant : MonoBehaviour
{
    [SerializeField] private GameObject player; // игрок
    [SerializeField] private Transform healthPanel; // панель здоровья
    [SerializeField] private GameObject dropItem;

    [SerializeField] private byte health; // здоровье, max = 100
    [SerializeField] private float speed; // скорость
    [SerializeField] private byte damage; // наносимый урон
    [SerializeField] private float frequency; // частота нанесения урона
    [SerializeField] private float viewDistance; // максимальное обозримое расстояние
    [SerializeField] private float kickDistance; // максимальное расстояние для удара

    private bool isPlayerClose = false; // как только игрок подходит на расстояние
                                        // viewDistance, то становится true
    private bool isKick = false; // равно true во время нанесения удара

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void Damage(byte damage) // получаем урон от игрока
    {
        if (health - damage <= 0)
        {
            var newDropItem = Instantiate(dropItem, transform.position, transform.rotation);
            newDropItem.GetComponent<DropItem>().player = player;
            Destroy(gameObject);
        }
            
        health -= damage;
        healthPanel.localScale = new Vector3((float)health / 100, 1, 1);
    }

    private void EndKick()
    {
        isKick = false;
    }

    private void StartKick() // наносим урон игроку
    {
        isKick = true;
        player.GetComponent<Player>().Damage(damage);
        Invoke(nameof(EndKick), frequency);
    }

    private void Update()
    {
        if (player == null)
            return;

        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (!isPlayerClose && distance < viewDistance)
            isPlayerClose = true;
        if (isPlayerClose)
        {
            if (!isKick && distance < kickDistance)
                StartKick();
            transform.position =
                Vector3.MoveTowards(transform.position, player.transform.position, speed);
        }
    }
}
