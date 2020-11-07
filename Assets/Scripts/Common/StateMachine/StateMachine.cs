using UnityEngine;

namespace Common
{
    public class StateMachine
    {
        private IState CurrentState;
        private IState PreviousState;

        public void SetState(IState state)
        {
            if (this.CurrentState != null)
            {
                this.PreviousState = CurrentState;
                this.CurrentState.Exit();
            }
            this.CurrentState = state;

            //Debug.Log("CurrentState : " + CurrentState.GetType().Name);
            this.CurrentState.Enter();
        }

        public void Execute()
        {
            if (this.CurrentState != null)
                this.CurrentState.Execute();
        }

        public void Exit()
        {
            if (this.CurrentState != null)
                this.CurrentState.Exit();
        }

        public void SetPreviosState()
        {
            if (this.PreviousState != null)
            {
                this.CurrentState.Exit();
                this.CurrentState = this.PreviousState;
                this.CurrentState.Enter();
            }
        }

        public string GetCurrentState()
        {            
            return CurrentState.ToString();
        }

        public void CloseGame()
        {
            Application.Quit();
        }
    }
}