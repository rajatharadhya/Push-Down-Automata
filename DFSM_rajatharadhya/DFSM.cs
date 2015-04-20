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
    /*! \brief Constructor */
    public class DFSM
    {
        List<string> K = new List<string>();    /*!< finite set of states */
        List<char> Sigma = new List<char>();    /*!< input alphabets  */
        List<char> StackAlphabets = new List<char>();/*!< alphabets which would go in stack  */
        List<Delta> deltA = new List<Delta>();  /*!< trasition function */
        string S;                               /*!< start state */
        List<string> A = new List<string>();    /*!< accepting states */
        Stack<char> Stackn = new Stack<char>(); /*!< Actual stack  */
        bool checkForMorePop = false;
        int popCount = 0; /*!< count number of pop in stack. To see if it is trying to pop more than required  */
        /*! \brief Constructor */
        public DFSM(List<string> k, List<char> sigma, List<char> stackAlphabet, List<Delta> delta, string s, List<string> a)
        {
            K = k.ToList();
            Sigma = sigma.ToList();
            AddstackAlphabet(stackAlphabet);
            AddDelta(delta);
            AddInitialState(s);
            Acceptingstates(a);
        }

        private bool PreviousStateDelta(Delta delt)  /*!< trasition function */
        {
            return deltA.Any(vari => vari.StartState == delt.StartState && vari.InputSymbol == delt.InputSymbol);
            /*!< return true if start state is same for the symbol */
        }
        private bool ValidDelta(Delta delt)
        {
            return K.Contains(delt.StartState) && K.Contains(delt.EndState) &&
                   Sigma.Contains(delt.InputSymbol) && /*!< check if start and end state is in delta and also check if thsymbol is valid */
                   !PreviousStateDelta(delt);
        }

        private void AddDelta(List<Delta> delta) /*!< Setting all transition states */
        {
            foreach (Delta del in delta.Where(ValidDelta))
            {
                deltA.Add(del);
            }
        }

        private void AddInitialState(string s)
        {
            if (s != null && K.Contains(s))
            {
                S = s;/*!< setting intial state */
            }
        }

        private bool checkStackAlphabet(List<char> stackAlphabet) /*!< To verify alphabets which are going to stack are valid */
        {
           foreach(char alpha in stackAlphabet)
           {
               if (!Sigma.Contains(alpha))
                   return false;
           }
           return true;
        }

        private void AddstackAlphabet(List<char> stackAlphabet)
        {
            if (stackAlphabet != null && checkStackAlphabet(stackAlphabet))
            {
                StackAlphabets = stackAlphabet;/*!< setting intial state */
            }
        }

        private void Acceptingstates(List<string> acceptingstates)
        {
            foreach (string acceptState in acceptingstates.Where(finalState => K.Contains(finalState)))
            {
                A.Add(acceptState); /*!< setting accepting state */
            }
        }

        /*! InputCheckAcceptance is used to check if the input entered by user is acceppted or rejected  */  
       
        public void InputCheckAcceptance(string input) /*!< input from the file */
        {
            popCount = 0;
            Stackn.Clear();
            if (InputValidate(input) && ValidateDFSM())
            {
                return;
            }
            string currentState = S;
            StringBuilder trace = new StringBuilder();
            foreach (char inputSym in input.ToCharArray())
            {
                Delta del = deltA.Find(t => t.StartState == currentState && t.InputSymbol == inputSym);
                
                if (del == null)
                {
                    return;
                }
                if (del.PushChar != 'E') /*!< to push to stack */
                {
                    Stackn.Push(del.PushChar);
                    popCount++;
                }
                if (del.PopChar != 'E') /*!< to pop to stack  */
                {
                    popCount--;
                    if (Stackn.Count > 0)
                    {
                        Stackn.Pop();
                    }
                }
                currentState = del.EndState;
                trace.Append(del + "\n");
            }
            if (A.Contains(currentState) && Stackn.Count == 0 && popCount >= 0)
            {
                Console.WriteLine("Input Accepted\n Trace -->\n" + trace);
                return;
            }
            Console.WriteLine("Rejected \nStopped state " + currentState);
            Console.WriteLine(trace.ToString());
            /*!< returns if it is acccepted or rejected */
        }


        /*!
          To check if the input entered by user is valid
        */
        private bool InputValidate(string inputs) /*!< input from the file  */
        {
            foreach (char input in inputs.ToCharArray().Where(input => !Sigma.Contains(input)))
            {
                Console.WriteLine(input + " --> wrong input \n");
                return true;
            }
            return false; /*!< true if input is valid */
        }

        /*!
        To check if DFSM has valid initial state and accepting state
      */
        private bool ValidateDFSM()
        {
            if (CheckIntialState())
            {
                Console.WriteLine("Missing intial state");
                return true;
            }
            if (checkAcceptingState())
            {
                Console.WriteLine("Missing final state");
                return true;
            }
            return false; /*!< return true if DFSM is valid */
        }



        private bool CheckIntialState()
        {
            return string.IsNullOrEmpty(S);
        }

        private bool checkAcceptingState()
        {
            return A.Count == 0;
        }

    }
}
