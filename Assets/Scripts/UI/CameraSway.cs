using UnityEngine;

public class CameraSway : MonoBehaviour
{
    public Transform target; // The object the camera should always look at
    public float swaySpeed = 1.0f;
    public float swayAmount = 5.0f;

    private float time;

    void Update()
    {
        if (target == null) return;

        // Calculate sway
        time += Time.deltaTime * swaySpeed;
        float swayAngle = Mathf.Sin(time) * swayAmount;

        // Look at the target first
        transform.LookAt(target);

        // Apply the sway by rotating around the Y-axis
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + swayAngle, transform.eulerAngles.z);
    }
}
