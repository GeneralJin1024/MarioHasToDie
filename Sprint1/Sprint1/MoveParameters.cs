using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint1
{
    public class MoveParameters
    {
        public bool IsLeft { get; set; }
        public bool IsHidden { get; set; }
        public Vector2 Velocity { get { return _velocity; } }
        public Vector2 Position { get { return _position; } }
        //new parameter : TimeOfFrame
        public float TimeOfFrame { get; set; }

        private Vector2 _velocity;
        private Vector2 _position;
        public MoveParameters()
        {
            _velocity = new Vector2(0, 0);
            _position = new Vector2(0, 0);
            IsLeft = false;
            IsHidden = false;
            TimeOfFrame = 0;
        }

        public void SetVelocity(float x, float y)
        {
            if (IsLeft)
                _velocity.X = -1 * x;
            else
                _velocity.X = x;
            _velocity.Y = y;
        }

        public void SetPosition(float x, float y)
        {
            _position.X = x;
            _position.Y = y;
        }

        public void UpdatePositionAndVelocity(float acceleration, float rate)
        {
            _position.X += (_velocity.X * rate);
            _position.Y += (_velocity.Y * rate);
            _velocity.Y += acceleration * rate;
        }
    }
}
