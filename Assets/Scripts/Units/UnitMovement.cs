using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace RTS.Units
{
        public class UnitMovement : NetworkBehaviour
    {
        [SerializeField] NavMeshAgent agent = null;

        #region Server
        [Command]
        public void CmdMove(Vector3 destination)
        {
            if (!NavMesh.SamplePosition(destination, out NavMeshHit hit, 1f, NavMesh.AllAreas)){return;}
            agent.SetDestination(hit.position);
        }
        #endregion
    }
}
//EOF.