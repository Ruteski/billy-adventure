using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{
    [SerializeField] private float velh = 2f;
    [SerializeField] private float esperaMovimento = 1f;
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask layerLevel;

    // box collider do meu colisor
    [SerializeField] private BoxCollider2D colisor;

    private BoxCollider2D boxCol;
    private Rigidbody2D meuRB;
    private bool morto = false;
    
    
    // Start is called before the first frame update
    private void Start() {
        meuRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update() {
        
    }

    private void FixedUpdate() {
        if (!morto) {    
            Move();
        }
    }

    public void Morrendo() {
        morto = true;
        meuRB.velocity = Vector2.zero;

        Destroy(gameObject, 2f);
        colisor.enabled = false;
    }

    private void Move() {
        if (IsCollisionWall()) {
            meuRB.velocity = new Vector2(meuRB.velocity.x * -1f, meuRB.velocity.y);

            if (meuRB.velocity.x != 0) {
                transform.localScale = new Vector3(Mathf.Sign(meuRB.velocity.x) * -1, 1f, 1f);
            }
        }


        if (esperaMovimento <= 0) {
            
            //lado de movimentacao aleatorio
            int direcao = Random.Range(-1, 2);

            meuRB.velocity = new Vector2(velh * direcao, meuRB.velocity.y);

            // so altera o lado da sprite se a velocidade x foi diferente de 0
            if (meuRB.velocity.x != 0) {
                transform.localScale = new Vector3(Mathf.Sign(meuRB.velocity.x) * -1, 1f, 1f);
            }

            esperaMovimento = Random.Range(1f, 4f);
        } else {
            esperaMovimento -= Time.deltaTime;
        }

        anim.SetBool("Movendo", meuRB.velocity.x != 0f);
    }

    //raycast de colisao com parede
    private bool IsCollisionWall () {
        Vector2 dir = new Vector2(Mathf.Sign(meuRB.velocity.x), 0f);

        bool wall = Physics2D.Raycast(boxCol.bounds.center, dir , .5f, layerLevel);

        ////definindo uma cor para a linha de debug
        //Color cor;
        
        //if (wall) {
        //    cor = Color.red;
        //} else {
        //    cor = Color.green;
        //}

        ////debug da linha
        //Debug.DrawRay(boxCol.bounds.center, dir * 0.5f, cor);

        return wall;
    }
}
