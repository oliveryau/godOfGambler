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
        Instantiate(platformPrefab, new Vector2(36.5f, 0f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(36.5f, 4.75f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(42f, 7.75f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(47.5f, 10.75f), platformPrefab.transform.rotation);
    }

    public IEnumerator SpawnPlatform(Vector2 spawnPosition)
    {
        yield return new WaitForSeconds(2f);
        Instantiate(platformPrefab, spawnPosition, platformPrefab.transform.rotation);
    }
}
