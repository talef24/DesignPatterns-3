using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AppLogic;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace AppUI
{
    public partial class MainForm : Form
    {
        private readonly AppManager r_AppManager;
        private eClickMode m_MouseClickStatus = eClickMode.UnClickedMouse;

        public MainForm()
        {
            r_AppManager = AppManager.Instance;
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("Logged in as {0}", r_AppManager.LoggedInUser.Name);
            this.StartPosition = FormStartPosition.Manual;
            appSettingsBindingSource.DataSource = r_AppManager.AppSettings;
            this.Location = r_AppManager.GetLastWindowLocation();
            this.BackColor = r_AppManager.GetLastPageColor();
            panelEditPost.BackColor = r_AppManager.GetLastPageColor();
            colorAllButtonsInSelectedColor(r_AppManager.GetLastButtonsColor());
            labelUserName.ForeColor = r_AppManager.GetLastNameColor();
            hideAllSettingsComboBoxs();
            panelEditPost.Hide();
            labelTodayTasks.Text = string.Format("Today tasks- {0} :", DateTime.Today.ToShortDateString());
            loadTodayTasks();
        }

        private void loadTodayTasks()
        {
            List<Task> todayTasks = r_AppManager.GetTasksOfChosenDate(DateTime.Today);

            loadRelevantTasksToListBox(todayTasks);
        }

        private void loadRelevantTasksToListBox(List<Task> i_TodayRelevantTasks)
        {
            listBoxTodayTasks.Items.Clear();
            foreach (Task task in i_TodayRelevantTasks)
            {
                listBoxTodayTasks.Items.Add(task);
            }

            sortAndRearrange();
        }

        private void hideAllSettingsComboBoxs()
        {
            comboBoxPageColor.Hide();
            comboBoxButtonsColor.Hide();
            comboBoxNameColor.Hide();
            comboBoxMotto.Hide();
        }

        private void loadFavoritePhotosToSwappingPhotosBox()
        {
            List<Image> favoritePhotos = r_AppManager.GetFavoritePhotos();

            m_SwappingPhotosBox.Invoke(new Action(() => m_SwappingPhotosBox.Images = favoritePhotos));
            m_SwappingPhotosBox.Invoke(new Action(() => m_SwappingPhotosBox.Start()));
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            fetchUserProfilePicture();
            new Thread(fetchUserInfo).Start();
            new Thread(fetchUserPosts).Start();
            new Thread(loadFavoritePhotosToSwappingPhotosBox).Start();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            r_AppManager.UpdateSettings(this.Location, this.BackColor, labelUserName.ForeColor, buttonAlbums.BackColor);
            Environment.Exit(Environment.ExitCode);
        }

        private void fetchUserProfilePicture()
        {
            roundPictureBoxProxy.LoadAsync(r_AppManager.LoggedInUser.PictureNormalURL);
        }

        private void fetchUserInfo()
        {
            labelUserName.Invoke(new Action(() => labelUserName.Text = r_AppManager.LoggedInUser.Name));

            City location = r_AppManager.LoggedInUser.Location;
            if (location != null)
            {
                labelLocation.Invoke(new Action(() => labelLocation.Text = string.Format("{0} {1}", labelLocation.Text, location.Name)));
            }

            string birthday = r_AppManager.LoggedInUser.Birthday;
            if (birthday != null)
            {
                labelLocation.Invoke(new Action(() => labelBirthday.Text = string.Format("{0} {1}", labelBirthday.Text, birthday)));
            }

            City homeTown = r_AppManager.LoggedInUser.Hometown;
            if (homeTown != null)
            {
                labelLocation.Invoke(new Action(() => labelHomeTown.Text = string.Format("{0} {1}", labelHomeTown.Text, homeTown.Name)));
            }

            User.eRelationshipStatus? relationshipStatus = r_AppManager.LoggedInUser.RelationshipStatus;
            if (relationshipStatus != null)
            {
                if(relationshipStatus.Equals(User.eRelationshipStatus.Single))
                {
                    labelLocation.Invoke(new Action(() => labelRelationshipStatus.Text = string.Format("{0} single", labelRelationshipStatus.Text)));
                }

                if (relationshipStatus.Equals(User.eRelationshipStatus.InARelationship))
                {
                    labelLocation.Invoke(new Action(() => labelRelationshipStatus.Text = string.Format("{0} In A Relationship", labelRelationshipStatus.Text)));
                }
            }
        }

        private void fetchUserPosts()
        {
            postBindingSource.DataSource = r_AppManager.LoggedInUser.Posts;
            listBoxPosts.Invoke(new Action(() => listBoxPosts.DataSource = this.postBindingSource));
            panelEditPost.Invoke(new Action(() => panelEditPost.Hide()));
        }

        private void buttonEvents_Click(object sender, EventArgs e)
        {
            new Thread(fetchEvents).Start();
        }

        private void fetchEvents()
        {
            listBoxEvents.Invoke(new Action(() => listBoxEvents.Items.Clear()));
            listBoxEvents.Invoke(new Action(() => listBoxEvents.DisplayMember = "Name"));
            foreach (Event fbEvent in r_AppManager.LoggedInUser.Events)
            {
                listBoxEvents.Invoke(new Action(() => listBoxEvents.Items.Add(fbEvent)));
            }

            if (r_AppManager.LoggedInUser.Events.Count == 0)
            {
                MessageBox.Show("No Events to retrieve");
            }
        }

        private void buttonCheckIn_Click(object sender, EventArgs e)
        {
            new Thread(fetchCheckIn).Start();
        }

        private void fetchCheckIn()
        {
            listBoxCheckIn.Invoke(new Action(() => listBoxCheckIn.Items.Clear()));
            listBoxCheckIn.Invoke(new Action(() => listBoxCheckIn.DisplayMember = "Name"));
            foreach (Checkin checkin in r_AppManager.LoggedInUser.Checkins)
            {
                listBoxCheckIn.Invoke(new Action(() => listBoxCheckIn.Items.Add(checkin)));
            }

            if (r_AppManager.LoggedInUser.Checkins.Count == 0)
            {
                MessageBox.Show("No check-in to retrieve");
            }
        }

        private void buttonGroups_Click(object sender, EventArgs e)
        {
            new Thread(fetchGroups).Start();
        }

        private void fetchGroups()
        {
            listBoxGroups.Invoke(new Action(() => listBoxGroups.Items.Clear()));
            listBoxGroups.Invoke(new Action(() => listBoxGroups.DisplayMember = "Name"));
            foreach (Group group in r_AppManager.LoggedInUser.Groups)
            {
                listBoxGroups.Invoke(new Action(() => listBoxGroups.Items.Add(group)));
            }

            if (r_AppManager.LoggedInUser.Groups.Count == 0)
            {
                MessageBox.Show("No group to retrieve");
            }
        }

        private void buttonFriends_Click(object sender, EventArgs e)
        {
            new Thread(fetchFriends).Start();
        }

        private void fetchFriends()
        {
            listBoxFriends.Invoke(new Action(() => listBoxFriends.Items.Clear()));
            listBoxFriends.Invoke(new Action(() => listBoxFriends.DisplayMember = "Name"));
            foreach (User friend in r_AppManager.LoggedInUser.Friends)
            {
                listBoxFriends.Invoke(new Action(() => listBoxFriends.Items.Add(friend)));
            }

            if (r_AppManager.LoggedInUser.Friends.Count == 0)
            {
                MessageBox.Show("No friends to retrieve");
            }
        }

        private void buttonAlbums_Click(object sender, EventArgs e)
        {
            ImageGallery photoAlbums = new ImageGallery();

            photoAlbums.FormClosed += PhotoAlbums_FormClosed;
            photoAlbums.ShowDialog();
        }

        private void PhotoAlbums_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_SwappingPhotosBox.Hide();
            m_SwappingPhotosBox.Images = r_AppManager.GetFavoritePhotos();
            m_SwappingPhotosBox.Start();
        }

        private void pictureBoxSetting_Click(object sender, EventArgs e)
        {
            if(m_MouseClickStatus.Equals(eClickMode.UnClickedMouse))
            {
                comboBoxPageColor.Show();
                comboBoxButtonsColor.Show();
                comboBoxNameColor.Show();
                comboBoxMotto.Show();
                m_MouseClickStatus = eClickMode.ClickedMouse;
            }
            else
            {
                hideAllSettingsComboBoxs();
                m_MouseClickStatus = eClickMode.UnClickedMouse;
            }
        }

        private void comboBoxPageColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxPageColor.SelectedIndex == Constants.k_FirstOption)
            {
                changePageColor("WhiteSmoke");
            }

            if(comboBoxPageColor.SelectedIndex == Constants.k_SecondOption)
            {
                changePageColor("Pink");
            }

            if(comboBoxPageColor.SelectedIndex == Constants.k_ThirdOption)
            {
                changePageColor("PaleGreen");
            }
        }

        private void changePageColor(string i_Color)
        {
            this.BackColor = Color.FromName(i_Color);
            panelEditPost.BackColor = Color.FromName(i_Color);
        }

        private void comboBoxButtonsColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color selectedColor = Color.MidnightBlue; 

            if (comboBoxButtonsColor.SelectedIndex == Constants.k_SecondOption)
            {
                selectedColor = Color.Black;
            }

            colorAllButtonsInSelectedColor(selectedColor);
        }

        private void colorAllButtonsInSelectedColor(Color i_SelectedColor)
        {
            colorButtons(this.Controls, i_SelectedColor);
            colorButtons(panelEditPost.Controls, i_SelectedColor);
        }

        private void colorButtons(Control.ControlCollection i_Controls, Color i_SelectedColor)
        {
            foreach (Control control in i_Controls)
            {
                if (control is Button)
                {
                    control.BackColor = i_SelectedColor;
                }
            }
        }

        private void comboBoxNameColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxNameColor.SelectedIndex == Constants.k_FirstOption)
            {
                labelUserName.ForeColor = Color.Black;
            }

            if (comboBoxNameColor.SelectedIndex == Constants.k_SecondOption)
            {
                labelUserName.ForeColor = Color.Red;
            }

            if (comboBoxNameColor.SelectedIndex == Constants.k_ThirdOption)
            {
                labelUserName.ForeColor = Color.Yellow;
            }
        }

        private void comboBoxMotto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxMotto.SelectedIndex != Constants.k_FirstOption)
            {
                labelMotto.Text = comboBoxMotto.Text;
            }
            else
            {
                labelMotto.Text = string.Empty;
            }
        }

        private void buttonCalendar_Click(object sender, EventArgs e)
        {
            CalendarForm calendar = new CalendarForm();
            calendar.TaskAdded += Calendar_TaskAdded;
            calendar.TaskRemoved += Calendar_TaskRemoved;

            calendar.ShowDialog();
        }

        private void Calendar_TaskRemoved(Task i_Task)
        {
            if (i_Task.Date.Equals(DateTime.Today))
            {
                listBoxTodayTasks.Items.Remove(i_Task);
            }
        }

        private void Calendar_TaskAdded(Task i_Task)
        {
            if (i_Task.Date.Equals(DateTime.Today))
            {
                listBoxTodayTasks.Items.Add(i_Task);
                filterAndRearrange();
            }
        }

        private void listBoxPosts_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelEditPost.Show();
        }

        private void buttonClosePanel_Click(object sender, EventArgs e)
        {
            panelEditPost.Hide();
        }

        private void comboBoxSortTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSortTasks.SelectedIndex == Constants.k_FirstOption)
            {
                r_AppManager.TasksSorter.ComparerMethod = (num1, num2) => num1 < num2;
            }

            if (comboBoxSortTasks.SelectedIndex == Constants.k_SecondOption)
            {
                r_AppManager.TasksSorter.ComparerMethod = (num1, num2) => num1 > num2;
            }

            sortAndRearrange();
        }
        
        private void sortAndRearrange()
        {
            Task[] todayTasks = new Task[listBoxTodayTasks.Items.Count];

            listBoxTodayTasks.Items.CopyTo(todayTasks, 0);
            r_AppManager.TasksSorter.BubbleSort(todayTasks);
            listBoxTodayTasks.Items.Clear();
            listBoxTodayTasks.Items.AddRange(todayTasks);
        }
        
        private void comboBoxPriorityFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            r_AppManager.SetPriorityFilter(comboBoxPriorityFilter.SelectedIndex.ToString());
            filterAndRearrange();
        }

        private void comboBoxTypeFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            r_AppManager.SetTypeFilter(comboBoxTypeFilter.SelectedIndex.ToString());
            filterAndRearrange();
        }

        private void filterAndRearrange()
        {
            if (r_AppManager.IsTaskFilterDefined())
            {
                List<Task> filteredTasks = r_AppManager.FilterTasks();
                loadRelevantTasksToListBox(filteredTasks);
            }
            else
            {
                loadTodayTasks();
            }
        }

        private void buttonAllTasks_Click(object sender, EventArgs e)
        {
            listBoxTodayTasks.Items.Clear();
            loadTodayTasks();
        }
    }
}