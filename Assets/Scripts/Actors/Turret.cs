using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Turret : Ally
{
    private protected override Transform CurrentTarget { get; set; }
    private protected override float FireRate { get; set; }
    private protected override float damage { get; set; }
}
