using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Prog3B
{
    /// ST10090336 - Gilad Benbenishti  
    /// PROG7312 - POE 
    ///  FindingCallNumbers 
    public partial class FindingCallNumbers : Window
    {

        static int userscore = 0;
        public static Random random = new Random();
        static TreeNode<DeweyDecimalClass> root = new TreeNode<DeweyDecimalClass>(new DeweyDecimalClass("root", "root"));

        public static int Mode = 0;

        static List<DeweyDecimalClass> QA = new List<DeweyDecimalClass>();
        static List<int> QAindexes = new List<int>();

        public static int selectedItem;

        public FindingCallNumbers()
        {
            InitializeComponent();

            btnEnd.IsEnabled = false;
            rbOne.Visibility = Visibility.Hidden;
            rbTwo.Visibility = Visibility.Hidden;
            rbThr.Visibility = Visibility.Hidden;
            rbFou.Visibility = Visibility.Hidden;

            //initialises tree from file data
            InitTree();

        }

        // Button to check and move onto the next question
        private void btnEnd_Click(object sender, RoutedEventArgs e)
        {
            if (rbOne.IsChecked == true || rbTwo.IsChecked == true || rbThr.IsChecked == true || rbFou.IsChecked == true)
            {
                CheckAnswer();
            }
            else
            {
                MessageBox.Show("Please Select one option");
            }

        }
        // Start button for a new game 
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btnEnd.IsEnabled = false;

            // calculates and displays possible answers
            CalcQA();
            PopAnswers();

            btnEnd.IsEnabled = true;


        }


        // Checks user answer
        private void CheckAnswer()
        {
            if (Mode == 0)
            {
                if (selectedItem == QAindexes[0])
                {
                    userscore += 10;
                    MessageBox.Show("Correct Item Selected!, Next Question.");
                    Mode = 1;
                    lblScore.Content = "Your score:  " + userscore;
                    PopAnswers();
                }
                else
                {
                    MessageBox.Show("Incorrect Item Selected! Press 'Ok' to start new game");
                    Mode = 0;
                    userscore = 0;
                    lblScore.Content = "Your score:  " + userscore;
                    NewRound();
                }


            }
            else if (Mode == 1)
            {
                if (selectedItem == QAindexes[0])
                {
                    userscore += 10;
                    MessageBox.Show("Correct Item Selected! Next Question.");
                    Mode = 2;
                    lblScore.Content = "Your score:  " + userscore;
                    PopAnswers();
                }
                else
                {
                    Mode = 0;
                    userscore = 0;
                    MessageBox.Show("Incorrect Item Selected! Press 'Ok' to start new game");
                    lblScore.Content = "Your score:  " + userscore;
                    NewRound();
                }
            }
            else if (Mode == 2)
            {
                if (selectedItem == QAindexes[0])
                {
                    userscore += 10;
                    MessageBox.Show("Correct Item Selected!");
                    Mode = 0;
                    lblScore.Content = "Your score:  " + userscore;
                    NewRound();
                }
                else
                {
                    Mode = 0;
                    userscore = 0;
                    MessageBox.Show("Incorrect Item Selected! Press 'Ok' to start new game");
                    lblScore.Content = "Your score:  " + userscore;
                    NewRound();
                }
            }

        }

        //Determines the correct answer and question  for the game
        public void CalcQA()
        {
            QA.Clear();
            QAindexes.Clear();
            int lvl1i = GetRandomNumber(0, root.GetCount());
            TreeNode<DeweyDecimalClass> lvl1 = root[lvl1i];
            QA.Add(lvl1.Value);
            QAindexes.Add(lvl1i);

            int lvl2i = GetRandomNumber(0, lvl1.GetCount());
            TreeNode<DeweyDecimalClass> lvl2 = lvl1[lvl2i];
            QA.Add(lvl2.Value);

            int lvl3i = GetRandomNumber(0, lvl2.GetCount());
            TreeNode<DeweyDecimalClass> lvl3 = lvl2[lvl3i];
            QA.Add(lvl3.Value);

            lblQ.Content = QA[2].callDesc;

        }

        // Calculates the  possible answers, and displays them in their buttons - per level 
        public void PopAnswers()
        {
            List<int> indexes = new List<int>();
            List<DeweyDecimalClass> ans = new List<DeweyDecimalClass>();
            List<TreeNode<DeweyDecimalClass>> sourceL1;

            if (Mode == 0)
            {
                ans.Add(QA[0]);
                sourceL1 = root.Children;
                for (int i = 0; i < 3; i++)
                {
                    int index = GetRandomNumber(0, sourceL1.Count());
                    if (index == QAindexes[0])
                    {
                        i--;
                        continue;
                    }

                    if (!indexes.Contains(index))
                    {
                        indexes.Add(index);
                        ans.Add(sourceL1[index].Value);
                    }
                    else i--;
                }

                ans = ans.OrderBy(x => x.callNum).ToList();
                QAindexes[0] = ans.IndexOf(QA[0]);

                rbOne.Content = ans[0].ToString();
                rbTwo.Content = ans[1].ToString();
                rbThr.Content = ans[2].ToString();
                rbFou.Content = ans[3].ToString();

                rbOne.Visibility = Visibility.Visible;
                rbTwo.Visibility = Visibility.Visible;
                rbThr.Visibility = Visibility.Visible;
                rbFou.Visibility = Visibility.Visible;

            }
            else if (Mode == 1)
            {
                List<string> temp = new List<string>();
                List<TreeNode<DeweyDecimalClass>> sourceL2 = new List<TreeNode<DeweyDecimalClass>>();
                List<DeweyDecimalClass> sources = new List<DeweyDecimalClass>();
                ans.Add(QA[1]);
                temp.Add(QA[1].callNum);
                sourceL1 = root.Children;
                foreach (var child in sourceL1)
                {
                    sourceL2.AddRange(child.Children.ToList());

                }
                foreach (var item in sourceL2)
                {
                    sources.Add(item.Value);
                }

                for (int i = 0; i < 3; i++)
                {
                    int index = GetRandomNumber(0, sources.Count());
                    DeweyDecimalClass deweydecimalclass = sources[index];

                    if (temp.Contains(deweydecimalclass.callNum))
                    {
                        i--;
                    }
                    else if (!ans.Contains(deweydecimalclass))
                    {
                        ans.Add(deweydecimalclass);
                        temp.Add(deweydecimalclass.callNum);
                    }

                }

                ans = ans.OrderBy(x => x.callNum).ToList();

                QAindexes[0] = ans.IndexOf(QA[1]);

                rbOne.Content = ans[0].ToString();
                rbTwo.Content = ans[1].ToString();
                rbThr.Content = ans[2].ToString();
                rbFou.Content = ans[3].ToString();

            }
            else if (Mode == 2)
            {
                List<string> temp = new List<string>();
                ans.Add(QA[2]);
                temp.Add(QA[2].callNum);
                List<TreeNode<DeweyDecimalClass>> lvl1 = root.Children;
                List<TreeNode<DeweyDecimalClass>> lvl2 = new List<TreeNode<DeweyDecimalClass>>();
                List<TreeNode<DeweyDecimalClass>> lvl3 = new List<TreeNode<DeweyDecimalClass>>();
                foreach (var l1 in lvl1)
                {
                    lvl2.AddRange(l1.Children);
                }
                foreach (var l2 in lvl2)
                {
                    lvl3.AddRange(l2.Children);
                }
                for (int i = 0; i < 3; i++)
                {
                    int index = GetRandomNumber(0, lvl3.Count());
                    DeweyDecimalClass deweydecimalclass = lvl3[index].Value;
                    if (temp.Contains(deweydecimalclass.callNum))
                    {
                        i--;
                    }
                    else if (!ans.Contains(deweydecimalclass))
                    {
                        ans.Add(deweydecimalclass);
                        temp.Add(deweydecimalclass.callNum);
                    }

                }

                ans = ans.OrderBy(x => x.callNum).ToList();

                QAindexes[0] = ans.IndexOf(QA[2]);

                rbOne.Content = ans[0].callNum.ToString();
                rbTwo.Content = ans[1].callNum.ToString();
                rbThr.Content = ans[2].callNum.ToString();
                rbFou.Content = ans[3].callNum.ToString();
            }
        }

        //new round 
        public void NewRound()
        {
            CalcQA();
            PopAnswers();
        }


        //generic method to get randomised number within specified parameters
        public static int GetRandomNumber(int min, int max)
        {
            
            int d = random.Next(min, max);
            return d;
        }

        //Reads data from txtfile to populate tree
        public void InitTree()
        {
            string dir = Environment.CurrentDirectory;
            string filePath = Directory.GetParent(dir).Parent.Parent.FullName + "\\DeweyCallNumbers.txt";
            string[] sysLines = File.ReadAllLines(filePath);

            List<String> tempL1 = new List<String>();
            List<String> tempL2 = new List<String>();

            foreach (string line in sysLines)
            {
                string[] objs = line.Split('/');

                DeweyDecimalClass call1 = splitString(objs[0]);
                TreeNode<DeweyDecimalClass> nodeLvl1;
                Console.WriteLine(call1.callNum + " " + call1.callDesc);

                if (root.Children.Count() == 0)
                {
                    Console.WriteLine("Node added becuase no children exist");
                    nodeLvl1 = root.AddChild(call1);
                    tempL1.Add(call1.callNum);

                }
                else
                {
                    if (tempL1.Contains(call1.callNum))
                    {
                        nodeLvl1 = FindNode(root, call1);
                        Console.WriteLine("Node found");
                    }
                    else
                    {
                        tempL1.Add(call1.callNum);
                        nodeLvl1 = root.AddChild(call1);
                        Console.WriteLine("Node added becuase does not exist");
                    }

                }

                DeweyDecimalClass call2 = splitString(objs[1]);
                TreeNode<DeweyDecimalClass> nodeLvl2;

                if (nodeLvl1 == null)
                {
                    nodeLvl1 = FindNode(root, call1);
                }

                if (!tempL2.Contains(call2.callNum))
                {
                    nodeLvl2 = nodeLvl1.AddChild(call2);
                    tempL2.Add(call2.callNum);
                }
                else
                {
                    nodeLvl2 = FindNode(nodeLvl1, call2);
                }

                DeweyDecimalClass call3 = splitString(objs[2]);
                TreeNode<DeweyDecimalClass> nodeLvl3;

                nodeLvl3 = nodeLvl2.AddChild(call3);
            }
        }
        //Method to split string into call number and call description.
        public static DeweyDecimalClass splitString(string line)
        {
            string[] div = line.Split("-");

            return new DeweyDecimalClass(div[0], div[1]);
        }

        public static TreeNode<DeweyDecimalClass> FindNode(TreeNode<DeweyDecimalClass> parent, DeweyDecimalClass search)
        {
            foreach (var node in parent.Children)
            {
                if (node.Value.callNum.Equals(search.callNum))
                    return node;
            }

            return null;
        }

        //Radio Button One 
        private void rbOne_Checked(object sender, RoutedEventArgs e)
        {
            selectedItem = 0;
        }

        //Radio Button Two
        private void rbTwo_Checked(object sender, RoutedEventArgs e)
        {
            selectedItem = 1;
        }

        //Radio Button Three

        private void rbThr_Checked(object sender, RoutedEventArgs e)
        {
            selectedItem = 2;
        }

        //Radio Button Four 
        private void rbFou_Checked(object sender, RoutedEventArgs e)
        {
            selectedItem = 3;
        }

        // Home Button to redirect user to the home page 
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objIncomePage = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objIncomePage.Show();
        }
    }
}
