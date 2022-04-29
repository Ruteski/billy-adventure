using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{
    [SerializeField] private float velh = 2f;
    [SerializeField] private float esperaMovimento = 1f;
    private Rigidbody2D meuRB;
    
    
    // Start is called before the first frame update
    private void Start() {
        meuRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update() {
        Move();
    }

    private void Move() {
        if (esperaMovimento <= 0) {
            //lado de movimentacao aleatorio
            velh *= Mathf.Sign(Random.Range(-1, 1));

            meuRB.velocity = new Vector2(velh, meuRB.velocity.y);
            transform.localScale = new Vector3(Mathf.Sign(meuRB.velocity.x) * -1, 1f, 1f);

            esperaMovimento = Random.Range(1f, 3f);
        } else {
            esperaMovimento -= Time.deltaTime;
        }
    }
}
