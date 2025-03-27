using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<DamageSource>() || 
                (other.gameObject.GetComponent<Projecttile>() && !other.gameObject.GetComponent<Projecttile>().IsEnemyProjecttile()))
        {
            GetComponent<PickUpSpawner>().DropItem();
            Instantiate(destroyVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (other.gameObject.GetComponent<Projecttile>() && other.gameObject.GetComponent<Projecttile>().IsEnemyProjecttile())
        {
            Destroy(other.gameObject);
        }
    }
}
