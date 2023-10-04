using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MatchRotation : MonoBehaviour
{
    [SerializeField][Required] Transform _target;
    // Start is called before the first frame update

    void LateUpdate()
    {
        transform.rotation = _target.rotation;
    }
}
