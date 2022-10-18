using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float gravity = -100;
    public Vector2 velocity;
    public float maxAcceleraiton = 10;
    public float jumpVelocity = 20;
    public float XVelocity = 100;
    public float acceleration = 10;
    public float distance = 0;
    public float groundHeight = 10;
    public bool isGrounded = false;

    public bool isHoldingSpace = false;
    public float maxHoldSpace = 0.4f;
    public float maxHold = 0.4f;
    public float time = 0.0f;
    public bool isDead = false;

    public LayerMask groundLayerMask;
    public LayerMask obstacleLayerMask;
    public AdsManager ads;

    void Start()
    {
        gravity = -400;
        ads.PlayBannerAd();
    }
    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
                isHoldingSpace = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingSpace = false;
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if (isDead)
        {
            return;
        }

        if(pos.y < -20)
        {
            isDead = true;
            ads.PlayAd();
        }


        if (!isGrounded)
        {
            if (isHoldingSpace)
            {
                time += Time.deltaTime;
                if (time >= maxHoldSpace)
                {
                    isHoldingSpace = false;
                }
            }
            pos.y += velocity.y * Time.fixedDeltaTime;
            if (!isHoldingSpace)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }

            Vector2 Origin = new Vector2(pos.x + 0.7f, pos.y);
            Vector2 Direction = Vector2.up;
            float Distance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(Origin, Direction, Distance,groundLayerMask);
            if(hit2D.collider != null)
            {
                Ground ground = hit2D.collider.GetComponent<Ground>();
                if(ground != null)
                {
                    if (pos.y >= ground.groundHeight)
                    {
                        groundHeight = ground.groundHeight;
                        pos.y = groundHeight;
                        velocity.y = 0;
                        isGrounded = true;
                        time = 0;
                    }
                }
            }



            Vector2 wallOrigin = new Vector2(pos.x, pos.y);
            RaycastHit2D wallHit = Physics2D.Raycast(wallOrigin, Vector2.right,velocity.x * Time.fixedDeltaTime,groundLayerMask);
            if(wallHit.collider != null)
            {
                Ground ground = wallHit.collider.GetComponent<Ground>();
                if(ground != null)
                {
                    if(pos.y < ground.groundHeight)
                    {
                        velocity.x = 0;
                    }
                }

            }
            
        }
        distance += velocity.x * Time.fixedDeltaTime;

        if (isGrounded)
        {
            float velocityRatio = velocity.x / XVelocity;
            acceleration = maxAcceleraiton * (1 - velocityRatio);
            maxHoldSpace = maxHold * velocityRatio;
            velocity.x += acceleration * Time.fixedDeltaTime;
            
            if(velocity.x >= XVelocity)
            {
                velocity.x = XVelocity;
            }

            Vector2 Origin = new(pos.x - 0.7f, pos.y);
            Vector2 Direction = Vector2.up;
            float Distance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(Origin, Direction, Distance);
            if (hit2D.collider == null)
            {
                isGrounded = false;
            }
        }

        Vector2 obstOrigin = new(pos.x, pos.y);
        RaycastHit2D obstHitX = Physics2D.Raycast(obstOrigin, Vector2.right, velocity.x * Time.fixedDeltaTime,obstacleLayerMask);
        if(obstHitX.collider != null)
        {
            Obstacle obstacle = obstHitX.collider.GetComponent<Obstacle>();
            if(obstacle != null)
            {
                hitBox(obstacle);
            }
        }

        RaycastHit2D obstHitY = Physics2D.Raycast(obstOrigin, Vector2.up, velocity.y * Time.fixedDeltaTime,obstacleLayerMask);
        if (obstHitY.collider != null)
        {
            Obstacle obstacle = obstHitY.collider.GetComponent<Obstacle>();
            if (obstacle != null)
            {
                hitBox(obstacle);
            }
        }

        transform.position = pos;
    }

    void hitBox(Obstacle obstacle)
    {
        Destroy(obstacle.gameObject);
        velocity.x *= 0.7f;
    }
}
