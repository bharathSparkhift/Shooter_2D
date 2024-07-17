using UnityEngine;

public class GyroscopeController : MonoBehaviour
{
    private Gyroscope gyro;
    private bool gyroSupported;

    void Start()
    {
        gyroSupported = SystemInfo.supportsGyroscope;

        if (gyroSupported)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
        }
        else
        {
            Debug.Log("Gyroscope not supported on this device");
        }
    }

    void Update()
    {
        if (gyroSupported)
        {
            // Gyroscope rotation rate
            Vector3 rotationRate = gyro.rotationRate;

            // Gyroscope attitude (rotation) in quaternion form
            Quaternion attitude = gyro.attitude;

            // Display the gyroscope data
            Debug.Log("Rotation Rate: " + rotationRate);
            Debug.Log("Attitude: " + attitude);
        }
    }
}
