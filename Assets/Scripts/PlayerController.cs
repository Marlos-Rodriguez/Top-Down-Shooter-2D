using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] [Range(15, 40)]private float speed = 5f; //Speed value for Player Movement
    private Rigidbody2D rb2d; //Rigidbody2D For movement
    private Vector2 movement; //Vector 2D for the Movement of the Player

    [SerializeField]private Camera cam; //Camara
    [SerializeField]private Transform gunPosition; //Position of the gun, When Shoot Start

  

    private Vector3 mousePosition; //Vector for mouse position
    private Vector3 DirectionLook; //Vector for the angle before to convert to Degrees 

    public LineRenderer line;

    // Start is called before the first frame update
    void Awake()
    {
        rb2d = GetComponentInChildren<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Player Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //Shooting Coroutine
        StartCoroutine(Shoot());

        //Rotation Function
        PlayerRotation();
    }

    private void FixedUpdate()
    {
        //Movement of the player
        rb2d.MovePosition(rb2d.position + movement * speed * Time.fixedDeltaTime);
    }

    private void PlayerRotation()
    {
        //Take the point in the screen when the mouse is
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        //Convert the Vector to Angles
        DirectionLook = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(DirectionLook.y, DirectionLook.x) * Mathf.Rad2Deg;

        //assign the angle to the player
        transform.eulerAngles = new Vector3(0, 0, angle);

    }

    private IEnumerator Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Raycats from the gun to the mouse position 
            //Fixed Later the distance
            RaycastHit2D hitInfo = Physics2D.Raycast(gunPosition.position, gunPosition.right);

            //If the Raycats collision with something
            if (hitInfo)
            {
                //Assign the position of the Line with a Collision
                line.SetPosition(0, gunPosition.position);
                line.SetPosition(1, hitInfo.transform.position);

                hitInfo.transform.gameObject.GetComponent<Enemy_Controller>().EnemyDamage();
            }
            else
            {
                //Assign the position of the Line
                line.SetPosition(0, gunPosition.position);
                line.SetPosition(1, mousePosition);
            }

            //Enabled the line
            line.enabled = true;

            yield return new WaitForSeconds(.05f);

            //Disable the line
            line.enabled = false;
        }
    }
}
