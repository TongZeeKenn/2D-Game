using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 moveInput;

    private float activeMoveSpeed;
    public float dashSpeed;

    public float dashLength = 0.5f, dashCooldown = 1f;

    private float dashCounter;
    private float dashCoolCounter;


    Rigidbody2D rb; 
    Vector2 clickPos;
    Vector2 targetPos;
    Vector2 playerPos;



    public GameObject aimAssistPlaceHolder;
    public GameObject aimAssist;
    public GameObject slashEffect;

    //public Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        activeMoveSpeed = moveSpeed;
        
    }
    
    void Update()
    {
        //move
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        moveInput.Normalize();

        rb.velocity = moveInput * activeMoveSpeed;


        //oppsite dir code

        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        aimAssistPlaceHolder.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 90f));

        if (Input.GetMouseButtonDown(0))
        {
            
            clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            playerPos = transform.position;

            targetPos = (playerPos - clickPos).normalized * 10+ playerPos;
            
            Instantiate(slashEffect, aimAssist.transform.position, Quaternion.Euler(new Vector3(0f, 0f, angle)));

        }
        //transform.position = Vector3.Lerp(transform.position, targetPos, 0.1f);



        //dash
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
            }

            
        }


        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }

        if(dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }





    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    
}
