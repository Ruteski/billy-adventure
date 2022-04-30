using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{
    [SerializeField] private float velh = 2f;
    [SerializeField] private float esperaMovimento = 1f;
    [SerializeField] private Animator anim;
    private Rigidbody2D meuRB;
    
    
    // Start is called before the first frame update
    private void Start() {
        meuRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update() {
        Move();
    }

    private void Move() {
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
}
