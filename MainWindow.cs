using SimpleGithubUserDataFetch.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SimpleGithubUserDataFetch
{
    public partial class MainWindow : Form
    {
        private readonly UserService service = new UserService();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            ActiveControl = usernameTextBox;
        }

        private void FetchButton_Click(object sender, EventArgs e)
        {
            var username = usernameTextBox.Text;

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show(
                    this, 
                    "Informe o nome de usuário", 
                    "Alerta", 
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            RetrieveUserData(username);
        }

        private void InProgress(bool inProgress)
        {
            progressBar.Visible = inProgress;
            FetchButton.Visible = !inProgress;

            if (inProgress)
                resultTextBox.Text = "";
        }

        private async void RetrieveUserData(string username)
        {
            InProgress(true);

            await service.FetchSimpleUserData(username).ContinueWith(user =>
            {
                try
                {
                    string s = "Usuário da consulta: " + user.Result.Name + Environment.NewLine + Environment.NewLine +
                    user.Result.GetFollowers().Count() + " Seguidores:" + Environment.NewLine;

                    user.Result.GetFollowers().ForEach(f =>
                    {
                        List<string> r = f.repositories.Select(t => "\t" + t.name).ToList();
                        s += string.Format(
                            "{0}{1}{2}{3}{4}",
                            Environment.NewLine,
                            f.FullName,
                            Environment.NewLine,
                            string.Join(Environment.NewLine, r),
                            Environment.NewLine
                        );
                    });

                    Invoke(new Action(() =>
                    {
                        resultTextBox.Text = s;
                        InProgress(false);
                    }));
                }
                catch (Exception)
                {
                    Invoke(new Action(() =>
                    {
                        InProgress(false);
                        resultTextBox.Text = string.Empty;

                        MessageBox.Show(
                            ActiveForm,
                            "Não foi possível obter os dados.",
                            "Falha",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }));
                }
            });
        }
    }
}
