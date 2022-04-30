using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Status")]
    [SerializeField] private int vida = 3;
    private float delayDano = 0f;

    [Header("Movimentacao")]
    [SerializeField] private float velh = 5f;
    [SerializeField] private float velv = 9.5f;
    private Rigidbody2D meuRB;
    private SpriteRenderer sprite;
    private Animator meuAnim;
    private int totalPulos = 1;
    private int qtdPulos = 1;

    [Header("Raycast")]
    [SerializeField] private LayerMask layerLevel;
    private BoxCollider2D boxCol;

    
    void Start()
    {
        meuRB = GetComponent<Rigidbody2D>();    
        sprite = GetComponent<SpriteRenderer>();
        meuAnim = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
    }

    void Update() {
        Pulando();
        Invencibilidade();
    }

    private void Invencibilidade() {
        if (delayDano > 0f) {
            delayDano -= Time.deltaTime;
        }
    }

    private void FixedUpdate() {
        Movimento();

        meuAnim.SetBool("NoChao", IsGrounded());

        // se toquei no chao, reseto os pulos
        if (IsGrounded()) {
            qtdPulos = totalPulos;
        }
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
        var pulo = Input.GetButtonDown("Jump") && IsGrounded();

        meuAnim.SetFloat("Velv", meuRB.velocity.y); 

        if (pulo && qtdPulos > 0) {    
            meuRB.velocity = new Vector2(meuRB.velocity.x, velv);
            qtdPulos--;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Chao")) {
            meuAnim.SetBool("NoChao", false); 
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Inimigo")) {
            if (transform.position.y > collision.transform.position.y + .2f) {
                meuRB.velocity = new Vector2(meuRB.velocity.x, velv); 
            } else {
                //perdendo vida
                if (delayDano <= 0) {
                    vida--; 

                    delayDano = 2f;  
                }
            }
        }
    }

    //Raycast de colisao no chao
    private bool IsGrounded() {
        //criando o raycast           limites do colisor, direcao(p/ baixo), distancia da linha ate o colisor, layer de colisao
        bool chao = Physics2D.Raycast(boxCol.bounds.center, Vector2.down, .5f, layerLevel);

        ////definindo uma cor para a linha de debug
        ////Color cor;
        //if (chao) {
        //    cor = Color.red;
        //} else {
        //    cor = Color.green;
        //}

        ////debug da linha
        //Debug.DrawRay(boxCol.bounds.center, Vector2.down * 0.5f, cor);

        return chao;
    }
}
