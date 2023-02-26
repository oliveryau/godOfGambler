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
        Instantiate(platformPrefab, new Vector2(75.5f, -12.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(98f, -12.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(103f, -15.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(127f, -10.5f), platformPrefab.transform.rotation);
    }

    public IEnumerator SpawnPlatform(Vector2 spawnPosition)
    {
        yield return new WaitForSeconds(2f);
        Instantiate(platformPrefab, spawnPosition, platformPrefab.transform.rotation);
    }
}
