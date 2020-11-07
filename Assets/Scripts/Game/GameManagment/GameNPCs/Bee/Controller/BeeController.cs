using UnityEngine;
using Common;
using System.Collections;

namespace Game
{    
    public class BeeController : Controller
    {
        private bool _isPlayer = false;
        public FloatVal PlayerHealth;
        protected override void Brain(Vector3 _playerPosition)
        {
            if (_playerPosition == Vector3.zero)// -> no player found
            { PatrolOrStand();}
            else // -> player found 
            {
                _playerDistance = (transform.position - _playerPosition).magnitude;
                if (_playerDistance > NpcBehaviors.StopAttackRadios)
                    _isPlayer = false;
                if (_isPlayer)
                    Attack();
                else // -> get to player 
                {
                    if (_playerDistance < NpcBehaviors.SearchRadius && _playerPosition != Vector3.zero)                    
                        MoveFast(_playerPosition);                                            
                }
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Player")
                _isPlayer = true;
        }

        #region States functions
        protected override void Patrol()
        {
            _stateMachine.SetState(new BeePatrol(transform, _animator, NpcBehaviors.MovingSpeed));
            _audioManager.PlaySound(0, true);
        }
        private void PatrolOrStand()
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
        private void Attack()
        {
            if (_stateMachine.GetCurrentState() != "Game.BeeAttack")
            { _stateMachine.SetState(new BeeAttack(transform, _animator));
                _audioManager.PlaySound(2, true);

                StartCoroutine(EffectPlayerHealth());                
            }
        }
        #endregion

        private IEnumerator EffectPlayerHealth()
        {
            PlayerHealth.Value -= 15f;
            yield return new WaitForSeconds(1);
        }
    }
}
