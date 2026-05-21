using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public float range = 2f;
    public float duration = 1.5f;
    public float movementSpeed = 2f;

    private Transform PlayerTransform;
    private Transform SwordPosition;
    private Transform SwordPivotPoint;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        SwordPosition = GameObject.FindGameObjectWithTag("EnemyWeapon").transform;
        SwordPivotPoint = GameObject.FindGameObjectWithTag("WeaponPivot").transform;

        Collider enemyCollider = GetComponent<Collider>();
        Collider swordCollider = SwordPosition.GetComponent<Collider>();
        if (enemyCollider != null && swordCollider != null)
        {
            Physics.IgnoreCollision(enemyCollider, swordCollider, true);
        }

        StartCoroutine(EnemyCycleRoutine());
    }

    private IEnumerator EnemyCycleRoutine()
    {
        while (true)
        {
            yield return StartCoroutine(ApproachPlayerRoutine());
            yield return StartCoroutine(AttackRoutine());
            yield return StartCoroutine(RetreatRoutine());
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator ApproachPlayerRoutine()
    {
        while (Vector3.Distance(transform.position, PlayerTransform.position) > range)
        {
            Vector3 directionToPlayer = PlayerTransform.position - transform.position;
            directionToPlayer.y = 0f;

            if (directionToPlayer != Vector3.zero)
            {
                Quaternion targetRot = Quaternion.LookRotation(directionToPlayer);
                rb.MoveRotation(targetRot);
            }

            Vector3 rayOrigin = transform.position;
            Vector3 forward = transform.forward;
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, forward, out hit))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    Vector3 nextPos = Vector3.MoveTowards(transform.position, PlayerTransform.position, movementSpeed * Time.deltaTime);
                    rb.MovePosition(nextPos);
                }
            }
            yield return null;
        }
    }

    private IEnumerator AttackRoutine()
    {
        Vector3 originalLocalPos = SwordPosition.localPosition;
        Quaternion originalLocalRot = SwordPosition.localRotation;

        yield return StartCoroutine(RotateObjectRoutine());

        float elapsed = 0;
        while (elapsed < 0.3f)
        {
            SwordPosition.localPosition = Vector3.Lerp(SwordPosition.localPosition, originalLocalPos, elapsed / 0.3f);
            SwordPosition.localRotation = Quaternion.Slerp(SwordPosition.localRotation, originalLocalRot, elapsed / 0.3f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        SwordPosition.localPosition = originalLocalPos;
        SwordPosition.localRotation = originalLocalRot;
    }

    private IEnumerator RotateObjectRoutine()
    {
        Vector3 diagonalAxis = (Vector3.down + Vector3.forward).normalized;
        float targetAngle = 30f;
        float elapsedAngle = 0f;

        while (elapsedAngle < targetAngle)
        {
            float angleThisFrame = (targetAngle / duration) * Time.deltaTime;

            if (elapsedAngle + angleThisFrame > targetAngle)
            {
                angleThisFrame = targetAngle - elapsedAngle;
            }

            SwordPosition.RotateAround(SwordPivotPoint.position, diagonalAxis, angleThisFrame);
            elapsedAngle += angleThisFrame;
            yield return null;
        }
    }

    private IEnumerator RetreatRoutine()
    {
        Vector3 retreatDirection = (transform.position - PlayerTransform.position).normalized;
        Vector3 targetPosition = transform.position + (retreatDirection * 3f);
        targetPosition.y = transform.position.y;

        while (Vector3.Distance(transform.position, targetPosition) > 0.05f)
        {
            Vector3 nextPos = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            rb.MovePosition(nextPos);
            yield return null;
        }
        rb.MovePosition(targetPosition);
    }
}
