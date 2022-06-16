using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;
using System;

namespace RTS.Units
{
    public class UnitSelectionHandler : NetworkBehaviour
    {
        [SerializeField] private LayerMask layerMask = new LayerMask();
        private Camera mainCamera;
        public List<Unit> SelectedUnits { get;}= new List<Unit>();

        private void Start() 
        {
            mainCamera = Camera.main;    
        }

        private void Update() 
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                //TO DO SELECTION AREA
                foreach (Unit selectedUnit in SelectedUnits)
                {
                    selectedUnit.Deselected();
                }
                SelectedUnits.Clear();
            }
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                ClearSelectionArea();
            }    
        }

        private void ClearSelectionArea()
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)) {return;}
            if (!hit.collider.TryGetComponent<Unit>(out Unit unit)) {return;}
            
            if (!unit.hasAuthority) {return;}

            SelectedUnits.Add(unit);

            foreach (Unit selectedUnit in SelectedUnits)
            {
                selectedUnit.Select();
            }

        }
    }
}
//EOF.
