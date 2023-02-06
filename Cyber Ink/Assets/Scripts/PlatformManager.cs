using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance = null;
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
        Instantiate(platformPrefab, new Vector2(39.5f, 9f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(44.5f, 6f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(39.5f, 3f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(49.5f, 9f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(62f, -20f), platformPrefab.transform.rotation);
    }

    public IEnumerator SpawnPlatform(Vector2 spawnPosition)
    {
        yield return new WaitForSeconds(2f);
        Instantiate(platformPrefab, spawnPosition, platformPrefab.transform.rotation);
    }
}
