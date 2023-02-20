using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("Player Focus")]
    [SerializeField] private Transform target; //Player
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;

    [Header("Screen Shake Effect")]
    public AnimationCurve curve;
    public float duration = 0.15f;

    [Header("Keys")]
    public Transform firstKey;
    public GameObject firstKeyTrigger;
    public bool movingTowardsFirstKey = false;
    public Transform secondKey;
    public GameObject secondKeyTrigger;
    public bool movingTowardsSecondKey = false;
    public Transform thirdKey;
    public GameObject thirdKeyTrigger;
    public bool movingTowardsThirdKey = false;
    public float switchTime;

    private void Update()
    {
        if (movingTowardsFirstKey == false || movingTowardsSecondKey == false || movingTowardsThirdKey == false)
        {
            //Smooth camera follow
            Vector3 targetposition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetposition, ref velocity, smoothTime);   
        }

        //if (movingTowardsFirstKey)
        //{
        //    MoveTowardsTarget(firstKey);
        //}

        //if (movingTowardsSecondKey)
        //{
        //    MoveTowardsTarget(secondKey);
        //}

        //if (movingTowardsThirdKey)
        //{
        //    MoveTowardsTarget(thirdKey);
        //}
    }
    
    public IEnumerator ScreenShake()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
    }

    //public void MoveTowardsTarget(Transform key)
    //{
    //    Vector3 targetposition = key.position + offset;
    //    transform.position = Vector3.SmoothDamp(target.position, targetposition, ref velocity, switchTime * Time.deltaTime);
    //}
}
