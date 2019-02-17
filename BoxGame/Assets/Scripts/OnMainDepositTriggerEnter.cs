using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMainDepositTriggerEnter : MonoBehaviour
{
    [SerializeField] private LevelScript level;
    private Box dep;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null && (dep =  other.transform.parent.GetComponent<Box>()) != null && !other.transform.parent.name.Equals("HouseGuyObj"))
        {
            level.HandleDeposit(dep);
        }
    }
}
