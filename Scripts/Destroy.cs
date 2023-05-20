using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject player;
    public GameObject platformPrefab;
    public GameObject springPrefab;

    private List<GameObject> spawnedPlatforms = new List<GameObject>();

    private float spawnHeightOffset = 5f; // Vertical offset for platform spawning

    private void Update()
    {
        float playerHeight = player.transform.position.y;

        // Check if the player has reached the next spawn height
        if (playerHeight >= GetNextSpawnHeight() - spawnHeightOffset)
        {
            int numPlatformsToSpawn = Random.Range(1, 4); // Random number of platforms to spawn
            for (int i = 0; i < numPlatformsToSpawn; i++)
            {
                SpawnPlatform();
            }
        }
    }

    private void SpawnPlatform()
    {
        // Randomly select a platform prefab
        GameObject selectedPlatformPrefab = GetRandomPlatformPrefab();

        // Spawn the selected platform prefab at a random position
        Vector2 spawnPosition = GetSpawnPosition();
        GameObject newPlatform = Instantiate(selectedPlatformPrefab, spawnPosition, Quaternion.identity);
        spawnedPlatforms.Add(newPlatform);
    }

    private GameObject GetRandomPlatformPrefab()
    {
        GameObject[] platformPrefabs = { platformPrefab, springPrefab };

        // Randomly select a platform prefab from the available options
        int randomIndex = Random.Range(0, platformPrefabs.Length);
        GameObject selectedPlatformPrefab = platformPrefabs[randomIndex];

        return selectedPlatformPrefab;
    }

    private Vector2 GetSpawnPosition()
    {
        float minX = -4.5f;
        float maxX = 4.5f;

        float spawnHeight = GetNextSpawnHeight();
        float minY = spawnHeight + spawnHeightOffset;
        float maxY = minY + 1f;

        Vector2 spawnPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        return spawnPosition;
    }

    private float GetNextSpawnHeight()
    {
        if (spawnedPlatforms.Count == 0)
        {
            // Spawn the first platform above the player's initial position
            return player.transform.position.y + spawnHeightOffset;
        }
        else
        {
            // Spawn subsequent platforms above the highest spawned platform
            float highestPlatformHeight = spawnedPlatforms[spawnedPlatforms.Count - 1].transform.position.y;
            return highestPlatformHeight + spawnHeightOffset;
        }
    }

    public void TriggerDeath()
    {
        foreach (GameObject platform in spawnedPlatforms)
        {
            Destroy(platform);
        }
        spawnedPlatforms.Clear();
    }
}
