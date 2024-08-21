using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private Rigidbody2D myRBD;
    private SpriteRenderer myRenderer;
    private Vector2 direction;
    [SerializeField] private float velocity;

    [Header("Raycast Properties")]
    [SerializeField] private Vector3 targetPoint;
    [SerializeField] private Vector3 raycastOrigin;
    [SerializeField] private LayerMask detectableLayer;
    [SerializeField] private float raycastDistance = 5f;


    private void Awake()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myRBD = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            direction = new Vector2(horizontal,vertical).normalized;

           Raycast();
    }
    private void FixedUpdate()
    {
        myRBD.velocity = direction * velocity;
    }
    private void Raycast()
    {
        raycastOrigin = transform.position;
        Debug.DrawRay(raycastOrigin, direction * raycastDistance, Color.blue);
        RaycastHit2D raycastHit = Physics2D.Raycast(raycastOrigin, direction, raycastDistance, detectableLayer);
        RaycastHit(raycastHit);
    }
    private void RaycastHit(RaycastHit2D raycastHit)
    {
        if (raycastHit.collider != null)
        {
            if (raycastHit.collider.gameObject != gameObject)
            {
                Debug.Log("Colisionando con: " + raycastHit.collider.gameObject.name);
                Debug.DrawRay(raycastOrigin, targetPoint * raycastDistance, Color.red);

                if (raycastHit.collider.gameObject.CompareTag("Shape"))
                {
                    Debug.Log("Colisiono con el GameObject: " + raycastHit.collider.gameObject.name);
                    Debug.Log("La posicion del objeto colisionado es: " + raycastHit.collider.gameObject.transform.position);
                    Debug.Log("El tag del objeto colisionado es: " + raycastHit.collider.gameObject.tag);
                }
                else if (raycastHit.collider.gameObject.CompareTag("Color"))
                {
                    Debug.Log("Colisiono con el GameObject: " + raycastHit.collider.gameObject.name);
                    Debug.Log("La posicion del objeto colisionado es: " + raycastHit.collider.gameObject.transform.position);
                    Debug.Log("El tag del objeto colisionado es: " + raycastHit.collider.gameObject.tag);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Color"))
        {
            myRenderer.color = collision.GetComponent<SpriteRenderer>().color;
        }
        else if (collision.CompareTag("Shape"))
        {
            myRenderer.sprite = collision.GetComponent<SpriteRenderer>().sprite;
            transform.localScale = collision.transform.localScale;
        }
    }
}
