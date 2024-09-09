using UnityEngine;

public class LerpCameraOnStart : MonoBehaviour
{
    public Transform Target;
    public float Speed;
    public bool Lerp;

    void Update()
    {
        if (!Lerp) return;
        if (Target != null)
        {
            transform.position = Vector3.Lerp(transform.position, Target.position, Speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Target.rotation, Speed * Time.deltaTime);
        }
    }
}
