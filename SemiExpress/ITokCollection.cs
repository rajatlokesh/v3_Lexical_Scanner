using System;
/////////////////////////////////////////////////////////////////////////////
//  Project 2 ITokCollection interface used for Scanner Interface FALL 18  //
//  ver 1.0                                                                //
//  Language:     C#, VS 2017                                              //
//  Platform:     Lenovo Ideapad, Windows 10 Student                       //
//  Application:  Interface for project 2 CSE681                           //
//  Author:       Rajat Vyas, Syracuse University                          //
//                (313) 324-2766, ravyas@syr.edu                           //
/////////////////////////////////////////////////////////////////////////////

/*
 * Package Operations:
 * -------------------
 * Builds a Semi-Expression which is a list of list of tokens
 * Gets token from tokenizer by calling tokenizer's gettok() method
 * and then makes a semiexpression frm thoese token and store it
 * Each semiexpression is stored in a list.
 * Also provide an interface for other class to use getsemi() funciton.
 * Public Interface:
 * -----------------
 * internal bool Open(String path)
 * internal List<List<String>> WholeSemi(String path)
 * public String getSemi(path)
 * public class Demo_semi() 
 * 
 * Required Files:
 * ---------------
 * Toker.cs
 * ITokCollection.cs
 * testSemi.txt
 * 
 * Maintenance History
 * -------------------
 * ver 1.0 : 09 Oct 2018
 * - first release
 */

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemiExpression
{
    interface ITokCollection
    {
        String getSemi(string path);
    }
}
