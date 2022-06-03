using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    // Variables
    public int maxHealth;
    public int actualHealth;
    public bool isInfected;
    public bool isCoroutineLoseSceneActive;
    
    void Awake()
    {
        maxHealth = 100;
        actualHealth = maxHealth;
    }

    public void PlayerDamaged(int damageValue)
    {
        if (isInfected)
        {
            actualHealth -= damageValue;
            GameManager.Instance.healthBarSlider.value = actualHealth;


            if (actualHealth <= 10 && !isCoroutineLoseSceneActive)
            {
                StartCoroutine(Coroutine_LoseScene());
            }
        }
    }

    public void StartInfectedState()
    {
        StartCoroutine(Coroutine_LoseHealthForTime());
    }
    

    IEnumerator Coroutine_LoseHealthForTime()
    {
        while (isInfected && !isCoroutineLoseSceneActive)
        {
            PlayerDamaged(1);
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator Coroutine_LoseScene()
    {
        isCoroutineLoseSceneActive = true;
        yield return new WaitForSeconds(2.0f);
        GameManager.Instance.EndGame();
    }
}
