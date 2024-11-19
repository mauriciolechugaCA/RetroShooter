using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroShooter.Entities;
using RetroShooter.Entities.Projectiles;
using System.Collections.Generic;

namespace RetroShooter.Scenes
{
    public class PlayScene : Scene
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _hudFont;
        private Player _player;
        private List<Projectile> _projectiles;

        public PlayScene(SpriteBatch spriteBatch, SpriteFont hudFont, Player player)
        {
            _spriteBatch = spriteBatch;
            _hudFont = hudFont;
            _player = player;
            _projectiles = new List<Projectile>();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            // Update player
            _player.Update(
                keyboardState,
                800,  // screen width
                1200, // screen height
                _projectiles,
                gameTime
            );

            // Update projectiles
            for (int i = _projectiles.Count - 1; i >= 0; i--)
            {
                _projectiles[i].Update();

                // Remove projectiles that are out of bounds
                if (_projectiles[i].IsOutOfBounds(800, 1200))
                {
                    _projectiles.RemoveAt(i);
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _spriteBatch.Begin();

            // Draw player
            _player.Draw(_spriteBatch);

            // Draw projectiles
            foreach (var projectile in _projectiles)
            {
                // You'll need to implement projectile drawing
                // This depends on how you want to represent projectiles visually
            }

            // Draw HUD
            _spriteBatch.DrawString(_hudFont, $"Health: {_player.Health}", new Vector2(10, 10), Color.White);
            _spriteBatch.DrawString(_hudFont, $"Score: {_player.Score}", new Vector2(10, 40), Color.White);

            _spriteBatch.End();
        }
    }
}