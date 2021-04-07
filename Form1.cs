﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Count_Words_in_a_Text_File__Part_1__Open_File_Dialog_Box20201030
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_chooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                textBox1.Text = File.ReadAllText(filePath);
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            textBox1.Clear();

            listBox1.DataSource = null;
            listBox1.Items.Clear();


            listView1.Items.Clear();
            //listView1.Clear();


        }


        private void button1CreateWordListNormal_Click(object sender, EventArgs e)
        {




            string allWords = textBox1.Text;
            string[] wordsArray = allWords.Split(' ', ',', '.', '!', '-');



            foreach (string word in wordsArray)
            {

                // only add a word if it is not yet in the list 
                if (!listBox1.Items.Contains(word))
                {
                    listBox1.Items.Add(word);
                }
            }




        }




        private void btn_createWordList_Click(object sender, EventArgs e)
        {
            string allWords = textBox1.Text;
            //string[] wordsArray = allWords.Split(' ', ',', '.', '!', '-');





            string[] wordsArray = allWords.Split(new[]
            {
                "#EXTINF:4.004", ",", "#EXTM3U", "#EXT-X-VERSION:5", "#EXT-X-PLAYLIST-TYPE:VOD"

                , "#EXT-X-MEDIA-SEQUENCE:0" , "#EXT-X-TARGETDURATION:5"
            }, StringSplitOptions.None);

            foreach (string word in wordsArray)
            {

                // only add a word if it is not yet in the list 
                if (word != "")
                {
                    listBox1.Items.Add(word);
                }
            }

        }




        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_sortWords_Click(object sender, EventArgs e)
        {
            listBox1.Sorted = true;
        }

        private void btn_countWords_Click(object sender, EventArgs e)
        {
            string allWords = textBox1.Text;
            string[] wordsArray = allWords.Split(' ', ',', '.', '!', '-');

            // change the array into a list. A list has more features than an array.
            // The list can use the find command to see if the word is already in the list

            List<WordCounter> wordCounters = new List<WordCounter>();

            // go through thre word array. If  the word is found in the list, add 1 to the frequency 
            // if the word is not found in the list. Add it to the list and its frequency to 1.

            foreach (string w in wordsArray)
            {

                WordCounter foundWord = wordCounters.Find(x => x.word == w);
                if (foundWord == null)
                {
                    // the word is not in the list yet . Add it.
                    wordCounters.Add(new WordCounter(w, 1));


                }
                else
                {
                    // the word if found in the list 
                    foundWord.frequency++;

                }

            }
            //   We are done with the loop. The list wordCounters should now have a list of all counted words.
            listView1.Columns.Add("Frequency", 70);
            listView1.Columns.Add("Word", 100);

            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.Sorting = SortOrder.Descending;



            foreach (WordCounter word in wordCounters)
            {
                String[] rowItem = new string[] { word.frequency.ToString("D5"), word.word };
                listView1.Items.Add(new ListViewItem(rowItem));

            }

            //OK


        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




        private void button_download_Click(object sender, EventArgs e)
        {


            try
            {
                string addressOfaLink = listBox1.Text;


                string filepath = @"C:\Users\Mike\source\repos\VideoDownloaderDeng\file1.mp4";

                //string filepath = string.Format(@"C:\Users\Mike\source\repos\VideoDownloaderDeng\{0}.mp4", Guid.NewGuid().ToString().Replace("-", ""));
                WebClient webClient = new WebClient();
                webClient.DownloadFile(addressOfaLink, filepath);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }





        }



        public string Reverse(string text)
        {
            char[] cArray = text.ToCharArray();
            string reverse = String.Empty;
            for (int i = cArray.Length - 1; i > -1; i--)
            {
                reverse += cArray[i];
            }
            return reverse;
        }

        private void buttonDownloadArray_Click(object sender, EventArgs e)
        {


            string allWords = textBox1.Text;
            string[] wordsArray = allWords.Split('\r');

            List<WordCounter> wordCounters = new List<WordCounter>();


            //Auto selected item, without it should manual select item by mouse 

            foreach (string downloadLink in wordsArray)
            {
                string cleanedDownloadLink = downloadLink.Replace("\n", "").Replace("\r", "");
                string cleaned;

                try { cleaned = cleanedDownloadLink.Remove(cleanedDownloadLink.Length - 3); }
                catch (Exception exception) { continue; }

                string reverseStr = Reverse(cleaned);

                try { reverseStr = reverseStr.Split(new string[] { "." }, StringSplitOptions.None)[1].Split(Convert.ToChar("/"))[0].Trim(); }
                catch (Exception exception) { continue; }
                reverseStr = Reverse(reverseStr);

                //string addressOfaLink = listBox1.Text;

                if (cleaned != "")
                {

                    string filepath = string.Format(@"C:\Mike\{0}.jpg", reverseStr.Replace("-", ""));

                    if (!Directory.Exists(filepath))
                    {
                        string newDirectory = @"C:\Mike";

                        Directory.CreateDirectory(newDirectory);
                    }

                    WebClient webClient = new WebClient();
                    webClient.DownloadFile(cleanedDownloadLink, filepath);
                }
            }
        }
    }
}
