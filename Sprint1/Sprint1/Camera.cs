using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1
{
    public class Camera
    {
        public Camera(Viewport viewport)
        {
            _viewport = viewport;
            Origin = new Vector2(viewport.Width / 2.0f, viewport.Height / 2.0f);
            Zoom = 1.0f;
        }

        public Rectangle? Limits
        {
            get { return _limits; }
            set
            {
                if (value != null)
                {
                    // Assign limit but make sure it's always bigger than the viewport
                    _limits = new Rectangle
                    {
                        X = value.Value.X,
                        Y = value.Value.Y,
                        Width = System.Math.Max(_viewport.Width, value.Value.Width),
                        Height = System.Math.Max(_viewport.Height, value.Value.Height)
                    };

                    // Validate camera position with new limit
                    Position = _position;
                }
                else
                {
                    _limits = null;
                }
            }
        }

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;

                // If there's a limit set and the camera is not transformed clamp position to limits
                if (Limits != null && Zoom == 1.0f && Rotation == 0.0f)
                {
                    _position.X = MathHelper.Clamp(_position.X, Limits.Value.X, Limits.Value.X + Limits.Value.Width - _viewport.Width);
                    _position.Y = MathHelper.Clamp(_position.Y, Limits.Value.Y, Limits.Value.Y + Limits.Value.Height - _viewport.Height);
                }
            }
        }

        private readonly Viewport _viewport;
        private Rectangle? _limits;
        private Vector2 _position;
        //public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public float Zoom { get; set; }
        public float Rotation { get; set; }

        public Matrix GetViewMatrix(Vector2 parallax)
        {
            // To add parallax, simply multiply it by the position
            return Matrix.CreateTranslation(new Vector3(-Position * parallax, 0.0f)) *
                // The next line has a catch. See note below.
                Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(Zoom, Zoom, 1) *
                Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }

        public void LookAt(Vector2 position)
        {
            Position = position - new Vector2(_viewport.Width / 2.0f, _viewport.Height / 2.0f);
        }
    }


    public class Layer
    {
        public Layer(Camera camera)
        {
            _camera = camera;
            Parallax = Vector2.One;
            Sprites = new ArrayList();
        }

        public Vector2 Parallax { get; set; }
        public ArrayList Sprites { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (spriteBatch is null)
                throw new ArgumentNullException(nameof(spriteBatch));
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _camera.GetViewMatrix(Parallax));
            foreach (ICharacter sprite in Sprites)
            {
                sprite.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        private readonly Camera _camera;
    }
}
