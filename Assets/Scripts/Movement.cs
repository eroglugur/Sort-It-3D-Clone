using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    private TubeController tubeController;
    private GameObject ball;

    private bool isUp = false;

    private void Awake()
    {
        tubeController = GetComponent<TubeController>();
        DOTween.Init();
    }

   
    
}