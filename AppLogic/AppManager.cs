using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace AppLogic
{
	public sealed class AppManager
	{
		private static readonly object sr_LockObj = new object();
		private static AppManager s_AppManager = null;
		private readonly Dictionary<int, List<int>> r_IndexOfFavoritePhotos = new Dictionary<int, List<int>>();
		private readonly AppSettings r_AppSettings;
		private LoginResult m_LoginResult;
		private User m_LoggedInUser;
		private Calendar m_Calendar;

		public static AppManager Instance
		{
			get
			{
				if (s_AppManager == null)
				{
					lock (sr_LockObj)
					{
						if (s_AppManager == null)
						{
							s_AppManager = new AppManager();
						}
					}
				}

				return s_AppManager;
			}
		}

		public Sorter TasksSorter { get; set; }

		private AppManager()
		{
			r_AppSettings = AppSettings.LoadAppSettingsFromFile();
			TasksSorter = new Sorter((num1, num2) => num1 > num2);
		}

		public LoginResult LoginResult
		{
			get { return m_LoginResult; }
			set { m_LoginResult = value; }
		}

		public User LoggedInUser
		{
			get { return m_LoggedInUser; }
			set { m_LoggedInUser = value; }
		}

		public AppSettings AppSettings
		{
			get { return r_AppSettings; }
		}

		public Dictionary<int, List<int>> IndexOfFavoritePhotos
		{
			get { return r_IndexOfFavoritePhotos; }
		}

		public void Login()
		{
			if (m_LoginResult == null)
			{
				m_LoginResult = FacebookService.Login(
					"952170521974282",
					"public_profile",
					"email",
					"user_birthday",
					"user_gender",
					"user_link",
					"user_tagged_places",
					"user_videos",
					"user_friends",
					"user_events",
					"user_likes",
					"user_location",
					"user_photos",
					"user_hometown",
					"user_posts");
			}

			Init();
		}

		public void Init()
		{
			m_LoggedInUser = m_LoginResult.LoggedInUser;
			m_Calendar = new Calendar(m_LoggedInUser);
			loadCalendar();
			loadFavoritePhotosToDictionary();
		}

		public bool IsAccessTokenNull()
		{
			return string.IsNullOrEmpty(m_LoginResult.AccessToken);
		}

		public void SaveFavoritePhotosIndexs()
		{
			List<List<int>> photosIndexs = new List<List<int>>();
			List<int> albumsIndexs = new List<int>();

			foreach (int key in r_IndexOfFavoritePhotos.Keys)
			{
				if (r_IndexOfFavoritePhotos.TryGetValue(key, out List<int> indexs))
				{
					photosIndexs.Add(indexs);
					albumsIndexs.Add(key);
				}
			}

			r_AppSettings.PhotosIndexsInAlbum = photosIndexs;
			r_AppSettings.AlbumsIndexs = albumsIndexs;
		}

		private void loadFavoritePhotosToDictionary()
		{
			for (int i = 0; i < r_AppSettings.AlbumsIndexs.Count; i++)
			{
				r_IndexOfFavoritePhotos.Add(r_AppSettings.AlbumsIndexs[i], r_AppSettings.PhotosIndexsInAlbum[i]);
			}
		}

		public List<Image> GetFavoritePhotos()
		{
			List<int> favoritePhotoAlbumsIndexs = r_AppSettings.AlbumsIndexs;
			List<Image> favoritePhotos = new List<Image>();

			for (int i = 0; i < favoritePhotoAlbumsIndexs.Count; i++)
			{
				int albumIndex = favoritePhotoAlbumsIndexs[i];
				List<int> favoritePhotosIndexs = r_AppSettings.PhotosIndexsInAlbum[i];

				foreach (int photoIndex in favoritePhotosIndexs)
				{
					favoritePhotos.Add(LoggedInUser.Albums[albumIndex].Photos[photoIndex].ImageNormal);
				}
			}

			return favoritePhotos;
		}

		private void loadCalendar()
		{
			m_Calendar.TasksToRemember = r_AppSettings.TasksInCalendar;
			m_Calendar.DatesToRemember = r_AppSettings.DatesInCalendar;
			m_Calendar.UpdateDictionary();
		}

		public void SaveTasksinCalendar()
		{
			m_Calendar.SaveTasksAndDates();
			r_AppSettings.TasksInCalendar = m_Calendar.TasksToRemember;
			r_AppSettings.DatesInCalendar = m_Calendar.DatesToRemember;
		}

		public List<string> GetBirthdaysOfChosenDate(DateTime i_Date)
		{
			List<string> friendsList = m_Calendar.GetBirthdayFriendsOfTheDay(i_Date);

			return friendsList;
		}

		public List<string> GetEventsOfChosenDate(DateTime i_Date)
		{
			List<string> eventsList = m_Calendar.GetEventsOfTheDay(i_Date);

			return eventsList;
		}

		public List<Task> GetTasksOfChosenDate(DateTime i_Date)
		{
			List<Task> tasksList = m_Calendar.GetTasksOfTheDay(i_Date);

			return tasksList;
		}

		public void RemoveTaskFromCalendar(DateTime i_Date, Task i_Task)
		{
			m_Calendar.RemoveTask(i_Date.ToShortDateString(), i_Task);
		}

		public void AddTaskToCalendar(DateTime i_Date, Task i_Task)
		{
			m_Calendar.AddTask(i_Date.ToShortDateString(), i_Task);
		}

		public void SetPriorityFilter(string i_Filter)
		{
			m_Calendar.TaskFilter = task => task.Priority.Equals(Enum.Parse(typeof(eTaskPriority), i_Filter));
		}

		public void SetTypeFilter(string i_Filter)
		{
			m_Calendar.TaskFilter = task => task.Type.Equals(Enum.Parse(typeof(eTaskType), i_Filter));
		}

		public List<Task> FilterTasks()
		{
			List<Task> tasks = new List<Task>();

			foreach(Task task in m_Calendar)
			{
				tasks.Add(task);
			}

			return tasks;
		}

		public bool IsTaskFilterDefined()
		{
			bool isDefined = false;

			if (m_Calendar.TaskFilter != null)
			{
				isDefined = true;
			}

			return isDefined;
		}

		public void UpdateSettings(Point i_WindowLocation, Color i_PageColor, Color i_NameColor, Color i_ButtonsColor)
		{
			r_AppSettings.LastWindowLocation = i_WindowLocation;
			r_AppSettings.LastPageColor = i_PageColor.Name;
			r_AppSettings.LastNameColor = i_NameColor.Name;
			r_AppSettings.LastButtonsColor = i_ButtonsColor.Name;
			r_AppSettings.LastAccessToken = m_LoginResult.AccessToken;
			r_AppSettings.SaveAppSettingsToFile();
		}

		public Point GetLastWindowLocation()
		{
			return r_AppSettings.LastWindowLocation;
		}

		public Color GetLastPageColor()
		{
			return Color.FromName(r_AppSettings.LastPageColor);
		}

		public Color GetLastButtonsColor()
		{
			return Color.FromName(r_AppSettings.LastButtonsColor);
		}

		public Color GetLastNameColor()
		{
			return Color.FromName(r_AppSettings.LastNameColor);
		}

		public bool GetRememberUser()
		{
			return r_AppSettings.RememberUser;
		}

		public string GetLastAccessToken()
		{
			return r_AppSettings.LastAccessToken;
		}
	}
}