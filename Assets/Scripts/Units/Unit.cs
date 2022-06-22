using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Events;
using RTS.Combat;

namespace RTS.Units
{
    public class Unit : NetworkBehaviour
    {
        [SerializeField] private UnityEvent onSelected = null;
        [SerializeField] private UnityEvent onDeselected = null;
        [SerializeField] private UnitMovement unitMovement = null;
        [SerializeField] private Targeter targeter = null;

        public static event Action<Unit> ServerOnUnitSpawned;
        public static event Action<Unit> ServerOnUnitDespawned;

        public static event Action<Unit> AuthorityOnUnitSpawned;
        public static event Action<Unit> AuthorityOnUnitDespawned;


        public UnitMovement GetUnitMovement() => unitMovement;
        public Targeter GetTargeter() => targeter;

        #region Server

        public override void OnStartServer()
        {
            ServerOnUnitSpawned?.Invoke(this);
        }
        
        public override void OnStopServer()
        {
            ServerOnUnitDespawned?.Invoke(this);
        }


        #endregion

        #region Client

        public override void OnStartClient()
        {
            if (!isClientOnly || !hasAuthority) {return;}
            AuthorityOnUnitSpawned?.Invoke(this);
        }

        public override void OnStopClient()
        {
            if (!isClientOnly || !hasAuthority) {return;}
            AuthorityOnUnitDespawned?.Invoke(this);
        }

        [Client]
        public void Select()
        {
            if (!hasAuthority) {return;}
            onSelected?.Invoke();
        }

        [Client]
        public void Deselected()
        {
            if (!hasAuthority) {return;}
            onDeselected?.Invoke();
        }
        #endregion
    }
}
//EOF.
