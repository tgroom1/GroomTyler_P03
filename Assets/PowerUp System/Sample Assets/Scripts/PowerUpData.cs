using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/PowerUps")]
public class PowerUpData : ScriptableObject
{
    [SerializeField] 
    public string _name;
    [SerializeField]
    public int _iD;
    [SerializeField]
    public float _duration;
    [SerializeField] 
    public float _increaseAmount;

}
