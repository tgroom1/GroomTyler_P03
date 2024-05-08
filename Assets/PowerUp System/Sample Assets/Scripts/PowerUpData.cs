using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/PowerUps")]
public class PowerUpData : ScriptableObject
{
    [SerializeField]
    public int _iD;
    [SerializeField]
    public float _duration;
    [Tooltip("If Not Applicable = 0")]
    [SerializeField] public float _increaseAmount;

}
