using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOD.Statemachine;

[CreateAssetMenu(fileName = "SP_Artillery", menuName = "StateMachine/Special Attacks/SP_Artillery")]
public class SpecialAttacks_Artillery : State
{
    [SerializeField] float specialAttackCooldown;
}

