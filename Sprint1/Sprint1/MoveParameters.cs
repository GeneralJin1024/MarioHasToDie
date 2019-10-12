﻿using System;
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
        public Vector2 Velocity { get { return _velocity; } }
        public Vector2 Position { get { return _position; } }
        public float TimeOfFrame { get; set; } // replace game time. change one frame when TimeOfFrame = 1.

        private Vector2 _velocity;
        private Vector2 _position;
        public MoveParameters()
        {
            //Set position and velocity to initial value
            _velocity = new Vector2(0, 0);
            _position = new Vector2(0, 0);
            //when the object is create, display and face to right.
            IsLeft = false;
            IsHidden = false;
            TimeOfFrame = 0; //start time is 0
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
            _position.X += (_velocity.X * rate);
            _position.Y += (_velocity.Y * rate);
            _velocity.Y += acceleration * rate;
        }
    }
}
