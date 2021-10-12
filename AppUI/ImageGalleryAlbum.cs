using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppLogic;
using FacebookWrapper.ObjectModel;

namespace AppUI
{
    public partial class ImageGalleryAlbum : Form
    {
        private readonly ImageGallery r_ImageGallery;
        private readonly AppManager r_AppManager;
        private readonly ListBox r_CommentsListBox = new ListBox();
        private Album m_CurrentAlbum;
        private List<int> m_IndexesOfFavoritePhotos = new List<int>();
        private int m_AlbumIndex = 0;
        private eClickMode m_MouseClickStatus = eClickMode.UnClickedMouse;

        public ImageGalleryAlbum()
        {
            InitializeComponent();
            r_AppManager = AppManager.Instance;
            r_ImageGallery = new ImageGallery();
        }

        public int AlbumIndex
        {
            get { return m_AlbumIndex; }
            set { m_AlbumIndex = value; }
        }

        private void init()
        {
            m_CurrentAlbum = r_AppManager.LoggedInUser.Albums[m_AlbumIndex];
            this.Text = m_CurrentAlbum.Name;
            r_AppManager.IndexOfFavoritePhotos.TryGetValue(m_AlbumIndex, out m_IndexesOfFavoritePhotos);
            if (m_IndexesOfFavoritePhotos == null)
            {
                m_IndexesOfFavoritePhotos = new List<int>();
            }
        }

        private void imageGalleryAlbum_Load(object sender, EventArgs e)
        {
            init();
            Controls.Add(r_CommentsListBox);
            r_CommentsListBox.Hide();
            displayPhotos();
        }

        private void displayPhotos()
        {
            Point locationpictureBox = new Point(Constants.k_StartLocationX, pictureBoxInstruction.Bottom + Constants.k_StartLocationY);
            Point locationbutton = new Point(Constants.k_StartLocationX, locationpictureBox.Y + Constants.k_PictureBoxHeight);
            Point locationLabel = new Point(Constants.k_StartLocationX, locationbutton.Y + 25);
            Point locationCheckBox = new Point(Constants.k_StartLocationX, locationLabel.Y + 20);
            FacebookObjectCollection<Photo> currentAlbumPhotos = m_CurrentAlbum.Photos;

            for (int i = 0; i < currentAlbumPhotos.Count; i++)
            {
                r_ImageGallery.PictureBoxSettings(ref locationpictureBox, this, currentAlbumPhotos[i].PictureAlbumURL, i);
                Button currentButton = r_ImageGallery.ButtonSettings(ref locationbutton, this, "Comments", i);
                currentButton.Click += new EventHandler(currentButton_Click);
                labelSettings(ref locationLabel, string.Format("Likes: {0}", currentAlbumPhotos[i].LikedBy.Count), i);
                checkBoxSettings(ref locationCheckBox, i, m_IndexesOfFavoritePhotos.Contains(i));
            }
        }

        private void labelSettings(ref Point io_Location, string i_LabelText, int i_Index)
        {
            Label currentLabel = new Label();

            currentLabel.Text = i_LabelText;
            currentLabel.Location = r_ImageGallery.SetLocation(ref io_Location, i_Index);
            Controls.Add(currentLabel);
            currentLabel.BringToFront(); 
        }

        private void checkBoxSettings(ref Point io_Location, int i_Index, bool i_IsChecked)
        {
            CheckBox addToFavoriteCheckBox = new CheckBox();

            addToFavoriteCheckBox.Name = i_Index.ToString();
            addToFavoriteCheckBox.Location = r_ImageGallery.SetLocation(ref io_Location, i_Index);
            Controls.Add(addToFavoriteCheckBox);
            addToFavoriteCheckBox.Checked = i_IsChecked;
            addToFavoriteCheckBox.CheckedChanged += addToFavoriteCheckBox_CheckedChanged;
        }

        private void addToFavoriteCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox currentCheckBox = sender as CheckBox;
            int photoIndex = int.Parse(currentCheckBox.Name);

            if (currentCheckBox.Checked)
            {
                if (!m_IndexesOfFavoritePhotos.Contains(photoIndex))
                {
                    m_IndexesOfFavoritePhotos.Add(photoIndex);
                }
            }
            else
            {
                m_IndexesOfFavoritePhotos.Remove(photoIndex);
            }
        }

        private void currentButton_Click(object sender, EventArgs e)
        {
            Button currentButton = sender as Button;

            if (m_MouseClickStatus.Equals(eClickMode.UnClickedMouse))
            {
                displayComments(currentButton);
            }
            else
            {
                actionAfterDisplayComments(currentButton);
            }
        }

        private void displayComments(Button i_CurrentButton)
        {
            r_CommentsListBox.Items.Clear();
            int photoIndex = int.Parse(i_CurrentButton.Name);

            foreach (Comment comment in m_CurrentAlbum.Photos[photoIndex].Comments)
            {
                if (comment.Message != null)
                {
                    r_CommentsListBox.Items.Add(comment.Message);
                }
            }

            i_CurrentButton.Text = "Hide Comments";
            Point locationcommentsListBox = new Point(i_CurrentButton.Location.X + Constants.k_PictureBoxWidth, i_CurrentButton.Location.Y);
            r_CommentsListBox.Location = locationcommentsListBox;
            r_CommentsListBox.BringToFront();
            r_CommentsListBox.Show();
            m_MouseClickStatus = eClickMode.ClickedMouse;
        }

        private void actionAfterDisplayComments(Button i_CurrentButton)
        {
            if (i_CurrentButton.Text.Equals("Comments"))
            {
                MessageBox.Show("You must first hide other comments");
            }
            else
            {
                r_CommentsListBox.Hide();
                i_CurrentButton.Text = "Comments";
                m_MouseClickStatus = eClickMode.UnClickedMouse;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (r_AppManager.IndexOfFavoritePhotos.ContainsKey(m_AlbumIndex))
            {
                r_AppManager.IndexOfFavoritePhotos.Remove(m_AlbumIndex);
            }

            r_AppManager.IndexOfFavoritePhotos.Add(m_AlbumIndex, m_IndexesOfFavoritePhotos);
            r_AppManager.SaveFavoritePhotosIndexs();
        }
    }
}