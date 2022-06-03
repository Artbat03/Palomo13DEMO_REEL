using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RadarPulse_Controller : MonoBehaviour
{
    // Variables
    [SerializeField] private Transform pfOtherPosRadar;
    [SerializeField] private Transform pulseTransform;
    [SerializeField] private float range;
    [SerializeField] private float rangeMax;
    [SerializeField] private float fadeRange;
    private SpriteRenderer pulseSpriteRenderer;
    private Color pulseColor;
    [SerializeField] private List<Collider> alreadyPingedColliderList;

    private void Awake()
    {
        pulseSpriteRenderer = pulseTransform.GetComponent<SpriteRenderer>();
        pulseColor = pulseSpriteRenderer.color;
        rangeMax = 2.5f;
        fadeRange = 0.5f;
        alreadyPingedColliderList = new List<Collider>();
    }

    private void Update()
    {
        float rangeSpeed = 2.5f;
        range += rangeSpeed * Time.deltaTime;

        if (range > rangeMax)
        {
            range = 0f;
            alreadyPingedColliderList.Clear();
        }

        pulseTransform.localScale = new Vector3(range, range);
        RaycastHit[] raycastHit2DArray = Physics.SphereCastAll(transform.position, range / 2f, Vector2.zero);

        foreach (RaycastHit raycastHit in raycastHit2DArray)
        {
            if (raycastHit.collider != null)
            {
                Debug.Log("Entrando en IF");
                alreadyPingedColliderList.Add(raycastHit.collider);
                Transform radarPingTransform = Instantiate(pfOtherPosRadar, raycastHit.point, Quaternion.identity);
                RadarPing_Controller radarPing = radarPingTransform.GetComponent<RadarPing_Controller>();
                if (raycastHit.collider.gameObject.GetComponent<Eggs_Controller>() != null)
                {
                    Debug.Log("Detectando EGGS");
                    radarPing.SetColor(new Color(1,1,0));
                }
                    
                if (raycastHit.collider.gameObject.GetComponent<Virus_Controller>() != null)
                {
                    radarPing.SetColor(new Color(1,0,0));
                }
                    
                if (raycastHit.collider.gameObject.GetComponent<Meteor_Controller>() != null)
                {
                    radarPing.SetColor(new Color(0.5f,0.3f,0.2f));
                }
                    
                if (raycastHit.collider.gameObject.GetComponent<Medkit_Controller>() != null)
                {
                    radarPing.SetColor(new Color(0,1,0));
                }

                radarPing.SetDisappearTimer(rangeMax / rangeSpeed * 3f);
               
                /*
                if (!alreadyPingedColliderList.Contains(raycastHit.collider))
                {

                }
                
                */
            }

            if (range > rangeMax - fadeRange)
            {
                pulseColor.a = Mathf.Lerp(0f, 1f, (rangeMax - range) / fadeRange);
            }

            else
            {
                pulseColor.a = 1f;
            }

            pulseSpriteRenderer.color = pulseColor;
        }
    }
}
