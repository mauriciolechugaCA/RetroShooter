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
        private List<Projectile> _projectiles;
        private List<Enemy> _enemies;
        private Texture2D _laserNormalTexture;
        private Texture2D _enemyBulletTexture;
        private InputManager _inputManager;

        public PlayScene(SpriteBatch spriteBatch, SpriteFont hudFont, Player player, Texture2D laserNormalTexture, Texture2D enemyBulletTexture)
        {
            _spriteBatch = spriteBatch;
            _hudFont = hudFont;
            _player = player;
            _projectiles = new List<Projectile>();
            _enemies = new List<Enemy>();
            _laserNormalTexture = laserNormalTexture;
            _enemyBulletTexture = enemyBulletTexture;
            _inputManager = new InputManager();

            _enemies.Add(new EnemyBasic(new Vector2(100, 100), _enemyBulletTexture));
            _enemies.Add(new EnemyShooter(new Vector2(100, 100), _enemyBulletTexture));
        }

        public override void Update(GameTime gameTime)
        {
            _inputManager.Update();

            _player.Update(
                _inputManager,
                768,  // screen width
                1024, // screen height
                _projectiles,
                gameTime,
                _laserNormalTexture
            );

            // Update projectiles
            for (int i = _projectiles.Count - 1; i >= 0; i--)
            {
                _projectiles[i].Update();

                // Remove projectiles that are out of bounds
                if (_projectiles[i].IsOutOfBounds(768, 1024))
                {
                    _projectiles.RemoveAt(i);
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _spriteBatch.Begin();

            _player.Draw(_spriteBatch);

            // Draw projectiles
            foreach (var projectile in _projectiles)
            {
                projectile.Draw(_spriteBatch);
            }

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