using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    [Header("Allows looping back, like 1->2->3->2->1")]
    [SerializeField]
    private bool loopback;

    public IEnumerable<Vector3> Positions
    {
        get
        {
            var transforms = GetComponentsInChildren<Transform>()
                .Where(t => t != transform)
                .ToArray();
            if (loopback)
                transforms = transforms
                    .Concat(transforms
                        .Skip(1)
                        .Reverse()
                        .Skip(1))
                    .ToArray();
            return transforms.Select(t => t.position);
        }
    }
}