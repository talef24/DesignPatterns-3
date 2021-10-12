using System;
using System.Collections.Generic;
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
    public partial class ImageGallery : Form
    {
        private readonly AppManager r_AppManager;

        public ImageGallery()
        {
            InitializeComponent();
            r_AppManager = AppManager.Instance;
        }

        private void imageGallery_Load(object sender, EventArgs e)
        {
            displayImageGallery();
        }

        private void displayImageGallery()
        {
            Point locationPictureBox = new Point(Constants.k_StartLocationX, pictureBoxCover.Bottom);
            Point locationbutton = new Point(Constants.k_StartLocationX, locationPictureBox.Y + Constants.k_PictureBoxHeight);
            FacebookObjectCollection<Album> albums = r_AppManager.LoggedInUser.Albums;

            for (int i = 0; i < albums.Count; i++)
            {
                PictureBoxSettings(ref locationPictureBox, this, albums[i].CoverPhoto.PictureAlbumURL, i);
                Button currentButton = ButtonSettings(ref locationbutton, this, albums[i].Name, i);
                currentButton.Click += new EventHandler(currentButton_Click);
            }
        }

        public void PictureBoxSettings(ref Point io_Location, Form i_Form, string i_PictureURL, int i_Index)
        {
            PictureBox currentPictureBox = new PictureBox();

            currentPictureBox.Load(i_PictureURL);
            currentPictureBox.Size = new Size(Constants.k_PictureBoxWidth, Constants.k_PictureBoxHeight);
            currentPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            currentPictureBox.Location = SetLocation(ref io_Location, i_Index);
            i_Form.Controls.Add(currentPictureBox);
        }

        public Button ButtonSettings(ref Point io_Location, Form i_Form, string i_ButtonText, int i_Index)
        {
            Button currentButton = new Button();

            currentButton.Name = i_Index.ToString();
            currentButton.Text = i_ButtonText;
            currentButton.Width = Constants.k_ButtonsWidth;
            currentButton.Location = SetLocation(ref io_Location, i_Index);
            i_Form.Controls.Add(currentButton);

            return currentButton;
        }

        public Point SetLocation(ref Point io_Location, int i_Index)
        {
            if (i_Index % Constants.k_NumberPhotosInRow == 0)
            {
                if (i_Index != 0)
                {
                    io_Location.X = Constants.k_StartLocationX;
                    io_Location.Y += Constants.k_Space;
                }
            }
            else
            {
                io_Location.X += Constants.k_Space;
            }

            return io_Location;
        }

        private void currentButton_Click(object sender, EventArgs e)
        {
            Button currentButton = sender as Button;
            ImageGalleryAlbum photosInCurrentAlbum = new ImageGalleryAlbum();

            photosInCurrentAlbum.AlbumIndex = int.Parse(currentButton.Name);
            photosInCurrentAlbum.ShowDialog();
        }
    }
}