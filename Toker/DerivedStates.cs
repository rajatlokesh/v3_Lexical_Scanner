/////////////////////////////////////////////////////////////////////////////
//  Dervied states from AToknStates                               FALL 18  //
//  ver 2.0                                                                //
//  Language:     C#, VS 2017                                              //
//  Platform:     Lenovo Ideapad, Windows 10 Student                       //
//  Application:  Derived states for Toker in project 2 CSE681             //
//  Author:       Rajat Vyas, Syracuse University                          //
//                (313) 324-2766, ravyas@syr.edu                           //
/////////////////////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 * Extends ATokenState and provide all the states for context to switch into
 * makes token of that state and return the token to toker.cs
 * overrides gettok function to acheie this functionality
 * 
 * Public Interface:
 * -----------------
 * getTok()
 * 
 * Required Files:
 * ---------------
 * TokenContext.cs
 * ATokenState.cs
 * Interfaces.cs
 * 
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
    //AlphaNumeric State
    internal class AlphaState : TokenState
    {
        internal AlphaState(TokenContext context)
        {
            context_ = context;
        }
        //----< manage converting extracted ints to chars >--------------
        bool isLetterOrDigit(int i)
        {
            int nextItem = context_.src.peek();
            if (nextItem < 0)
                return false;
            char ch = (char)nextItem;
            return Char.IsLetterOrDigit(ch);
        }
        //----< keep extracting until get none-alpha >-------------------

        override public Token getTok()
        {
            Token tok = new Token();
            tok.Append((char)context_.src.next());          // first is alpha

            while (isLetterOrDigit(context_.src.peek()))    // stop when non-alpha
            {
                tok.Append((char)context_.src.next());
            }
            return tok;
        }
    }
    //Comment State
    class CommentState : TokenState
    {
        public CommentState(TokenContext context)
        {
            context_ = context;
        }
        //----< manage converting extracted ints to chars and doing comment check>--------------

        public override Token getTok()
        {
            Token tok = new Token();
            tok.Append((char)context_.src.next());                      // first is backslash
            while (context_.src.peek() != 13)
            {
                tok.Append((char)context_.src.next());                  // add everything untill newline is reached
            }
            return tok;
        }

    }


    //multiline comment state
    class MultiCommentState : TokenState
    {
        public MultiCommentState(TokenContext context)
        {
            context_ = context;
        }

        public override Token getTok()
        {
            Token tok = new Token();
            tok.Append((char)context_.src.next());                      // first is backslash
            while (1 == 1)         ////peek next is always empty 
            {
                if (context_.src.peeknext() == "*/")
                {
                    tok.Append((char)context_.src.next()).Append((char)context_.src.next());
                    break;
                }
                else if ((char)context_.src.peek() == '\r' || (char)context_.src.peek() == '\n')
                {
                    context_.src.next();
                }
                else
                {
                    tok.Append((char)context_.src.next());
                    continue;
                }
            }
            return tok;
            //throw new NotImplementedException();
        }
    }


    // Punctutator State
    class PunctState : TokenState
    {

        internal PunctState(TokenContext context)
        {
            context_ = context;
        }
        //----< manage converting extracted ints to chars >--------------

        bool isPunctuation(int i)
        {
            int nextItem = context_.src.peek();
            if (nextItem < 0)
                return false;
            char ch = (char)nextItem;
            return (!Char.IsWhiteSpace(ch) && !ch.Equals('\n') && !context_.singlespec_char.Contains(ch) && ch != '\'' && ch != '\"' && ch != '\"' && ch != '/' && !Char.IsLetterOrDigit(ch));
        }
        //----< keep extracting until get none-punctuator >--------------

        override public Token getTok()
        {
            Token tok = new Token();
            tok.Append((char)context_.src.next());       // first is punctuator

            while (isPunctuation(context_.src.peek()))   // stop when non-punctuator
            {
                tok.Append((char)context_.src.next());
            }
            return tok;
        }
    }

    //Quoted String State
    class QStringState : TokenState
    {
        internal QStringState(TokenContext context)
        {
            context_ = context;
        }


        internal bool escape(Token to)
        {
            return ((char)context_.src.peek() == '"' && to.ToString().Last() == '\\');
        }
        //----< keep extracting until get none-alpha >-------------------

        override public Token getTok()
        {
            Token tok = new Token();
            tok.Append((char)context_.src.next());          // first is quotes

            while ((char)context_.src.peek() != '"')    // stop when quotes again
            {
                tok.Append((char)context_.src.next());
                if (tok.Length > 1 && escape(tok))
                    tok.Append((char)context_.src.next());
            }
            return tok.Append((char)context_.src.next());
        }
    }

    // Single Special Character State
    class SingleSpecialChar : TokenState
    {
        internal SingleSpecialChar(TokenContext context)
        {
            context_ = context;
        }



        //----< keep extracting until get none-alpha >-------------------

        override public Token getTok()
        {
            Token tok = new Token();
            tok.Append((char)context_.src.next());          // first is special char
            return tok;
        }


    }

    // Double Special Character State
    class DoubleSpecialCharState : TokenState
    {
        internal DoubleSpecialCharState(TokenContext context)
        {
            context_ = context;
        }



        //----< keep extracting until get none-alpha >-------------------

        override public Token getTok()
        {
            Token tok = new Token();
            tok.Append((char)context_.src.next());          // first special char
            tok.Append((char)context_.src.next());          // second special char
            return tok;
        }


    }

    //Single Quoted String State
    class SQStringState : TokenState
    {
        internal SQStringState(TokenContext context)
        {
            context_ = context;
        }
        internal bool escape(Token to)
        {
            return ((char)context_.src.peek() == '\'' && to.ToString().Last() == '\\');
        }


        //----< keep extracting until get non-quote >-------------------

        override public Token getTok()
        {
            Token tok = new Token();
            tok.Append((char)context_.src.next());          // first is quotes

            while ((char)context_.src.peek() != '\'')    // stop when quotes again
            {
                tok.Append((char)context_.src.next());
                if (tok.Length > 1 && escape(tok))
                    tok.Append((char)context_.src.next());
            }
            return tok.Append((char)context_.src.next());
        }
    }


    //WhiteSpace State

    class WhiteSpaceState : TokenState
    {
        internal WhiteSpaceState(TokenContext context)
        {
            context_ = context;
        }
        //----< manage converting extracted ints to chars >--------------

        bool isWhiteSpace(int i)
        {
            int nextItem = context_.src.peek();
            if (nextItem < 0)
                return false;
            char ch = (char)nextItem;
            return Char.IsWhiteSpace(ch) && !ch.Equals('\n');
        }
        //----< keep extracting until get none-whitespace >--------------

        override public Token getTok()
        {
            Token tok = new Token();
            tok.Append((char)context_.src.next());     // first is WhiteSpace

            while (isWhiteSpace(context_.src.peek()))  // stop when non-WhiteSpace
            {
                tok.Append((char)context_.src.next());
            }
            return tok;
        }
    }
    class NewLineState : TokenState
    {
        internal NewLineState(TokenContext context)
        {
            context_ = context;
        }
        //----< manage converting extracted ints to chars >--------------

        bool isNewLine(int i)
        {
            int nextItem = i;
            if (nextItem < 0)
                return false;
            char ch = (char)nextItem;
            return ch.Equals('\n');
        }
        //----< keep extracting until get none-NewLine >--------------

        override public Token getTok()
        {
            Token tok = new Token();
            //tok.Append("\\n");
            //context_.src.next();
            tok.Append((char)context_.src.next());  // first is NewLine
            while (isNewLine(context_.src.peek()))  // stop when non-NewLine
            {
                tok.Append((char)context_.src.next());
                //tok.Append("\\n");
            }
            return tok;
        }
    }
}
