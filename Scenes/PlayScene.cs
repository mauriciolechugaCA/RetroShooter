using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroShooter.Entities;
using RetroShooter.Entities.Enemies;
using RetroShooter.Entities.Projectiles;
using RetroShooter.Managers;
using System.Collections.Generic;

namespace RetroShooter.Scenes
{
    public class PlayScene : Scene
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _hudFont;
        private Player _player;
        private Texture2D _laserNormalTexture;
        private Texture2D _enemyBulletTexture;
        private List<Projectile> _projectiles;
        private InputManager _inputManager;
        private EnemyManager _enemyManager;
        private PowerupManager _powerupManager;
        private Texture2D _enemyTexture;
        private Texture2D _powerupHealthTexture;
        private Texture2D _powerupLaserTexture;

        public PlayScene(SpriteBatch spriteBatch, SpriteFont hudFont, Player player, Texture2D laserNormalTexture, Texture2D enemyBulletTexture, Texture2D enemyTexture, Texture2D powerupHealthTexture, Texture2D powerupLaserTexture)
        {
            _spriteBatch = spriteBatch;
            _hudFont = hudFont;
            _player = player;
            _laserNormalTexture = laserNormalTexture;
            _enemyBulletTexture = enemyBulletTexture;
            _projectiles = new List<Projectile>();
            _inputManager = new InputManager();
            _enemyTexture = enemyTexture;
            _enemyManager = new EnemyManager(_enemyTexture, enemyBulletTexture);
            _powerupHealthTexture = powerupHealthTexture;
            _powerupLaserTexture = powerupLaserTexture;
            _powerupManager = new PowerupManager(_powerupHealthTexture, _powerupLaserTexture);
        }

        public override void Update(GameTime gameTime)
        {
            _inputManager.Update();
            _player.Update(_inputManager, 768, 1024, _projectiles, gameTime, _laserNormalTexture);

            // Update projectiles
            foreach (var projectile in _projectiles)
            {
                projectile.Update();
            }

            // Remove out-of-bounds projectiles
            _projectiles.RemoveAll(p => p.IsOutOfBounds(768, 1024));

            // Update enemies
            _enemyManager.Update(gameTime, _player);

            if (gameTime.TotalGameTime.TotalSeconds % 5 == 0) // Spawn an enemy every 5 seconds
            {
                _enemyManager.SpawnEnemy(new Vector2(100, 100), "Basic");
            }

            _powerupManager.Update(gameTime, _player);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _spriteBatch.Begin();

            // Draw player
            _player.Draw(_spriteBatch);

            // Draw projectiles
            foreach (var projectile in _projectiles)
            {
                projectile.Draw(_spriteBatch);
            }

            // Draw enemies
            _enemyManager.Draw(_spriteBatch);

            // Draw power-ups
            _powerupManager.Draw(_spriteBatch);


            // Draw HUD
            string healthText = $"Health: {_player.Health}";
            string scoreText = $"Score: {_player.Score}";

            // Calculate positions to align text in the corners
            Vector2 healthTextPosition = new Vector2(30, 15);
            Vector2 scoreSize = _hudFont.MeasureString(scoreText);
            Vector2 scoreTextPosition = new Vector2(_spriteBatch.GraphicsDevice.Viewport.Width - scoreSize.X - 30, 15);

            // Draw texts
            _spriteBatch.DrawString(_hudFont, healthText, healthTextPosition, Color.White);
            _spriteBatch.DrawString(_hudFont, scoreText, scoreTextPosition, Color.White);

            _spriteBatch.End();
        }
    }
}