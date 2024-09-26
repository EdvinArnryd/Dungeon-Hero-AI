using Game;
using Game.Actions;
using Graphs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// Replace the FirstName and LastName with your name and last name :)
/// </summary>
namespace Edvin_Arnryd
{
    public class Edvin_Arnryd_Controller : HeroController
    {
        public override ControllerAction Think()
        {
			EnemyController enemy = EnemyController.AllEnemies.Find(e => IsNeighbor(e));
            if (enemy != null)
            {
                return new Action_Attack(this, enemy);
            }

            Heart heart = Heart.AllHearts.Count > 0 ? Heart.AllHearts[0] : null;
            if (heart != null)
            {
                return new Action_MoveTowards(this, heart.Node);
            }
            
            return new Action_Wait(this, 1.0f);
        }
    }
}