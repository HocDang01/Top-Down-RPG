using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletMoveSpeed;
    [SerializeField] private int burstCount;
    [SerializeField] private int projecttilePerBurst;
    [SerializeField][Range(0, 359)] private float angleSpread;
    [SerializeField] private float startingDistance = 0.1f;
    [SerializeField] private float timeBetweenBurst;
    [SerializeField] private float restTime = 1f;
    [SerializeField] private bool stagger;
    [Tooltip("Stagger must be enabled for oscillate to function properly!")]
    [SerializeField] private bool oscillate;
    private int level = 0;

    private bool isShooting = false;

    private void OnValidate()
    {
        if (oscillate) stagger = true;
        if (!oscillate) stagger = false;
        if(projecttilePerBurst < 1) projecttilePerBurst = 1;
        if(burstCount < 1) burstCount = 1;
        if (timeBetweenBurst < 0.1f) timeBetweenBurst = 0.1f;
        if (restTime < 0.1f) restTime = 0.1f;
        if (startingDistance < 0.1f) startingDistance = 0.1f;
        if (angleSpread == 0) projecttilePerBurst = 1;
        if (bulletMoveSpeed <= 0) bulletMoveSpeed = 0.1f;
    }
    private void Start()
    {
        level = GetComponent<EnemyAI>().level;
    }
    public void Attack()
    {
        if (!bulletPrefab) return;
        if (!isShooting) StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine()
    {
        isShooting = true;

        AudioController.Instance.PlaySound(AudioController.Instance.enemyShoot);
        float startAngle, currentAngle, angleStep, endAngle;
        float timeBetweenProjecttile = 0f;

        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
        if (stagger)
        {
            timeBetweenProjecttile = timeBetweenBurst / projecttilePerBurst;
        }

        for (int i = 0; i < burstCount; i++)
        {
            if (!oscillate)
            {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }
            if (oscillate && i % 2 != 1)
            {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }
            else if(oscillate)
            {
                currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1;
            }
            for (int j = 0; j < projecttilePerBurst; j++)
            {
                Vector2 pos = FindBulletSpawnPos(currentAngle);

                GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                newBullet.transform.right = newBullet.transform.position - transform.position;

                if (newBullet.TryGetComponent(out Projecttile projecttile))
                {
                    projecttile.UpdateMoveSpeed(bulletMoveSpeed + level*2f);
                }
                currentAngle += angleStep;

                if (stagger) yield return new WaitForSeconds(timeBetweenProjecttile);

            }
            currentAngle = startAngle;
            if(!stagger) yield return new WaitForSeconds(timeBetweenBurst);
        }
        yield return new WaitForSeconds(restTime);
        isShooting=false;
    }
    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep, out float endAngle)
    {
        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;

        endAngle = targetAngle;
        currentAngle = targetAngle;

        float halfAngleSpread = 0f;
        angleStep = 0;

        if (angleSpread != 0)
        {
            angleStep = angleSpread / (projecttilePerBurst - 1);
            halfAngleSpread = angleSpread / 2f;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }
    }

    private Vector2 FindBulletSpawnPos(float currentAngle)
    {
        float x = transform.position.x +startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y +startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        Vector2 pos = new Vector2(x, y);
        return pos;
    }
}
