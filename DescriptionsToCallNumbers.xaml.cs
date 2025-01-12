using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Threading;

namespace Prog3B
{
    // ST10090336 - Gilad Benbenishti PROG7312-Part Two
    /// <summary>
    /// Interaction logic for DescriptionsToCallNumbers.xaml
    /// </summary>
    public partial class DescriptionsToCallNumbers : Window
    {
        // Declaration of the Dictionary, Timer , Random, Score and Matches
        private List<DictionaryInfo> questions;
        private Random random = new Random();
        private int currentIndex = 0;
        private int userscore = 0;
        private bool MatchingDescriptionToCallNumber = true;
        private DispatcherTimer timer;
        private int remainingSeconds = 16;


        public DescriptionsToCallNumbers()
        {
            InitializeComponent();
            InitializeQuestions();
            DisplayNextQuestion();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            DisplayNextQuestion();
            timer.Start();
        }

        // Initialization for all questions - Dictionary 
        // CallNumber = pair.Value, Description = pair.Key (CHANGED FOR ALTERNATIVE QUESTIONS)
        private void InitializeQuestions()
        {
            questions = DictionaryData.Information
                .Select(pair => new DictionaryInfo { CallNumber = pair.Value, Description = pair.Key })
                .ToList();

            ShuffleQuestions();
        }

        // Question randomizer 
        private void ShuffleQuestions()
        {
            for (int i = 0; i < questions.Count; i++)
            {
                int randomIndex = random.Next(i, questions.Count);
                DictionaryInfo temp = questions[i];
                questions[i] = questions[randomIndex];
                questions[randomIndex] = temp;
            }
        }

        // Next question code for the rolling questions
        private void DisplayNextQuestion()
        {
            if (currentIndex < questions.Count)
            {
                DictionaryInfo nextQuestion = questions[currentIndex];

                if (MatchingDescriptionToCallNumber)
                {
                    SetQuestionText(nextQuestion.Description);
                    SetAnswers(GenerateRandomCallNumbers(nextQuestion.CallNumber));
                }
                else
                {
                    SetQuestionText(nextQuestion.CallNumber);
                    SetAnswers(GenerateRandomDescriptions(nextQuestion.Description));
                }

                currentIndex++;
                remainingSeconds = 16;
            }
            else
            {
                MessageBox.Show($" Nice Game! Your Score is: {userscore}/{questions.Count}");
            }
        }

        // Generation for randomization of all call numbers per round
        private List<string> GenerateRandomCallNumbers(string correctCallNumber)
        {
            List<string> answers = new List<string> { correctCallNumber };

            while (answers.Count < 10)
            {
                string randomCallNumber = questions[random.Next(questions.Count)].CallNumber;
                if (!answers.Contains(randomCallNumber))
                {
                    answers.Add(randomCallNumber);
                }
            }

            answers = answers.OrderBy(x => random.Next()).ToList();
            return answers;
        }

        // Generation for randomization of all Questions/Descriptions  per round
        private List<string> GenerateRandomDescriptions(string correctDescription)
        {
            List<string> answers = new List<string> { correctDescription };

            while (answers.Count < 4)
            {
                string randomDescription = questions[random.Next(questions.Count)].Description;
                if (!answers.Contains(randomDescription))
                {
                    answers.Add(randomDescription);
                }
            }

            answers = answers.OrderBy(x => random.Next()).ToList();
            return answers;
        }


        private void SetQuestionText(string text)
        {
            questionTextBlock.Text = text;
        }

        private void SetAnswers(List<string> answers)
        {
            answersListBox.ItemsSource = answers;
        }

        // Statement to check and validated the answers per question.
        private void CheckAnswer(string selectedAnswer)
        {
            DictionaryInfo currentQuestion = questions[currentIndex - 1];

            if (MatchingDescriptionToCallNumber)
            {
                if (selectedAnswer == currentQuestion.CallNumber)
                {
                    userscore++;
                }
            }
            else
            {
                if (selectedAnswer == currentQuestion.Description)
                {
                    userscore++;
                }
            }

            DisplayNextQuestion();
        }


        // UserTimer code, New  Gamification feature
        private void Timer_Tick(object sender, EventArgs e)
        {
            remainingSeconds--;
            if (remainingSeconds <= 0)
            {
                timer.Stop();
                DisplayNextQuestion();
            }
            else
            {
                timerTextBlock.Text = $"Time left: {remainingSeconds} seconds";
            }
        }


        private void answersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                string selectedAnswer = e.AddedItems[0].ToString();
                CheckAnswer(selectedAnswer);
            }
        }

        // Code to generate the quiz to re-start for additional rounds.
        private void Try_Click(object sender, RoutedEventArgs e)
        {
            currentIndex = 0;
            userscore = 0;
            ShuffleQuestions();
            DisplayNextQuestion();
        }

        // Home page redirection
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objIncomePage = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objIncomePage.Show();
        }

        // Identifying Areas page redirection
        private void NQT_Click(object sender, RoutedEventArgs e)
        {
            Identifying_Areas objIncomePage = new Identifying_Areas();
            this.Visibility = Visibility.Hidden;
            objIncomePage.Show();
        }
    }
}
