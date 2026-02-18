using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projectile;
    public float launchSpeed = 10f;

    float elapsedTime = 0f;
    float firetime = 3f;
    bool measureTime = false;

    void StartTimer()
    {
        measureTime = true;
    }

    void ResetTimer()
    {
        elapsedTime = 0f;
    }

    void Update()
    {
        StartTimer();

        if (measureTime)
            elapsedTime += Time.unscaledDeltaTime;

        if (elapsedTime >= firetime)
        {
            var _projectile = Instantiate(projectile, launchPoint.position, launchPoint.rotation);
            _projectile.transform.forward = launchPoint.forward;

            _projectile.GetComponent<Rigidbody>().linearVelocity = launchPoint.forward * launchSpeed;

            ResetTimer();
        }
    }

    void OnDrawGizmos()
    {
        if (launchPoint == null)
            return;

        Gizmos.color = Color.blueViolet; // color of line

        Vector3 start = launchPoint.position;
        Vector3 direction = launchPoint.forward; // change to .up or .right if needed
        Vector3 end = start + direction;

        Gizmos.DrawLine(start, end);
        Gizmos.DrawSphere(end, 0.1f); // shape at the end of the line
    }

}


