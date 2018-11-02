/////////////////////////////////////////////////////////////////////////////
//  Internal Toker file                                           FALL 18  //
//  ver 2.0                                                                //
//  Language:     C#, VS 2017                                              //
//  Platform:     Lenovo Ideapad, Windows 10 Student                       //
//  Application:  Abstrac Class used  for Toker in project 2 CSE681        //
//  Author:       Rajat Vyas, Syracuse University                          //
//                (313) 324-2766, ravyas@syr.edu                           //
/////////////////////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 * Abstract Class extens ItokenState interface
 * Base class for all derived state in derived class
 * 
 * Public Interface:
 * -----------------
 * 
 * Required Files:
 * ---------------
 * TokenContext.cs
 * ATokenState.cs
 * Interfaces.cs
 *  Maintenance History
 * -------------------
 * version 2    Date- 10/02/2018
 * added QStringState for quoted string token.
 * end() modified to accomodate double peek.
 * return the single token before closing the file.
 * this can happen as we may reach the end of file we we are peeking the second element. 
 * -------------------------------------------------------------------------------------
 * version 1    Date-08/28/2018
 * provided by prof fawcett
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toker
{
    using Token = StringBuilder;

    // TokenState class                                                
    // - base for all the tokenizer states        
    internal abstract class TokenState : ITokenState
    {
        internal TokenContext context_ { get; set; }  // derived classes store context ref here
        //----< delegate source opening to context's src >---------------
        public bool open(string path)
        {
            return context_.src.open(path);
        }
        //----< pass interface's requirement onto derived states >-------
        public abstract Token getTok();
        //----< derived states don't have to know about other states >---
        static public TokenState nextState(TokenContext context)
        {
            int nextItem = context.src.peek();
            if (nextItem < 0)
                return null;
            char ch = (char)nextItem;
            if (ch.Equals('\n'))
                return context.nls_;

            if (Char.IsWhiteSpace(ch))
                return context.ws_;
            if (Char.IsLetterOrDigit(ch))
                return context.as_;

            //Test for single line comment here
            if (context.src.peeknext() == "//")
            {
                return context.cs_;
            }
            //test for multi line comment here
            if (context.src.peeknext() == "/*")
            {
                return context.mcs_;
            }
            //test for double quote
            if ((char)nextItem == '"' )
            {
                return context.qs_;
            }
           
            //Test for single quote
            if ((char)nextItem == '\'')
            {
                return context.sqs_;
            }

            //Test for double special character
            if (context.doublespec_char.Contains(context.src.peeknext()))
            {
                return context.dscs_;
            }

            //test for single special char here
            if (context.singlespec_char.Contains((char)nextItem))
            {
                return context.ssc_;
            }
            //rest is punctuator
            return context.ps_;
        }
        public bool isDone()
        {
            if (context_.src == null)
                return true;
            // return context_.src.end();                   //to accomodate double peek we need to return the last single token atlest
            return false;                                   //ask akash as well
        }
    }
#if (TEST_CONTEXT)
    class demoAState
    {
    
        static void Main(string[] args)
        {

        }
    }
#endif
}
