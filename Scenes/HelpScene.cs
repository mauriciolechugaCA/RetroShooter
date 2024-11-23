using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroShooter.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RetroShooter.Scenes
{
    internal class HelpScene : Scene
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private Game1 _game;
        private BackgroundManager _backgroundManager;

        public HelpScene(SpriteBatch spriteBatch, SpriteFont font, Game1 game)
        {
            _spriteBatch = spriteBatch;
            _font = font;
            _game = game;
            _backgroundManager = new BackgroundManager(_game.backgroundTexture, _game.floatingMeteorsTextures, 1000, 1500, _spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            // Update background and floating meteors
            _backgroundManager.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _spriteBatch.Begin();

            // Draw background and floating meteors
            _backgroundManager.Draw(spriteBatch);

            string helpText = "Help info";
            var textSize = _font.MeasureString(helpText);
            var textPosition = new Vector2((_spriteBatch.GraphicsDevice.Viewport.Width - textSize.X) / 2, 200);
            _spriteBatch.DrawString(_font, helpText, textPosition, Color.White);

            _spriteBatch.End();
        }
    }
}
