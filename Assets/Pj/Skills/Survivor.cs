using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivor : MonoBehaviour
{
    private float _playerHP;
    private void Start()
    {
        if ( Player.instance != null)
        {
            _playerHP = Player.instance.maxLife;
        }
    }

    private void Update()
    {
        
    }
}
