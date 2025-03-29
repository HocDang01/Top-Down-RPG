using UnityEngine;

public class DamageSource : MonoBehaviour
{
    private int damageAmount;
    private void Start()
    {
        MonoBehaviour currentActiveWeapon = ActiveWeapon.Instance.CurrentActiveWeapon;
        switch ((currentActiveWeapon as IWeapon).GetWeaponInfo().weaponName)
        {
            case "Sword":
                damageAmount = PlayerPrefs.GetInt(PrefConsts.SWORD_DAME, 2);
                break;
            case "Bow":
                damageAmount = PlayerPrefs.GetInt(PrefConsts.BOW_DAME, 1);
                break;
            case "Staff":
                damageAmount = PlayerPrefs.GetInt(PrefConsts.STAFF_DAME, 3);
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
         
        if (col.gameObject.GetComponent<EnemyHealth>())
        {
            EnemyHealth enemyHealth = col.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
            }
        }
    }
}
