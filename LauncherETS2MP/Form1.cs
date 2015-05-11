using System;
using System.IO;
using Ionic.Zip;
using System.Net;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;

namespace LauncherETS2MP
{
    public partial class Form1 : Form
    {
        private int formPosX = 0; private int formPosY = 0;
        string ets2mpLogin, ets2mpID, ets2mpVersion, ets2mpLink;
        string pattern, regInstallLocation, regInstallDir;
        bool checkState = false;
        Regex regex;
        Uri dwnldUrl            = new Uri("http://ets2mp.com/index.php?action=getMod");
        CookieContainer cookies = new CookieContainer();

        public void saveLogs(string log)
        {
            Properties.Settings.Default.logs.Add(String.Format("[{0}:{1:D2}] {2}", DateTime.Now.Hour, DateTime.Now.Minute, log));
        }

        public Form1()
        {
            InitializeComponent();
            saveLogs("InitializeComponent");
            cookies = ReadCookiesFromDisk("cookies.dat");

            if ((!check()) && (Properties.Settings.Default.email != "") && (Properties.Settings.Default.password != ""))
            {
                if (!auth())
                    resultLabel.Text = "Error authentication";
            }

            Microsoft.Win32.RegistryKey readKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\ETS2MP");
            if (readKey != null)
            {
                regInstallLocation  = (string)readKey.GetValue("InstallLocation");
                regInstallDir       = (string)readKey.GetValue("InstallDir");
            }
            else
            {
                saveLogs("Game not found!");
            }

            System.Drawing.Drawing2D.GraphicsPath Button_Path = new System.Drawing.Drawing2D.GraphicsPath();
            Button_Path.AddEllipse(0, 0, btnPlay.Width, btnPlay.Height);
            System.Drawing.Region Button_Region = new System.Drawing.Region(Button_Path);
            this.btnPlay.Region = Button_Region;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            runGame();
        }

        private void runGame()
        {
            if ((runSteam()) && (check()) && (gameVersion() == ets2mpVersion))
            {
                resultLabel.Text = "Run ETS2 Multiplayer";
                Process ets2mp = new Process();
                ets2mp.StartInfo.FileName = regInstallDir + "\\launcher.exe";
                ets2mp.StartInfo.Arguments = "-nointro";
                ets2mp.Start();
            }
            else if (gameVersion() != ets2mpVersion)
            {
                saveLogs("Version failed");
                if (check())
                    backgroundDownload.RunWorkerAsync();
                else
                    auth();
            }
            else if (!check())
            {
                auth();
            }
        }

        private bool runSteam()
        {
            string steamPath;
            Microsoft.Win32.RegistryKey readKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Valve\\Steam");

            if (readKey != null)
            {
                steamPath = (string)readKey.GetValue("InstallPath");
                var steamProc = Process.GetProcessesByName("Steam");
                if (steamProc.Length == 0)
                {
                    Process.Start(steamPath + "\\Steam.exe");
                    resultLabel.Text = "Run Steam service";
                    // Вешаем программу вечными циклами, которые заканчиваются только после полной зарузки стима
                    // Либо проверка окончания работы steamerrorreporter, либо проверка запущенных двух steamwebhelper
                    if (Properties.Settings.Default.steamReporter)
                    {
                        while (Process.GetProcessesByName("steamerrorreporter").Length == 0)
                            steamPath = steamPath;
                        while (Process.GetProcessesByName("steamerrorreporter").Length != 0)
                            steamPath = steamPath;
                    }
                    else
                    {
                        while (Process.GetProcessesByName("steamwebhelper").Length <= 1)
                            steamPath = steamPath;
                    }
                }
                return true;
            }
            else
            {
                resultLabel.Text = "Steam not found!";
                saveLogs("Steam not found!");
            }

            return false;
        }

        private string gameVersion()
        {
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(regInstallDir + "\\launcher.exe");
            string ver = String.Format("{0}.{1}.{2} R{3}",
                                        versionInfo.ProductMajorPart.ToString(),
                                        versionInfo.ProductMinorPart.ToString(),
                                        versionInfo.ProductBuildPart.ToString(),
                                        versionInfo.ProductPrivatePart.ToString()
            );
            return ver;
        }

