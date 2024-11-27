using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroShooter.Entities;
using RetroShooter.Entities.Enemies;
using RetroShooter.Entities.Projectiles;
using RetroShooter.Entities.Powerups;
using RetroShooter.Managers;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Xna.Framework.Media;

namespace RetroShooter.Scenes
{
    public class PlayScene : Scene
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _hudFont;
        private Player _player;
        private Game1 _game;
        private Texture2D _laserNormalTexture;
        private Texture2D _enemyBulletTexture;
        private List<Projectile> _projectiles;
        private InputManager _inputManager;
        private EnemyManager _enemyManager;
        private PowerupManager _powerupManager;
        private CollisionManager _collisionManager;
        private Texture2D _enemyTexture;
        private Texture2D enemyShooterTexture;
        private Texture2D _powerupHealthTexture;
        private Texture2D _powerupLaserTexture;
        private BackgroundManager _backgroundManager;
        private SoundManager _soundManager;
        private Texture2D _playerDeathTexture;
        private int _frameCount;
        private float _frameSpeed;

        public PlayScene(SpriteBatch spriteBatch, SpriteFont hudFont, Player player, Texture2D laserNormalTexture, Texture2D enemyBulletTexture, Texture2D enemyTexture, Texture2D enemyShooterTexture, Texture2D powerupHealthTexture, Texture2D powerupLaserTexture, Game1 game, SoundManager soundManager, Texture2D playerDeathTexture, int frameCount, float frameSpeed)
        {
            _spriteBatch = spriteBatch;
            _hudFont = hudFont;
            _player = player;
            _laserNormalTexture = laserNormalTexture;
            _enemyBulletTexture = enemyBulletTexture;
            _projectiles = new List<Projectile>();
            _inputManager = new InputManager();
            _enemyTexture = enemyTexture;
            enemyShooterTexture = game.enemyShooterTexture;
            _enemyManager = new EnemyManager(_enemyTexture, enemyBulletTexture, enemyShooterTexture, _projectiles, soundManager);
            _powerupHealthTexture = powerupHealthTexture;
            _powerupLaserTexture = powerupLaserTexture;
            _powerupManager = new PowerupManager(_powerupHealthTexture, _powerupLaserTexture);
            _collisionManager = new CollisionManager(_player, _projectiles, _enemyManager.Enemies, _powerupManager.Powerups);
            _game = game;
            _backgroundManager = new BackgroundManager(_game.backgroundTexture, _game.floatingMeteorsTextures, 1000, 1500, _spriteBatch);
            _soundManager = soundManager;
            _playerDeathTexture = playerDeathTexture;
            _frameCount = frameCount;
            _frameSpeed = frameSpeed;

            _soundManager.PlaySong("gameplay_music");
        }

        public override void Update(GameTime gameTime)
        {
            _inputManager.Update();

            if (MediaPlayer.State != MediaState.Playing)
            {
                _soundManager.PlaySong("gameplay_music");
            }


            if (_inputManager.IsKeyPressed(Keys.Escape))
            {
                SceneManager.ChangeScene(new PauseScene(_spriteBatch, _game._menuTitle, _game._menuItems, _game, _soundManager));
                return;
            }

            _player.Update(_inputManager, 768, 1024, _projectiles, gameTime, _laserNormalTexture);

            // Update background and floating meteors
            _backgroundManager.Update(gameTime);

            // Update projectiles
            foreach (var projectile in _projectiles)
            {
                projectile.Update();
            }

            // Remove out-of-bounds projectiles
            _projectiles.RemoveAll(p => p.IsOutOfBounds(768, 1024));

            // Update enemies
            _enemyManager.Update(gameTime, _player);

            _powerupManager.Update(gameTime, _player);

            _collisionManager.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _spriteBatch.Begin();

            // Draw background and floating meteors
            _backgroundManager.Draw(_spriteBatch);

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
            var shadowOffset = new Vector2(4, 4);

            // Calculate positions to align text in the corners
            Vector2 healthTextPosition = new Vector2(30, 15);
            Vector2 scoreSize = _hudFont.MeasureString(scoreText);
            Vector2 scoreTextPosition = new Vector2(_spriteBatch.GraphicsDevice.Viewport.Width - scoreSize.X - 30, 15);

            // Draw texts
            _spriteBatch.DrawString(_hudFont, healthText, healthTextPosition + shadowOffset, Color.Black);
            _spriteBatch.DrawString(_hudFont, healthText, healthTextPosition, Color.LightGray);
            _spriteBatch.DrawString(_hudFont, scoreText, scoreTextPosition + shadowOffset, Color.Black);
            _spriteBatch.DrawString(_hudFont, scoreText, scoreTextPosition, Color.LightGray);

            _spriteBatch.End();
        }
    }
}