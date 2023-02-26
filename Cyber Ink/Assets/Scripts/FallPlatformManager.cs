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
        //Checkpoint 1 - 2
        Instantiate(platformPrefab, new Vector2(53.75f, -12.25f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(50f, -21.25f), platformPrefab.transform.rotation);

        //Checkpoint 2 - 3
        Instantiate(platformPrefab, new Vector2(74.5f, -12.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(102f, -13.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(127f, -10.5f), platformPrefab.transform.rotation);

        //Checkpoint 3 - 4
        Instantiate(platformPrefab, new Vector2(156f, -10.25f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(161f, -13.25f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(176.5f, -11.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(184.5f, -15.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(192.5f, -19.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(229f, -13.25f), platformPrefab.transform.rotation);

        //Checkpoint 4 - 5
        Instantiate(platformPrefab, new Vector2(252.5f, -17.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(259.5f, -21.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(274.5f, -22.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(311.5f, -8.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(326.5f, -8.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(333.5f, -11.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(340.5f, -15.5f), platformPrefab.transform.rotation);
    }

    public IEnumerator SpawnPlatform(Vector2 spawnPosition)
    {
        yield return new WaitForSeconds(2f);
        Instantiate(platformPrefab, spawnPosition, platformPrefab.transform.rotation);
    }
}