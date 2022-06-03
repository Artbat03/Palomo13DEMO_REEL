using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarPing_Controller : MonoBehaviour
{
    // Variables
    private SpriteRenderer _spriteRenderer;
    private float disappearTimer;
    private float disappearTimerMax;
    private Color _color;
    
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        disappearTimerMax = 1f;
        disappearTimer = 0f;
        _color = new Color(1, 1, 1, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        disappearTimer += Time.deltaTime;
        _color.a = Mathf.Lerp(disappearTimerMax, 0f, disappearTimer / disappearTimerMax);
        _spriteRenderer.color = _color;
        if (disappearTimer >= disappearTimerMax)
        {
            Destroy(gameObject);
        }
    }

    public void SetColor(Color color)
    {
        _color = color;
    }

    public void SetDisappearTimer(float disappearTimerMax)
    {
        this.disappearTimerMax = disappearTimerMax;
        disappearTimer = 0f;
    }
}
