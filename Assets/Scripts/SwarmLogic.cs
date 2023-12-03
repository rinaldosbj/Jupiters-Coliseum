    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;

    public class SwarmLogic : MonoBehaviour
    {
        [SerializeField] private GameObject rockTrow;
        [SerializeField] private GameObject lightning;
        [SerializeField] private GameObject fallingPlatforms;
        [SerializeField] private GameObject fallingPlatformStop;
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
            int attackType = Random.Range(1, 3);

            switch (attackType)
            {
                case 0:
                    RockThrow();
                    break;
                case 1:
                    LightningAttack();
                    break;
                case 2:
                    FallingPlatforms();
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

        void FallingPlatforms()
        {

            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 4);
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            moveScript._isTransition = false;
            floorIsMovingDown = true;

            heightLimitator.SetActive(true);
            Instantiate(spawnBirds, new Vector3(9f, cameraTransform.position.y, 0), Quaternion.identity);
            // zeroGravity();
            Debug.Log("Boss releases falling platforms");
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

            // Must stop in Platform
            if (Input.GetKeyDown(KeyCode.O))
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5);
                player.GetComponent<Rigidbody2D>().gravityScale = 0;
                //player.GetComponent<PlayerMovement>().canJump = false;
                floorIsMovingDown = true;
                heightLimitator.SetActive(true);
                zeroGravityChangeState();
            }

            // Passarinho
            if (Input.GetKeyDown(KeyCode.B))
            {
                Instantiate(spawnBirds, new Vector3(9f, cameraTransform.position.y-Random.Range(-2,8), 0), Quaternion.identity);
            }
        }

        async void zeroGravityChangeState()
        {
            await Task.Delay(1000);
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            floorIsMovingDown = false;
            Instantiate(fallingPlatformStop, new Vector3(-5.5f, cameraTransform.position.y + 14, 0), Quaternion.identity);
            await Task.Delay(8000);
            moveScript._isTransition = true;
            heightLimitator.SetActive(false);
        }

        async void zeroGravity()
        {
            await Task.Delay(1000);
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            floorIsMovingDown = false;
            Instantiate(fallingPlatforms, new Vector3(-5.5f, cameraTransform.position.y + 16, 0), Quaternion.identity);
            await Task.Delay(12000);
            floorIsMovingUp = true;
            await Task.Delay(1000);
            floorIsMovingUp = false;
            player.GetComponent<Rigidbody2D>().gravityScale = playerGravity;
            playerGravity = 0f;
            //player.GetComponent<PlayerMovement>().canJump = true;
            heightLimitator.SetActive(false);
        }
    }
