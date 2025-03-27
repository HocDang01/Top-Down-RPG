using UnityEngine;
using System.Collections;

public class SlimeAttack : MonoBehaviour, IEnemy
{
    [SerializeField] AnimationCurve attackCurve;
    [SerializeField] private float popDuration = 1f;
    [SerializeField] private float heightY = 1.5f;

    public void Attack()
    {
        if (GetComponent<KnockBack>().GettingKnockedBack) return;
        AudioController.Instance.PlaySound(AudioController.Instance.slimeAttack);
        StartCoroutine(AnimCurveAttack());
    }

    private IEnumerator AnimCurveAttack()
    {
        Vector2 startPoint = transform.position;

        Vector2 endPoint = PlayerController.Instance.transform.position;
        float timePassed = 0f;
        GetComponent<Animator>().SetTrigger("Attack");

        while (timePassed < popDuration)
        {
            if (GetComponent<KnockBack>().GettingKnockedBack) break;
            timePassed += Time.deltaTime;
            float linearT = timePassed / popDuration;
            float heightT = attackCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);
            yield return null;
        }
    }
}
