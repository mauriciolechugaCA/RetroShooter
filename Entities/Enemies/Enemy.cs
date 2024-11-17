using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using RetroShooter.Entities.Projectiles;

/*
 * Represents a general enemy, with position, health, speed and points attributes.
 */

namespace RetroShooter.Entities.Enemies
{
    internal abstract class Enemy
    {
        public Vector2 Position { get; protected set; }
        public int Health { get; protected set; }
        public float Speed { get; protected set; }
        public int Points { get; protected set; }
        protected List<Projectile> projectiles;

        public Enemy(Vector2 startPosition, int health, float speed, int points)
        {
            Position = startPosition;
            Health = health;
            Speed = speed;
            Points = points;
            projectiles = new List<Projectile>();
        }

        public abstract void Move();

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public virtual void Attack()
        {
            // Attack logic
            var projectile = new Projectile(Position, new Vector2(0, 1), 5, 1);
            projectiles.Add(projectile);
        }

        public bool IsDead()
        {
            return Health <= 0;
        }
    }
}
