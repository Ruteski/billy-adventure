using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{
    [SerializeField] private float velh = 2f;
    private Rigidbody2D meuRB;
    
    // Start is called before the first frame update
    private void Start() {
        meuRB = GetComponent<Rigidbody2D>();
        meuRB.velocity = new Vector2(velh, meuRB.velocity.y);

        transform.localScale = new Vector3(Mathf.Sign(meuRB.velocity.x) * -1, 1f, 1f);
    }

    // Update is called once per frame
    private void Update() {
        
    }
}
