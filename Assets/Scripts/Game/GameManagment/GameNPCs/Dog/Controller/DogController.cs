using UnityEngine;
using Common;


namespace Game
{
    public class DogController : Controller
    {
        private bool _isFood = false;

        private void Start()
        {
            _stateMachine.SetState(new DogRun(transform, _animator, NpcBehaviors.FastMovingSpeed));
        }

        protected override void DogBrain(Vector3 FoodPos)
        {
            if (_isFood) // -> on top of Food            
                Eat();
            else // look for food
            {
                _foodDistance = (transform.position - FoodPos).magnitude;
                if (_foodDistance < NpcBehaviors.SearchRadius && FoodPos != Vector3.zero) // found food location                
                    RunToFood(FoodPos);
                else // no food to be found -> stroll...                
                    Run();
            }
        }


        #region States functions
        private void Run()
        {
            if (_state != "Game.DogRun")
            {
                _stateMachine.SetState(new DogRun(transform, _animator, NpcBehaviors.FastMovingSpeed));
                _audioManager.PlaySound(1, true);
            }
            _isFood = false;
        }
        private void RunToFood(Vector3 FoodrPos)
        {
            if (_state != "Game.DogRun")
            {
                _stateMachine.SetState(new DogRun(transform, _animator, NpcBehaviors.FastMovingSpeed));
                _audioManager.PlaySound(1, true);
            }
            NPCRotateToTarget(FoodrPos);
            _isFood = false;
        }
        private void Eat()
        {
            if (_state != "Game.DogEat")
            {
                _stateMachine.SetState(new Eating(transform, _animator, NpcBehaviors.MovingSpeed));
                _audioManager.PlaySound(0, false);
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Bone")
                _isFood = true;
        }
        #endregion
    }
}