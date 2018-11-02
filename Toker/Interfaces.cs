//////////////////////////////////////////////////////////////////////////////
// ITokenState.cs - Interface for Toker.cs                                  //
// ver 1                                date - 10/02/18                     //
// Author- Rajat Vyas, CSE681 - Software Modeling and Analysis, Fall 2018   //
// Provided by Jim Fawcett                                                  //
//////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////
// ITokenSource interface                                                   //
// - Declares operations expected of any source of tokens                   //
// - Typically we would use either files or strings.  This demo             //      
//   provides a source only for Files, e.g., TokenFileSource, below.        //
//////////////////////////////////////////////////////////////////////////////

/* Maintenance History
 * v2 date - 10/02/18
 * added peeknext()
 * changed peek(n=0) to peek (n=1)
 * ------------------------------------
 * version 1    Date- 10/02/2018
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
    internal interface ITokenSource
    {
        bool open(string path);
        void close();
        int next();
        string peeknext();
        int peek(int n = 1);
        bool end();
        int lineCount { get; set; }
    }
    internal interface ITokenState
    {
        Token getTok();
        bool isDone();
    }

}
