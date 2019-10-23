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
        public bool IsLeft { get; set; } //whether the object is facing left.
        public bool IsHidden { get; set; } // whether display the object on the screen.

        //当该值为true，则物体每次更新会有一个向下的加速度。反之则没有
        //注意：初始值取决于生成该类时的参量，但后期也可以修改
        public bool HasGravity { get; set; } //whether the object has grativity(negative y acceleration)
        public bool InScreen { get; set; }
        public Vector2 Velocity { get { return _velocity; } }
        public Vector2 Position { get { return _position; } }
        public float TimeOfFrame { get; set; } // replace game time. change one frame when TimeOfFrame = 1.

        private Vector2 _velocity;
        private Vector2 _position;
        private float grativity;
        public MoveParameters(bool hasGravity)
        {
            //Set position and velocity to initial value
            _velocity = new Vector2(0, 0);
            _position = new Vector2(0, 0);
            //when the object is create, display and face to right.
            IsLeft = false;
            IsHidden = false;
            TimeOfFrame = 0; //start time is 0
            HasGravity = hasGravity;
            InScreen = true;
        }

        public void SetVelocity(float x, float y)
        {
            //x and y are all absolute value of velocity. The direction of velocity depends on where the object is facing.
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
            grativity = HasGravity ? 2 : 0; //由于后期修改，需要随时注意HasGrativity的变化
            _position.X += (_velocity.X * rate);
            _position.Y += (_velocity.Y * rate);
            _velocity.Y += grativity * rate;
        }
    }
}
