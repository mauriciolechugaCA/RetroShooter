using System.Collections.Generic;
using Microsoft.Xna.Framework;
using RetroShooter.Entities;
using RetroShooter.Entities.Powerups;
using RetroShooter.Entities.Projectiles;
using RetroShooter.Entities.Enemies;

/*
 * Centralizes collision detection and response between game objects.
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

        public void Update(GameTime gameTime)
        {
            CheckPlayerEnemyCollisions();
            CheckPlayerProjectileCollisions();
            CheckPlayerPowerupCollisions(gameTime);
            CheckProjectileEnemyCollisions();

            _projectiles.RemoveAll(p => !p.IsAlive);
            _powerups.RemoveAll(p => !p.IsAlive);
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

        private void CheckPlayerPowerupCollisions(GameTime gameTime)
        {
            foreach (var powerup in _powerups)
            {
                if (_player.Bounds.Intersects(powerup.Bounds))
                {
                    powerup.ApplyEffect(_player, gameTime);
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