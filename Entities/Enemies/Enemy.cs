using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using RetroShooter.Entities.Projectiles;
using Microsoft.Xna.Framework;

/*
 * Represents a general enemy, with position, health, speed and points attributes.
 */

namespace RetroShooter.Entities.Enemies
{
    public abstract class Enemy
    {
        public Vector2 Position { get; protected set; }
        public int Health { get; protected set; }
        public float Speed { get; protected set; }
        public int Points { get; protected set; }
        protected List<Projectile> projectiles;
        public Texture2D enemyBullet;

        public Enemy(Vector2 startPosition, int health, float speed, int points, Texture2D enemyBulletTexture)
        {
            Position = startPosition;
            Health = health;
            Speed = speed;
            Points = points;
            projectiles = new List<Projectile>();
            enemyBullet = enemyBulletTexture;
        }

        public abstract void Move();

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public virtual void Attack()
        {
            // Attack logic
            var projectile = new Projectile(Position, new Vector2(0, 1), 5, 1, enemyBullet);
            projectiles.Add(projectile);
        }

        public bool IsDead()
        {
            return Health <= 0;
        }
    }
}
