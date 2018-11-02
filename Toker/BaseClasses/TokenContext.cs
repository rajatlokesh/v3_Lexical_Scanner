/////////////////////////////////////////////////////////////////////////////
//  Token Context for toker.cs                                    FALL 18  //
//  ver 3.0                                                                //
//  Language:     C#, VS 2017                                              //
//  Platform:     Lenovo Ideapad, Windows 10 Student                       //
//  Application:  Token Context for project 2 CSE681                       //
//  Author:       Rajat Vyas, Syracuse University                          //
//                (313) 324-2766, ravyas@syr.edu                           //
/////////////////////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 * Provides basic structure of each Token State
 * Also holds source of token and limit access to this assembly
 * 
 * Required Files:
 * ---------------
 * Toker.cs
 * ITokenSource.cs 
 * ITokenState.cs
 * 
 * Maintenance History
 * -------------------
 * ver3 : 02 Oct 2018
 * - Added qs_ for quoted string
 * - Added remaining states
 * -------------------------------------------------------------------------------
 * ver 2 : 29 Sep 2018
 * - Modularizing code
 *   making different .cs file for class and interaces
 * - Added single cs_ and multi-line comment states mcs_
 * -------------------------------------------------------------------------------
 * version 1    Date- 08/28/2018
 * provided by prof fawcett
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Toker
{
    ///////////////////////////////////////////////////////////////////
    // TokenContext class
    // - holds all the tokenizer states
    // - holds source of tokens
    // - internal qualification limits access to this assembly
    internal class TokenContext
    {
        internal List<char> singlespec_char { get; set; }
        internal List<string> doublespec_char { get; set; }
        internal TokenContext()
        {
            ws_ = new WhiteSpaceState(this);
            ps_ = new PunctState(this);
            as_ = new AlphaState(this);
            // more states here
            cs_ = new CommentState(this);
            mcs_ = new MultiCommentState(this);
            qs_ = new QStringState(this);
            ssc_ = new SingleSpecialChar(this);
            sqs_ = new SQStringState(this);
            dscs_ = new DoubleSpecialCharState(this);
            nls_ = new NewLineState(this);
            currentState_ = ws_;
            singlespec_char = new List<char> {'<', '>', '[', ']', '(', ')', '{', '}', ':', '=', '+', '-', '*' };
            doublespec_char = new List<string> { "<<", ">>", "::", "++", "--", "==", "+=", "-=", "*=", "/=", "&&", "||"};

        }
        internal WhiteSpaceState ws_ { get; set; }
        internal PunctState ps_ { get; set; }
        internal AlphaState as_ { get; set; }
        // more states here
        internal NewLineState nls_ { get; set; }
        internal CommentState cs_ { get; set; }
        internal MultiCommentState mcs_ { get; set; }
        internal QStringState qs_ { get; set; }
        internal SingleSpecialChar ssc_ { get; set; }
        internal SQStringState sqs_ { get; set; }
        internal DoubleSpecialCharState dscs_{get; set;}
        // emd more states

        internal TokenState currentState_ { get; set; }
        internal ITokenSource src { get; set; }  // can hold any derived class
    }
#if (TEST_CONTEXT)
    class demoTokContext
    {


        static void Main(string[] args)
        {

        }
    }
#endif
}
