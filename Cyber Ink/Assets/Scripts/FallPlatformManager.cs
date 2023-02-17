using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatformManager : MonoBehaviour
{
    public static FallPlatformManager Instance = null;
    [SerializeField] private GameObject platformPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(platformPrefab, new Vector2(166f, 13f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(107.25f, 5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(168f, -45.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(163.5f, -45.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(147f, -55f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(86f, -67.5f), platformPrefab.transform.rotation);
    }

    public IEnumerator SpawnPlatform(Vector2 spawnPosition)
    {
        yield return new WaitForSeconds(2f);
        Instantiate(platformPrefab, spawnPosition, platformPrefab.transform.rotation);
    }
}
