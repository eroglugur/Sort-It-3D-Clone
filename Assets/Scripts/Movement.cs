using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    private TubeController tubeController;
    private GameObject ball;

    private void Awake()
    {
        tubeController = GetComponent<TubeController>();
        DOTween.Init();
    }

    private void Start()
    {
    }

    private void OnMouseDown()
    {
        
        Debug.Log(this.gameObject.name);
        
    }
}