using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnVirus_Controller : MonoBehaviour
{
    // Variables
    [SerializeField] private GameObject virus;
    
    public List<Transform> startPoints;
    public List<Transform> endPoints;
    
    private Transform currentStartVirusPoint;
    private Transform currentEndVirusPoint;
    
    public int randomIndex_ArrayStartVirus;
    public int randomIndex_ArrayEndVirus;
    
    void Start()
    {
        // Llamada de la coroutine para spawnear virus
        StartCoroutine(GenerateVirus());
    }
    
    ///<summary>
    /// Este método genera un número aleatorio desde 0 a la longitud del array de enemies y almacena un elemento
    /// GameObject que indicamos a partir del index aleatorio. Seguidamente creamos un enemigo en la posición y
    /// rotación indicada
    /// </summary>
    private void InstantiateVirus()
    {
        var startPos = currentStartVirusPoint.position;
        GameObject obj = Instantiate(virus, startPos, Quaternion.identity);

        var endPos = currentEndVirusPoint.position;
        RotateTo(obj, endPos);
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
    private void SelectRandomStartVirusPosition()
    {
        // Generamos un número aleatorio de 0 a la longitud actual de la array de spawns
        randomIndex_ArrayStartVirus = Random.Range(0, startPoints.Count);
        
        // Actualizamos el gameObject currentSpawnPoint para almacenar dentro el spawn indicado de la posición del array
        currentStartVirusPoint = startPoints[randomIndex_ArrayStartVirus];
    }
    
    /// <summary>
    /// Este método genera un número desde 0 a la longitud del array de spawns y almacena un elemento GameObject
    /// que indicamos a partir del index aleatorio
    /// </summary>
    private void SelectRandomEndMeteorPosition()
    {
        // Generamos un número aleatorio de 0 a la longitud actual de la array de spawns
        randomIndex_ArrayEndVirus = Random.Range(0, endPoints.Count);

        // Actualizamos el gameObject currentSpawnPoint para almacenar dentro el spawn indicado de la posición del array
        currentEndVirusPoint = endPoints[randomIndex_ArrayEndVirus];
    }
    
    /// <summary>
    /// Coroutine que se ejecuta cada X segundos. Llamando a los métodos privados de SelectRandomSpawnVirusPosition y
    /// InstantiateVirus
    /// </summary>
    /// <returns> La generación constante de un meteorito aleatorio en un spawn aleatorio </returns>
    IEnumerator GenerateVirus()
    {
        while (!GameManager.Instance.isGameOver)
        {
            SelectRandomStartVirusPosition();
            SelectRandomEndMeteorPosition();
            InstantiateVirus();
            yield return new WaitForSeconds(0.5f);   
        }
    }
}
