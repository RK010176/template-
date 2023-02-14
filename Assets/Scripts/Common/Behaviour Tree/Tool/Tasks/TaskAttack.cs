using UnityEngine;
using BehaviourTree;
using RPGCharacterAnims;
using Game;

public class TaskAttack : Node
{

    private Transform _lastTarget;
    private Controls _controls;
    private System.Random rnd;
    private Death _death;
    private Attack _attack;
    private RPGCharacterController _rpgCharacterController;

    public TaskAttack(Transform transform)
    {
        _controls = transform.GetComponent<Controls>();
        rnd = new System.Random();
    }

    public override NodeState Evaluate()
    {
        //Debug.Log("RUNNING Task : Attack on");

        Transform target = (Transform)GetData("target");
        if (target != _lastTarget)
        {
            _death = target.GetComponent<Death>();
            _attack = target.GetComponent<Attack>();
            _rpgCharacterController = target.GetComponent<RPGCharacterController>();
            _lastTarget = target;
            Debug.Log("Attack new target " + target.name);
        }

        if (_death.IsDead)
        {
            _attack.IsAttacking = false;
            ClearData("target");
            StopAttacks();
        }
        else                   
            Attack();
        

        state = NodeState.RUNNING;
        return state;
    }



    public void Attack()
    {
        if (_rpgCharacterController.hasTwoHandedWeapon)
            _controls.attack = true;
        else
        {            
            if (!IsAttacking())
                RandomAttack(rnd.Next(0, 5));
        }        
    }
    private void RandomAttack(int num)
    {        
        StopAttacks();
        _attack.IsAttacking = true;
        switch (num)
        {
            case 0:
                _controls.attackL = true;
                break;
            case 1:
                _controls.attackR = true;
                break;
            case 2:
                _controls.rightKick = true;
                break;
            case 3:
                _controls.leftKick = true;
                break;
            case 4:
                _controls.leftKick2 = true;
                break;
            case 5:
                _controls.rightKick2 = true;
                break;
        }

    }
    private void StopAttacks()
    {
        _attack.IsAttacking = false;
        _controls.attack = false;
        _controls.attackL = false;
        _controls.attackR = false;
        _controls.leftKick = false;
        _controls.rightKick = false;
        _controls.leftKick2 = false;
        _controls.rightKick2 = false;
    }
    private bool IsAttacking()
    {
        return _controls.attackL || _controls.attackR || _controls.leftKick || _controls.rightKick || _controls.leftKick2 || _controls.rightKick2 || _controls.attackDual;
    }


    

}
