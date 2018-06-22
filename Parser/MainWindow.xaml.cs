using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Parser
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string user_match = "@</span>[a-zA-Z0-9]{1,}"; //регулярное выражение для логинов


            string site = EnterSite.Text;
            TextBox tb;
            tb = Logins;


            List<string> logins = new List<string>();       //набор найденных
            StringBuilder sbuild = new StringBuilder();
            Regex regex = new Regex(user_match, RegexOptions.Compiled | RegexOptions.IgnoreCase); //RegexOptions.Compiled для того, чтобы дольше запускалось, но быстрее парсилось
            System.Net.WebClient wc = new System.Net.WebClient();
      
                try
                {
                    String Response = wc.DownloadString(site);

                    MatchCollection matches = regex.Matches(Response);      //набор совпадений

                    foreach (Match match in matches)
                    {
                        string anchor = match.Value.ToString();             //по этим совпадениям
                        anchor = anchor.Substring(anchor.IndexOf('>') + 1); //обрезка лишнего
                        if (!logins.Contains(anchor))                       //проверка есть ли уже записанным
                        {
                            logins.Add(anchor);                             //если нет, то дописать
                        }
                    }
                    foreach (string anchor in logins)
                    {
                        sbuild.Append(anchor + "\r\n");                     //и дописать в вывод
                    }   
                    tb.Text = sbuild.ToString();              //вывести и очистить память
                    sbuild.Clear();
                    logins.Clear();
                }
                catch (ArgumentException)
                {
                    tb.Text = " Empty Field! ";
                    tb.Text = tb.Text;
                    sbuild.Clear();
                    logins.Clear();
                }
                catch (System.Net.WebException)
                {
                    tb.Text = "Wrong!";
                    tb.Text = tb.Text;
                    sbuild.Clear();
                    logins.Clear();
                }
            }
    }
}
