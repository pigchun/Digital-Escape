using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private CameraFollow cameraFollow; // 引用 CameraFollow
    private float shakeTimeRemaining, shakePower, shakeFadeTime;

    void Awake()
    {
        Instance = this;
        cameraFollow = GetComponent<CameraFollow>();
    }

    void Update()
    {
        if (shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;

            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;

            cameraFollow.SetShakeOffset(new Vector3(xAmount, yAmount, 0));

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);
        }
        else
        {
            cameraFollow.SetShakeOffset(Vector3.zero);
        }
    }

    public void ShakeCamera(float duration, float magnitude)
    {
        shakeTimeRemaining = duration;
        shakePower = magnitude;

        shakeFadeTime = magnitude / duration;
    }
}
