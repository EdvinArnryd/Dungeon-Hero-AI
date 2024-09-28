using Game;
using Game.Actions;
using Graphs;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AI.Nodes;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// Replace the FirstName and LastName with your name and last name :)
/// </summary>
///
///
/// Things to add:
/// Only pick up health if not full health -V
/// Never attack or kick -R
/// Always move
/// Move around traps so that enemies kill themselves
namespace Edvin_Arnryd
{
    public class Edvin_Arnryd_Controller : HeroController
    {
        bool southVisited = true;  // Track if the hero should go south or north
        Vector3Int northPosition = new Vector3Int(5, 0, -8);  // Example north position
        Vector3Int southPosition = new Vector3Int(-12, 0, 13); // Example south position
        public override ControllerAction Think()
        {
            //Dungeon.Node randomNode = Dungeon.Instance.GetRandomNode();
            // Find the closest heart
            Heart closestHeart = Heart.AllHearts.Count > 0 ? Heart.AllHearts[0] : null;
            
            if (closestHeart != null && HP < 5)
            {
                GraphAlgorithms.Path shortestPath = Dungeon.Instance.GetShortestPath(this, this.Node, closestHeart.Node);
                foreach (var heart in Heart.AllHearts)
                {
                    GraphAlgorithms.Path path = Dungeon.Instance.GetShortestPath(this, this.Node, heart.Node);
                    if (path != null)
                    {
                        if (path.Count < shortestPath.Count) // <- here is the error Im getting
                        {
                            shortestPath = path;
                            closestHeart = heart;
                        }
                    }
                }
                return new Action_MoveTowards(this, closestHeart.Node);
            }
            //EnemyController enemy = EnemyController.AllEnemies.Count > 0 ? EnemyController.AllEnemies[0] : null;

            Vector3Int currentTarget = southVisited ? northPosition : southPosition;
            if (this.Node.Position == currentTarget)
            {
                southVisited = !southVisited;
            }
            return new Action_MoveTowards(this, currentTarget);
            
            //return new Action_MoveTowards(this, randomNode);
            
            
            
            
            
            
            // {
            //     return new Action_Kick(this, enemy);
            // }
            
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