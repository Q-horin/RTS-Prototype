using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace RTS.Combat
{
    public class Targeter : NetworkBehaviour
    {   
        private Targetable target;

        public Targetable GetTarget() => target;

        [Command]
        public void CmdSetTarget(GameObject targetGameObject)
        {
            if (!targetGameObject.TryGetComponent<Targetable>(out Targetable target)) {return;}
            this.target = target;
        }

        public void ClearTarget()
        {
            target = null;
        }
    }
}
//EOF.