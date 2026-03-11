using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [SerializeField] bool isStationary;

    [Header("Target follow Settings")]
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothTime = 0.25f;
    Vector3 velocity = Vector3.zero;

    [SerializeField] bool hasShake;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (!isStationary)
            target = GameObject.FindGameObjectWithTag("Player").transform;

        if (hasShake)
            CameraShake(0.05f, 100);
    }

    private void OnEnable()
    {
        // health.OnDeath += ChangeTarget;
    }

    private void OnDisable()
    {
        // health.OnDeath -= ChangeTarget;
    }

    private void FixedUpdate()
    {
        if (!isStationary)
        {
            FollowTarget();
        }
    }

    Vector3 FollowTarget()
    {
        if (target == null)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }

            return Vector3.zero;
        }

        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        return targetPosition;
    }

    public void ChangeTarget(Transform _newTarget)
    {
        target = _newTarget;
    }

    public void CameraShake(float _shakeForce = 0.4f, float _duration = 0.5f)
    {
        StartCoroutine(ShakeCoroutine(_shakeForce, _duration));
    }

    private IEnumerator ShakeCoroutine(float shakeForce, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float xOffset = Random.Range(-1f, 1f) * shakeForce;
            float yOffset = Random.Range(-1f, 1f) * shakeForce;

            transform.position = FollowTarget() + new Vector3(xOffset, yOffset, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = FollowTarget();
    }
}
