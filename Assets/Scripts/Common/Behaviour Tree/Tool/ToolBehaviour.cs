using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using RPGCharacterAnims;
using System;

public class ToolBehaviour : BehaviourTree.Tree
{
    public ToolBehaviours_SO So;
    private Controls _controls;
    private Node Guard, BodyGuard, Aggressive, Work;
    [SerializeField] private int _mode = 0;
    [SerializeField] private string _workType;
    private Transform _player;

    public void Start_ListenToActions() { Events.OnToolAction += OnToolAction; }
    public void Stop_ListenToActions() { Events.OnToolAction -= OnToolAction; }
    private void OnToolAction(int action)
    {
        _mode = action;
        base.SetTree();
    }

    private void OnEnable()
    {
        _controls = GetComponent<Controls>();
        _controls.useNavigation = true;
        _controls.OnMousePoint += MousePoint;
    }
    private void OnDisable()
    {
        _controls.OnMousePoint -= MousePoint;
    }
    private void MousePoint(Vector3 pos)
    {
        SetData("mousePos", pos);
        SetData("guardPos", pos);
    }

    protected override Node UpdateTree()
    {
        Node root = null;

        Debug.unityLogger.Log("BTree", "_mode " + _mode.ToString());

        switch (_mode)
        {
            case 0: root = Guard; break;
            case 1: root = BodyGuard; break;
            case 2: root = Aggressive; break;
            case 3: _workType = "GoldMaine"; root = Work; break;
                //case 4: _workType = "Woods";root = Work;break;
        }
        root.ClearData("mousePos");
        root.ClearData("guardPos");
        root.ClearData("target");
        return root;
    }

    protected override void Start()
    {
        InitNodes();
        base.SetTree();
    }

    private void InitNodes()
    {
        // Not Dead-> Attack or GoToTarget Or GoToPos -> ELSE Guard 
        Guard = new Selector(new List<Node>
                {
                    new CheckIsDead(transform), // if dead -> TaskDead               
                    new Selector(new List<Node> // if none of the below -> Guard
                    {
                        new Sequence(new List<Node> // priority 3 - go to position 
                        {
                            new CheckMousePosition(),
                            new TaskGoToPosition(transform,So.AttackRange,So.GoToSpeed)
                        }),
                        new Sequence(new List<Node> // priority 1 - attack if in range
                        {
                            new CheckInRange(transform, So.AttackRange),
                            new TaskAttack(transform)
                        }),
                        new Sequence(new List<Node> // priority 2 - go to target if in range
                        {
                            new CheckIsRangeTo(transform,So.FOVRange,1 << LayerMask.NameToLayer("Character")),
                            new TaskGoToTarget(transform,So.AttackRange,So.GoToSpeed)
                        }),                        
                        new TaskGuard(transform, So.AttackRange, So.GoToSpeed)
                    }),
                    new TaskDead(transform)
                });

        // Not Dead-> Attack or GoToTarget Or GoToPos -> ELSE follow target 
        BodyGuard = new Selector(new List<Node>
                {
                    new CheckIsDead(transform), // if dead -> TaskDead               
                    new Selector(new List<Node> // if none of the below -> Guard
                    {
                        new Sequence(new List<Node> // priority 3 - go to position 
                        {
                            new CheckMousePosition(),
                            new TaskGoToPosition(transform,So.AttackRange,So.GoToSpeed)
                        }),
                        new Sequence(new List<Node> // priority 1 - attack if in range
                        {
                            new CheckInRange(transform, So.AttackRange),
                            new TaskAttack(transform)
                        }),
                        new Sequence(new List<Node> // priority 2 - go to target if in range
                        {
                            new CheckIsRangeTo(transform,So.FOVRange,1 << LayerMask.NameToLayer("Character")),
                            new TaskGoToTarget(transform,So.AttackRange,So.GoToSpeed)
                        }),                        
                        new TaskBodyGuard(transform, So.AttackRange, So.GoToSpeed,GetPlayer())
                    }),
                    new TaskDead(transform)
                });

        // Not Dead-> Attack or GoToTarget Or GoToPos -> ELSE Patrol 
        Aggressive = new Selector(new List<Node>
                {
                    new CheckIsDead(transform), // if dead -> TaskDead               
                    new Selector(new List<Node> // if none of the below -> patrol
                    {
                        new Sequence(new List<Node> // priority 3 - go to position 
                        {
                            new CheckMousePosition(),
                            new TaskGoToPosition(transform,So.AttackRange,So.GoToSpeed)
                        }),
                        new Sequence(new List<Node> // priority 1 - attack if in range
                        {
                            new CheckInRange(transform,So.AttackRange),
                            new TaskAttack(transform)
                        }),
                        new Sequence(new List<Node> // priority 2 - go to target if in range
                        {
                            new CheckIsRangeTo(transform,So.FOVRange,1 << LayerMask.NameToLayer("Character")),
                            new TaskGoToTarget(transform,So.AttackRange,So.GoToSpeed)
                        }),                        
                        new TaskPatrol(transform, So.PatrolSpeed, So.PatrolTurnSpeed,So.StandTime,So.PatrolTime)
                    }),
                    new TaskDead(transform)
                });

        // Not Dead-> Attack or GoToTarget Or GoToPos -> ELSE Work       
        Work = new Selector(new List<Node>
                {
                    new CheckIsDead(transform), // if dead -> TaskDead               
                    new Selector(new List<Node> // if none of the below -> Work
                    {
                        new Sequence(new List<Node> // priority 3 - go to position 
                        {
                            new CheckMousePosition(),
                            new TaskGoToPosition(transform,So.AttackRange,So.GoToSpeed)
                        }),
                        new Sequence(new List<Node> // priority 1 - attack if in attackRange
                        {
                            new CheckInRange(transform,So.AttackRange),
                            new TaskAttack(transform)
                        }),
                        new Sequence(new List<Node> // priority 2 - go to target if in GoToRange
                        {
                            new CheckIsRangeTo(transform,So.FOVRange,1 << LayerMask.NameToLayer("Character")),
                            new TaskGoToTarget(transform,So.AttackRange,So.GoToSpeed)
                        }),                        
                        new Selector(new List<Node> // priority 4 - go to WorkPlace ->  
                        {
                            new Sequence(new List<Node> // priority 4.1 - WORK if in Range
                            {
                                new CheckInRange(transform,So.StartWorkRange,_workType),
                                new TaskWork(transform)
                            }),
                            new Sequence(new List<Node> // priority 4.2 - go to WorkPlace if in Range
                            {
                                new CheckIsWorkPlaceRange(transform,So.WorkPlaceRange, 1 << LayerMask.NameToLayer(_workType),_workType),
                                new TaskGoToTarget(transform,So.StartWorkRange,So.GoToSpeed,_workType),
                            })
                         })
                    }),
                    new TaskDead(transform)
                });
    }

