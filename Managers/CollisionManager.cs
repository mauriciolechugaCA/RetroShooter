using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using RetroShooter.Entities;
using RetroShooter.Entities.Powerups;
using RetroShooter.Entities.Projectiles;
using RetroShooter.Entities.Enemies;

/*
 * Centralizes collision detection and response between game objects.
 * ## Detect collisions between player and enemies, projectiles and enemies, player and power-ups, etc.
 * ## Trigger appropriate responses, such as reducing player health or destroying enemies.
 */

namespace RetroShooter.Managers
{
    internal class CollisionManager
    {
        private Player _player;
        private List<Projectile> _projectiles;
        private List<Enemy> _enemies;
        private List<Powerup> _powerups;

        public CollisionManager(Player player, List<Projectile> projectiles, List<Enemy> enemies, List<Powerup> powerups)
        {
            _player = player;
            _projectiles = projectiles;
            _enemies = enemies;
            _powerups = powerups;
        }

        public void Update()
        {
            CheckPlayerEnemyCollisions();
            CheckPlayerProjectileCollisions();
            CheckPlayerPowerupCollisions();
            CheckProjectileEnemyCollisions();

            _projectiles.RemoveAll(p => !p.IsAlive);
        }

        private void CheckPlayerEnemyCollisions()
        {
            foreach (var enemy in _enemies)
            {
                if (_player.Bounds.Intersects(enemy.Bounds))
                {
                    _player.TakeDamage(enemy.CollisionDamage);
                    enemy.IsAlive = false;
                }
            }
        }

        private void CheckPlayerProjectileCollisions()
        {
            foreach (var projectile in _projectiles)
            {
                if (projectile.Owner == ProjectileOwner.Enemy && projectile.Bounds.Intersects(_player.Bounds))
                {
                    _player.TakeDamage(projectile.Damage);
                    projectile.IsAlive = false; // Mark projectile for removal
                }
            }
        }

        private void CheckPlayerPowerupCollisions()
        {
            foreach (var powerup in _powerups)
            {
                if (_player.Bounds.Intersects(powerup.Bounds))
                {
                    powerup.ApplyEffect(_player);
                    powerup.IsAlive = false; // Mark powerup for removal
                }
            }
        }

        private void CheckProjectileEnemyCollisions()
        {
            foreach (var projectile in _projectiles)
            {
                foreach (var enemy in _enemies)
                {
                    if (projectile.Owner == ProjectileOwner.Player && projectile.Bounds.Intersects(enemy.Bounds))
                    {
                        enemy.TakeDamage(projectile.Damage, _player);
                        projectile.IsAlive = false; // Mark projectile for removal
                    }
                }
            }
        }
    }
}