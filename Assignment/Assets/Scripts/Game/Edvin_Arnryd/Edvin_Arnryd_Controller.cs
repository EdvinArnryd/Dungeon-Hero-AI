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
///
///
/// Things to add:
/// Only pick up health if not full health
/// Never attack or kick
/// Always move
/// Move around traps so that evenmies kill themselves
namespace Edvin_Arnryd
{
    public class Edvin_Arnryd_Controller : HeroController
    {
        public override ControllerAction Think()
        {
            
            // Find the closest heart
            Heart closestHeart = Heart.AllHearts.Count > 0 ? Heart.AllHearts[0] : null;
            
            if (closestHeart != null)
            {
                GraphAlgorithms.Path aPath = Dungeon.Instance.GetShortestPath(this, this.Node, closestHeart.Node);
                GraphAlgorithms.Path shortestPath = aPath;
                foreach (var heart in Heart.AllHearts)
                {
                    GraphAlgorithms.Path path = Dungeon.Instance.GetShortestPath(this, this.Node, heart.Node);
                    if (path.Count < shortestPath.Count)
                    {
                        shortestPath = path;
                        closestHeart = heart;
                    }
                }
                return new Action_MoveTowards(this, closestHeart.Node);
            }
            else
            {
                // Move towards closest trap tile and circle around it
            }
            
            EnemyController enemy = EnemyController.AllEnemies.Find(e => IsNeighbor(e));
            if (enemy != null)
            {
                return new Action_Kick(this, enemy);
            }
            
            /*
            // Walk towards closest heart
            if (closestHeart != null)
            {
                
            }*/
            
            
            // if (heart != null)
            // {
            //     return new Action_MoveTowards(this, heart.Node);
            // }
            
            return new Action_Wait(this, 1.0f);
        }
    }
}