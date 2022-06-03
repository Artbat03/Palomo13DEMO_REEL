using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eggs_Controller : MonoBehaviour
{
    // Variables
    public float speed;
    private Rigidbody rb;
    [SerializeField] private GameObject eggFridge;

    public Player_Controller _PlayerController;

    public bool fridgeON;

    private void Awake()
    {
        _PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        eggFridge.SetActive(false);
        _PlayerController.egg.SetActive(false);
        fridgeON = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if (speed != 0 && rb != null)
        {
            rb.position += transform.forward * (speed * Time.deltaTime);
        }
    }

    private void Update()
    {
        DestroyEggs();
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameManager.Instance.GODModeOn = true;

            if (GameManager.Instance.GODModeOn)
            {
                GameManager.Instance.collected = GameManager.Instance.collectiblesMax;

                if (GameManager.Instance.collected == GameManager.Instance.collectiblesMax)
                {
                    // Llamamos a la coroutine
                    StartCoroutine(Coroutine_WinGameWaitSeconds());
                    
                    // Llamamos al método IncreaseScore() del GameManager que se encarga de subir la puntuación
                    GameManager.Instance.IncreaseScore();
                }
            }
        }
        
        else
        {
            if (GameManager.Instance.collected == GameManager.Instance.collectiblesMax)
            {
                // Llamamos a la coroutine
                StartCoroutine(Coroutine_WinGameWaitSeconds());
            }
        }
    }

    public void DestroyEggs()
    {
        while (!fridgeON && eggFridge == false)
        {
            Destroy(gameObject, 100f);
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        // Si el tag es el del player ejecutar� la logica que hay dentro
        if (other.gameObject.tag == "Player")
        {
            // Si la variable booleana fridgeON est� a false, podremos entrar en el m�todo
            if (!fridgeON)
            {
                // Si la lista de neveras es mayor que 0 y hemos pulsado la F, se ejecutar� la logica
                if (_PlayerController.listaNeveras.Count > 0 && Input.GetKeyDown(KeyCode.F))
                {
                    // Ponemos a true la variable fridgeON
                    fridgeON = true;

                    // Activamos la nevera de ese huevo
                    eggFridge.SetActive(true);

                    // Quitamos una nevera de la lista de neveras
                    _PlayerController.listaNeveras.RemoveAt(0);

                    // Actualizamos el texto de la UI
                    GameManager.Instance.fridgesText.text = _PlayerController.listaNeveras.Count + " / 2";
                }
            }

            if (fridgeON)
            {
                if (Input.GetKeyDown(KeyCode.E) && _PlayerController.listaEgg.Count == 0)
                {
                    _PlayerController.listaEgg.Add(new GameObject());
                    Destroy(gameObject);
                    _PlayerController.egg.SetActive(true);
                }
            }
        }
    }

    IEnumerator Coroutine_WinGameWaitSeconds()
    {
        // Después de 4 segundos, se llamará al método WinGame() del GameManager que se encarga de activar el panel de victoria
        yield return new WaitForSeconds(2.0f);
        GameManager.Instance.WinGame();
    }
}
