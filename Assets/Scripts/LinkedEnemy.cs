using UnityEngine;
using System.Collections.Generic;

public class LinkedEnemy : MonoBehaviour
{
    // Enemy prefab to spawn next in the sequence
    public GameObject nextEnemyPrefab;

    // The linked list of enemies in the sequence
    private LinkedList<LinkedEnemy> enemyChain;

    // Entity reference to access enemy components
    private EC_Entity entity;

    void Start()
    {
        // Initialize the linked list
        enemyChain = new LinkedList<LinkedEnemy>();

        // Add the current enemy to the linked list as the first enemy
        LinkedListNode<LinkedEnemy> currentEnemyNode = enemyChain.AddLast(this);

        // Set up the enemy entity
        entity = GetComponent<EC_Entity>();
        entity.removeEvent.AddListener(SpawnNext);  // Listen for the removal event to spawn the next enemy
    }

    void SpawnNext()
    {
        // Check if there's a next enemy prefab and we are not at the end of the chain
        if (nextEnemyPrefab != null && entity.room != null)
        {
            // Instantiate the next enemy
            GameObject next = Instantiate(nextEnemyPrefab, transform.position + Vector3.right * 2, Quaternion.identity);

            // Create the new enemy and add it to the linked list
            LinkedEnemy nextLinkedEnemy = next.GetComponent<LinkedEnemy>();
            enemyChain.AddLast(nextLinkedEnemy);

            // Add the new enemy to the room's entities
            EC_Entity newEntity = next.GetComponent<EC_Entity>();
            newEntity.room = entity.room;
            entity.room.roomEntities.Add(newEntity);

            // Enable the new enemy
            newEntity.IsEnabled(true);

            // Optionally: Remove LinkedEnemy script from the new enemy to stop further spawns
            Destroy(newEntity.GetComponent<LinkedEnemy>());
        }
    }

    // Function to clear the linked list (when the chain ends or for cleanup)
    public void ClearChain()
    {
        enemyChain.Clear();
    }
}
