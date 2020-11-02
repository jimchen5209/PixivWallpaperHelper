﻿using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using PixivWallpaperHelper.Pixiv.OAuth;
using System.IO;
using PixivWallpaperHelper.Utils;
using System.Diagnostics;
using System.ComponentModel;

namespace PixivWallpaperHelper
{
    public partial class MainForm : Form
    {
        private string CurrentImagePath = "";
        private string Url = "";
        private readonly SettingForm SettingForm;
        private readonly WallpaperFetcher Wallpaper;
        public MainForm()
        {
            InitializeComponent();
            SettingForm = new SettingForm();
            Wallpaper = new WallpaperFetcher();
        }

        private static readonly string AppGuid = "7bcbe405-0325-4f8d-8527-afd151d13ff4";

        private void MainForm_Load(object sender, EventArgs e)
        {
            Mutex mutex = new Mutex(false, AppGuid);
            if (!mutex.WaitOne(0, false))
            {
                MessageBox.Show("Instance already running");
                Close();
            }
            ChangeThumbnail();
            RegisterEvent();

            backgroundWorker1.DoWork += new DoWorkEventHandler(FetchEvent);
            if (!backgroundWorker1.IsBusy) backgroundWorker1.RunWorkerAsync();
        }

        private void Form1_Click(object sender, EventArgs e) {
            menuStrip1.Visible = !menuStrip1.Visible;
            titlePanel.Visible = !titlePanel.Visible;
        }
        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            if (Width < 768) { Width = 768; }
            if (Height < 432) { Height = 432; }
        }

        private void WallpaperRefreshTimer_Tick(object sender, EventArgs e)
        {
            ChangeThumbnail();
        }

        private void SettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ = SettingForm.ShowDialog(this);
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeThumbnail();
        }

        private void MenuStrip1_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush brush = new LinearGradientBrush(
                menuStrip1.ClientRectangle,
                Color.FromArgb(204, 0, 0, 0),
                Color.FromArgb(0, 0, 0, 0),
                90F
            );
            e.Graphics.FillRectangle(brush, menuStrip1.ClientRectangle);
        }
        private void TitlePanel_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush brush = new LinearGradientBrush(
                titlePanel.ClientRectangle,
                Color.FromArgb(0, 0, 0, 0),
                Color.FromArgb(204, 0, 0, 0),
                90F
            );
            e.Graphics.FillRectangle(brush, titlePanel.ClientRectangle);
        }

        private void ChangeThumbnail()
        {
            if (IsWallpaperColorOnly())
            {
                if (BackgroundImage != null)
                {
                    BackgroundImage.Dispose();
                    BackgroundImage = null;
                    CurrentImagePath = "";
                };
                BackColor = GetWallpaperColor();
                Url = "";
                titleLabel.Text = "純色桌布";
                authorLabel.Text = "這似乎不是由本程式自動下載的相片輪播圖庫，請檢查桌布設定";
            }else
            {
                string newPath = GetCurrentWallpaperPath();
                if (!newPath.Equals(CurrentImagePath))
                {
                    BackColor = Color.Black;
                    CurrentImagePath = newPath;
                    if (!File.Exists(newPath))
                    {
                        titleLabel.Text = "無法載入預覽";
                        authorLabel.Text = "桌布原始圖片似乎被刪除了";
                        Url = "";
                        return;
                    }
                    if (BackgroundImage != null) { BackgroundImage.Dispose(); }
                    BackgroundImage = System.Drawing.Image.FromFile(newPath);
                    switch (Wallpaper.GetWallpaperInfo(newPath, out LocalArtwork localArtwork))
                    {
                        case WallPaperInfoStatus.Success:
                            titleLabel.Text = localArtwork.Title;
                            authorLabel.Text = localArtwork.Author;
                            Url = localArtwork.WebUrl;
                            break;
                        case WallPaperInfoStatus.NotFound:
                            titleLabel.Text = "未命名的桌布";
                            authorLabel.Text = "找不到此桌布的資訊";
                            Url = "";
                            break;
                        case WallPaperInfoStatus.PathInvalid:
                            titleLabel.Text = "未命名的桌布";
                            authorLabel.Text = "這似乎不是由本程式自動下載的相片輪播圖庫，請檢查桌布設定";
                            Url = "";
                            break;
                    }
                }
            }
        }

        private bool IsWallpaperColorOnly()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop");
            if (key != null)
            {
                string path = (string)key.GetValue("WallPaper");
                return path.Equals("");
            }
            else {
                return false;
            }
        }

        private Color GetWallpaperColor()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Colors");
            if (key != null)
            {
                string colorRaw = (string)key.GetValue("Background");
                string[] color = colorRaw.Split(' ');
                return Color.FromArgb(
                    Convert.ToInt32(color[0]),
                    Convert.ToInt32(color[1]),
                    Convert.ToInt32(color[2])
                );
            }
            else
            {
                return Color.FromArgb(0, 0, 0);
            }
        }

        private string GetCurrentWallpaperPath()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop");
            if (key != null)
            {
                byte[] encodedPath = (byte[])key.GetValue("TranscodedImageCache");
                char[] chars = new char[(encodedPath.Length - 24) / sizeof(char)];
                Buffer.BlockCopy(encodedPath, 24, chars, 0, encodedPath.Length - 24);
                string str = new string(chars);
                return str.Split('\0')[0];
            }
            else
            {
                return "";
            }
        }

        private void RegisterEvent()
        {
            menuStrip1.Paint += new PaintEventHandler(MenuStrip1_Paint);
            titlePanel.Paint += new PaintEventHandler(TitlePanel_Paint);
            Click += new EventHandler(Form1_Click);
            ResizeEnd += new EventHandler(Form1_ResizeEnd);
        }

        private void TitleLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Url != "")
            {
                _ = Process.Start(Url);
            }
        }

        private void FetchEvent(object sender, DoWorkEventArgs e)
        {
            Wallpaper.FetchWallpaper();
        }
    }
}
