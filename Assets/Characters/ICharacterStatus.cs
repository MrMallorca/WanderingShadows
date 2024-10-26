using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public interface ICharacterStatus
    {
        bool Hit { get; }
        bool Dead { get; }
    }

