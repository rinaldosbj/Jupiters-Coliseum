    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;

    public class SwarmLogic : MonoBehaviour
    {
        [SerializeField] private GameObject rockTrow;
        [SerializeField] private GameObject lightning;
        [SerializeField] public GameObject fallingPlatforms;
        [SerializeField] public GameObject fallingPlatformStop;
        [SerializeField] private GameObject spawnBirds;
        [SerializeField] private Transform cameraTransform;
        private GameObject player;
        private PlayerMovement moveScript;
        [SerializeField] private GameObject floor;
        [SerializeField] private GameObject heightLimitator;
        private bool floorIsMovingDown = false;
        private bool floorIsMovingUp = false;
        private float playerGravity;
        public SpriteRenderer sprite { get; private set; }

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            moveScript = player.GetComponent<PlayerMovement>();
            playerGravity = player.GetComponent<Rigidbody2D>().gravityScale;
            sprite = GetComponent<SpriteRenderer>();
            InvokeRepeating("RandomAttack", 3f, Random.Range(5f, 10f));
        }

        void RandomAttack()
        {
            int attackType = Random.Range(1, 2);

            switch (attackType)
            {
                case 0:
                    RockThrow();
                    break;
                case 1:
                    LightningAttack();
                    break;
            }
        }

        void RockThrow()
        {
            Instantiate(rockTrow, new Vector3(9f, cameraTransform.position.y, 0), Quaternion.identity);
            Debug.Log("Boss throws rocks");
        }

        void LightningAttack()
        {
            Instantiate(lightning, new Vector3(6f, cameraTransform.position.y, 0), Quaternion.identity);
            Debug.Log("Boss performs lightning attack");
        }

        public void FallingPlatforms()
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 4);
            player.GetComponent<Rigidbody2D>().gravityScale = 0.05f;
            moveScript._isTransition = true;
            floorIsMovingDown = true;
            //heightLimitator.SetActive(true);
            Instantiate(spawnBirds, new Vector3(9f, cameraTransform.position.y, 0), Quaternion.identity);
            zeroGravity();
        }
        
        void Update()
        {
            if (floorIsMovingDown)
            {
                floor.transform.position = new Vector3(floor.transform.position.x, floor.transform.position.y - 0.004f, floor.transform.position.z);
            }
            if (floorIsMovingUp)
            {
                floor.transform.position = new Vector3(floor.transform.position.x, floor.transform.position.y + 0.004f, floor.transform.position.z);
            }
        }

        async void zeroGravity()
        {
            await Task.Delay(1000);
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            floorIsMovingDown = false;
            Instantiate(fallingPlatforms, new Vector3(-5.5f, cameraTransform.position.y + 16, 0), Quaternion.identity);
            await Task.Delay(10);
            floorIsMovingUp = true;
            await Task.Delay(1);
            floorIsMovingUp = false;
            moveScript._isTransition = true;
            heightLimitator.SetActive(false);
        }
    }
