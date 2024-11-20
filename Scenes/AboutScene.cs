using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RetroShooter.Scenes
{
    internal class AboutScene : Scene
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _creditsTitleFont;
        private SpriteFont _menuItemsfont;
        private SpriteFont _menuTitlefont;
        private List<string> _aboutText;
        private Game1 _game; // Change type to Game1 to access player
        private KeyboardState _previousKeyboardState;

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
            _previousKeyboardState = Keyboard.GetState();
        }

        public override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Enter) && !_previousKeyboardState.IsKeyDown(Keys.Enter))
            {
                // Switches to the StartScene using the SceneManager
                SceneManager.ChangeScene(new StartScene(_spriteBatch, _creditsTitleFont, _menuItemsfont, _menuTitlefont, _game, _game.player)); // Pass player
            }

            _previousKeyboardState = keyboardState;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            for (int i = 0; i < _aboutText.Count; i++)
            {
                var text = _aboutText[i];
                var textSize = _creditsTitleFont.MeasureString(text);
                var position = new Vector2(
                    (_game.GraphicsDevice.Viewport.Width - textSize.X) / 2,
                    225 + i * 50
                );

                spriteBatch.DrawString(_creditsTitleFont, text, position, Color.White);
            }

            spriteBatch.End();
        }
    }
}

