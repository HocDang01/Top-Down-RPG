    using UnityEngine;

    public class Projecttile : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 22f;
        [SerializeField] private GameObject particleOnHitPrefabVFX;
        [SerializeField] private bool isEnemyProjecttile = false;
        [SerializeField] private float projecttileRange = 10f;

        private Vector3 startPosition;
        
        private void Start()
        {
            startPosition = transform.position;
        }
        private void Update()
        {
            MoveProjecttile();
            DetectFireDistance();
        }
        
        public bool IsEnemyProjecttile()
        {
            return isEnemyProjecttile;
        }

        public void UpdateProjecttileRange(float projecttileRange)
        {
            this.projecttileRange = projecttileRange;
        }
        public void UpdateMoveSpeed(float moveSPeed)
        {
            this.moveSpeed = moveSPeed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();
            PlayerHealth player =other.gameObject.GetComponent<PlayerHealth>();

            if (!other.isTrigger && (enemyHealth || indestructible || player))
            {
                if ((player && isEnemyProjecttile) || (enemyHealth && !isEnemyProjecttile))
                {
                    player?.TakeDamage(1, transform);
                    Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
                    Destroy(gameObject);
                }
                else if (!other.isTrigger && indestructible)
                {
                    Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
                    Destroy(gameObject);
                }
            }
        }



        private void MoveProjecttile()
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        private void DetectFireDistance()
        {
            if (Vector3.Distance(transform.position, startPosition) > projecttileRange)
            {
                Destroy(gameObject);
            }
        }
    }
