using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroShooter.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Xna.Framework.Input;

namespace RetroShooter.Scenes
{
    internal class HelpScene : Scene
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private SpriteFont _menuTitleFont;
        private SpriteFont _menuItemsFont;
        private Game1 _game;
        private BackgroundManager _backgroundManager;
        private InputManager _inputManager;
        private List<string> _instructions;
        private SoundManager _soundManager;

        public HelpScene(SpriteBatch spriteBatch, SpriteFont font, SpriteFont menuTitleFont, SpriteFont menuItemsFont, Game1 game, SoundManager soundManager)
        {
            _spriteBatch = spriteBatch;
            _font = font;
            _menuTitleFont = menuTitleFont;
            _menuItemsFont = menuItemsFont;
            _game = game;
            _backgroundManager = new BackgroundManager(_game.backgroundTexture, _game.floatingMeteorsTextures, 1000, 1500, _spriteBatch);
            _inputManager = new InputManager();

            _instructions = new List<string>
            {
                "WASD or Arrows to move",
                "Space to shoot",
                "ESC to pause",
                " ",
                "Collect powerups and",
                "enhance your damage",
                " ",
                "Survive and destroy",
                "enemies to score points",
                " ",
                "Enter to return..."
            };

            _soundManager = soundManager;
        }

        public override void Update(GameTime gameTime)
        {
            _inputManager.Update();

            // Update background and floating meteors
            _backgroundManager.Update(gameTime);

            if (_inputManager.IsKeyPressed(Keys.Enter) || _inputManager.IsKeyPressed(Keys.Escape))
            {
                SceneManager.ChangeScene(new StartScene(_spriteBatch, _font, _menuItemsFont, _menuTitleFont, _game, _game.PlayerManager.Player, _soundManager));
                _soundManager.PlaySoundEffect("confirmation_001"); // Sound effect for menu selection confirmation
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _spriteBatch.Begin();

            // Draw background and floating meteors
            _backgroundManager.Draw(spriteBatch);

            string helpText = "Instructions";
            var titleSize = _menuTitleFont.MeasureString(helpText);
            var titlePosition = new Vector2((_spriteBatch.GraphicsDevice.Viewport.Width - titleSize.X) / 2, 175);

            var shadowOffset = new Vector2(4, 4);

            _spriteBatch.DrawString(_menuTitleFont, helpText, titlePosition + shadowOffset, Color.Black);
            _spriteBatch.DrawString(_menuTitleFont, helpText, titlePosition, Color.LightSeaGreen);


            // Draw instructions
            for (int i = 0; i < _instructions.Count; i++)
            {
                var text = _instructions[i];
                var textSize = _font.MeasureString(text);
                var textPosition = new Vector2((_spriteBatch.GraphicsDevice.Viewport.Width - textSize.X) / 2, 300 + i * 50);

                _spriteBatch.DrawString(_font, text, textPosition + shadowOffset, Color.Black);
                _spriteBatch.DrawString(_font, text, textPosition, Color.LightGray);
            }

            _spriteBatch.End();
        }
    }
}
