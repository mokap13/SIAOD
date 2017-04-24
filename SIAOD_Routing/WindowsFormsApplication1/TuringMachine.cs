using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class TuringMachine
    {
        public string StartState { get; }
        public string FinishState { get; }
        public List<string> States { get; }
        public List<string> Alphabet { get; }
        public string ZeroSymbol { get; }
        public List<List<Tuple<Direction, string, string>>> PassagesTable { get; }

        private string currentState;

        public TuringMachine(
            List<string> alphabet,
            List<string> states,
            List<List<Tuple<Direction, string, string>>> passagesTable)
        {
            Alphabet = alphabet;
            
            ZeroSymbol = Alphabet.First();

            States = states;
 
            StartState = states.First();
            FinishState = states.Last();

            PassagesTable = passagesTable;
        }

        public void writeSymbol()
        {
            
        }
        public void moveRight()
        {

        }
        public void moveLeft()
        {

        }
        public void changeState(string newState)
        {

        }
    }

   
    public enum Direction
    {
        N,
        R,
        L
    }
}
