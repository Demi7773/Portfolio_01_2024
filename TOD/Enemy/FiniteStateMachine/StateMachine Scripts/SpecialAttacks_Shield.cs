using System.Collections;
using System.Collections.Generic;
using TOD.Statemachine;
using UnityEngine;

[CreateAssetMenu(fileName ="SP_Shield",menuName ="StateMachine/Special Attacks/SP_Shield")]
public class SpecialAttacks_Shield : State
{
    [SerializeField] float specialAttackCooldown;

}
