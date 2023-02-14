using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace BehaviourTree
{
    // BehaviourTree https://www.youtube.com/watch?v=aR6wt5BlE-E&t=1026s
    // Sequence -> if both node SUCCESS -> AND gate
    // Selector -> if one node SUCCESS/RUNNING -> OR gate
    // priority in Selector is Top to Bottom

    // Leafs: 
    // Tasks - constant RUNNING
    // Checks - SUCCESS/FAILURE

    public abstract class Tree : MonoBehaviourPun
    {
        private Node _root = null;

        protected virtual void Start()
        {
            _root = SetupTree();           
        }
        private void Update()
        {
            if (!photonView.IsMine) return;
            
            if (_root != null)            
                _root.Evaluate();            
        }
        protected abstract Node SetupTree();
        protected abstract Node UpdateTree();
        protected virtual void SetTree()
        {
            _root = UpdateTree();
        }
        
        public void SetData(string key, object value)
        {
            _root.ClearData(key);            
            _root.SetData(key, value);
        }
    }
}