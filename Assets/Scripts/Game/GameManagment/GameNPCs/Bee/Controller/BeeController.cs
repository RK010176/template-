using UnityEngine;
using Common;

namespace Game
{    
    public class BeeController : Controller
    {
        private bool _isPlayer = false;
        protected override void Brain(Vector3 _playerPosition)
        {
            if (_playerPosition == Vector3.zero)// -> no player found
            { PatrolrStand();}
            else // -> player found 
            {
                _playerDistance = (transform.position - _playerPosition).magnitude;
                if (_playerDistance > 0.5f)
                    _isPlayer = false;
                if (_isPlayer)
                    Attack();
                else // -> get to player 
                {                    
                    if (_playerDistance < 2 && _playerPosition != Vector3.zero)
                        MoveFast(_playerPosition);
                }
            }
        }

        #region States functions
        protected override void Patrol()
        {
            _stateMachine.SetState(new BeePatrol(transform, _animator, NpcBehaviors.MovingSpeed));
            _audioManager.PlaySound(0, true);
        }
        private void PatrolrStand()
        {
            if (_stateMachine.GetCurrentState() != "Game.BeePatrol")
            {
                _stateMachine.SetState(new BeePatrol(transform, _animator, NpcBehaviors.MovingSpeed));
                _audioManager.PlaySound(0, true);
            }
        }
        private void MoveFast(Vector3 _playerPosition)
        {
            if (_stateMachine.GetCurrentState() != "Game.BeeMoveFast")
            { _stateMachine.SetState(new BeeMoveFast(transform, _animator, NpcBehaviors.FastMovingSpeed));
                _audioManager.PlaySound(1, true); }
            NPCRotateToTarget(_playerPosition);           
        }
        private void Walk(Vector3 FoodrPos)
        {
            if (_stateMachine.GetCurrentState() != "Game.BeeMove")
            { _stateMachine.SetState(new BeeMove(transform, _animator, NpcBehaviors.MovingSpeed));
                _audioManager.PlaySound(0, true);
            }
            NPCRotateToTarget(FoodrPos);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Player")
                _isPlayer = true;            
        }
        private void Attack()
        {
            if (_stateMachine.GetCurrentState() != "Game.BeeAttack")
            { _stateMachine.SetState(new BeeAttack(transform, _animator));
                _audioManager.PlaySound(2, true); ; }
        }       
        #endregion
    }
}
