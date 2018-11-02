/////////////////////////////////////////////////////////////////////////////
//  State-Based - Tokenizer                                       FALL 18  //
//  ver 2.0                                                                //
//  Language:     C#, VS 2017                                              //
//  Platform:     Lenovo Ideapad, Windows 10 Student                       //
//  Application:  Automated Test Executive for project 2 CSE681            //
//  Author:       Rajat Vyas, Syracuse University                          //
//                (313) 324-2766, ravyas@syr.edu                           //
/////////////////////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 * Builds a state based Tokenizer for the first part of Lexical Scanner.
 * 
 * Public Interface:
 * -----------------
 * public Toker()
 * public bool open(string path)
 * public void close()
 * internal void setdspecialchar(List<string> l)
 * internal void setspecialchar(List<char> l)
 * public int lineCount() 
 * public Token getTok()
 * Public Test class
 * 
 * Required Files:
 * ---------------
 * TokenContext.cs
 * ATokenState.cs
 * DerivedStates.cs
 * Interfaces.cs
 * Test.txt
 * 
 * Maintenance History
 * -------------------
 * ver 2 : 29 Sep 2018
 * - Modularizing code
 *   making different .cs file for class and interfaces
 * ver 1.1 : 02 Sep 2018
 * - Changed Toker, TokenState, TokenFileSource, and TokenContext to fix a bug
 *   in setting the initial state.  These changes are cited, below.
 * - Removed TokenState state_ from toker so only TokenContext instance manages 
 *   the current state.
 * - Changed TokenFileSource() to TokenFileSource(TokenContext context) to allow the 
 *   TokenFileSource instance to set the initial state correctly.
 * - Changed TokenState.nextState() to static TokenState.nextState(TokenContext context).
 *   That allows TokenFileSource to use nextState to set the initial state correctly.
 * - Changed TokenState.nextState(context) to treat everything that is not whitespace
 *   and is not a letter or digit as punctuation.  Char.IsPunctuation was not inclusive
 *   enough for Toker.
 * - changed current_ to currentState_ for readability
 * ver 1.0 : 30 Aug 2018
 * - first release
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toker
{
    using Token = StringBuilder;

    ///////////////////////////////////////////////////////////////////
    // Toker class
    // - applications need to use only this class to collect tokens

    public class Toker
    {
        private TokenContext context_/*sfgsdfg*/;       // holds single instance of all states and token source

        //----< initialize state machine >-------------------------------

        public Toker()
        {
            context_ = new TokenContext();      // context is the glue that holds all of the state machine parts 
        }
        //----< attempt to open source of tokens >-----------------------
        /*
         * If src is successfully opened, it uses TokenState.nextState(context_)
         * to set the initial state, based on the source content.
         */
        public bool open(string path)
        {
            TokenSourceFile src = new TokenSourceFile(context_);
            context_.src = src;
            return src.open(path);
        }
        //----< Setter for singlespecialcharlist >---------------------------------
        internal void setspecialchar(List<char> l)
        {
            context_.singlespec_char.Clear();
            context_.singlespec_char = l;
        }
        //----< Setter for doublespecialcharlist >---------------------------------
        internal void setdspecialchar(List<string> l)
        {
            context_.doublespec_char.Clear();
            context_.doublespec_char = l;
        }
        //----< getter for singlespecialcharlist >---------------------------------
        internal List<char> getspecialchar()
        {
            return context_.singlespec_char;
        }
        //----< getter for doublespecialcharlist >---------------------------------
        internal List<string> getdspecialchar()
        {
            return context_.doublespec_char;
        }
        //----< close source of tokens >---------------------------------
        public void close()
        {
            context_.src.close();
        }
        //----< extract a token from source >----------------------------

        private bool isWhiteSpaceToken(Token tok)
        {
            return (tok.Length > 0 && Char.IsWhiteSpace(tok[0]) && !tok[0].Equals('\n'));
        }

        public Token getTok()
        {
            Token tok = null;
            while (!isDone())
            {
                tok = context_.currentState_.getTok();
                context_.currentState_ = TokenState.nextState(context_);
                if (!isWhiteSpaceToken(tok))
                    break;
            }
            return tok;
        }
        //----< has Toker reached end of its source? >-------------------

        public bool isDone()
        {
            if (context_.currentState_ == null)
                return true;
            return context_.currentState_.isDone();
        }
        public int lineCount() { return context_.src.lineCount; }
    }



#if (TEST_TOKER)

    public class DemoToker
    {
        public static StringBuilder testTokera(string path, int t)
        {
            StringBuilder s = new StringBuilder();
            Toker toker = new Toker();
            string fqf = System.IO.Path.GetFullPath(path);
            if (!toker.open(fqf))
            {
                return null;
            }
            if (t == 1)
            {
                toker.setspecialchar(new List<char> { });
                toker.setdspecialchar(new List<string> { });
            }
            while (!toker.isDone())
            {
                Token tok = toker.getTok();
                if (!new List<int> { 19, 16, 18, 11, 12, 13, 14, 15 }.Contains(toker.lineCount()))
                    continue;
                if (tok.ToString() != "\n")
                {
                    s.Append("\n   - " + toker.lineCount() + "      | ").Append(tok);
                    if (Char.IsLetterOrDigit(tok[0]))
                        s.Append(' ', (50 - tok.Length)).Append("|AlphaNumeric Tokens");
                    else if (tok.ToString().Contains("\'"))
                        s.Append(' ', (50 - tok.Length)).Append("|Single Quoted Tokens");
                    else if (tok.ToString().Contains("\""))
                        s.Append(' ', (50 - tok.Length)).Append("|Double Quoted Tokens");
                    else if (tok.ToString().Contains("//"))
                        s.Append(' ', (50 - tok.Length)).Append("|Single Line Comment Token");
                    else if (tok.ToString().Contains("/*"))
                        s.Append(' ', (50 - tok.Length)).Append("|Multi Line Comment Token");
                    else if (toker.getdspecialchar().Contains(tok.ToString()))
                        s.Append(' ', (50 - tok.Length)).Append("|Double Special Char Token");
                    else if (toker.getspecialchar().Intersect(tok.ToString().ToList()).Any())
                        s.Append(' ', (50 - tok.Length)).Append("|Single Special Char Token");
                    else
                        s.Append(' ', (50 - tok.Length)).Append("|Punctuator Token");

                }
            }
            //toker.close();
            return s;
        }
        public static StringBuilder test5to(string path)
        {
            StringBuilder s = new StringBuilder();
            Toker toker = new Toker();
            string fqf = System.IO.Path.GetFullPath(path);
            if (!toker.open(fqf))
            {
                return null;
            }
            int count = 0;
            while (!toker.isDone())
            {
                
                Token tok = toker.getTok();
                ++count;

                if (toker.lineCount() != 1)
                    continue;
                if (tok.ToString() != "\n")
                {
                    s.Append("\n   - " + toker.lineCount() + "      | ").Append(tok);
                    s.Append(' ', (50 - tok.Length)).Append("|"+count);
                }
            }
            return s;
        }
        public static StringBuilder testToker(string path)
        {
            StringBuilder s = new StringBuilder();
            Toker toker = new Toker();
            string fqf = System.IO.Path.GetFullPath(path);
            if (!toker.open(fqf))
            {
                return null;
            }
            int count = 0;
            while (!toker.isDone())
            {

                Token tok = toker.getTok();
                ++count;
                if (tok.ToString() != "\n")
                {
                    s.Append("\n   - " + toker.lineCount() + "      | ").Append(tok);
                    s.Append(' ', (50 - tok.Length)).Append("|" + count);
                }
            }
            return s;
        }

        static void Main(string[] args)
        {
            Console.Write("\n  Demonstrate Toker class");
            Console.Write("\n =========================");

            StringBuilder msg = new StringBuilder();
            msg.Append("\n  Some things this demo does not do for CSE681 Project #2:");
            msg.Append("\n  - collect comments as tokens");
            msg.Append("\n  - collect double quoted strings as tokens");
            msg.Append("\n  - collect single quoted strings as tokens");
            msg.Append("\n  - collect specified single characters as tokens");
            msg.Append("\n  - collect specified character pairs as tokens");
            msg.Append("\n  - remove Byte Order Marks (BOMs) at the beginning of the source");
            msg.Append("\n  - integrate with a SemiExpression collector");
            msg.Append("\n  - provide the required package structure");
            msg.Append("\n  - Line Count").Append(' ', 45).Append("Token Count");

            Console.Write(msg);

            Console.Write(testToker("../../Test.txt"));
            //testToker("../../Toker.cs");

            Console.WriteLine("\n\n");
            Console.ReadLine();
        }
    }
  
#endif
}