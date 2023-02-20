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
        //Top side
        Instantiate(platformPrefab, new Vector2(170f, 15f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(165f, 15f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(106.5f, 4.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(128f, -2.25f), platformPrefab.transform.rotation);

        //Right bottom side
        Instantiate(platformPrefab, new Vector2(177f, -48.25f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(126f, -52.75f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(120f, -58.75f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(167.75f, -46.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(163.5f, -46.5f), platformPrefab.transform.rotation);

        //Left bottom side
        Instantiate(platformPrefab, new Vector2(86f, -67.5f), platformPrefab.transform.rotation);
    }

    public IEnumerator SpawnPlatform(Vector2 spawnPosition)
    {
        yield return new WaitForSeconds(2f);
        Instantiate(platformPrefab, spawnPosition, platformPrefab.transform.rotation);
    }
}
