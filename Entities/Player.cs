using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroShooter.Entities.Powerups;
using RetroShooter.Entities.Projectiles;
using RetroShooter.Managers;
using System;
using System.Collections.Generic;
using RetroShooter.Entities;

namespace RetroShooter.Entities
{
    public class Player
    {
        // Constants
        private const float DEFAULT_SPEED = 5f;
        private const float DEFAULT_SHOOT_COOLDOWN = 0.2f;
        private const float PROJECTILE_OFFSET_Y = -65f;
        private const int DEFAULT_POWER_LASER_DAMAGE = 2;
        private const int DEFAULT_NORMAL_DAMAGE = 1;
        private static readonly Vector2 PROJECTILE_DIRECTION_UP = new Vector2(0f, -1f);

        // Properties
        public Vector2 Position { get; set; }
        public int Health { get; set; }
        public int Score { get; set; }
        public bool IsPowerLaserActive { get; set; }
        public bool IsAlive { get; set; } = true;

        // Texture related properties
        private Texture2D texture;
        private Vector2 origin;
        private float rotation;
        private float scale;

        // Rectangle for collision detection
        public Rectangle Bounds => new Rectangle(
            (int)(Position.X - origin.X * scale),
            (int)(Position.Y - origin.Y * scale),
            (int)(texture.Width * scale),
            (int)(texture.Height * scale)
        );

        // Events
        public event Action<int> OnHealthChanged;
        public event Action<int> OnScoreChanged;
        public event Action OnPowerLaserStatusChanged;
        public event Action OnProjectileFired;

        // Private fields
        private float speed;
        private float shootCooldown;
        private float lastShotTime;
        private Vector2 dimensions;

        public Player(Vector2 startPosition, int health, Texture2D playerTexture, float scale)
        {
            Position = startPosition;
            Health = health;
            texture = playerTexture;
            this.scale = scale;
            dimensions = new Vector2(texture.Width * scale, texture.Height * scale);
            origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
            speed = DEFAULT_SPEED;
            shootCooldown = DEFAULT_SHOOT_COOLDOWN;
            lastShotTime = 0f;
            Score = 0;
            IsPowerLaserActive = false;
            IsAlive = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsAlive) return;

            Color tint = Color.White;
            if (IsPowerLaserActive)
            {
                tint = Color.Red;
            }

            spriteBatch.Draw(
                texture,
                Position,
                null,
                tint,
                rotation,
                origin,
                scale,
                SpriteEffects.None,
                0
            );
        }

        // This method is used to set the texture of the player
        public void SetTexture(Texture2D newTexture)
        {
            texture = newTexture;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        // This method is used to set the scale of the player
        public void SetScale(float newScale)
        {
            scale = newScale;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public void Move(InputManager inputManager, int screenWidth, int screenHeight)
        {
            Vector2 direction = GetMovementDirection(inputManager);
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
                Position += direction * speed;
                ClampPositionToScreen(screenWidth, screenHeight);
            }
        }

        private Vector2 GetMovementDirection(InputManager inputManager)
        {
            Vector2 direction = Vector2.Zero;
            if (inputManager.IsKeyDown(Keys.W) || inputManager.IsKeyDown(Keys.Up)) direction.Y -= 1;
            if (inputManager.IsKeyDown(Keys.S) || inputManager.IsKeyDown(Keys.Down)) direction.Y += 1;
            if (inputManager.IsKeyDown(Keys.A) || inputManager.IsKeyDown(Keys.Left)) direction.X -= 1;
            if (inputManager.IsKeyDown(Keys.D) || inputManager.IsKeyDown(Keys.Right)) direction.X += 1;
            return direction;
        }


        private void ClampPositionToScreen(int screenWidth, int screenHeight)
        {
            Position = new Vector2(
                Math.Clamp(Position.X, dimensions.X / 2, screenWidth - dimensions.X / 2),
                Math.Clamp(Position.Y, dimensions.Y / 2, screenHeight - dimensions.Y / 2)
            );
        }

        public void Shoot(List<Projectile> projectiles, GameTime gameTime, Vector2 direction, Texture2D laserNormalTexture)
        {
            if (!CanShoot(gameTime)) return;

            CreateProjectile(projectiles, direction, laserNormalTexture);
            lastShotTime = (float)gameTime.TotalGameTime.TotalSeconds;
            OnProjectileFired?.Invoke();
        }

        private bool CanShoot(GameTime gameTime)
        {
            return gameTime.TotalGameTime.TotalSeconds - lastShotTime > shootCooldown;
        }

        private void CreateProjectile(List<Projectile> projectiles, Vector2 direction, Texture2D laserNormalTexture)
        {
            Vector2 projectilePosition = new Vector2(
                Position.X - 4,
                Position.Y + PROJECTILE_OFFSET_Y
            );

            var projectile = new Projectile(
                projectilePosition,
                PROJECTILE_DIRECTION_UP,
                10f,
                IsPowerLaserActive ? DEFAULT_POWER_LASER_DAMAGE : DEFAULT_NORMAL_DAMAGE,
                laserNormalTexture
            );
            projectiles.Add(projectile);
        }

        public void TakeDamage(int damage)
        {
            int previousHealth = Health;
            Health = Math.Max(0, Health - damage);

            if (previousHealth != Health)
            {
                OnHealthChanged?.Invoke(Health);
            }
        }

        public void AddScore(int points)
        {
            Score += points;
            OnScoreChanged?.Invoke(Score);
        }

        public void ApplyEffect(Powerup powerUp)
        {
            switch (powerUp)
            {
                case PowerupHealth:
                    SetHealth(Health + 1);
                    break;
                case PowerupLaser:
                    SetIsPowerLaserActive(true);
                    break;
                default:
                    throw new ArgumentException("Unknown powerup type");
            }
        }

        public void SetHealth(int health)
        {
            if (Health != health)
            {
                Health = health;
                OnHealthChanged?.Invoke(Health);
            }
        }

        public void SetIsPowerLaserActive(bool isPowerLaserActive)
        {
            if (IsPowerLaserActive != isPowerLaserActive)
            {
                IsPowerLaserActive = isPowerLaserActive;
                OnPowerLaserStatusChanged?.Invoke();
            }
        }

        public void Update(InputManager inputManager, int screenWidth, int screenHeight, List<Projectile> projectiles, GameTime gameTime, Texture2D _laserNormalTexture)
        {
            if (!IsAlive) return;

            Move(inputManager, screenWidth, screenHeight);

            if (inputManager.IsKeyDown(Keys.Space))
            {
                Shoot(projectiles, gameTime, PROJECTILE_DIRECTION_UP, _laserNormalTexture);
            }
        }
    }
}