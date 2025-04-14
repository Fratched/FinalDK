using UnityEngine;
using System.Collections.Generic;

public class LinkedEnemy : MonoBehaviour
{
    public GameObject nextEnemyPrefab;
    private EC_Entity entity;

    // Static shared linked list across all LinkedEnemies
    public static LinkedList<LinkedEnemy> enemyChain = new LinkedList<LinkedEnemy>();

    // Keep reference to this enemy's node in the chain
    private LinkedListNode<LinkedEnemy> myNode;

    void Start()
    {
        entity = GetComponent<EC_Entity>();

        // Add this enemy to the shared chain
        myNode = enemyChain.AddLast(this);

        // Listen for when this enemy is removed/destroyed
        entity.removeEvent.AddListener(SpawnNext);
    }

    void SpawnNext()
    {
        if (nextEnemyPrefab != null && entity.room != null)
        {
            Vector3 spawnPosition = transform.position + Vector3.right * 2;
            GameObject next = Instantiate(nextEnemyPrefab, spawnPosition, Quaternion.identity);

            EC_Entity newEntity = next.GetComponent<EC_Entity>();
            newEntity.room = entity.room;
            newEntity.IsEnabled(true);
            entity.room.roomEntities.Add(newEntity);

            LinkedEnemy nextLinked = next.GetComponent<LinkedEnemy>();

            // Add the new enemy to the chain *after this one*
            LinkedListNode<LinkedEnemy> newNode = enemyChain.AddAfter(myNode, nextLinked);
            nextLinked.myNode = newNode;

            // Prevent infinite spawns
            Destroy(nextLinked); // If you want only one spawn per death
        }
    }

    public static void ClearChain()
    {
        enemyChain.Clear();
    }
}