    protected override Node SetupTree()
    {
        return new Node();
    }


    // return Player transform  
    private Transform GetPlayer()
    {
        if (_player == null)
            _player = GameObject.Find("Player").transform;
        return _player;
    }


    #region Not Dead-> Attack or GoToTarget Or GoToPos -> ELSE Guard 
    //Guard = 
    #endregion


    #region Not Dead-> Attack or GoToTarget Or GoToPos -> ELSE Patrol       
    //new Selector(new List<Node>
    //{
    //    new CheckIsDead(transform), // if dead -> TaskDead               
    //    new Selector(new List<Node> // if none of the below -> patrol
    //    {
    //        new Sequence(new List<Node> // priority 1 - attack if in range
    //        {
    //            new CheckEnemyInAttackRange(transform,_so.AttackRange),
    //            new TaskAttack(transform)
    //        }),
    //        new Sequence(new List<Node> // priority 2 - go to target if in range
    //        {
    //            new CheckEnemyInFOVRange(transform,_so.FOVRange),
    //            new TaskGoToTarget(transform,_so.AttackRange,_so.GoToSpeed)
    //        }),
    //        new Sequence(new List<Node> // priority 3 - go to position 
    //        {
    //            new CheckMousePosition(),
    //            new TaskGoToPosition(transform,_so.AttackRange,_so.GoToSpeed)
    //        }),
    //        new TaskPatrol(transform, _so.PatrolSpeed, _so.PatrolTurnSpeed,_so.StandTime,_so.PatrolTime)
    //    }),
    //    new TaskDead(transform)
    //});
    #endregion

    #region Patrol-> Attack 
    //new Selector(new List<Node>
    //{                
    //    new Sequence(new List<Node>
    //    {
    //        new CheckEnemyInAttackRange(transform,_so.AttackRange),
    //        new TaskAttack(transform)// constant RUNNING
    //    }),
    //    new Sequence(new List<Node>
    //    {
    //        new CheckEnemyInFOVRange(transform,_so.FOVRange),
    //        new TaskGoToTarget(transform,_so.AttackRange,_so.GoToSpeed)// constant RUNNING
    //    }),
    //    new TaskPatrol(transform, _so.PatrolSpeed, _so.PatrolTurnSpeed,_so.StandTime,_so.PatrolTime)
    //});
    #endregion

    #region Not Dead-> Patrol-> Attack     
    //new Selector(new List<Node>
    //{
    //    new CheckIsDead(transform), // if dead -> TaskDead               
    //    new Selector(new List<Node> // if not attacking or not goto target -> patrol
    //    {
    //        new Sequence(new List<Node> // attack if in range
    //        {
    //            new CheckEnemyInAttackRange(transform,_so.AttackRange),
    //            new TaskAttack(transform)
    //        }),
    //        new Sequence(new List<Node> // go to target if in range
    //        {
    //            new CheckEnemyInFOVRange(transform,_so.FOVRange),
    //            new TaskGoToTarget(transform,_so.AttackRange,_so.GoToSpeed)
    //        }),
    //        new TaskPatrol(transform, _so.PatrolSpeed, _so.PatrolTurnSpeed,_so.StandTime,_so.PatrolTime)
    //    }),
    //    new TaskDead(transform)
    //});
    #endregion
}
