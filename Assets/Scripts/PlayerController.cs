using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movimentacao")]
    [SerializeField] private float velh = 5f;
    [SerializeField] private float velv = 5f;
    private Rigidbody2D meuRB;
    private SpriteRenderer sprite;
    private Animator meuAnim;
    
    
    void Start()
    {
        meuRB = GetComponent<Rigidbody2D>();    
        sprite = GetComponent<SpriteRenderer>();
        meuAnim = GetComponent<Animator>();
    }

    void Update()
    {
        Pulando();
    }

    private void FixedUpdate() {
        Movimento();
    }

    private void Movimento() {
        var horizontal = Input.GetAxis("Horizontal") * velh;
        Vector2 minhaVelocidade = new Vector2(horizontal, meuRB.velocity.y);

        meuRB.velocity = minhaVelocidade;

        if (horizontal != 0) {
            meuAnim.SetBool("Movendo", true);

            if (horizontal >= 0) {
                sprite.flipX = false;
            } else {
                sprite.flipX = true;
            }
        } else {
            meuAnim.SetBool("Movendo", false);
        }



        //tb poderia ter sido feito assim
        //transform.localScale = new Vector3(Mathf.Sign(horizontal), 1f, 1f);

        //para mudar a transicao da animacao tb poderia fazer assim
        //meuAnim.SetBool("Movendo", horizontal != 0);    
    }

    private void Pulando() {
         var pulo = Input.GetButtonDown("Jump");

        if (pulo) {    
            meuRB.velocity = new Vector2(meuRB.velocity.x, velv);   
        }
    }
}
