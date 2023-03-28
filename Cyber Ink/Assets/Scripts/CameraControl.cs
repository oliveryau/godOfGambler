using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public SceneManagement sceneManagement;

    [Header("Player Focus")]
    [SerializeField] private Transform target; //Player
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;

    [Header("Screen Shake Effect")]
    public AnimationCurve curve;
    public float duration = 0.2f;

    private void Update()
    {
        //Smooth camera follow
        if (sceneManagement.scene.name == "Level 3") //Different offset
        {
            Vector3 targetPos = new Vector3(target.position.x, 2.5f, -10f);
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        }
        else
        {
            Vector3 targetPos = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);   
        }
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
}
