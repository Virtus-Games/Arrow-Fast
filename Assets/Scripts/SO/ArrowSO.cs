using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pontaap.Studio
{
    [CreateAssetMenu(menuName ="Asset/Create Arrow",fileName ="New Arrow")]
    public class ArrowSO : ScriptableObject
    {
        public GameObject arrowPrefab;
        public float throwForce;
        public float maxThrowForce;
        public float disableDuration;
        public float maxDisableDuration;

    }
}
