using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pong
{
    class MainItem //haris wrote this
    {
        protected Rectangle rectangle;
        protected Texture2D t2d; //rectangle, texture2d, adn color variables for when you construct this object
        protected Color clr;
        
        public Rectangle getRectangle()
        { //takes rectangle input
            return rectangle;
        }
        public void setRectangle(Rectangle aRectangle)
        { //uses it for object
            rectangle = aRectangle;
        }

        public Texture2D gett2d()
        { //takes texture2d input
            return t2d;
        }
        public void sett2d(Texture2D at2d)
        {
            t2d = at2d;
        } //uses it for object

        public Color getclr()
        { //takes color input
            return clr;
        }
        public void setclr(Color aclr)
        {
            clr = aclr;
        } //uses it for object

        public MainItem(Texture2D t2d, Rectangle rectangle, Color clr)
        {
            this.setRectangle(rectangle);
            this.sett2d(t2d); //use inputed values to construct object
            this.setclr(clr);
        }

        public void movementy (int dy) //takes int input and uses it as speed of vertical movement
        {
            this.rectangle.Y += dy; //give object ablility to move vertically
        }

        public void Draw(SpriteBatch t2d)
        { //draws constructed object
            t2d.Draw(this.t2d, this.rectangle, this.clr);
        }


    }
}
