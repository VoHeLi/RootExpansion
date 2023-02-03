using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TileSelector : MonoBehaviour
{
    [SerializeField] private Material defaultOutline;
    [SerializeField] private Material selectedOutline;


    private Vector2 _selectedPos;
    private CaseInfo _caseInfo;

    private Action pendingAction;

    private PlayerInput playerInput;
    private RoundManager roundManager;

    public Vector2 selectedPos
    {
        get
        {
            return _selectedPos;
        }
        private set
        {
            _selectedPos = value;
        }
    }


    private void Awake()
    {
        playerInput = GameObject.Find("MovingCamera").GetComponent<PlayerInput>();
        roundManager = GameObject.Find("RoundManager").GetComponent<RoundManager>();
    }

    void Update()
    {
        if(_caseInfo != null)
        {
            _caseInfo.SetOutline(defaultOutline);
        }

        if (pendingAction == null) return;
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100))
        {
            _caseInfo = hit.collider.gameObject.GetComponentInParent<CaseInfo>();
            _selectedPos = _caseInfo.casePos;
            _caseInfo.SetOutline(selectedOutline);
            if (playerInput.actions["Click"].ReadValue<float>() >= 0.5f)
            {
                pendingAction.actionTile = _caseInfo;
                roundManager.currentPlayer.SetAction(pendingAction);
                pendingAction = null;
            }
        }
        else
        {
            _caseInfo = null;
            _selectedPos = Vector2.left + Vector2.down; //-1,-1 = no case selected;
        }
    }


    public void BeginPlantAction()
    {
        pendingAction = new Action(Action.ActionType.Plant);
    }

    public void BeginAttackAction()
    {
        pendingAction = new Action(Action.ActionType.Attack);
    }

    public void BeginArroserAction()
    {
        pendingAction = new Action(Action.ActionType.Arroser);
    }
}
