using RPG.Combat;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {

        IAction CurrentAction;



        public void StartAction(IAction action)
        {
            if (CurrentAction == action) return;
            if (CurrentAction != null)
            {
                CurrentAction.Cancel();

            }
            CurrentAction = action;
        }
        public void CancelAction()
        {
            StartAction(null);
        }


    }
}