        private bool auth()
        {
            saveLogs("Start auth");
            string data         =   "mail=" + Properties.Settings.Default.email +
                                    "&password=" + Properties.Settings.Default.password +
                                    "&action=doLogin";

            byte[] byteData = Encoding.UTF8.GetBytes(data);

            HttpWebRequest request  = (HttpWebRequest)WebRequest.Create("http://ets2mp.com/index.php?page=login");
            request.Method          = "POST";
            request.UserAgent       = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
            request.CookieContainer = cookies;
            request.ContentType     = "application/x-www-form-urlencoded";
            request.ContentLength   = byteData.Length;
            request.GetRequestStream().Write(byteData, 0, byteData.Length);
            request.GetRequestStream().Close();

            HttpWebResponse res     = (HttpWebResponse)request.GetResponse();

            string response = new StreamReader(res.GetResponseStream(), Encoding.UTF8).ReadToEnd();

            if (check(response))
                return true;
            else
                return false;
        }

        private bool check(string response = null)
        {
            saveLogs("Start check");
            if (response == null)
            {
                HttpWebRequest request  = (HttpWebRequest)WebRequest.Create("http://ets2mp.com");
                request.UserAgent       = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
                request.CookieContainer = cookies;

                HttpWebResponse res = (HttpWebResponse)request.GetResponse();

                response = new StreamReader(res.GetResponseStream(), Encoding.UTF8).ReadToEnd();
            }

            pattern                     = "<a.*page=profile&id=(?<id>.*?)\">(?<login>.+?)</a>";
            regex                       = new Regex(pattern, RegexOptions.ExplicitCapture);
            MatchCollection loginPanel  = regex.Matches(response);

            pattern                     = "<div class=\"version\">.*: (?<ver>.*?)<a href=\"(?<url>.*?)\" class=\"button\">.*</a></div>";
            regex                       = new Regex(pattern, RegexOptions.ExplicitCapture);
            MatchCollection verPanel    = regex.Matches(response);

            ets2mpVersion = verPanel[0].Groups["ver"].Value;
            ets2mpLink = verPanel[0].Groups["url"].Value;

            if (loginPanel.Count > 0)
            {
                saveLogs("User found!");
                ets2mpID        = loginPanel[0].Groups["id"].Value;
                ets2mpLogin     = loginPanel[0].Groups["login"].Value;

                checkState = true;
                return true;
            }
            else
                return false;
        }

        public void WriteCookiesToDisk(string file, CookieContainer cookieJar)
        {
            using (Stream stream = File.Create(file))
            {
                try
                {
                    saveLogs("Writing cookies to disk... ");
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, cookieJar);
                    saveLogs("Done.");
                }
                catch (Exception e)
                {
                    saveLogs("Problem writing cookies to disk: " + e.GetType());
                }
            }
        }

        public CookieContainer ReadCookiesFromDisk(string file)
        {

            try
            {
                using (Stream stream = File.Open(file, FileMode.Open))
                {
                    saveLogs("Reading cookies from disk... ");
                    BinaryFormatter formatter = new BinaryFormatter();
                    saveLogs("Done.");
                    return (CookieContainer)formatter.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                saveLogs("Problem reading cookies from disk: " + e.GetType());
                return new CookieContainer();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            System.Drawing.Point position = new System.Drawing.Point(this.Location.X, this.Location.Y);
            Properties.Settings.Default.mainPosition = position;
            Properties.Settings.Default.Save();
            Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                trayIcon.Visible = true;
            }
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Location = Properties.Settings.Default.mainPosition;
            if (ets2mpLogin != null)
                lblUser.Text = ets2mpLogin;
            if (ets2mpVersion != null)
                lblCurVersion.Text = ets2mpVersion;
            else
                lblCurVersion.Text = "Bad request :(";
            string curVer = gameVersion();
            if (ets2mpVersion != curVer)
            {
                lblVersion.Text = gameVersion();
                lblVersion.Visible = true;
                label4.Visible = true;
            }
            
            if (gameVersion() != ets2mpVersion)
            {
                resultLabel.Text = "Need update!";
                btnPlay.Text = "Update";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            WriteCookiesToDisk("cookies.dat", cookies);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            formPosX = e.X; formPosY = e.Y;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                this.Location = new System.Drawing.Point(this.Location.X + (e.X - formPosX), this.Location.Y + (e.Y - formPosY));
        }

        private void lblUser_Click(object sender, EventArgs e)
        {
            if (checkState)
                System.Diagnostics.Process.Start("http://ets2mp.com/index.php?page=profile&id=" + ets2mpID);
            else
            {
                userLogin login = new userLogin();
                login.ShowDialog();
            }
        }

        private void lblUser_MouseMove(object sender, MouseEventArgs e)
        {
            lblUser.ForeColor = System.Drawing.Color.BurlyWood;
        }

        private void lblUser_MouseLeave(object sender, EventArgs e)
        {
            lblUser.ForeColor = System.Drawing.Color.White;
        }

        private void resultLabel_DoubleClick(object sender, EventArgs e)
        {

            showLogs logs = new showLogs();
            logs.ShowDialog();
        }

        private void backgroundDownload_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            Directory.CreateDirectory("_tmp");
            saveLogs("Start download");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(dwnldUrl);
            request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
            request.CookieContainer = cookies;
            HttpWebResponse res = (HttpWebResponse)request.GetResponse();
            Stream str = res.GetResponseStream();

            byte[] inBuf = new byte[1024];
            int bytesReadTotal = 0;
            FileStream fstr = new FileStream("_tmp/ETS2MP.zip", FileMode.Create, FileAccess.Write);
            while (true)
            {
                worker.ReportProgress((Int32)bytesReadTotal * 100 / (Int32)res.ContentLength, bytesReadTotal.ToString() + "/" + res.ContentLength.ToString());

                int n = str.Read(inBuf, 0, 1024);
                if ((n == 0) || (n == -1))
                {
                    break;
                }
                fstr.Write(inBuf, 0, n);
                bytesReadTotal += n;
            }
            str.Close();
            fstr.Close();
        }

        private void backgroundDownload_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            resultLabel.Text = "Downloading file: " + e.UserState.ToString();
        }

