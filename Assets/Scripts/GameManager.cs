using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image[] coracoes;
    
    private static int vida = 3;
    private int vidaInicial = 3;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MudaCena(string destino) {
        SceneManager.LoadScene(destino);
    }

    public int GetVida() {
        return vida;
    }

    public void SetVida(int novaVida) {
        vida = novaVida;
    }

    public void GameOver() {
        vida = vidaInicial;
        SceneManager.LoadScene("Jogo");
    }
}
