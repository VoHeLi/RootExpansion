using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TileSelector : MonoBehaviour
{
    [SerializeField] private Material defaultOutline;
    [SerializeField] private Material okOutline;
    [SerializeField] private Material notOkOutline;
    [SerializeField] private Material selectedOkOutline;
    [SerializeField] private Material selectedNotOkOutline;

    [SerializeField] private MapBase map;


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
            _caseInfo.SetOutline(_caseInfo.IsCasePlantable() ? okOutline : notOkOutline);
        }

        if (pendingAction == null) return;
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100))
        {
            _caseInfo = hit.collider.gameObject.GetComponentInParent<CaseInfo>();
            _selectedPos = _caseInfo.casePos;

            

            _caseInfo.SetOutline(_caseInfo.IsCasePlantable() ? selectedOkOutline : selectedNotOkOutline);

            pendingAction.actionTile = _caseInfo;

            if (_caseInfo.IsCasePlantable())
            {
                pendingAction.Previsualize(map);
            }
            

            if (playerInput.actions["Click"].ReadValue<float>() >= 0.5f)
            {
                
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
        map.ResetTempStructure();
        pendingAction = new Action(Action.ActionType.Plant);

        List<CaseInfo> possible = new List<CaseInfo>();
        List<CaseInfo> notPossible = new List<CaseInfo>();

        map.GetPossibleCases(possible, notPossible);

        foreach(CaseInfo tile in possible)
        {
            tile.SetOutline(okOutline);
        }

        foreach (CaseInfo tile in notPossible)
        {
            tile.SetOutline(notOkOutline);
        }
    }

    public void BeginAttackAction()
    {
        map.ResetTempStructure();
        pendingAction = new Action(Action.ActionType.Attack);
    }

    public void BeginArroserAction()
    {
        map.ResetTempStructure();
        pendingAction = new Action(Action.ActionType.Arroser);
    }
}
