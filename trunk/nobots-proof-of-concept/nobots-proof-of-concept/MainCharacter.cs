using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace nobots_proof_of_concept
{
    class MainCharacter : Character
    {
        private MainCharacter instance;

        private MainCharacter(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public MainCharacter Instance()
        {
            if(instance == null)
                lock(this)
                {
                    if(instance == null)
                    {
                        instance = new MainCharacter(Game);
                    }
                }
            return instance;
        }

        public void Raise(Character character)
        {
        }

        public void Drop(Character character)
        {
        }

        public void PutOnBox(Character character)
        {
        }

        public void Die()
        {
        }
    }
}
