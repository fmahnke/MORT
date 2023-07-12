﻿using Microsoft.VisualBasic.Logging;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Updater
{
    public partial class Updater : Form
    {
        private string newVersion = "";
        private string url = "";
        private string info = "";


        public void OpenURL(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch
            {

            }

        }

        public Updater(string newVersion, string url, string info)
        {
            InitializeComponent();
            this.newVersion = newVersion;
            this.url = url;
            this.info = info;
        }

        public void DoDownload()
        {
            try
            {
                lbStatus.Text = "다운로드 준비중";
                using (var client = new WebClient())
                {
                    Uri uri = new Uri(url);
                    client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback4);
                    client.DownloadFileAsync(uri, "MORT_backup.exe"); 
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadConfig);
                }
            }
            catch
            {

            }
        }

        private async Task AfterAsync(bool isError)
        {
            await Task.Delay(TimeSpan.FromSeconds(2.5f));
            if (!isError)
            {
                try
                {
                    RemoveOldFile("MORT.exe", "MORT_backup.exe", "MORT_2.exe");
                    RemoveOldFile("MORT.dll.config", "MORT_backup.dll.config", "MORT_2.dll.config");
                }
                catch (Exception excep)
                {
                    Console.WriteLine(excep);
                    isError = true;
                    lbStatus.Text = "오류가 발생했습니다! 수동 업데이트를 해주시기 바랍니다";
                }
            }

            if (!isError)
            {
                lbStatus.Text = "업데이트 완료!" + System.Environment.NewLine + "MORT를 다시 실행합니다";


                if (DialogResult.OK == MessageBox.Show("업데이트를 완료했습니다.\r\n업데이트 내역을 확인해 보시겠습니까?", "업데이트 완료!", MessageBoxButtons.OKCancel))
                {
                    try
                    {
                        OpenURL(info);
                    }
                    catch { }
                    DoClose();
                }
                else
                {
                    DoClose();
                }
            }
            else
            {
                if (DialogResult.OK == MessageBox.Show("업데이트를 실패했습니다.\r\n동 업데이트를 해주시기 바랍니다\r\n\r\n다운로드 페이지로 이동하시겠습니까?", "업데이트 실패!", MessageBoxButtons.OKCancel))
                {
                    try
                    {
                        OpenURL(info);
                    }
                    catch { }

                    if (Application.MessageLoop)
                        Application.Exit();
                    else
                        Environment.Exit(1);
                }
            }
        }

        private void DoDownloadAfter(object sender, AsyncCompletedEventArgs e)
        {
            bool isError = false;
            if (e.Cancelled || e.Error != null)
            {
                isError = true;
            }

            var result = Task.Run(async () => await AfterAsync(isError));

        }

        private void DownloadConfig(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                lbStatus.Text = "설정 파일 다운로드";
                using (var client = new WebClient())
                {
                    string configUrl = url.Replace("MORT.exe", "MORT.dll.config");
                    Uri uri = new Uri(configUrl);
                    client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback4);
                    client.DownloadFileAsync(uri, "MORT_backup.dll.config");
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(DoDownloadAfter);
                }
            }
            catch
            {

            }

        }

        private void RemoveOldFile(string originalFile, string downloadFile, string backupFile)
        {

            if (File.Exists(backupFile))
            {
                File.Delete(backupFile);
                Console.WriteLine("Delete backup - " + backupFile);
            }

            if(WaitForFile(originalFile))
            {
                File.Move(originalFile, backupFile);
                Console.WriteLine("originalFile to back - " + originalFile);
            }
            else
            {
                throw new Exception("File still locked");
            }


            if (WaitForFile(downloadFile))
            {
                File.Move(downloadFile, originalFile);
                Console.WriteLine("downloadFile to originalFile - " + downloadFile);
            }
            else
            {
                throw new Exception("File still locked");
            }        

        }

        private bool WaitForFile(string fullPath)
        {
            int numTries = 0;
            while (true)
            {
                ++numTries;
                try
                {
                    // Attempt to open the file exclusively.
                    using (FileStream fs = new FileStream(fullPath,
                        FileMode.Open, FileAccess.ReadWrite,
                        FileShare.None, 100))
                    {
                        fs.ReadByte();

                        // If we got this far the file is ready
                        break;
                    }
                }
                catch (Exception ex)
                {

                    if (numTries > 10)
                    {
                        return false;
                    }

                    // Wait for the lock to be released
                    System.Threading.Thread.Sleep(500);
                }
            }

            return true;
        }

        private async void DoClose()
        {
            Process.Start("MORT.exe");

            if (Application.MessageLoop)
                Application.Exit();
            else
                Environment.Exit(1);
        }

        private void DownloadProgressCallback4(object sender, DownloadProgressChangedEventArgs e)
        {
            // Displays the operation identifier, and the transfer progress.
            Console.WriteLine("{0}    downloaded {1} of {2} bytes. {3} % complete...",
                (string)e.UserState,
                e.BytesReceived,
                e.TotalBytesToReceive,
                e.ProgressPercentage);

            lbStatus.Text = "받는 중 : " + e.ProgressPercentage.ToString() + "%";
            progressBar1.Value = e.ProgressPercentage;

          
        }

        private void Updater_Load(object sender, EventArgs e)
        {
            DoDownload();
        }
    }
}