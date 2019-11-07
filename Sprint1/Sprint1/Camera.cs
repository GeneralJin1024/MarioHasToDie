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

    //特别注释，当前Camera为老师给的教程的作者后来的一篇文章，新的Camera具备放大功能，如果想要使用原来的camera，
    //请把当前Camera类名改成Camera1并把Camera1的类名改成Camera，此外取消220行注释并注释221行即可还原
    public class Camera1
    {
        public Camera1(Viewport viewport)
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

    public class Camera
    {
        public Camera(Viewport viewport)
        {
            _viewport = viewport;
            _origin = new Vector2(_viewport.Width / 2.0f, _viewport.Height / 2.0f);
        }

        /// <summary>
        /// Gets or sets the position of the camera.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                ValidatePosition();
            }
        }

        /// <summary>
        /// Gets or sets the zoom of the camera.
        /// </summary>
        public float Zoom
        {
            get
            {
                return _zoom;
            }
            set
            {
                _zoom = MathHelper.Max(value, MinZoom);
                ValidateZoom();
                ValidatePosition();
            }
        }

        /// <summary>
        /// Sets a rectangle that describes which region of the world the camera should
        /// be able to see. Setting it to null removes the limit.
        /// </summary>
        public Rectangle? Limits
        {
            get { return Rectangle.Empty; }
            set
            {
                _limits = value;
                ValidateZoom();
                ValidatePosition();
            }
        }

        /// <summary>
        /// Calculates a view matrix for this camera.
        /// </summary>
        public Matrix ViewMatrix
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-_position, 0f)) *
                       Matrix.CreateTranslation(new Vector3(-_origin, 0f)) *
                       Matrix.CreateScale(_zoom, _zoom, 1f) *
                       Matrix.CreateTranslation(new Vector3(_origin, 0f));
            }
        }

        /// <summary>
        /// When using limiting, makes sure the camera position is valid.
        /// </summary>
        private void ValidatePosition()
        {
            if (_limits.HasValue)
            {
                Vector2 cameraWorldMin = Vector2.Transform(Vector2.Zero, Matrix.Invert(ViewMatrix));
                Vector2 cameraSize = new Vector2(_viewport.Width, _viewport.Height) / _zoom;
                Vector2 limitWorldMin = new Vector2(_limits.Value.Left, _limits.Value.Top);
                Vector2 limitWorldMax = new Vector2(_limits.Value.Right, _limits.Value.Bottom);
                Vector2 positionOffset = _position - cameraWorldMin;
                _position = Vector2.Clamp(cameraWorldMin, limitWorldMin, limitWorldMax - cameraSize) + positionOffset;
            }
        }

        /// <summary>
        /// When using limiting, makes sure the camera zoom is valid.
        /// </summary>
        private void ValidateZoom()
        {
            if (_limits.HasValue)
            {
                float minZoomX = (float)_viewport.Width / _limits.Value.Width;
                float minZoomY = (float)_viewport.Height / _limits.Value.Height;
                _zoom = MathHelper.Max(_zoom, MathHelper.Max(minZoomX, minZoomY));
            }
        }
        public void LookAt(Vector2 position)
        {
            Position = position - new Vector2(_viewport.Width / 2.0f, _viewport.Height / 2.0f);
        }

        private const float MinZoom = 0.01f;

        private readonly Viewport _viewport;
        private readonly Vector2 _origin;

        private Vector2 _position;
        private float _zoom = 1.5f;
        private Rectangle? _limits;
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
            //spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _camera.GetViewMatrix(Parallax));
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _camera.ViewMatrix);
            foreach (ICharacter sprite in Sprites)
            {
                sprite.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        private readonly Camera _camera;
    }
}
