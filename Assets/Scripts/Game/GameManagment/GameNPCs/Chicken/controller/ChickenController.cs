using UnityEngine;
using Common;

namespace Game
{  
    public class ChickenController : Controller
    {
        private bool _isFood = false;
        protected override void Brain(Vector3 _playerPosition, Vector3 FoodPos)
        {            
            _playerDistance = (transform.position - _playerPosition).magnitude;

            if (_playerPosition == Vector3.zero)// -> no player found
            {
                if (_isFood) // -> on top of Food            
                    EatOrStand();
                else // look for food
                {
                    _foodDistance = (transform.position - FoodPos).magnitude;
                    if (_foodDistance < NpcBehaviors.SearchRadius && FoodPos != Vector3.zero) // found food location                
                        Walk(FoodPos);
                    else // no food to be found -> stroll...                
                        PatrolOrStand();
                }
            }
            else // -> player found 
            {
                if (_playerDistance < 2 && _playerPosition != Vector3.zero)
                    Run(_playerPosition);
            }
        }

        #region States functions
        protected override void Patrol()
        { base.Patrol();
            _audioManager.PlaySound(0, false);}
        private void PatrolOrStand()
        {
            if (_state != "Game.Patrol" && !_stroll)
            { _stateMachine.SetState(new Patrol(transform, _animator, NpcBehaviors.MovingSpeed));
                _audioManager.PlaySound(0, false);
            }
            if (_state != "Game.Standing" && _stroll)
            { _stateMachine.SetState(new Standing(transform, _animator, NpcBehaviors.MovingSpeed));
                _audioManager.PlaySound(0, false);
            }
        }
        private void Walk(Vector3 FoodrPos)
        {
            if (_state != "Game.Walking")
            { _stateMachine.SetState(new Walking(transform, _animator, NpcBehaviors.MovingSpeed));
                _audioManager.PlaySound(0, false);}
            NPCRotateToTarget(FoodrPos);
        }
        private void Run(Vector3 _playerPosition)
        {
            if (_state != "Game.Running")
            { _stateMachine.SetState(new Running(transform, _animator, NpcBehaviors.FastMovingSpeed));
                _audioManager.PlaySound(1, true);
            }
            NPCRotateToTarget(_playerPosition);
            _isFood = false;            
        }               
        private void EatOrStand()
        {
            if (_state != "Game.Eating" && !_stroll)
            { _stateMachine.SetState(new Eating(transform, _animator, NpcBehaviors.MovingSpeed));
                _audioManager.PlaySound(0, false);
            }
            if (_state != "Game.Standing" && _stroll)
            { _stateMachine.SetState(new Standing(transform, _animator, NpcBehaviors.MovingSpeed));
                _audioManager.PlaySound(0, false);
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Food")
                _isFood = true;
        }        
        #endregion
    }
}