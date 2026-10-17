using UnityEngine;

public class Collectable : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int xpScore = 10;
    [SerializeField] private float vacuumPullSpeed = 8f;

    private bool isBeingPulled = false;
    private Transform vacuumTransform;

    private void Update()
    {
        if (isBeingPulled && vacuumTransform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, vacuumTransform.position, vacuumPullSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, vacuumTransform.position) < 0.2f)
            {
                if (LevelManager.Instance != null)
                {
                    LevelManager.Instance.GetXP(xpScore);
                }

                Destroy(gameObject); 
            }
        }
    }

    public void StartVacuumPull(Transform targetVacuumTransform)
    {
        isBeingPulled = true;
        vacuumTransform = targetVacuumTransform;
    }
}