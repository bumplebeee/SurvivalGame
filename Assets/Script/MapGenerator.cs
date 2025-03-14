using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject mapPrefab;
    public Transform player;
    public int chunkSizeX = 30;
    public int chunkSizeY = 16;
    public float loadDistance = 15f;

    private Dictionary<Vector2Int, GameObject> activeChunks = new Dictionary<Vector2Int, GameObject>();
    private Vector2Int currentChunk;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentChunk = GetChunkPosition(player.position);
        LoadChunk(currentChunk);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int newChunk = GetChunkPosition(player.position);

        if (newChunk != currentChunk)
        {
            currentChunk = newChunk;
            LoadChunksAround(currentChunk);
            UnloadFarChunks();
        }
    }

    Vector2Int GetChunkPosition(Vector3 position)
    {
        return new Vector2Int(
            Mathf.FloorToInt(position.x / chunkSizeX),
            Mathf.FloorToInt(position.y / chunkSizeY)
        );
    }

    void LoadChunksAround(Vector2Int centerChunk)
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2Int chunkPos = centerChunk + new Vector2Int(x, y);
                if (!activeChunks.ContainsKey(chunkPos))
                {
                    LoadChunk(chunkPos);
                }
            }
        }
    }
    void LoadChunk(Vector2Int chunkPos)
    {
        Vector3 worldPos = new Vector3(chunkPos.x * chunkSizeX, chunkPos.y * chunkSizeY, 0);
        GameObject newChunk = Instantiate(mapPrefab, worldPos, Quaternion.identity);
        activeChunks.Add(chunkPos, newChunk);
    }
    void UnloadFarChunks()
    {
        List<Vector2Int> chunksToRemove = new List<Vector2Int>();

        foreach (var chunk in activeChunks)
        {
            float distance = Vector2.Distance(player.position, chunk.Value.transform.position);
            if (distance > loadDistance * 2) 
            {
                Destroy(chunk.Value);
                chunksToRemove.Add(chunk.Key);
            }
        }

        foreach (var chunkKey in chunksToRemove)
        {
            activeChunks.Remove(chunkKey);
        }
    }
}
