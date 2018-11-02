/////////////////////////////////////////////////////////////////////////////
//  Project 2 Requirement Demonstration- Test Executive           FALL 18  //
//  ver 1.0                                                                //
//  Language:     C#, VS 2017                                              //
//  Platform:     Lenovo Ideapad, Windows 10 Student                       //
//  Application:  Automated Test Executive for project 2 CSE681            //
//  Author:       Rajat Vyas, Syracuse University                          //
//                (313) 324-2766, ravyas@syr.edu                           //
/////////////////////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 * A Test Executive to automate all the Functionsl Requirements for project 2
 * Have a Class text executive which have all the test function 
 * Each function settles one requirements
 * 
 * Public Interface:
 * -----------------
 * internal bool Open(String path)
 * internal List<List<String>> WholeSemi(String path)
 * public String getSemi(path)
 * public class Demo_semi() 
 * 
 * Required Files:
 * ---------------
 * Toker.csproj
 * SemiExpress.csproj
 * testSemi.txt
 * test.txt
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
using SemiExpression;
using Toker;

namespace TestExec
{
#if (TESTEXEC)
    internal class TestExecutive
    {
        StringBuilder Test2()
        {
            int linecount = 0;
            StringBuilder s = new StringBuilder();
            string t = "";
            using (StreamReader st = new StreamReader("../../TestExecutive.cs"))
            {
                while ((t = st.ReadLine()) != null)
                {
                    linecount++;
                    if (t.ToString() == "using System.Text;" || t.ToString() == "using System.IO;")
                    {
                        s.Append("\n  - At Line ");
                        s.Append(linecount + " : ");
                        s.Append(t);
                    }
                }
            }
            return s;
        }
        StringBuilder Test3()
        {
            StringBuilder t = new StringBuilder();
            String p = Path.GetFullPath("../..");
            string pr = Directory.GetParent(p).ToString();
            DirectoryInfo d = new DirectoryInfo(pr);
            FileInfo[] files = d.GetFiles("*.csproj", SearchOption.AllDirectories);
            FileInfo[] fi = d.GetFiles("*.cs", SearchOption.AllDirectories);
            files.Concat(fi);
            foreach (FileInfo f in files)
            {
                if (f.ToString() == "SemiExpress.csproj")
                {
                    t.Append("\n    -- Files : " + f).Append(' ', 40 - f.ToString().Length).Append("Semi-Expression Assembly");
                }
                if (f.ToString() == "Toker.csproj")
                    t.Append("\n    -- Files : " + f).Append(' ', 40 - f.ToString().Length).Append("Tokenizer Assembly");
                if (f.ToString() == "Test_Executive.csproj")
                    t.Append("\n    -- Files : " + f).Append(' ', 40 - f.ToString().Length).Append("Test Assembly");

            }
            foreach (FileInfo f in fi)
            {
                if (f.ToString() == "ITokCollection.cs")
                    t.Append("\n    -- Files : " + f).Append(' ',40-f.ToString().Length).Append("Scanner Interface");
            }
            return t;
        }
        StringBuilder Test4abdef(string path,int i)
        {
            StringBuilder s = new StringBuilder();
            string pr = Directory.GetParent(path).ToString();
            s.Append("\n   - Test File for requirement Demonstration is :-").Append("\n   - ");
            s.Append("Tokenizing few line of the file").Append("\n   - " + pr + "\\Toker\\Test.txt");
            s.Append("\n   - ").Append('-', 100);
            s.Append("\n   - LineNo.| Tokens").Append(' ', 44).Append("|Type State").Append("\n   - ").Append('-',100);
            return s.Append(DemoToker.testTokera((pr + "\\Toker\\Test.txt"),i)); 
        }
        StringBuilder Test4c()
        {
            StringBuilder st = new StringBuilder();
            st.Append("\n   - resetting SingleSpecialChar list and DoubleSpecial Char list by calling setter defined");
            st.Append("\n   - in TokenContext at line 44 & 45 to null. ");
            st.Append("\n   - the output for previous toker becomes : ");
            st.Append(Test4abdef(("../.."),1));
            return st;
        }
        StringBuilder Test5(string p)
        {
            StringBuilder s = new StringBuilder();
            string pr = Directory.GetParent(p).ToString();
            s.Append("\n   - Test File for requirement 5 is :-").Append("\n   - ");
            s.Append("Tokenizing second line of file").Append("\n   - " + pr + "\\Toker\\Test.txt");
            s.Append("\n   - ").Append('-', 100);
            s.Append("\n   - LineNo.| Token").Append(' ', 44).Append("|No. of tokens").Append("\n   - ").Append('-', 100);
            s.Append(DemoToker.test5to(pr + "\\Toker\\Test.txt"));
            s.Append("\n   - ").Append('-', 100);
            s.Append("\n   - Thus our tokenizer uses gettok() on line 105 in file toker.cs at location ..\\Toker\\Toker.cs.");
            s.Append("\n   - Satisfying Requirement 5.");
            return s;
        }
        StringBuilder Test6(string p)
        {
            StringBuilder s = new StringBuilder();
            string pr = Directory.GetParent(p).ToString();
            string prr = Directory.GetParent(p).ToString();
            s.Append("\n   - Test File for requirement 6 is :-").Append("\n   - " + pr + "\\Toker\\testSemi.txt");
            s.Append("\n   - The file has been divided as semiexpression.");
            s.Append("\n   - according to all the rules described in requirement 7,8");
            s.Append("\n   - This uses a semiex() function which extractes token unitll endcondition() is met.");
            s.Append("\n   - This function can be found in file").Append("\n   - "+prr +"\\SemiExpress\\Semi_Exp.cs");
            s.Append("\n   - On line No. 33.");
            s.Append("\n   - ").Append('-', 100) ;
            s.Append("\n   - SemiExpression").Append(' ', 47).Append("| Conditions");
            s.Append("\n   - ").Append('-', 100);
            s.Append(Demo_semi.Test_semi(pr + "\\Toker\\testSemi.txt"));

            return s;
        }
        StringBuilder Test7()
        {
            StringBuilder s = new StringBuilder();
            s.Append("\n   - As in above semiexpressions all the terminating conditions are met.");
            s.Append("\n   - This Takes care of Requirement 7");
            return s;
        }
        StringBuilder Test8()
        {
            StringBuilder s = new StringBuilder();
            s.Append("\n   - In Requirement 6, the Semi-Expression takes care of for loop condition.");
            s.Append("\n   - Thus satisfying requirement 8.");
            return s;
        }
        StringBuilder Test9()
        {
            StringBuilder s = new StringBuilder();
            s.Append("\n   - SemiExpression class extends the interface ITokCollection shown in requirement 3.");
            s.Append("\n   - This Interface have one function that is getSemi()");
            s.Append("\n   - SemiExpression class implements this function and for now returns one Semi-Expression");
            s.Append("\n   - This function can be found on line 89 of file :-");
            string p = "../..";
            string pr = Directory.GetParent(p).ToString();
            s.Append("\n   - " +pr + "\\SemiExpress\\Semi_Exp.cs");
            s.Append("\n   - This function Implements as follows");
            s.Append(Demo_semi.Test_inter(pr + "\\Toker\\testSemi.txt"));
            s.Append("\n   - Another SemiExpression from different file.");
            s.Append(Demo_semi.Test_inter(pr + "\\Toker\\Test.txt"));
            return s;
        }
        StringBuilder Test10()
        {
            StringBuilder s = new StringBuilder();
            s.Append("\n   - As all these test are runnig under the package Test_Executive.");
            s.Append("\n   - And this test executive call test function to automate the testing of all requirements.");
            s.Append("\n   - Hence, this takes care of requirement 10.");
            s.Append("\n   - To run indivisual toker and semiexpression test.");
            s.Append("\n   - Each package have its own test stub. Please run those.");

            return s;
        }
        public static void Main(string[] args)
        {
            int saveBufferWidth = Console.WindowWidth;
            int saveBufferHeight = Console.WindowHeight;
            Console.SetWindowSize(130, 45);
            int t = 104;
            StringBuilder st = new StringBuilder();
            st.Append("\n    CSE 681 - Software Modelling and Analysis").Append(' ', 32).Append("Date : "+ DateTime.Now) ;
            st.Append("\n=").Append('=',t);
            st.Append("\n").Append(' ',35).Append("Demonstrate Project 2 Requirements");
            st.Append("\n=").Append('=',t);
            Console.Write(st);
            TestExecutive te = new TestExecutive();
            StringBuilder msg = new StringBuilder();
            msg.Append("\n  - Reqirement 1 :");
            msg.Append("\n ").Append('-',t);
            msg.Append("\n  - As this program is running in c# windows console project,");
            msg.Append("\n  - this satisfies requirement 1 of using Visual Studio 2017 and c# windows console project.");
            msg.Append("\n  - the specific version being " + typeof(string).Assembly.ImageRuntimeVersion);
            msg.Append("\n ").Append('-', t);
            msg.Append("\n  - Requirement 2 :");
            msg.Append("\n ").Append('-', t);
            msg.Append("\n  - This file have following using directives:- ");
            msg.Append(te.Test2());
            msg.Append("\n  - Thus showing the use of System.IO and System.Text for all IO");
            msg.Append("\n ").Append('-', t);
            msg.Append("\n  - Requirement 3 :");
            msg.Append("\n ").Append('-', t);
            msg.Append("\n  - This Project have following Assemblies :- ");
            msg.Append(te.Test3());
            msg.Append("\n ").Append('-', t);
            msg.Append("\n  - Requirement 4-a) 4-b) 4-d) 4-e) 4-f)  ");
            msg.Append("\n ").Append('-', t);
            msg.Append(te.Test4abdef(("../.."),0));
            msg.Append("\n ").Append('-', t);
            msg.Append("\n  - Requirement 4-c)");
            msg.Append("\n ").Append('-', t);
            msg.Append(te.Test4c());
            msg.Append("\n ").Append('-', t);
            msg.Append("\n  - Requirement 5)");
            msg.Append("\n ").Append('-', t);
            
            msg.Append(te.Test5("../.."));
            msg.Append("\n ").Append('-', t);
            msg.Append("\n  - Requirement 6)");
            msg.Append("\n ").Append('-', t);
            msg.Append(te.Test6("../.."));
            msg.Append("\n ").Append('-', t);
            msg.Append("\n  - Requirement 7)");
            msg.Append("\n ").Append('-', t);
            msg.Append(te.Test7());
            msg.Append("\n ").Append('-', t);
            msg.Append("\n  - Requirement 8)");
            msg.Append("\n ").Append('-', t);
            msg.Append(te.Test8());
            msg.Append("\n ").Append('-', t);
            msg.Append("\n  - Requirement 9)");
            msg.Append("\n ").Append('-', t);
            msg.Append(te.Test9());
            msg.Append("\n ").Append('-', t);
            msg.Append("\n  - Requirement 10)");
            msg.Append("\n ").Append('-', t);
            msg.Append(te.Test10());
            msg.Append("\n ").Append('-', t);

          
            msg.Append("\n");
            Console.Write(msg);
            Console.WriteLine("\n\n");
            Console.SetCursorPosition(0,0);
            Console.ReadLine();
        }

    }
#endif
}