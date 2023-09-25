using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnMutants : MonoBehaviour
{
    [SerializeField] private GameObject mutant;
    [SerializeField] private int mutantsCount;
    [SerializeField] private Transform[] spawnPositions;

    void Start()
    {
        if (mutantsCount > spawnPositions.Length)
            mutantsCount = spawnPositions.Length;
        List<int> takePositions = new List<int>();
        for (int i = 0; i < mutantsCount; i++)
        {
            int random;
            do random = Random.Range(0, spawnPositions.Length);
            while (takePositions.Contains(random));
            takePositions.Add(random);
            Instantiate(mutant, spawnPositions[random].position, transform.rotation, transform);
        }
    }
}
