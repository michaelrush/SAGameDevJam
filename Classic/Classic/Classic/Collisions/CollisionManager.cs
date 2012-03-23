using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace Classic
{
    public static class CollisionManager
    {
        /// <summary>
        /// Finds the set of all collisions for all entities and resolves them in order of collision time
        /// TODO: Cannot restrict to only MovableEntities
        /// </summary>
        /// <param name="entities">All active collidable entities in the game</param>
        public static void resolveCollisions(List<Entity> entities)
        {
            List<CollisionData> contacts = new List<CollisionData>();

            for (int i = 0; i < entities.Count; i++)
            {
                // Do not check against self and do not repeats pairs
                for (int j = i + 1; j < entities.Count; j++)
                {
                    if (entities[i].collidesWith(entities[j]) || entities[j].collidesWith(entities[i]))
                    {
                        MovableEntity a = entities[i] as MovableEntity;
                        MovableEntity b = entities[j] as MovableEntity;

                        // Check for collisions. Only add contacts with smaller or equal collision time to map
                        CollisionData cd = AABB.AABBSweep(a, b);
                        if (cd != null)
                        {
                            contacts.Add(cd);
                        }
                    }
                }
            }

            // Sort each entity's contacts by time and resolve in order
            foreach (CollisionData cd in contacts)
            {
                cd.a.doCollision(cd.b);
                cd.b.doCollision(cd.a);
            }
        }
    }
}
