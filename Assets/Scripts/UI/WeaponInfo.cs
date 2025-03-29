using UnityEngine;

[CreateAssetMenu(menuName = "New Weapon")]
public class WeaponInfo : ScriptableObject
{
    public GameObject weaponPrefab;
    public string weaponName;
    public float weaponCooldown;
    public int weaponDamage;
    public float weaponRange;

}
