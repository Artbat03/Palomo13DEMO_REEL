using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus_Controller : MonoBehaviour
{
    // Variables
    public float speed;

    private GameObject playerFather;
    private HealthSystem _healthSystem;
    private int virusDamage = 5;

    private void Awake()
    {
        // Buscamos al player con un tag
        playerFather = GameObject.FindGameObjectWithTag("PlayerFather");

        // Recuperamos el script del sistema de vida del player
        _healthSystem = playerFather.GetComponent<HealthSystem>();
    }

    void Start()
    {
        // Le asignamos una velocidad estandar a los virus
        speed = 5.0f;
        Destroy(gameObject, 30f);
    }

    void Update()
    {
        // Llamamos al método Virus_Movement
        Virus_Movement();
    }

    // Método que mueve a los virus en una dirección, en este caso, hacia atrás, hacia el player
    private void Virus_Movement()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si colisiona con el jugador, la vida del player irá bajando hasta morir
        if (other.gameObject.tag == "Player")
        {
            _healthSystem.PlayerDamaged(virusDamage);
            _healthSystem.isInfected = true;
            _healthSystem.StartInfectedState();
        }
    }
}
