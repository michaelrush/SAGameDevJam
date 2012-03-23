using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Classic
{
    public enum CollisionType
    {
        /// <summary>
        /// An passable tile does not collide with any other object
        /// </summary>
        Passable = 0,

        /// <summary>
        /// </summary>
        Projectile = 1,

        /// <summary>
        /// </summary>
        Enemy = 2,
        
        /// <summary>
        /// </summary>
        Player = 3
    }
}
