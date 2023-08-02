using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDisplayManager : MonoBehaviour
{
    [SerializeField]
    private Transform _atkDestination, _defDestination, _hpDestination, _centerStage, _calculationSymbols;

    private GameObject _attackObject, _defenseObject, _hpObject;
    private int _damage;
    public IEnumerator StartDamageCalculations(GameObject attackObject, GameObject defenseObject, GameObject hpObject, int result)
    {
        _attackObject = Instantiate(attackObject, _centerStage);
        _attackObject.transform.position = attackObject.transform.position;
        _defenseObject = Instantiate(defenseObject, _centerStage);
        _defenseObject.transform.position = defenseObject.transform.position;
        _hpObject = Instantiate(hpObject, _centerStage);
        _hpObject.transform.position = _hpDestination.position;
        _hpObject.transform.localScale = _hpDestination.localScale;
        _hpObject.SetActive(false);
        _damage = result;

        yield return StartCoroutine(MoveObjectsToPosition());
    }


    private IEnumerator MoveObjectsToPosition()
    {
        while (Vector3.Distance(_attackObject.transform.position, _atkDestination.position) > 0.001f) 
        {
            _attackObject.transform.position = Vector3.MoveTowards(_attackObject.transform.position, _atkDestination.position, Time.deltaTime * 5f);
            _attackObject.transform.localScale = Vector3.MoveTowards(_attackObject.transform.localScale, new Vector3(0.75f, 0.75f, 1f), Time.deltaTime * 5f);

            yield return null;
        }
        _attackObject.transform.position = _atkDestination.position;
        _attackObject.transform.localScale = new Vector3(0.75f, 0.75f, 1f);


        while (Vector3.Distance(_defenseObject.transform.position, _defDestination.position) > 0.001f)
        {
            _defenseObject.transform.position = Vector3.MoveTowards(_defenseObject.transform.position, _defDestination.position, Time.deltaTime * 5f);
            _defenseObject.transform.localScale = Vector3.MoveTowards(_defenseObject.transform.localScale, new Vector3(0.45f, 0.45f, 1f), Time.deltaTime * 5f);

            yield return null;
        }
        _defenseObject.transform.position = _defDestination.position;
        _defenseObject.transform.localScale = new Vector3(0.45f, 0.45f, 1f);
        _calculationSymbols.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _hpObject.SetActive(true);
        _hpObject.GetComponentInChildren<NumberDisplayManager>().DisplayNumber(_damage);
    }
}
