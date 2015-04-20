/**
 * Author- Rajath Aradhya Mysore Shekar
 * Course - CS5800
 * Assignment 6
 * Date - 3/22/2015
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace DFSM_rajatharadhya
{
    /*! \brief Driving Program */
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader DFSMReader = new StreamReader("DFSM.txt");
            /*! \brief Reading DFSM from a text file.
             *          
             *   text files location is in the project folder under bin\debug\DFSM.txt or input.txt
             *   streamreader is used to read from a text file. 
             *   first line defines DFSM 
             *   each variable is seperated using string.split with a delimeter.
             *   and everything is read into its respective Lists.
             *   After user enters the input, the input is checked for acceptance
             *   */
            string readingline =  DFSMReader.ReadLine();
            string[] line = readingline.Split('/');
            List<string> K = new List<string>();    /*!< finite set of states */
            List<char> sigma = new List<char>();    /*!< input alphabets  */
            List<char> stackAlphabet = new List<char>();
            List<Delta> delta = new List<Delta>();  /*!< trasition function */
            string s;                               /*!< start state */
            List<string> A = new List<string>();    /*!< accepting states */
            Stack<char> stackn = new Stack<char>();
            K = line[0].Split(',').ToList<string>();
     
            int j = 0;
            foreach (string str in line[1].Split(','))
            {
                sigma.Add (Convert.ToChar(str));
                j++;
            }

            foreach (string str in line[2].Split(','))
            {
                stackAlphabet.Add(Convert.ToChar(str));
            }
            s = line[3];

            A = line[4].Split(',').ToList<string>();
            
            while ((readingline = DFSMReader.ReadLine()) != null)
            {
                line = readingline.Split('/');
                delta.Add(new Delta(line[0], Convert.ToChar(line[1]),Convert.ToChar(line[2]), line[3],Convert.ToChar(line[4])));
            }
            DFSMReader.Close();
            DFSM dfsm = new DFSM(K, sigma,stackAlphabet, delta, s, A);
            string inputs;
            StreamReader inputReader = new StreamReader("input.txt");
            while ((inputs = inputReader.ReadLine()) != null)
            {
                Console.WriteLine("Input --> " + inputs);
                dfsm.InputCheckAcceptance(inputs);
            }
            Console.ReadLine();
        }
    }
}
