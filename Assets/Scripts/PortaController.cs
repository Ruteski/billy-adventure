using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaController : MonoBehaviour
{
    [SerializeField] private string destino;

    private Animator meuAnim;

    // Start is called before the first frame update
    void Start()
    {
        meuAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Abrindo() {
        meuAnim.SetTrigger("Abrir");
    }

    public void IndoParaDestino() {
        FindObjectOfType<GameManager>().MudaCena(destino);
    }
}
