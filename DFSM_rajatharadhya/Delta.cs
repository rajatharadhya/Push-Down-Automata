/**
 * Author- Rajath Aradhya Mysore Shekar
 * Course - CS5800
  * Assignment 6
 * Date - 3/22/2015
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFSM_rajatharadhya
{

    /*! \brief Object Class 
     * 
     * Object for Delta
     * Every state is object.
     * Delta gives the picture of the DFSM
 */
    public class Delta
    {
        public string StartState { get; private set; } /*!< Start state. */
        public char InputSymbol { get; private set; } /*!< each input symbols */
        public char PopChar { get; private set; } /*!< char to pop from stack */
        public string EndState { get; private set; } /*!< End states. */
        public char PushChar { get; private set; } /*!< char to push from stack  */
        public Delta(string startState, char inputSymbol, char popChar, string endState, char pushChar)
        {
            StartState = startState;  /*!< Start state. */
            InputSymbol = inputSymbol; /*!< each input symbols */
            PopChar = popChar;
            EndState = endState;  /*!< End states. */
            PushChar = pushChar;
        }
        /*! \brief Formatting  */
        public override string ToString() /*!< Formatting  */
        {
            return string.Format("{0}/{1}/{2}/{3}/{4}", StartState, InputSymbol, PopChar, EndState, PushChar);
        }
    }
}
