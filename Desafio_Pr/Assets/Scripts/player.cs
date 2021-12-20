using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    private Rigidbody2D Rigidbody2D;
    internal Animator Animator;

    private float Horizontal;
    public bool Grounded;
    //private float LastShoot;

    public int saltos = 0;

    [SerializeField] float JumpForce;
    [SerializeField] float Speed;

    public GameObject sonidoSalto;
    public GameObject sonidoFall;
    public GameObject sonidoSlash;
    public GameObject sonidoRun;

    private bool ver;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Space) && saltos==0)
        {
            Instantiate(sonidoSalto);
            Animator.SetBool("saltar", true);
            Rigidbody2D.AddForce(Vector2.up * JumpForce);
            
            saltos++;
            
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            Down();
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
           
            transform.localScale = new Vector3(6.0f, 6.0f, 1.0f);
            Animator.SetBool("Running", true);
            transform.position += Vector3.right * Time.deltaTime * Speed;
            
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            //Instantiate(sonidoRun);
            transform.localScale = new Vector3(-6.0f, 6.0f, 1.0f);
            Animator.SetBool("Running", true);
            transform.position += Vector3.left * Time.deltaTime * Speed;
        }
        
        if (!(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
        {
            Animator.SetBool("Running", false);
        }

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-6.0f, 6.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(6.0f, 6.0f, 1.0f);


        Debug.DrawRay(transform.position, Vector3.down * 1.2f, Color.red, 1);
        if (Physics2D.Raycast(transform.position, Vector3.down, 1.5f))
        {
            Grounded = true;
            Animator.SetBool("Jump", false);

        }
        else
        {
            Grounded = false;
        }
        Debug.Log(Grounded);
       

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            
            Animator.SetBool("Down", true);
        }
        else
        {
            Animator.SetBool("Down", false);
        }

        if (Input.GetKeyDown(KeyCode.X)&&ver==false)// && Time.time > LastShoot + 0.26f)
        {
            Animator.SetBool("slash", true);
            Instantiate(sonidoSlash);
            ver = true;
            //LastShoot = Time.time;

        }
        else
        {
            Animator.SetBool("slash", false);
            ver = false;
        }

    }

    public void run()
    {
        Animator.SetBool("Running", true); 
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
        Animator.SetBool("Jump", true);
    }

    private void Down()
    {
        Animator.SetBool("Down", true);
    }

    private void Slash()
    {
        Animator.SetBool("Attack", true);

    }

    public void EndSlash()
    {
        Animator.SetBool("Attack", false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "suelo")
        {
            saltos = 0;
            Animator.SetBool("saltar", false);
            Instantiate(sonidoFall);
        }
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y);
    }
}
