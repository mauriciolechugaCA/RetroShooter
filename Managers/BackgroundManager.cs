using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroShooter.Managers
{
    public class BackgroundManager
    {
        private Texture2D _backgroundTexture;
        private Texture2D[] _floatingMeteorTextures;
        private List<FloatingMeteor> floatingMeteors;
        private Random _random;
        private int _screenWidth;
        private int _screenHeight;
        private SpriteBatch _spriteBatch;

        public BackgroundManager(Texture2D backgroundTexture, Texture2D[] floatingMeteorTextures, int screenWidth, int screenHeight, SpriteBatch spriteBatch)
        {
            _backgroundTexture = backgroundTexture;
            _floatingMeteorTextures = floatingMeteorTextures;
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _spriteBatch = spriteBatch;
            floatingMeteors = new List<FloatingMeteor>();
            _random = new Random();

            for (int i = 0; i < 21; i++) // Modify amount of floating meteors
            {
                AddFloatingMeteor();
            }
        }

        private void AddFloatingMeteor()
        {
            var texture = _floatingMeteorTextures[_random.Next(0, _floatingMeteorTextures.Length)];
            var position = new Vector2(_random.Next(_screenWidth), _random.Next(_screenHeight));
            var speed = new Vector2((float)(_random.NextDouble() - 1.2), (float)(_random.NextDouble() - 0.5)); 
            var scale = ((float)_random.NextDouble() * 0.5f) + 0.9f;
            var opacity = ((float)_random.NextDouble() * 1f) + 0.7f;
            floatingMeteors.Add(new FloatingMeteor(texture, position, speed, scale, opacity));
        }

        public void Update(GameTime gameTime)
        {
            foreach (var meteor in floatingMeteors)
            {
                meteor.Update(_screenWidth, _screenHeight);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw tiled background
            for (int x = 0; x < _screenWidth; x += _backgroundTexture.Width)
            {
                for (int y = 0; y < _screenHeight; y += _backgroundTexture.Height)
                {
                    spriteBatch.Draw(_backgroundTexture, new Vector2(x, y), Color.White);
                }
            }

            // Draw floating meteors
            foreach (var meteor in floatingMeteors)
            {
                meteor.Draw(spriteBatch);
            }
        }

        private class FloatingMeteor
        {
            private Texture2D _texture;
            private Vector2 _position;
            private Vector2 _speed;
            private float _scale;
            private float _opacity;

            public FloatingMeteor(Texture2D texture, Vector2 position, Vector2 speed, float scale, float opacity)
            {
                _texture = texture;
                _position = position;
                _speed = speed;
                _scale = scale;
                _opacity = opacity;
            }

            public void Update(int screenWidth, int screenHeight)
            {
                _position += _speed;

                if (_position.X < 0) _position.X = screenWidth;
                if (_position.X > screenWidth) _position.X = 0;
                if (_position.Y < 0) _position.Y = screenHeight;
                if (_position.Y > screenHeight) _position.Y = 0;
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.Draw(_texture, _position, null, Color.White * _opacity, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
            }
        }
    }
}
