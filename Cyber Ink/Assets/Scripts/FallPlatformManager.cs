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
        Instantiate(platformPrefab, new Vector2(48.5f, -9.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(53.5f, -13.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(51f, -21.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(61f, -11.5f), platformPrefab.transform.rotation);

        //Checkpoint 2 - 3
        Instantiate(platformPrefab, new Vector2(72.5f, -15.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(102.5f, -14.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(126.5f, -10.75f), platformPrefab.transform.rotation);

        //Checkpoint 3 - 4
        Instantiate(platformPrefab, new Vector2(156.5f, -10.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(162.5f, -13.5f), platformPrefab.transform.rotation);

        //Checkpoint 4 - 5
        Instantiate(platformPrefab, new Vector2(213.5f, -19.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(220.5f, -23.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(233.5f, -23.5f), platformPrefab.transform.rotation);

        //Checkpoint 5 - 6
        Instantiate(platformPrefab, new Vector2(266.5f, -15.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(273.5f, -15.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(287.5f, -16.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(294.5f, -16.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(301.5f, -16.5f), platformPrefab.transform.rotation);

        //Checkpoint 6 - 7
        Instantiate(platformPrefab, new Vector2(351.5f, -11.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(358.5f, -11.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(365.5f, -14.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(379.5f, -14.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(386.5f, -17.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(393.5f, -20.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(407.5f, -20.5f), platformPrefab.transform.rotation);

        //Checkpoint 7 - 8
        Instantiate(platformPrefab, new Vector2(423.5f, -20.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(437.5f, -20.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(446.5f, -20.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(461.5f, -20.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(468.5f, -18f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(461.5f, -15.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(476.5f, -18f), platformPrefab.transform.rotation);
    }

    public IEnumerator SpawnPlatform(Vector2 spawnPosition)
    {
        yield return new WaitForSeconds(2f);
        Instantiate(platformPrefab, spawnPosition, platformPrefab.transform.rotation);
    }
}