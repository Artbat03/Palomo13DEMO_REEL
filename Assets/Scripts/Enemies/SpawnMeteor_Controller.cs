using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnMeteor_Controller : MonoBehaviour
{
    // Variables
    [SerializeField] private GameObject meteorvfx;

    public List<Transform> startPoints;
    public List<Transform> endPoints;

    private Transform currentStartMeteorPoint;
    private Transform currentEndMeteorPoint;

    public int randomIndex_ArrayStartMeteor;
    public int randomIndex_ArrayEndMeteor;
    
    public void Start()
    {
        StartCoroutine(GenerateMeteors());
    }

    /// <summary>
    /// Este método genera
    /// </summary>
    
    private void InstantiateMeteor()
    {
        var startPos = currentStartMeteorPoint.position;
        GameObject objVFX = Instantiate(meteorvfx, startPos, Quaternion.identity);

        var endPos = currentEndMeteorPoint.position;
        RotateTo(objVFX, endPos);
    }
    
    void RotateTo(GameObject obj, Vector3 destination)
    {
        var direction = destination - obj.transform.position;
        var rotation = Quaternion.LookRotation(direction);
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation,rotation, 1);
    }
    
    /// <summary>
    /// Este método genera un número desde 0 a la longitud del array de spawns y almacena un elemento GameObject
    /// que indicamos a partir del index aleatorio
    /// </summary>
    private void SelectRandomStartMeteorPosition()
    {
        // Generamos un número aleatorio de 0 a la longitud actual de la array de spawns
        randomIndex_ArrayStartMeteor = Random.Range(0, startPoints.Count);
        
        // Actualizamos el gameObject currentSpawnPoint para almacenar dentro el spawn indicado de la posición del array
        currentStartMeteorPoint = startPoints[randomIndex_ArrayStartMeteor];
    }

    /// <summary>
    /// Este método genera un número desde 0 a la longitud del array de spawns y almacena un elemento GameObject
    /// que indicamos a partir del index aleatorio
    /// </summary>
    private void SelectRandomEndMeteorPosition()
    {
        // Generamos un número aleatorio de 0 a la longitud actual de la array de spawns
        randomIndex_ArrayEndMeteor = Random.Range(0, endPoints.Count);
        
        // Actualizamos el gameObject currentSpawnPoint para almacenar dentro el spawn indicado de la posición del array
        currentEndMeteorPoint = endPoints[randomIndex_ArrayEndMeteor];
    }
    
    /// <summary>
    /// Coroutine que se ejecuta cada X segundos. Llamando a los métodos privados de SelectRandomSpawnMeteorPosition y
    /// InstantiateMeteor
    /// </summary>
    /// <returns> La generación constante de un meteorito aleatorio en un spawn aleatorio </returns>
    IEnumerator GenerateMeteors()
    {
        while (!GameManager.Instance.isGameOver)
        {
            SelectRandomStartMeteorPosition();
            SelectRandomEndMeteorPosition();
            InstantiateMeteor();
            yield return new WaitForSeconds(0.3f);   
        }
    }
}
