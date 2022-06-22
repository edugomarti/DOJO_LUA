using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using KopiLua;
using NLua;

namespace FSM2020_2
{
    public enum STATE
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        RUP,
        RDOWN,
        LUP,
        LDOWN
    };

    public class _GameObject
    {
        Vector2 position;
        Point size;
        Texture2D texture;
        STATE state;
        Vector2 speed;
        static Random random;
        float currentTime;
        const float GAP = 0.5f;

        //https://www.lua.org/portugues.html
        NLua.Lua lua;
        float gt;
        static int cont;

        public _GameObject(Vector2 position, Point size, ref Texture2D texture)
        {
            this.position = position;
            this.size = size;
            this.texture = texture;
            this.state = STATE.UP;
            this.speed = new Vector2(100, 100);
            random = new Random();
            this.currentTime = 0;

            this.lua = new NLua.Lua();
            
            this.lua.RegisterFunction("Print", this, this.GetType().GetMethod("Print"));
            //this.lua.RegisterFunction("UpdateState", this, this.GetType().GetMethod("UpdateState"));
            this.lua.RegisterFunction("MoveUp", this, this.GetType().GetMethod("MoveUp"));
            this.lua.RegisterFunction("MoveDown", this, this.GetType().GetMethod("MoveDown"));
            this.lua.RegisterFunction("MoveLeft", this, this.GetType().GetMethod("MoveLeft"));
            this.lua.RegisterFunction("MoveRight", this, this.GetType().GetMethod("MoveRight"));
            this.lua.RegisterFunction("MoveRup", this, this.GetType().GetMethod("MoveRup"));
            this.lua.RegisterFunction("MoveRdown", this, this.GetType().GetMethod("MoveRdown"));
            this.lua.RegisterFunction("MoveLup", this, this.GetType().GetMethod("MoveLup"));
            this.lua.RegisterFunction("MoveLdown", this, this.GetType().GetMethod("MoveLdown"));
            this.lua.RegisterFunction("ChangeState", this, this.GetType().GetMethod("ChangeState"));

            this.lua["aux"] = cont;

            try
            {
                ScriptLua("Start");
            }
            catch (Exception e)
            {
                Console.WriteLine("StartError: " + e.Message);
            }

            cont = (int)this.lua.GetNumber("aux");
            //Console.WriteLine("Cont: " + cont);
        }

        public void Update(GameTime gameTime)
        {
            this.gt = gameTime.ElapsedGameTime.Milliseconds * 0.001f;

            //this.UpdateState(gameTime);

            this.lua["state"] = (int)state;

            try
            {
                ScriptLua("Update");
            }
            catch (Exception e)
            {
                Console.WriteLine("StartError: " + e.Message);
            }

           


            this.currentTime += gameTime.ElapsedGameTime.Milliseconds * 0.001f;
            if (this.currentTime >= GAP)
            {
                this.currentTime = 0;
                this.ChangeState();

               // try
               //{
               //     ScriptLua("ChangeState");
              // }
              //  catch (Exception e)
              //  {
              //    Console.WriteLine("StartError: " + e.Message);
              //  }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(this.texture, new Rectangle((int)this.position.X, (int)this.position.Y, this.size.X, this.size.Y), Color.White);
        }

        public void UpdateState()
        {
            switch (state)
            {
                case STATE.UP:
                    this.position.Y -= this.speed.Y * this.gt;
                    break;

                case STATE.DOWN:
                    this.position.Y += this.speed.Y * this.gt;
                    break;

                case STATE.LEFT:
                    this.position.X -= this.speed.X * this.gt;
                    break;

                case STATE.RIGHT:
                    this.position.X += this.speed.X * this.gt;
                    break;

                case STATE.RUP:
                    this.position.X += this.speed.X * this.gt;
                    this.position.Y -= this.speed.Y * this.gt;
                    break;

                case STATE.RDOWN:
                    this.position.X += this.speed.X * this.gt;
                    this.position.Y += this.speed.Y * this.gt;
                    break;

                case STATE.LUP:
                    this.position.X -= this.speed.X * this.gt;
                    this.position.Y -= this.speed.Y * this.gt;
                    break;

                case STATE.LDOWN:
                    this.position.X -= this.speed.X * this.gt;
                    this.position.Y += this.speed.Y * this.gt;
                    break;
            }
        }

        public void ChangeState()

        {
            

            int r = random.Next(7);
            switch (state)
            {
                case STATE.UP:
                    switch (r)
                    {
                        case 0: this.state = STATE.DOWN; break;
                        case 1: this.state = STATE.LEFT; break;
                        case 2: this.state = STATE.RIGHT; break;

                        case 3: this.state = STATE.RUP; break;
                        case 4: this.state = STATE.RDOWN; break;
                        case 5: this.state = STATE.LUP; break;
                        case 6: this.state = STATE.LDOWN; break;
                    }
                    break;

                case STATE.DOWN:
                    switch (r)
                    {
                        case 0: this.state = STATE.UP; break;
                        case 1: this.state = STATE.LEFT; break;
                        case 2: this.state = STATE.RIGHT; break;
                        case 3: this.state = STATE.RUP; break;
                        case 4: this.state = STATE.RDOWN; break;
                        case 5: this.state = STATE.LUP; break;
                        case 6: this.state = STATE.LDOWN; break;

                    }
                    break;

                case STATE.LEFT:
                    switch (r)
                    {
                        case 0: this.state = STATE.UP; break;
                        case 1: this.state = STATE.DOWN; break;
                        case 2: this.state = STATE.RIGHT; break;
                        case 3: this.state = STATE.RUP; break;
                        case 4: this.state = STATE.RDOWN; break;
                        case 5: this.state = STATE.LUP; break;
                        case 6: this.state = STATE.LDOWN; break;
                    }
                    break;

                case STATE.RIGHT:
                    switch (r)
                    {
                        case 0: this.state = STATE.UP; break;
                        case 1: this.state = STATE.DOWN; break;
                        case 2: this.state = STATE.LEFT; break;
                        case 3: this.state = STATE.RUP; break;
                        case 4: this.state = STATE.RDOWN; break;
                        case 5: this.state = STATE.LUP; break;
                        case 6: this.state = STATE.LDOWN; break;
                    }
                    break;

                case STATE.RUP:
                    switch (r)
                    {
                        case 0: this.state = STATE.UP; break;
                        case 1: this.state = STATE.DOWN; break;
                        case 2: this.state = STATE.LEFT; break;
                        case 3: this.state = STATE.RIGHT; break;
                        case 4: this.state = STATE.RDOWN; break;
                        case 5: this.state = STATE.LUP; break;
                        case 6: this.state = STATE.LDOWN; break;
                    }
                    break;

                case STATE.RDOWN:
                    switch (r)
                    {
                        case 0: this.state = STATE.UP; break;
                        case 1: this.state = STATE.DOWN; break;
                        case 2: this.state = STATE.LEFT; break;
                        case 3: this.state = STATE.RIGHT; break;
                        case 4: this.state = STATE.RUP; break;
                        case 5: this.state = STATE.LUP; break;
                        case 6: this.state = STATE.LDOWN; break;
                    }
                    break;

                case STATE.LUP:
                    switch (r)
                    {
                        case 0: this.state = STATE.UP; break;
                        case 1: this.state = STATE.DOWN; break;
                        case 2: this.state = STATE.LEFT; break;
                        case 3: this.state = STATE.RIGHT; break;
                        case 4: this.state = STATE.RDOWN; break;
                        case 5: this.state = STATE.RUP; break;
                        case 6: this.state = STATE.LDOWN; break;
                    }
                    break;

                case STATE.LDOWN:
                    switch (r)
                    {
                        case 0: this.state = STATE.UP; break;
                        case 1: this.state = STATE.DOWN; break;
                        case 2: this.state = STATE.LEFT; break;
                        case 3: this.state = STATE.RIGHT; break;
                        case 4: this.state = STATE.RDOWN; break;
                        case 5: this.state = STATE.LUP; break;
                        case 6: this.state = STATE.RUP; break;
                    }
                    break;

                    
            }
        }

        private void ScriptLua(string function)
        {
            this.lua.DoFile(@"GameObjectLua.txt");
            ((LuaFunction)this.lua[function]).Call();
        }

        public void Print(string s)
        {
            Console.WriteLine("[Print] " + s);
        }

        public void MoveUp()
        {
            this.position.Y -= this.speed.Y * this.gt;
        }

        public void MoveDown()
        {
            this.position.Y += this.speed.Y * this.gt;
        }

        public void MoveLeft()
        {
            this.position.X -= this.speed.X * this.gt;
        }

        public void MoveRight()
        {
            this.position.X += this.speed.X * this.gt;
        }

        public void MoveRup() 
        {
            this.position.X += this.speed.X * this.gt;
            this.position.Y -= this.speed.Y * this.gt;
        
        }
        public void MoveRdown() 
        {
            this.position.X += this.speed.X * this.gt;
            this.position.Y += this.speed.Y * this.gt;
        
        }
        public void MoveLup() 
        {
            this.position.X -= this.speed.X * this.gt;
            this.position.Y -= this.speed.Y * this.gt;
        
        }
        public void MoveLdown() 
        {
            this.position.X -= this.speed.X * this.gt;
            this.position.Y += this.speed.Y * this.gt;
        
        }
    }
}
