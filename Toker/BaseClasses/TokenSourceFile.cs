/////////////////////////////////////////////////////////////////////////////
//  TokenSourceFile class                                         FALL 18  //
//  ver 2.0                                                                //
//  Language:     C#, VS 2017                                              //
//  Platform:     Lenovo Ideapad, Windows 10 Student                       //
//  Application:  TokenSourceFile class for project 2 CSE681               //
//  Author:       Rajat Vyas, Syracuse University                          //
//                (313) 324-2766, ravyas@syr.edu                           //
/////////////////////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------                                        
 * - extracts integers from token source                            
 * - Streams often use terminators that can't be represented by     
 *   a character, so we collect all elements as ints                
 * - keeps track of the line number where a token is found           
 * - uses StreamReader which correctly handles byte order mark      
 *   characters and alternate text encodings. 
 * Required Files:
 * ---------------
 * Interface.cs
 * 
 * Maintenance History
 * -------------------
 * version 2    Date- 10/02/2018
 * added peeknext() to peek double into file
 * modified peek()'s n == 0 to 1 to accomodate double peek
 * also added eof condition so that last single token can be read
 * ------------------------------------------------------------------
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
    internal class TokenSourceFile : ITokenSource
    {
        public int lineCount { get; set; } = 1;
        private System.IO.StreamReader fs_;           // physical source of text
        private List<int> charQ_ = new List<int>();   // enqueing ints but using as chars
        private TokenContext context_;

        internal TokenSourceFile(TokenContext context)
        {
            context_ = context;
        }
        //----< attempt to open file with a System.IO.StreamReader >-----

        public bool open(string path)
        {
            try
            {
                fs_ = new System.IO.StreamReader(path, true);
                context_.currentState_ = TokenState.nextState(context_);
            }
            catch (Exception ex)
            {
                Console.Write("\n  {0}\n", ex.Message);
                return false;
            }
            return true;
        }
        //----< close file >---------------------------------------------

        public void close()
        {
            fs_.Close();
        }
        //----< extract the next available integer >---------------------
        /*
         *  - checks to see if previously enqueued peeked ints are available
         *  - if not, reads from stream
         */
        public int next()
        {
            int ch;
            if (charQ_.Count == 0)  // no saved peeked ints
            {
                if (end())
                    return -1;
                ch = fs_.Read();
            }
            else                    // has saved peeked ints, so use the first
            {
                //foreach (char c in charQ_) { Console.WriteLine("\n{0}", c); }
                ch = charQ_[0];
                charQ_.Remove(ch);
            }
            if ((char)ch == '\n')   // track the number of newlines seen so far
                ++lineCount;
            return ch;
        }
        //----< peek n ints into source without extracting them >--------
        /*
         *  - This is an organizing prinicple that makes tokenizing easier
         *  - We enqueue because file streams only allow peeking at the first int
         *    and even that isn't always reliable if an error occurred.
         *  - When we look for two punctuator tokens, like ==, !=, etc. we want
         *    to detect their presence without removing them from the stream.
         *    Doing that is a small part of your work on this project.
         */
        //Peeknext()
        //supposed to look into all the extracted charcters from file
        public string peeknext()
        {
            string st = "";
            if (charQ_.Count == 1)  // no saved peeked ints
            {
                if (peek() == 1)
                    st = st + (char)charQ_[0];
                else
                    st = st + (char)charQ_[0] + (char)charQ_[1];
            }
            else                    // has saved peeked ints, looking at these to see two character tokens
            {
                foreach (int t in charQ_)
                {
                    st = st + (char)t;
                }

            }
            return st;


        }
        public int peek(int n = 1)
        {
            if (n < charQ_.Count)  // already peeked, so return
            {
                return charQ_[n-1];
            }
            else                  // nth int not yet peeked
            {
                for (int i = charQ_.Count; i <= 1; ++i)
                {
                    if (end() && charQ_.Count == 0)
                        return -1;
                    else if (!end())
                        charQ_.Add(fs_.Read());  // read and enqueue
                    else 
                        return 1;
                }
                return charQ_[n-1];   // now return the last peeked always 0'th index value
            }
        }
        //----< reached the end of the file stream? >--------------------
        
        public bool end()
        {
            return fs_.EndOfStream;
        }
    }
#if (TEST_CONTEXT)
    class demoTokSrcFile
    {


        static void Main(string[] args)
        {

        }
    }
#endif
}
