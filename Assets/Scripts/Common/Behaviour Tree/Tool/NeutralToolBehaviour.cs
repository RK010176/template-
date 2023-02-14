using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using RPGCharacterAnims;
using System;
using Game;
using Photon.Pun;

public class NeutralToolBehaviour : BehaviourTree.Tree
{
    public ToolBehaviours_SO So;
    public ItemSO PickAxe;
    public ItemSO WoodAxe;
    private Controls _controls;
    private ToolAttack _toolAttack;
    private ItemsInventory _invantory;
    private Health _health;
    private Node Patrol, Work;
    [SerializeField] private int _mode = 0;
    [SerializeField] private bool _isBending;
    [SerializeField] private string _workType;
    private Transform _player;


    private void OnEnable()
    {
        _controls = GetComponent<Controls>();
        _controls.useNavigation = true;
        _controls.OnCharacterClick += SetTarget;
        _health = GetComponent<Health>();
        _health.OnHealthPctChanged += CheckForKneeBend;
    }
    private void OnDisable()
    {
        _controls.OnCharacterClick -= SetTarget;
    }
    public void SetTarget(Transform target)
    {
        if (!photonView.IsMine) return;
        SetData("target", target);        
    }

    protected override void Start()
    {
        _toolAttack = GetComponent<ToolAttack>();
        _invantory = GetComponent<ItemsInventory>();
        InitNeutralTool();
        InitNodes();
        base.SetTree();
    }
    private void InitNodes()
    {
        // Not Dead-> Attack or GoToTarget Or GoToPos -> ELSE Patrol 
        Patrol = new Selector(new List<Node>
        {
            new CheckIsDead(transform), // if dead -> TaskDead               
            new Selector(new List<Node> // if none of the below -> patrol
            {
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
                new TaskPatrol(transform, So.NeutralPatrolSpeed, So.PatrolTurnSpeed,So.StandTime,So.PatrolTime)
            }),
            new TaskDead(transform)
        });
        
        // Not Dead-> Attack or GoToTarget Or GoToPos -> ELSE Work       
        Work = new Selector(new List<Node>
        {
            new CheckIsDead(transform), // if dead -> TaskDead               
            new Selector(new List<Node> // if none of the below -> Work
            {
                new Sequence(new List<Node> // priority 1 - attack if in attackRange
                {
                    new CheckInRange(transform,So.AttackRange),
                    new TaskAttack(transform)
                }),

                new Selector(new List<Node> // priority 4 - go to WorkPlace   
                {
                    new Sequence(new List<Node> // priority 1 - attack if in attackRange
                    {
                        new CheckInRange(transform,So.StartWorkRange,_workType),
                        new TaskWork(transform)
                    }),
                    new Sequence(new List<Node> // priority 4.1 - go to WorkPlace if not there
                    {
                        new CheckIsWorkPlaceRange(transform,So.WorkPlaceRange, 1 << LayerMask.NameToLayer(_workType),_workType),
                        new TaskGoToTarget(transform,So.StartWorkRange,So.GoToSpeed,_workType),
                    })
                 })
            }),
            new TaskDead(transform)
        });      
    }
    private void InitNeutralTool()
    {
        // random mode 
        _mode = UnityEngine.Random.Range(0, 2);

        // if mode == 1 -> what Work to do
        if (_mode == 1)
        {
            // random Work
            int work = UnityEngine.Random.Range(0, 2);
            switch (work)
            {
                case 0: // GoldMaine
                    _workType = "GoldMaine";
                    _invantory.WorkItem = ItemsEnumsOS.WorkEnum.PickAxe;
                    _invantory.Items.Add(PickAxe);
                    _invantory.Set();
                    break;
                case 1: // Woods
                    _workType = "Woods";
                    _invantory.WorkItem = ItemsEnumsOS.WorkEnum.WoodAxe;
                    _invantory.Items.Add(WoodAxe);
                    _invantory.Set();
                    break;
            }
        }
    }



    protected override Node SetupTree()
    {
        return new Node();
    }

    protected override Node UpdateTree()
    {
        Node root = null;
        switch (_mode)
        {
            case 0:
                root = Patrol;
                break;
            case 1:
                root = Work;
                break;
        }
        return root;       
    }

    


    private void CheckForKneeBend(float healthPct)
    {
        if (_isBending) return;
        
        if (healthPct > 0 && healthPct <= 0.35)
        {
            // if attacker changed -> no Dual -> no bend the knee
            if (!_toolAttack.IsSameAttacker) return;

            _isBending = true;
            _controls.Relax = true;
            _controls.useCrouch = true;
            _toolAttack.IsAttacking = false;

            photonView.RPC("SetIsBendTheKnee", RpcTarget.AllBufferedViaServer, true);
            StartCoroutine(DoDelay());
        }                                
    }
    private IEnumerator DoDelay()
    {        
        yield return new WaitForSeconds(3f);
        _controls.useCrouch = false;
        photonView.RPC("ChangeToolLoyalty",
                        RpcTarget.All,
                        GetComponent<Attack>().AttackTarget.name.Contains("Red") ? 0 : 1);                                              
    }



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




    private void OnDestroy()
    {
        _health.OnHealthPctChanged -= CheckForKneeBend;
        _controls.OnCharacterClick -= SetTarget;
    }

}