        private void backgroundDownload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            resultLabel.Text = "";
            backgroundExtract.RunWorkerAsync();
            saveLogs("File downloaded");
        }

        private void backgroundExtract_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            worker.ReportProgress(35);
            saveLogs("Start extract archive");
            ZipFile zip = ZipFile.Read("_tmp/ETS2MP.zip");

            foreach (ZipEntry entry in zip)
            {
                Stream file = File.Create("_tmp/tmpInstall.exe");
                entry.Extract(file);
                file.Close();
            }
        }

        private void backgroundExtract_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            resultLabel.Text = "Extract zip archive";
            progressBar.Value = e.ProgressPercentage;
        }

        private void backgroundExtract_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            resultLabel.Text = "";
            backgroundInnoUP.RunWorkerAsync();
            saveLogs("Archive extracted");
        }

        private void backgroundInnoUP_DoWork(object sender, DoWorkEventArgs e)
        {
            saveLogs("Start extract innup");
            BackgroundWorker worker = sender as BackgroundWorker;

            worker.ReportProgress(70);
            Process innounp = new Process();
            innounp.StartInfo.FileName = "innounp.exe";
            innounp.StartInfo.Arguments = "-x -d_tmp/App -c{App} -y _tmp/tmpInstall.exe";
            innounp.StartInfo.UseShellExecute = false;
            innounp.StartInfo.RedirectStandardOutput = true;
            innounp.StartInfo.CreateNoWindow = true;
            innounp.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            innounp.Start();
            innounp.WaitForExit();
        }

        private void backgroundInnoUP_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            resultLabel.Text = "Extract files from installation file";
            progressBar.Value = e.ProgressPercentage;
        }

        private void backgroundInnoUP_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            resultLabel.Text = "Files extracted!";
            progressBar.Value = 100;
            backgroundCopy.RunWorkerAsync();
        }

        private void backgroundCopy_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            string[] files = Directory.GetFiles("_tmp\\App", "*", SearchOption.AllDirectories);
            for (int i = 0; i < files.Count(); i++)
            {
                string file = files[i].Substring(files[i].LastIndexOf('\\'), files[i].Length - files[i].LastIndexOf('\\'));
                string dir = regInstallDir + files[i].Replace("_tmp\\App", "").Replace(file, "");
                if (Directory.Exists(dir) != true)
                {
                    Directory.CreateDirectory(dir);
                }
                File.Copy(files[i], dir + file, true);
                worker.ReportProgress(i * 100 / files.Count());
            }
        }

        private void backgroundCopy_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            resultLabel.Text = "Copy files";
            progressBar.Value = e.ProgressPercentage;
        }

        private void backgroundCopy_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar.Value = 100;
            backgroundDelete.RunWorkerAsync();
            progressBar.Value = 100;
        }

        private void backgroundDelete_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            string[] files = Directory.GetFiles("_tmp", "*", SearchOption.AllDirectories);
            for (int i = 0; i < files.Count(); i++)
            {
                string file = files[i].Substring(files[i].LastIndexOf('\\'), files[i].Length - files[i].LastIndexOf('\\'));
                string dir = files[i].Replace(file, "");
                File.Delete(files[i]);
                worker.ReportProgress(i * 100 / files.Count(), dir);
            }
            Directory.Delete("_tmp", true);
        }

        private void backgroundDelete_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            resultLabel.Text = "Delete temporary files";
            progressBar.Value = e.ProgressPercentage;
        }

        private void backgroundDelete_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnPlay.Text = "Play";
            resultLabel.Text = "All ok";
            progressBar.Value = 100;
        }
    }
}
