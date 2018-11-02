/////////////////////////////////////////////////////////////////////////////
//  Project 2 Requirement Demonstration- Semi Expression          FALL 18  //
//  ver 1.0                                                                //
//  Language:     C#, VS 2017                                              //
//  Platform:     Lenovo Ideapad, Windows 10 Student                       //
//  Application:  Semi Expression Collection for project 2 CSE681          //
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Toker;
namespace SemiExpression  
{
    using Token = StringBuilder;
    internal class SemiExp : ITokCollection
    {
        Toker.Toker tok = new Toker.Toker();
        List<List<string>> SemiExpression { get; set; }
        internal bool Open(String path)
        {
            return (tok.open(path));
        }
        bool done()
        {
            return (tok.isDone());
        }
        String Gettok()
        {
            Token t = tok.getTok();
            if (t == null)
            {
                return "-1";
            }
            else
                return t.ToString();
        }
        List<String> semiex()
        {
            List<string> OneSemi = new List<string> { } ;
            String toki;
            do
            {
                toki = Gettok();
                if (toki == "\n")
                    continue;
                OneSemi.Add(toki);
            } while (!endcondition(toki, OneSemi.Contains("#")));
            if (OneSemi.Contains("for"))
                OneSemi = forcondition(OneSemi,toki);
            return OneSemi;            
        }
        List<string> forcondition(List<string> OneSemi,string toki)
        {
            int count = 1;
            int loopindex = 1;
            while (count < 3)
            {
                toki = Gettok();
                OneSemi.Add(toki.ToString());
                if (endcondition(toki,OneSemi.Contains("#")))
                    ++count;
                if (OneSemi.LastIndexOf("for") > loopindex)
                {
                    count = 0;
                    loopindex = OneSemi.LastIndexOf("for");
                }
            }
            return OneSemi;
        }
        internal List<List<String>> WholeSemi(String path)
        {
            if (!this.Open(path))
                return null;
            
            SemiExpression = new List<List<string>> { };
            do
            {
                SemiExpression.Add(semiex());
            } while (!this.done());
            return SemiExpression;
        }
        bool endcondition(String to,bool OneSemi)
        {
            if (to == null || to == "-1")
                return true;
            switch (to)
            {
                case ";": return true;
                case "{": return true;
                case "}": return true;
                case "\n":return OneSemi;
                    
                default: return false;
            }
        }
        public String getSemi(string path)
        {
            if (!this.Open(path))
                return null;
            return String.Join(String.Empty,semiex().ToArray());
        }
    }
    public class Demo_semi
    {
        public static StringBuilder Test_semi(String path)
            {
                StringBuilder str = new StringBuilder();
                SemiExp s = new SemiExp();
                int count = 0;
                List<List<string>> s_ex = s.WholeSemi(path);
                foreach (List<String> t in s_ex)
                {

                    if (count > 7)
                        continue;
                    StringBuilder msg = new Token();
                    for (int i =0; i< t.Count();i++)
                    {
                        if (!t[i].Equals("\n"))
                        {
                                msg.Append(t[i]);
                                msg.Append(" ");
                        }
                    }
                count++;
                str.Append("\n   -  " + msg).Append(' ', 60-msg.Length);
                if (msg.ToString().Contains("#"))
                    str.Append("| # condition satisfied");
                if (msg.ToString().Contains("for"))
                    str.Append("| for condition satisfied");
                if (msg.ToString().Contains("{") || msg.ToString().Contains("}") || msg.ToString().Contains(";") && !msg.ToString().Contains("for"))
                    str.Append("| end condition satisfied");
            }
            return str;
            }
        public static StringBuilder Test_inter(String path)
        {
            Token s = new Token();
            SemiExp se = new SemiExp();
            s.Append("\n   - Semiexpression : "+se.getSemi(path)).Append(' ', 40 - se.getSemi(path).Length).Append("One SemiExpression.");
            return s;
        }
        public static StringBuilder Test_semi_here(String path)
        {
            StringBuilder str = new StringBuilder();
            SemiExp s = new SemiExp();
            int count = 0;
            List<List<string>> s_ex = s.WholeSemi(path);
            foreach (List<String> t in s_ex)
            {
                StringBuilder msg = new Token();
                for (int i = 0; i < t.Count(); i++)
                {
                    if (!t[i].Equals("\n"))
                    {
                        msg.Append(t[i]);
                        msg.Append(" ");
                    }
                }
                count++;
                str.Append("\n   -  " + msg);
            }
            return str;
        }
#if (MYSOL)
            static void Main(string[] args)
            {

            Console.WriteLine(Test_semi_here("../../Semi_Exp.cs"));
            // never leave these issues
            //Console.WriteLine(Test_semi_here(@"C:\Users\lokes\OneDrive\Desktop\SMA_681\Project 2\v2_Lexical Scanner\Toker\testSemi.txt"));
            //
            Console.WriteLine("Press any key to Continue......");
                Console.WriteLine("\n\n");
                Console.ReadLine();
            }
#endif

    }

}
