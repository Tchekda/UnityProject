using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Animator animator;
    public bool isAttacking;
    public int atk;
    public bool attacked;
    public Vector3 offset;
    public GameObject player;
    public Character playerChar;
    public bool canDamage;
    public float initialRot;

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        player = GameObject.Find("Test1");
        offset = player.transform.position - transform.position;
        if (player.TryGetComponent(out Character tmp))
        {
            playerChar = tmp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = player.transform.position - offset;
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            animator.SetBool("isAttacking", true);
            isAttacking = true;
            canDamage = true;
            initialRot = transform.eulerAngles.z;
        }

        if (isAttacking)
        {
            Attacking();
        }
    }

    private void OnGUI()
    {
        if (!isAttacking)
        {
            RotateSwordTowardsMouse();
        }
    }

    private void Attacking()
    {
        if (playerChar.facingRight)
        {
            Debug.Log(transform.eulerAngles.z);
            canDamage = !(transform.eulerAngles.z <= initialRot - 140f) && canDamage;
            isAttacking = canDamage || !(transform.eulerAngles.z >= initialRot);
            if (canDamage)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, transform.eulerAngles.z - 2f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, transform.eulerAngles.z + 2f);
            }
        }
        else
        {
            canDamage = !(transform.eulerAngles.z >= initialRot + 140f) && canDamage;
            isAttacking = !canDamage || !(transform.eulerAngles.z <= initialRot);
            if (canDamage)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, transform.eulerAngles.z + 2f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, transform.eulerAngles.z - 2f);
            }
        }
    }

    private void RotateSwordTowardsMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

        if (mousePos.x < Screen.width / 2)
        {
            playerChar.switchSide(false);
        }
        else
        {
            playerChar.switchSide(true);
        }
        
        Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
 
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        if (!playerChar.facingRight)
        {
            angle -= 180f;
        }
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void rotateXdegrees(float degrees)
    {
            //
    }
    void finishedAttack() // Used by the animator event
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canDamage && other.gameObject.TryGetComponent(out ClassicEnnemy enemy))
        {
            enemy.TakingDamage(atk);
        }
    }
}
