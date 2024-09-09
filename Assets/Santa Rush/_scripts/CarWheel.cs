using UnityEngine;

public class CarWheel : MonoBehaviour
{
    public WheelCollider targetWheel;
    Vector3 wheelPosition = new Vector3();
    Quaternion wheelRotation = new Quaternion();

    private void Update()
    {
        targetWheel.GetWorldPose(out wheelPosition, out wheelRotation);

        transform.position = wheelPosition;
        transform.rotation = wheelRotation;
    }
}
