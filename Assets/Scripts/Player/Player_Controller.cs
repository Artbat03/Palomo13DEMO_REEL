using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Animator))]

public class Player_Controller : MonoBehaviour
{
    // Variables
    [SerializeField] private float speed = 15.0f;
    [SerializeField] private float speedRotHorizontal = 100.0f;
    [SerializeField] private float speedRotVertical = 50.0f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator anim;

    public List<GameObject> listaNeveras; 
    public List<GameObject> listaEgg;
    public bool estoyEnZonaNevera;
    
    private Vector2 inputMov;

    public GameObject egg;

    private void Awake()
    {
        // Buscamos el huevo oculto de la espalda del player
        egg = GameObject.FindGameObjectWithTag("HiustonEgg");
    }

    void Start()
    {
        // Recuperamos los parametros del animator y el rigidbody
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        egg.SetActive(false);
    }
    
    void Update()
    {
        // Llamamos al método de movimiento del player y al método para saber si estamos en la zona de las neveras para poder recogerlas o dejar huevos
        Movement();
        ImInFridgesZone();
    }

    #region Movement

    public void Movement()
    {
        // Leemos los inputs para el desplazamiento del jugador
        inputMov.y = Input.GetAxis("Vertical");
        inputMov.x = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed_X", inputMov.x);
        anim.SetFloat("Speed_Y", inputMov.y);
        
        anim.SetBool("Idle", true);
        anim.SetBool("Hit", false);
        anim.SetBool("Move", false);

        // Lógica para mover al player a lado y lado pero sin impulsarla hacia adelante
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, (inputMov.x * -1)  * speedRotHorizontal * Time.deltaTime);
            anim.SetBool("Idle", false);
            anim.SetBool("Hit", false);
            anim.SetBool("Move", true);
        }
        
        // Lógica para mover al player de arriba a abajo pero sin impulsarla hacia adelante
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            transform.Rotate(inputMov.y  * speedRotVertical * Time.deltaTime, 0, 0);
            anim.SetBool("Idle", true);
            anim.SetBool("Hit", false);
            anim.SetBool("Move", false);
        }

        // Lógica para impulsar la nave hacia adelante
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.AddForce(transform.forward * speed, ForceMode.Force);
            anim.SetBool("Idle", false);
            anim.SetBool("Hit", false);
            anim.SetBool("Move", true);
        }
    }

    #endregion

    #region Egg Module

    public void ImInFridgesZone()
    {
        if (estoyEnZonaNevera && Input.GetKeyDown(KeyCode.F))
        {
            if (listaNeveras.Count < 2)
            {
                listaNeveras.Add(new GameObject());
                GameManager.Instance.fridgesMax = listaNeveras.Count;
                GameManager.Instance.fridgesText.text = listaNeveras.Count + " / 2";
            }
        }

        if (estoyEnZonaNevera && Input.GetKeyDown(KeyCode.E))
        {
            if (egg == true && listaEgg.Count == 1)
            {
                // Llamamos al método IncreaseScore() del GameManager que se encarga de subir la puntuación
                GameManager.Instance.IncreaseScore();
                listaEgg.RemoveAt(0);
                egg.SetActive(false);
            }
        }
    }

    #endregion

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Meteor" || other.gameObject.tag == "Virus" ||
            other.gameObject.tag == "EggFridge" || other.gameObject.tag == "Palomo13")
        {
            anim.SetBool("Hit", true);
            anim.SetBool("Move", false);
            anim.SetBool("Idle", false);
            GameManager.Instance.collisionFX.Play();
        }
        else
        {
            anim.SetBool("Hit", false);
            anim.SetBool("Move", false);
            anim.SetBool("Idle", true);
            GameManager.Instance.collisionFX.Stop();
        }
    }
}