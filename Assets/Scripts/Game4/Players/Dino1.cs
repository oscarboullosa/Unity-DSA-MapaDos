using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Dino1 : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 500f;
    private Animator animator;

    public int collisionCount = 0;

    private Rigidbody2D rb;

    //public Text punt;
    [SerializeField] private TMP_Text punt;

    private Vector2 touchOrigin = -Vector2.one;    //Used to store location of screen touch origin for mobile controls.

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public float speed = 5.0f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        punt = GameObject.Find("Canvas/Text").GetComponent<TMP_Text>();

        Camera camera = Camera.main;
        float cameraHalfWidth = camera.orthographicSize * camera.aspect;
        float cameraHalfHeight = camera.orthographicSize;
        minX = camera.transform.position.x - cameraHalfWidth;
        maxX = camera.transform.position.x + cameraHalfWidth;
        minY = camera.transform.position.y - cameraHalfHeight;
        maxY = camera.transform.position.y + cameraHalfHeight;



        collisionCount = 0;

        punt.text = "Puntuación: " + collisionCount;
    }

    void Update()
    {


        /*float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");*/

        int horizontalMovement = 0;      //Used to store the horizontal move direction.
        int verticalMovement = 0;        //Used to store the vertical move direction.

#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.touches[0];

            if (myTouch.phase == TouchPhase.Began)
            {
                touchOrigin = myTouch.position;
            }

            else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
            {
                Vector2 touchEnd = myTouch.position;
                float x = touchEnd.x - touchOrigin.x;
                float y = touchEnd.y - touchOrigin.y;
                touchOrigin.x = -1;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                    horizontalMovement = x > 0 ? 1 : -1;
                else
                    verticalMovement = y > 0 ? 1 : -1;
            }

            Vector3 movement = new Vector3(horizontalMovement, verticalMovement, 0);

            transform.position = new Vector3(Mathf.Clamp(transform.position.x + movement.x * speed * Time.deltaTime * 10, minX, maxX),
                                       Mathf.Clamp(transform.position.y + movement.y * speed * Time.deltaTime * 10, minY, maxY),
                                       transform.position.z);

            
        }



#elif UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR

        //Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
        horizontalMovement = (int)(Input.GetAxisRaw("Horizontal"));

        //Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
        verticalMovement = (int)(Input.GetAxisRaw("Vertical"));

        Vector3 movement = new Vector3(horizontalMovement, verticalMovement, 0);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x + movement.x * speed * Time.deltaTime, minX, maxX),
                                   Mathf.Clamp(transform.position.y + movement.y * speed * Time.deltaTime, minY, maxY),
                                   transform.position.z);

        //Check if moving horizontally, if so set vertical to zero.
        if (horizontalMovement != 0)
        {
            verticalMovement = 0;
        }



#endif


        if (horizontalMovement != 0)
        {
            /*Vector3 movement = new Vector3(horizontalMovement, verticalMovement, 0);

            transform.position = new Vector3(Mathf.Clamp(transform.position.x + movement.x * speed * Time.deltaTime * 10, minX, maxX),
                                       Mathf.Clamp(transform.position.y + movement.y * speed * Time.deltaTime * 10, minY, maxY),
                                       transform.position.z);*/
            animator.SetTrigger("DinoRun");
        }
        else if (verticalMovement != 0)
        {
            animator.SetTrigger("DinoJump");
        }
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fantasma"))
        {

            collisionCount++;

            Destroy(collision.gameObject);

            punt.text = "Puntuación: " + collisionCount;

            if (collisionCount >= 40)
            {
                yield return new WaitForSeconds(0.9f);
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("WinSceneJuego4");
                while (!asyncLoad.isDone)
                {
                    yield return null;
                }
            }


        }
    }

}

