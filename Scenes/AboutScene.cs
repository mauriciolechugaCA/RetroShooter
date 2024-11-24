using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroShooter.Managers;

namespace RetroShooter.Scenes
{
    internal class AboutScene : Scene
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _creditsTitleFont;
        private SpriteFont _menuItemsfont;
        private SpriteFont _menuTitlefont;
        private List<string> _aboutText;
        private Game1 _game;
        private InputManager _inputManager;
        private BackgroundManager _backgroundManager;

        public AboutScene(SpriteBatch spriteBatch, SpriteFont creditsTitleFont, SpriteFont menuItemsfont, SpriteFont menuTitlefont, Game1 game)
        {
            _spriteBatch = spriteBatch;
            _creditsTitleFont = creditsTitleFont;
            _menuItemsfont = menuItemsfont;
            _menuTitlefont = menuTitlefont;
            _game = game;
            _aboutText = new List<string>
            {
                "Retro Shooter v1.0",
                " ",
                "Developed by",
                "Mauricio Lechuga",
                " ",
                "Assets provided by",
                "www.kenney.nl",
                " ",
                "Enter to return..."
            };
            _inputManager = new InputManager();
            _backgroundManager = new BackgroundManager(_game.backgroundTexture, _game.floatingMeteorsTextures, 1000, 1500, _spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            _inputManager.Update();

            // Update background and floating meteors
            _backgroundManager.Update(gameTime);

            if (_inputManager.IsKeyPressed(Keys.Enter) || _inputManager.IsKeyPressed(Keys.Escape))
            {
                // Switches to the StartScene using the SceneManager
                SceneManager.ChangeScene(new StartScene(_spriteBatch, _creditsTitleFont, _menuItemsfont, _menuTitlefont, _game, _game.PlayerManager.Player));
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            // Draw background and floating meteors
            _backgroundManager.Draw(spriteBatch);

            var shadowOffset = new Vector2(4, 4);

            for (int i = 0; i < _aboutText.Count; i++)
            {
                var text = _aboutText[i];
                var textSize = _creditsTitleFont.MeasureString(text);
                var position = new Vector2(
                    (_game.GraphicsDevice.Viewport.Width - textSize.X) / 2,
                    225 + i * 50
                );
                spriteBatch.DrawString(_creditsTitleFont, text, position + shadowOffset, Color.Black);
                spriteBatch.DrawString(_creditsTitleFont, text, position, Color.LightGray);
            }

            spriteBatch.End();
        }
    }
}

