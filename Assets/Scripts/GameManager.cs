using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Variables
    public static GameManager Instance;

    [Header("Panels")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject historyPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject startMenuPanel;
    [SerializeField] private GameObject textHistoryPanel;

    [Header("Buttons")]
    [Space(25)]
    [SerializeField] private GameObject backBtn;
    [SerializeField] private GameObject pausebtn;

    [Header("AudioSource")]
    [Space(25)]
    [SerializeField] public AudioSource collisionFX;
    [SerializeField] private AudioSource hiustonFX;
    [SerializeField] private AudioSource menuMusic;
    [SerializeField] private AudioSource gameMusic;

    private HealthSystem _healthSystem;
    public Slider healthBarSlider;

    [Space(25)]
    private bool pausedGame;
    public bool isGameOver;

    public GameObject[] eggsList;
    public int collectiblesMax;
    public int collected;
    public Text collectiblesText;
    
    public int fridgesMax;
    public Text fridgesText;

    public bool historyON;
    private bool controlsON;
    public bool GODModeOn;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

        eggsList = GameObject.FindGameObjectsWithTag("Egg");

        collectiblesMax = eggsList.Length;
        fridgesText.text = "0 / 2";
        
        // Activanod y desactivando audiosources
        collisionFX.Stop();
        hiustonFX.Stop();
        menuMusic.Play();
        gameMusic.Stop();

        _healthSystem = GameObject.FindGameObjectWithTag("PlayerFather").GetComponent<HealthSystem>();
    }

    private void Start()
    {
        // Activamos el panel del menú principal y desactivamos el panel de juego, el de las opciones, el de ganar
        // y el de perder
        winPanel.SetActive(false);
        gamePanel.SetActive(false);
        losePanel.SetActive(false);
        historyPanel.SetActive(false);
        optionsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        startMenuPanel.SetActive(true);
        textHistoryPanel.SetActive(false);

        Time.timeScale = 0f;
        
        UpdatePickablesScore();
    }

    private void Update()
    {
        PauseEsc();
    }

    #region PickUps

    private void UpdatePickablesScore()
    {
        // Texto en la UI
        collectiblesText.text = collected + " / " + collectiblesMax;
    }

    public void IncreaseScore()
    {
        collected++;
        UpdatePickablesScore();
    }

    #endregion

    #region Options 
    
    // Método para pausar el juego con la tecla "Esc"
    private void PauseEsc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Si la variable pausedGame está activa se activará el método Resume()
            if (pausedGame)
            {
                Resume();
            }
            
            // Si la variable no está activa, se activará el método Pause()
            else
            {
                Pause();
            }
        }
    }
    
    // Método para pausar el juego con un botón
    public void Pause()
    {
        // Se pone la variable booleana a true
        pausedGame = true;
        
        // Se detiene el tiempo del juego
        Time.timeScale = 0f;
        
        // Se desactiva el boton de pausa del panel de juego
        pausebtn.SetActive(false);

        // Desactivamos todos los paneles menos el de juego
        winPanel.SetActive(false);
        gamePanel.SetActive(false);
        losePanel.SetActive(false);
        historyPanel.SetActive(false);
        optionsPanel.SetActive(true);
        controlsPanel.SetActive(false);
        startMenuPanel.SetActive(false);
        textHistoryPanel.SetActive(false);

        // Desactivamos el botón de volver al juego
        backBtn.SetActive(true);
    }

    // Método para despausar el juego
    public void Resume()
    {
        // Se pone la variable booleana a false
        pausedGame = false;
        
        // Se vuelve a poner el tiempo normal del juego
        Time.timeScale = 1f;
        
        // Se activa el botón de pausa en el panel de juego
        pausebtn.SetActive(true);

        // Desactivamos todos los paneles menos el de juego
        winPanel.SetActive(false);
        gamePanel.SetActive(true);
        losePanel.SetActive(false);
        historyPanel.SetActive(false);
        optionsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        startMenuPanel.SetActive(false); 
        textHistoryPanel.SetActive(false);

        // Desactivamos el botón de volver al juego
        backBtn.SetActive(false);
    }

    // Método para reiniciar el juego
    public void ToMenu()
    {
        // Se pone la variable booleana a false
        pausedGame = false;
        
        // Se detiene el tiempo del juego
        Time.timeScale = 0f;

        // Desactivamos todos los paneles menos el del start
        winPanel.SetActive(false);
        gamePanel.SetActive(false);
        losePanel.SetActive(false);
        historyPanel.SetActive(false);
        optionsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        startMenuPanel.SetActive(true);
        textHistoryPanel.SetActive(false);

        // Cargamos la escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Método para activar el panel de opciones
    public void OptionsPanel()
    {
        // Desactivamos todos los paneles menos el de las options
        winPanel.SetActive(false);
        gamePanel.SetActive(false);
        losePanel.SetActive(false);
        historyPanel.SetActive(false);
        optionsPanel.SetActive(true);
        controlsPanel.SetActive(false);
        startMenuPanel.SetActive(false);
        textHistoryPanel.SetActive(false);

        // Desactivamos el botón de volver al juego si se accede a las opciones desde el menú principal
        backBtn.SetActive(false);
    }

    // Método para salir del juego
    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion
    
    #region History

    public void HistoryPanel()
    {
        // Desactivamos todos los paneles menos el de juego
        winPanel.SetActive(false);
        gamePanel.SetActive(false);
        losePanel.SetActive(false);
        historyPanel.SetActive(true);
        optionsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        startMenuPanel.SetActive(false);
        textHistoryPanel.SetActive(false);
    }

    public void ShowHistory()
    {
        historyON = !historyON;

        if (historyON)
        {
            controlsPanel.SetActive(false);
            textHistoryPanel.SetActive(true);
            controlsON = false;
        }

        else
        {
            controlsPanel.SetActive(false);
            textHistoryPanel.SetActive(false);
        }    
    }

    public void ShowControls()
    {
        controlsON = !controlsON;

        if (controlsON)
        {
            controlsPanel.SetActive(true);
            textHistoryPanel.SetActive(false);
            historyON = false;
        }
        else
        {
            controlsPanel.SetActive(false);
            textHistoryPanel.SetActive(false);
        }
    }

    #endregion

    #region Start Game

    public void StartGame()
    {
        Time.timeScale = 1f;
        
        // Activamos el audiosource del game y el FX de hiuston
        collisionFX.Stop();
        hiustonFX.Play();
        menuMusic.Stop();
        gameMusic.Play();

        // Llamamos a la Coroutine para 
        StartCoroutine(Coroutine_StartGame());
    }

    IEnumerator Coroutine_StartGame()
    {
        yield return new WaitForSeconds(0.5f);
        // Activamos el panel de juego y desactivamos el panel de opciones y el del menú
        winPanel.SetActive(false);
        gamePanel.SetActive(true);
        losePanel.SetActive(false);
        historyPanel.SetActive(false);
        optionsPanel.SetActive(false);
        startMenuPanel.SetActive(false);
    }

    #endregion

    #region Win Game

    public void WinGame()
    {
        // Se detiene el tiempo del juego
        Time.timeScale = 0f;
        
        // Activamos el panel de perder y desactivamos el panel de juego, el de las opciones, el de ganar
        // y el del menú principal 
        winPanel.SetActive(true);
        gamePanel.SetActive(false);
        losePanel.SetActive(false);
        historyPanel.SetActive(false);
        optionsPanel.SetActive(false);
        startMenuPanel.SetActive(false);

        // Actualizamos de 0 los contadores de los huevos, las neveras y el slider de la vida
        _healthSystem.actualHealth = _healthSystem.maxHealth;
        healthBarSlider.value = _healthSystem.actualHealth; 
        
        collected = 0;
        collectiblesText.text = collected + " / " + collectiblesMax;
        fridgesText.text = "0 / 2";
        
        // Activamos el audiosource del game
        collisionFX.Stop();
        hiustonFX.Stop();
        menuMusic.Stop();
        gameMusic.Play();
    }

    #endregion

    #region End Game

    // Método para cuando el juego se acabe
    public void EndGame()
    {
        // Se detiene el tiempo del juego
        Time.timeScale = 0f;
        
        // Activamos el panel de perder y desactivamos el panel de juego, el de las opciones, el de ganar
        // y el del menú principal 
        winPanel.SetActive(false);
        gamePanel.SetActive(false);
        losePanel.SetActive(true);
        historyPanel.SetActive(false);
        optionsPanel.SetActive(false);
        startMenuPanel.SetActive(false);

        // Actualizamo de 0 los contadores de los huevos, la nevera y el slider de la vida
        _healthSystem.actualHealth = _healthSystem.maxHealth;
        healthBarSlider.value = _healthSystem.actualHealth;

        collected = 0;
        collectiblesText.text = collected + " / " + collectiblesMax;
        fridgesText.text = "0 / 2";
        
        // Activamos el audiosource del game
        collisionFX.Stop();
        hiustonFX.Stop();
        menuMusic.Stop();
        gameMusic.Play();
    }

    #endregion
}