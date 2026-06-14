using System.Text.Json;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace DiscordGameSpoofer
{
    public partial class Form1 : Form
    {
        private List<DiscordDetectableApplication> allGames = new List<DiscordDetectableApplication>();
        private DiscordDetectableApplication? selectedGame = null;
        private List<DateTime> gameStartTimes = new List<DateTime>();
        private System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer();
        string appPath = AppContext.BaseDirectory;
        List<Process> procList = new List<Process>();

        string gamePath = "";
        string blankWindowPath = "";

        bool logShown = false;

        // Move these to interfaces folder
        public class DiscordDetectableApplication
        {
            [JsonPropertyName("id")]
            public string Id { get; set; } = "";

            [JsonPropertyName("name")]
            public string Name { get; set; } = "";

            [JsonPropertyName("executables")]
            public List<DiscordExecutable> Executables { get; set; } = new();

            public override string ToString()
            {
                return Name;
            }
        }

        public class DiscordExecutable
        {

            [JsonPropertyName("name")]
            public string Name { get; set; } = "";

            [JsonPropertyName("os")]
            public string Os { get; set; } = "";
        }

        public Form1()
        {
            InitializeComponent();

            versionLabel.Text = $"v{Config.version}";

            gamesList.Items.Add("Loading...");
            gamesList.Items.Add("This can");
            gamesList.Items.Add("take a second.");
            gamesList.Items.Add("UI may lag.");

            log("Welcome to Discord Game Spoofer");
            log(Config.githubUrl);
            logBox.Hide();

            _ = InitializeAsync();

            detailsBox.AppendText("Make a selection on the left to display details.\nInterface may lag while filtering, it has to check through 23.000 games.");

            gameTimer.Interval = 1000;
            gameTimer.Tick += gameTimer_Tick;
            gameTimer.Start();
        }

        private async Task InitializeAsync()
        {
            // This is just incase
            startButton.Enabled = false;

            await CheckVersion();
            await SetupApplicationAsync();
            await LoadDetectableGamesAsync();

            startButton.Enabled = true;
        }

        private void gameTimer_Tick(object? sender, EventArgs e)
        {
            for (int i = runningGamesTime.Items.Count - 1; i >= 0 && i < gameStartTimes.Count; i--)
            {
                TimeSpan elapsed = DateTime.Now - gameStartTimes[i];
                runningGamesTime.Items[i] = elapsed.ToString(@"hh\:mm\:ss");

                Process proc = procList[i];
                if (proc.HasExited)
                {
                    string gameName = runningGamesList.Items[i].ToString() ?? "Unknown game";
                    procList.RemoveAt(i);
                    runningGamesList.Items.RemoveAt(i);
                    runningGamesTime.Items.RemoveAt(i);
                    gameStartTimes.RemoveAt(i);

                    log($"Spoof process exited for {gameName}.");
                }
            }
        }

        private void log(string message)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            logBox.AppendText($"[{timestamp}] {message}\n");

            logBox.SelectionStart = logBox.TextLength;
            logBox.ScrollToCaret();
        }

        private async Task CheckVersion()
        {
            try
            {
                log("Checking version.");
                using HttpClient client = new HttpClient();

                string latestVersion = (await client.GetStringAsync(Config.versionCheckUrl)).Trim();
                
                if (latestVersion != Config.version)
                {
                    log("A new version of Discord Game Spoofer is available, please update to the latest version.\nThis version will keep working.");
                    MessageBox.Show("A new version of Discord Game Spoofer is available, please update to the latest version.\nThis version will keep working.");
                }
                else
                {
                    log("Latest version of Discord Game Spoofer in use.");
                }
            }
            catch (Exception ex)
            {
                log($"Failed to check version: {ex.Message}");
                MessageBox.Show("Failed to check version: " + ex.Message);
            }
        }

        private async Task LoadDetectableGamesAsync()
        {
            try
            {
                log("Loading Discord detectable games...");

                using HttpClient client = new HttpClient();

                string json = await client.GetStringAsync(Config.discordAPIUrl);

                List<DiscordDetectableApplication>? games =
                    JsonSerializer.Deserialize<List<DiscordDetectableApplication>>(json);

                gamesList.Items.Clear();
                allGames.Clear();

                if (games == null)
                {
                    log("No games were returned from the Discord API.");
                    return;
                }
                    
                games.Sort((a, b) => a.Name.CompareTo(b.Name));

                foreach (DiscordDetectableApplication game in games)
                {
                    allGames.Add(game);
                    gamesList.Items.Add(game);
                }

                log($"Loaded {games.Count} detectable games.");
            }
            catch (Exception ex)
            {
                log($"Failed to load games: {ex.Message}");
                MessageBox.Show("Failed to load games: " + ex.Message);
            }
        }

        private async Task SetupApplicationAsync()
        {
            try
            {
                log("Setting up application folders...");

                // Setup the bin path
                string binPath = Path.Combine(appPath, "bin");
                gamePath = Path.Combine(binPath, "games");
                if (!Path.Exists(binPath))
                {
                    Directory.CreateDirectory(binPath);
                    log($"Created bin folder: {binPath}");
                }
                
                if (!Path.Exists(gamePath))
                {
                    Directory.CreateDirectory(gamePath);
                    log($"Created games folder: {gamePath}");
                }

                string sha256 = "";
                bool justDownloaded = false; // need a better solution, it'll work for now

                blankWindowPath = Path.Combine(binPath, "blank.exe");
                if (!File.Exists(blankWindowPath))
                {
                    justDownloaded = true;
                    log("blank.exe not found. Downloading...");

                    // Last time I used C#, we used a WebClient but apparently it's deprecated
                    using var client = new HttpClient();

                    await using var input = await client.GetStreamAsync(Config.blankDownloadUrl);
                    await using var output = File.Create(blankWindowPath);

                    await input.CopyToAsync(output);

                    client.DefaultRequestHeaders.UserAgent.ParseAdd("DiscordGameSpoofer"); // Got HTTP 403 without a header
                    using JsonDocument jsonDoc = JsonDocument.Parse(await client.GetStringAsync(Config.blankAPIUrl));
                    JsonElement blankJson = jsonDoc.RootElement.GetProperty("assets")[0];
                    sha256 = blankJson.GetProperty("digest").ToString().Replace("sha256:", "");
                }

                if (justDownloaded)
                {
                    using FileStream stream = File.OpenRead(blankWindowPath);
                    byte[] hashBytes = SHA256.HashData(stream);
                    string downloadedBlankHash = Convert.ToHexString(hashBytes).ToLowerInvariant();

                    if (downloadedBlankHash == sha256)
                    {
                        log("blank.exe downloaded and verified successfully.");
                        log("Application setup complete.");
                    }
                    else
                    {
                        log("SHA256 check on blank failed, deleting file.");

                        try
                        {
                            if (File.Exists(blankWindowPath))
                                File.Delete(blankWindowPath);
                        }
                        catch (Exception ex)
                        {
                            log($"Failed to delete invalid blank.exe: {ex.Message}");
                        }

                        MessageBox.Show("blank.exe failed the SHA256 security check.\n\nThe downloaded file did not match the expected Github release hash, so it was deleted.\n\nPlease try again later. If this keeps happening, check the GitHub release or open an issue.");
                        Application.Exit();
                    }
                }
            }

            catch (Exception ex)
            {
                log($"Failed during setup: {ex.Message}");
                MessageBox.Show("Failed during setup: " + ex.Message);
            }
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            string search = searchBox.Text.Trim().ToLower();

            gamesList.Items.Clear();

            foreach (DiscordDetectableApplication game in allGames)
            {
                if (game.Name.ToLower().Contains(search))
                {
                    gamesList.Items.Add(game);
                }
            }
        }

        private void gamesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedGame = gamesList.SelectedItem as DiscordDetectableApplication;

            if (selectedGame == null)
                return;

            log($"Selected game: {selectedGame.Name}");

            detailsBox.Clear();

            List<String> winGames = new List<String>();

            if (selectedGame.Executables == null || selectedGame.Executables.Count == 0)
            {
                detailsBox.Text = "No executable details found.";
                log($"No Windows executables found for {selectedGame.Name}.");
                return;
            }

            foreach (DiscordExecutable exe in selectedGame.Executables)
            {
                if (exe.Os == "win32")
                {
                    winGames.Add(exe.Name);
                }
            }

            detailsBox.AppendText("Executable paths:\n");
            foreach (String exe in winGames)
            {
                detailsBox.AppendText($" - {exe}\n");
            }

            log($"Found {winGames.Count} Windows executable path(s) for {selectedGame.Name}.");
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (selectedGame == null)
            {
                log("Start clicked, but no game is selected.");
                return;
            }

            foreach (String name in runningGamesList.Items)
            {
                if (name == selectedGame.Name)
                {
                    log($"{selectedGame.Name} is already running.");
                    return;
                }
            }

            if (selectedGame.Executables == null || selectedGame.Executables.Count == 0)
            {
                log($"Cannot start {selectedGame.Name}: no executable list found.");
                return;
            }

            List<String> winGames = new List<String>();
            foreach (DiscordExecutable exe in selectedGame.Executables)
            {
                if (exe.Os == "win32")
                {
                    winGames.Add(exe.Name);
                }
            }

            if (winGames.Count == 0)
            {
                log($"Cannot start {selectedGame.Name}: no Windows executable found.");
                return;
            }

            // We've validated its a valid game
            string[] disallowedChars = new string[] { "<", ">", "\"", "\\", "|", "*", "?", "~" };

            string? pathGame = null;
            foreach (String exe in winGames)
            {
                bool isInvalid = exe.Any(c => disallowedChars.Contains(c.ToString()));
                bool hasPathUp = exe.Contains("..");
                if (!isInvalid && !hasPathUp)
                {
                    pathGame = exe;
                    break;
                }
            }

            if (pathGame == null)
            {
                log($"No valid executable path found for {selectedGame.Name}.");
                DialogResult result = MessageBox.Show("No valid game paths were detected.\n\nPlease open a Github issue if this is unexpected.\nOpen Github issues page?", "No Valid Game Path", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = Config.githubIssuesUrl,
                        UseShellExecute = true,
                    });
                }
                return;
            }

            string[] splitPath = pathGame.Split("/");
            string full_dir = Path.Combine(gamePath, string.Join("\\", splitPath.Take(splitPath.Length - 1).ToArray()));
            string exe_path = Path.Combine(full_dir, splitPath.Last());

            // Security check - this is an edge case. Normally, the return from Discord's API should be safe,
            // but this ensures the spoof executables can only be created inside the app's games folder.
            string fullGamePath = Path.GetFullPath(gamePath);
            string fullExePath = Path.GetFullPath(exe_path);

            if (!fullGamePath.EndsWith(Path.DirectorySeparatorChar))
                fullGamePath += Path.DirectorySeparatorChar;

            if (!fullExePath.StartsWith(fullGamePath, StringComparison.OrdinalIgnoreCase))
            {
                log($"Blocked unsafe executable path. Game: {selectedGame.Name}, Path: {fullExePath}, Allowed root: {fullGamePath}");
                MessageBox.Show("Game launch aborted because last minute safety check failed. View log for more details.");
                return;
            }

            string? safeGameDir = Path.GetDirectoryName(fullExePath);
            if (safeGameDir == null)
            {
                log($"Could not determine game's folder for {selectedGame.Name}.");
                MessageBox.Show("An unexpected error occurred. View logs for more details.");
                return;
            }

            if (!Path.Exists(safeGameDir))
            {
                Directory.CreateDirectory(safeGameDir);
                log($"Created spoof folder: {safeGameDir}");
            }
                
            if (!File.Exists(fullExePath))
            {
                File.Copy(blankWindowPath, fullExePath);
                log($"Created spoof executable: {fullExePath}");
            }

            // Use fullExePath here after validation that it's safe
            ProcessStartInfo startInfo = new ProcessStartInfo(fullExePath);
            startInfo.CreateNoWindow = false;

            Process? proc = Process.Start(startInfo);
            if (proc == null)
            {
                log($"Failed to start spoof process for {selectedGame.Name}.");
                return;
            }

            procList.Add(proc);

            runningGamesList.Items.Add(selectedGame.Name);
            runningGamesTime.Items.Add("00:00:00");
            gameStartTimes.Add(DateTime.Now);

            log($"Started spoofing {selectedGame.Name}.");
        }

        private void runningGamesList_SelectedIndexChanged(object sender, EventArgs e) => runningGamesTime.SelectedIndex = runningGamesList.SelectedIndex;

        private void runningGamesTime_SelectedIndexChanged(object sender, EventArgs e) => runningGamesList.SelectedIndex = runningGamesTime.SelectedIndex;

        private void stopButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = runningGamesList.SelectedIndex;
            if (selectedIndex < 0)
            {
                log("Stop clicked, but no running game is selected.");
                return;
            }

            string gameName = runningGamesList.Items[selectedIndex].ToString() ?? "Unknown game";

            if (!procList[selectedIndex].HasExited)
                procList[selectedIndex].Kill();

            procList.RemoveAt(selectedIndex);

            runningGamesList.Items.RemoveAt(selectedIndex);
            runningGamesTime.Items.RemoveAt(selectedIndex);
            gameStartTimes.RemoveAt(selectedIndex);

            log($"Stopped spoofing {gameName}.");
        }

        private void toggleLogButton_Click(object sender, EventArgs e)
        {
            if (!logShown)
            {
                logBox.Show();
                logShown = true;
            }
            else
            {
                logBox.Hide();
                logShown = false;
            }
        }
    }
}
