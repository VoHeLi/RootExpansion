using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
    [SerializeField] private Material defaultOutline;
    [SerializeField] private Material selectedOutline;

    private Vector2 _selectedPos;
    private CaseInfo _caseInfo;
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
    // Update is called once per frame
    void Update()
    {
        if(_caseInfo != null)
        {
            _caseInfo.SetOutline(defaultOutline);
        }
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100))
        {
            _caseInfo = hit.collider.gameObject.GetComponentInParent<CaseInfo>();
            _selectedPos = _caseInfo.casePos;
            _caseInfo.SetOutline(selectedOutline);
        }
        else
        {
            _caseInfo = null;
            _selectedPos = Vector2.left + Vector2.down; //-1,-1 = no case selected;
        }
    }
}
