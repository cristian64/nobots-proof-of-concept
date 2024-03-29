﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework.Input;
using nobots_proof_of_concept.ParticleSystem;

namespace nobots_proof_of_concept
{
    public class Ghost : Character
    {
        
        SpriteBatch spriteBatch;

        Rectangle rectangle;
        Vector2 position;
        ParticleEmitter particleEmitter;

        Character prey;

        bool isSpaceDown;
        public int unhauntNumber;

        public Ghost(Game game)
            : base(game)
        {
            isHaunted = false;
        }

        public override void Initialize()
        {
            position = new Vector2(-100,-100);
            particleEmitter = new ParticleEmitter(PlasmaExplosionParticleSystem.LastInstance, 20, new Vector3(position.X, position.Y, 0));
            stepSize = 2;
            isSpaceDown = false;
            unhauntNumber = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
         //   if (isHaunted)
            ProcessKeyboard();
            particleEmitter.Update(gameTime, new Vector3(position.X, position.Y, 0));
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void ProcessKeyboard()
        {
            KeyboardState keybState = Keyboard.GetState();

            if (isHaunted)
            {
                if (keybState.IsKeyDown(Keys.Left))
                {
                    position.X -= stepSize;
                }

                if (keybState.IsKeyDown(Keys.Right))
                {
                    position.X += stepSize;
                }

                if (keybState.IsKeyDown(Keys.Up))
                {
                    position.Y -= stepSize;
                }

                if (keybState.IsKeyDown(Keys.Down))
                {
                    position.Y += stepSize;
                }

                if (keybState.IsKeyUp(Keys.Space) && isSpaceDown)
                {
                    isSpaceDown = false;
                    unhauntNumber = 0;
                }

                if (keybState.IsKeyDown(Keys.Space))
                    if (!isSpaceDown && (prey = checkHauntingPossibility())!= null)
                    {
                        if (unhauntNumber != 1)
                        {
                            Console.WriteLine("space pressed in ghost"); 
                            isHaunted = false;
                            haunt(prey);
                        }
                        isSpaceDown = true;
                    }
                }
        }

        private Character checkHauntingPossibility()
        {
            Vector2 characterPos;
            int characterWidth;
            int characterHeight;

            characterPos = ((Game1)Game).mainCharacter.body.Position * 50;
            characterWidth = ((Game1)Game).mainCharacter.texture.Width;
            characterHeight = ((Game1)Game).mainCharacter.texture.Height;
            if (characterPos.X - 20 <= position.X && characterPos.X + characterWidth + 20 >= position.X &&
                characterPos.Y - characterHeight + characterWidth / 4 - 20 <= position.Y && characterPos.Y + characterWidth / 4 + 20 >= position.Y)
                return ((Game1)Game).mainCharacter;

            characterPos = ((Game1)Game).mouse.body.Position * 50;
            characterWidth = ((Game1)Game).mouse.texture.Width;
            characterHeight = ((Game1)Game).mouse.texture.Height;
            if (characterPos.X - 20 <= position.X && characterPos.X + characterWidth + 20 >= position.X &&
                characterPos.Y - characterHeight / 2 - 20 <= position.Y && characterPos.Y + characterHeight / 2 + 20 >= position.Y)
                return ((Game1)Game).mouse;

            return null;
        }

        private void haunt(Character character)
        {
            ((Game1)Game).Change.Play();
            isHaunted = false;
            character.isHaunted = true;
            position.X = -100;
            position.Y = -100;
        }

        public void Unhaunt(Character character)
        {
            ((Game1)Game).Change.Play();
            unhauntNumber = 1;
            isHaunted = true;
            Console.WriteLine(character.body.Position.X + ", " + character.body.Position.Y);
            position.X = character.body.Position.X * 50;
            position.Y = character.body.Position.Y * 50 - 50;
        }
    }
}
