using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RetroShooter.Scenes
{
    internal class AboutScene : Scene
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private List<string> _aboutText;
        private Game _game;
        private KeyboardState _previousKeyboardState;

        public AboutScene(SpriteBatch spriteBatch, SpriteFont font, Game game)
        {
            _spriteBatch = spriteBatch;
            _font = font;
            _game = game;
            _aboutText = new List<string>
                {
                    "Retro Shooter",
                    " ",
                    "Version 1.0",
                    "Developed by: Mauricio Lechuga",
                    " ",
                    "Assets provided by Kenney",
                    "(www.kenney.nl)",
                    " ",
                    "Press Enter to return..."
                };
            _previousKeyboardState = Keyboard.GetState();
        }
        public override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Enter) && !_previousKeyboardState.IsKeyDown(Keys.Enter))
            {
                // Switches to the StartScene using the SceneManager
                SceneManager.ChangeScene(new StartScene(_spriteBatch, _font, _game));
            }

            _previousKeyboardState = keyboardState;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            for (int i = 0; i < _aboutText.Count; i++)
            {
                var text = _aboutText[i];
                var textSize = _font.MeasureString(text);
                var position = new Vector2(
                    (_game.GraphicsDevice.Viewport.Width - textSize.X) / 2,
                    100 + i * 50
                );
                
                spriteBatch.DrawString(_font, text, position, Color.White);
            }

            spriteBatch.End();
        }
    }
}
