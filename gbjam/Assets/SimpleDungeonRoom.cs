using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class SimpleDungeonRoom
{
    public HashSet<Direction> exits = new HashSet<Direction>();    

    public Vector2 location = Vector2.zero;
}
