using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public interface ITargeter
    {
        public List<Collider2D> AvaliableTargets { get; set; }
        public GameObject TargetObject { get; set; }
        public GameObject CurrentGameObject { get; set; }
    }
}