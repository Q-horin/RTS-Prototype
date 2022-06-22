using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using RTS.Combat;

namespace RTS.Units
{
        public class UnitMovement : NetworkBehaviour
    {
        [SerializeField] private NavMeshAgent agent = null;
        [SerializeField] private Targeter targeter = null;
        [SerializeField] private float chaseRange = 10f;

        [ServerCallback]
        private void Update() 
        {
            Targetable target = targeter.GetTarget();

            if (target != null)
            {   
                if ((target.transform.position - transform.position).sqrMagnitude > chaseRange * chaseRange)
                {
                    agent.SetDestination(target.transform.position);
                }
                else if (agent.hasPath)
                {
                    agent.ResetPath();
                }
                return;
            }

            if ( !agent.hasPath) { return;}
            if ( agent.remainingDistance > agent.stoppingDistance) { return;}
            agent.ResetPath();
        }
        #region Server
        [Command]
        public void CmdMove(Vector3 destination)
        {
            targeter.ClearTarget();
            if (!NavMesh.SamplePosition(destination, out NavMeshHit hit, 1f, NavMesh.AllAreas)){return;}
            agent.SetDestination(hit.position);
        }
        #endregion
    }
}
//EOF.