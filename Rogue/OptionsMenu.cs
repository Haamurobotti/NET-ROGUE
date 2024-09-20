using RayGuiCreator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroElectric.Vinculum;

namespace Rogue
{
    internal class OptionsMenu
    {
        public event EventHandler BackButtonPressedEvent;
        private Game g;
        public OptionsMenu( Game g) { this.g = g; }
         
        public void DrawMenu(int x, int y, int width)
        {
           
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.GetColor(((uint)RayGui.GuiGetStyle(((int)GuiControl.DEFAULT), ((int)GuiDefaultProperty.BACKGROUND_COLOR)))));
            MenuCreator c = new MenuCreator(x, y, Raylib.GetScreenHeight() / 20, width);
            if (c.Button("Back"))
            {
                BackButtonPressedEvent.Invoke(this, EventArgs.Empty);
                
                g.stateStack.Pop();
                Console.WriteLine(g.stateStack.Count);
            }
            

            Raylib.EndDrawing();
        }
    }
}
