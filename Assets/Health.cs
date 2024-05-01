using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int _maxhealth = 100;
    public int _currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxhealth;
    }

}
