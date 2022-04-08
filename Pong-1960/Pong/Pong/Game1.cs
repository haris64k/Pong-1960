//Haris Khan and Kimani Ellison
//ICS 4U1
//Pong 1970

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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch; //haris wrote this

        MainItem rightPaddleTop, rightPaddleBottom, leftPaddleTop, leftPaddleBottom; //this constructs 4 main item objects, 2 paddles with 2 hitboxes each
        ball pongBall; //constructs a ball object, for the projectile
        Texture2D background;//texture for background image
        Rectangle backgroundRect;//rectanbgle for background image
        SpriteFont font; //call font that words will use
        int score1 = 0;//scores for player 1 and 2
        int score2 = 0;
        bool gameOver = false;//bool for controlling whether game is over or not
        bool possessionx = true; //bools for horizontal and vertical movement
        bool possessiony;
        int timerint = 0; //int for time related functions
        int speed = 4;//int for speed increase over time

        Random rnd = new Random();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            backgroundRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            //makes rectangle same size as form
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice); //haris wrote this
            //gives each paddle piece the same image, and sets size and location of paddle
            rightPaddleTop = new MainItem(Content.Load<Texture2D>("paddle"), new Rectangle(GraphicsDevice.Viewport.Width-50, GraphicsDevice.Viewport.Height / 2 - 40, 25, 40), Color.White);
            rightPaddleBottom = new MainItem(Content.Load<Texture2D>("paddle"), new Rectangle(GraphicsDevice.Viewport.Width - 50, GraphicsDevice.Viewport.Height / 2, 25, 40), Color.White);
            leftPaddleTop = new MainItem(Content.Load<Texture2D>("paddle"), new Rectangle(25, GraphicsDevice.Viewport.Height / 2 - 40, 25, 40), Color.White);
            leftPaddleBottom = new MainItem(Content.Load<Texture2D>("paddle"), new Rectangle(25, GraphicsDevice.Viewport.Height / 2, 25, 40), Color.White);
            pongBall = new ball(Content.Load<Texture2D>("pongBall"), new Rectangle(GraphicsDevice.Viewport.Width / 2-15, GraphicsDevice.Viewport.Height / 2, 15, 15), Color.White);
            //^puts ball image in ball object
            background = Content.Load<Texture2D>("background"); //sets background image
            font = Content.Load<SpriteFont>("SpriteFont1"); //put correct font in font variable
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            GamePadState pad1 = GamePad.GetState(PlayerIndex.One); //get input from controller
            KeyboardState keyboard = Keyboard.GetState(); //get input from keyboard

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboard.IsKeyDown(Keys.Escape))
                this.Exit(); //if you press back or escape, game ends

            //movement code
            if (timerint <= 600) //if time is less than 10 seconds
            {
                if (possessionx == true) //if player 1(left) is in possession, this is true, and ball moves towards the right
                    pongBall.movementx(speed); 
                else
                    pongBall.movementx(-speed); //if player 2(right) is in possession, this is false, and ball moves towards the left

                if (possessiony == true) //if this is true, ball moves down
                    pongBall.movementy(speed);
                else
                    pongBall.movementy(-speed); //if this is false, ball moves up
            }
            else //if time is greater than 10 seconds (it gets set to 0 whenever somebody scores)
            {
                if (timerint % 200 == 0) //every 200 ticks after 10 seconds,
                {
                    speed++; //speed increases by 1
                }

                if (possessionx == true)
                    pongBall.movementx(speed);
                else //same as code above, but with faster speeds
                    pongBall.movementx(-speed);

                if (possessiony == true)
                    pongBall.movementy(speed);
                else
                    pongBall.movementy(-speed);
            }

            if(gameOver == false) //if game is not over
            {
                //controls code
                if (pad1.DPad.Up == ButtonState.Pressed || keyboard.IsKeyDown(Keys.Up))//if you press up on controller or keyboard
                {
                    rightPaddleTop.movementy(-2);
                    rightPaddleBottom.movementy(-2);  //move right paddle (both hitboxes) up
                }

                if (pad1.DPad.Down == ButtonState.Pressed || keyboard.IsKeyDown(Keys.Down))//if you press dowb on controller or keyboard
                {
                    rightPaddleTop.movementy(2); //move right paddle (both hitboxes) down
                    rightPaddleBottom.movementy(2);
                }

                if (pad1.Buttons.Y == ButtonState.Pressed || keyboard.IsKeyDown(Keys.W))//if you press y on controller or w on keyboard
                {
                    leftPaddleTop.movementy(-2);  //move left paddle (both hitboxes) up
                    leftPaddleBottom.movementy(-2);
                }

                if (pad1.Buttons.A == ButtonState.Pressed || keyboard.IsKeyDown(Keys.S))//if you press a on controller or s on keyboard
                {
                    leftPaddleTop.movementy(2); //move left paddle (both hitboxes) down
                    leftPaddleBottom.movementy(2);
                }
            }
             //kimani wrote everything in update above this
             //haris wrote everything in update bellow this
            //score code
            if (pongBall.getRectangle().Right >= GraphicsDevice.Viewport.Width) //if right of ball touches right wall
            {
                score1++; //increase player 1 score                                             // \/ respawn ball in the middle
                pongBall = new ball(Content.Load<Texture2D>("pongBall"), new Rectangle(GraphicsDevice.Viewport.Width / 2 - 15, GraphicsDevice.Viewport.Height / 2, 15, 15), Color.White);
                timerint = 0; //set time to 0
                speed = 3; //set speed to 3
            }

            if (pongBall.getRectangle().Left <= 0) //if left of wall touches left wall
            {
                score2++; //increase player 2 score                                             // \/ respawn ball in the middle
                pongBall = new ball(Content.Load<Texture2D>("pongBall"), new Rectangle(GraphicsDevice.Viewport.Width / 2 - 15, GraphicsDevice.Viewport.Height / 2, 15, 15), Color.White);
                timerint = 0;//set time to 0
                speed = 3;//set speed to 3
            }
            
            //intersect code
            if (pongBall.getRectangle().Intersects(rightPaddleTop.getRectangle()) || pongBall.getRectangle().Intersects(rightPaddleBottom.getRectangle()))
            { //if ball touches right paddle (top or bottom)
                possessionx = false; //give player 2 possession
            }

            if (pongBall.getRectangle().Intersects(leftPaddleTop.getRectangle()) || pongBall.getRectangle().Intersects(leftPaddleBottom.getRectangle()))
            {//if ball touches left paddle (top or bottom)
                possessionx = true; //give player 1 possession
            }

            if (pongBall.getRectangle().Intersects(leftPaddleBottom.getRectangle()) || pongBall.getRectangle().Intersects(rightPaddleBottom.getRectangle()))
                possessiony = true; //if ball touches bottom hitbox of either players paddle, make ball move down

            if (pongBall.getRectangle().Intersects(leftPaddleTop.getRectangle()) || pongBall.getRectangle().Intersects(rightPaddleTop.getRectangle()))
                possessiony = false; //if ball touches top hitbox of either players paddle, make ball move up

            if (pongBall.getRectangle().Top <= 0) //if ball touches top of form
                possessiony = true; //make ball move down

            if (pongBall.getRectangle().Bottom >= GraphicsDevice.Viewport.Height) //if ball touches bottom of form
                possessiony = false; //make ball move up

            if(rightPaddleTop.getRectangle().Top <= 0) //if right paddle touches top of form
            {
                rightPaddleTop.movementy(3); //prevent from leaving form
                rightPaddleBottom.movementy(3);
            }

            if (rightPaddleBottom.getRectangle().Bottom >= GraphicsDevice.Viewport.Height) //if right paddle touches bottom of form
            {
                rightPaddleTop.movementy(-3);  //prevent from leaving form
                rightPaddleBottom.movementy(-3);
            }

            if (leftPaddleTop.getRectangle().Top <= 0) //if left paddle touches top of form
            { 
                leftPaddleTop.movementy(3);  //prevent from leaving form
                leftPaddleBottom.movementy(3);
            }

            if (leftPaddleBottom.getRectangle().Bottom >= GraphicsDevice.Viewport.Height) //if left paddle touches bottom of form
            {
                leftPaddleTop.movementy(-3);  //prevent from leaving form
                leftPaddleBottom.movementy(-3);
            }
            
            if (score1 >= 10) //if player 1 score is 10
            {
                score1 = 10; //constantly set to 10 to prevent from going higher
                gameOver = true; //make game over bool true
            }
            if(score2 >= 10) //if player 2 score is 10
            {
                score2 = 10;  //constantly set to 10 to prevent from going higher
                gameOver = true; //make game over bool true
            }

            timerint++; //increase timer
            base.Update(gameTime); //update
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            Vector2 textVector = new Vector2(325, 20); //Kimani wrote this
            Vector2 textVector2 = new Vector2(440, 20); //text vectors for score

            spriteBatch.Begin(); //begin drawing 

            spriteBatch.Draw(background, backgroundRect, Color.White); //draw background image
            spriteBatch.DrawString(font, Convert.ToString(score1), textVector, Color.White); //show scores on screen
            spriteBatch.DrawString(font, Convert.ToString(score2), textVector2, Color.White);

            rightPaddleTop.Draw(spriteBatch); //draw both paddles with both hitboxes
            rightPaddleBottom.Draw(spriteBatch);
            leftPaddleTop.Draw(spriteBatch);
            leftPaddleBottom.Draw(spriteBatch);

            if(gameOver == false) //if game is not over, draw ball
                pongBall.Draw(spriteBatch);

            spriteBatch.End(); //end drawing         

            base.Draw(gameTime);
        }
    }
}
