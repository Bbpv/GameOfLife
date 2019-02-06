using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Renderer
    {
        private StringBuilder stringBuilder;
        private char filledCell;
        private char emptyCell;
        private const string renderStr = "{0}";
        private const string renderStrEndRow = "{0}\n";

        public Renderer(char filledCellSymbol, char emptyCellSymbol)
        {
            stringBuilder = new StringBuilder();
            filledCell = filledCellSymbol;
            emptyCell = emptyCellSymbol;
        }

        public void Render()
        {
            Console.Write(stringBuilder.ToString());
            stringBuilder.Clear();
        }
        
        public void PreRenderBegin(string clearToString)
        {
            Console.Clear();
            stringBuilder.Append(clearToString + "\n");
        }

        public void PreRenderUpdate(bool state, bool endRow)
        {
            char displayChar = state ? filledCell : emptyCell;
            if (endRow)
            {
                stringBuilder.AppendFormat(renderStrEndRow, displayChar);
            }
            else
            {
                stringBuilder.AppendFormat(renderStr, displayChar);
            }
        }
    }
}
