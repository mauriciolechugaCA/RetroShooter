using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroShooter.Entities.Powerups;
using RetroShooter.Entities.Projectiles;
using RetroShooter.Managers;
using System;
using System.Collections.Generic;
using RetroShooter.Entities;
using RetroShooter.Scenes;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RetroShooter.Entities
{
    public class Player
    {
        // Constants
        private const float DEFAULT_SPEED = 8f;
        private const float DEFAULT_SHOOT_COOLDOWN = 0.3f;
        private const float PROJECTILE_OFFSET_Y = -65f;
        private const int DEFAULT_POWER_LASER_DAMAGE = 20;
        private const int DEFAULT_NORMAL_DAMAGE = 10;
        private const int MAX_HEALTH = 100;
        private const float LASER_EFFECT_DURATION = 10f;
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
        public event Action<int> OnHealthChanged; // ## AI assisted ##
        public event Action<int> OnScoreChanged;
        public event Action OnPowerLaserStatusChanged;
        public event Action OnProjectileFired;

        // Private fields
        private float speed;
        private float shootCooldown;
        private float lastShotTime;
        private float laserEffectStartTime;
        private Vector2 dimensions;
        private Game1 _game;
        private SoundManager _soundManager;
        public SoundManager SoundManager => _soundManager;
        private AnimationManager _animationManager;
        private Vector2 _lastPosition;

        public Player(Vector2 startPosition, int health, Texture2D playerTexture, float scale, Game1 game, Texture2D playerDeathTexture, int frameCount, float frameSpeed)
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
            _game = game;
            _soundManager = game.SoundManager;
            _animationManager = new AnimationManager();
            _animationManager.AddAnimation("playerDeath", new Animation(playerDeathTexture, frameCount, 0.0333f, false));
            _lastPosition = startPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsAlive)
            {
                Color tint = IsPowerLaserActive ? Color.IndianRed : Color.White;

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
            else
            {
                float deathAnimationScale = 0.3f;
                _animationManager.Draw(spriteBatch, _lastPosition, deathAnimationScale);
            }
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
                _lastPosition = Position;
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
            // ## AI assisted ##
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

            _soundManager.PlaySoundEffect("playerLaser");
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
                laserNormalTexture,
                ProjectileOwner.Player
            );
            projectiles.Add(projectile);
        }

        public void TakeDamage(int damage)
        {
            int previousHealth = Health;
            Health = Math.Max(0, Health - damage);
            _soundManager.PlaySoundEffect("player_hit");

            if (previousHealth != Health)
            {
                OnHealthChanged?.Invoke(Health);
            }

            if (Health <= 0)
            {
                IsAlive = false;
                _soundManager.PlaySoundEffect("player_death");
                _soundManager.StopSong();

                // Wait for 5 seconds before changing the scene
                Task.Delay(3000).ContinueWith(_ =>
                {
                    SceneManager.ChangeScene(new GameOverScene(_game._spriteBatch, _game._menuTitle, _game._menuItems, _game, _soundManager));
                });
            }
        }

        public void AddScore(int points)
        {
            Score += points;
            OnScoreChanged?.Invoke(Score);
        }

        private float lastScoreUpdateTime;

        public void UpdateTimeBasedScore(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.TotalGameTime.TotalSeconds;

            if (elapsedTime - lastScoreUpdateTime >= 5)
            {
                int pointsToAdd = 0;
                if (elapsedTime < 60)
                {
                    pointsToAdd = 10;
                }
                else if (elapsedTime < 120)
                {
                    pointsToAdd = 20;
                }
                else if (elapsedTime < 180)
                {
                    pointsToAdd = 30;
                }
                else
                {
                    pointsToAdd = 40;
                }

                AddScore(pointsToAdd);
                lastScoreUpdateTime = elapsedTime;
            }
        }
        public void ApplyEffect(Powerup powerUp, GameTime gameTime)
        {
            switch (powerUp)
            {
                case PowerupHealth:
                    SetHealth(Health + 30);
                    break;
                case PowerupLaser:
                    SetIsPowerLaserActive(true);
                    laserEffectStartTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    break;
                default:
                    throw new ArgumentException("Unknown powerup type");
            }
        }

        public void SetHealth(int health)
        {
            if (Health != health)
            {
                Health = Math.Min(health, MAX_HEALTH);
                OnHealthChanged?.Invoke(Health);
            }
        }

        public void SetIsPowerLaserActive(bool isPowerLaserActive)
        {
            if (IsPowerLaserActive != isPowerLaserActive)
            {
                IsPowerLaserActive = isPowerLaserActive;
                OnPowerLaserStatusChanged?.Invoke();
                //Debug.WriteLine($"Power laser is now {(IsPowerLaserActive ? "active" : "inactive")}");
            }
        }

        public void SetLaserEffectStartTime(float startTime)
        {
            laserEffectStartTime = startTime;
        }

        public void Update(InputManager inputManager, int screenWidth, int screenHeight, List<Projectile> projectiles, GameTime gameTime, Texture2D _laserNormalTexture)
        {
            if (IsAlive)
            {
                Move(inputManager, screenWidth, screenHeight);

                if (inputManager.IsKeyDown(Keys.Space))
                {
                    Shoot(projectiles, gameTime, PROJECTILE_DIRECTION_UP, _laserNormalTexture);
                }

                if (IsPowerLaserActive && gameTime.TotalGameTime.TotalSeconds - laserEffectStartTime > LASER_EFFECT_DURATION)
                {
                    SetIsPowerLaserActive(false);
                }
            }
            else
            {
                _animationManager.Play("playerDeath");
                _animationManager.Update(gameTime);
            }
        }
    }
}