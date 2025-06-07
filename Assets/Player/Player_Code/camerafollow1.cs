using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private Vector3 shakeOffset;

    void LateUpdate()
    {
        transform.position = target.position + offset + shakeOffset;
    }

    // 供 CameraShake 调用
    public void SetShakeOffset(Vector3 offset)
    {
        shakeOffset = offset;
    }
}
