using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppLogic;
using FacebookWrapper.ObjectModel;

namespace AppUI
{
    public partial class CalendarForm : Form
    {
        private readonly AppManager r_AppManager;
        private DateTime m_LastDateTasksDisplayed;

        public event Action<Task> TaskAdded;

        public event Action<Task> TaskRemoved;

        public CalendarForm()
        {
            InitializeComponent();
            r_AppManager = AppManager.Instance;
            fetchCalendarData(DateTime.Today);
        }

        private void fetchCalendarData(DateTime i_Date)
        {
            fetchBirthdaysOfChosenDate(i_Date);
            fetchEventsOfChosenDate(i_Date);
            fetchTasksOfChosenDate(i_Date);
        }

        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime chosenDate = monthCalendar.SelectionStart;

            deleteCheckedTasks();
            clearPreviouslyData();
            fetchCalendarData(chosenDate);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            r_AppManager.SaveTasksinCalendar();
            deleteCheckedTasks();
        }

        private void fetchBirthdaysOfChosenDate(DateTime i_Date)
        {
            List<string> friendsList = r_AppManager.GetBirthdaysOfChosenDate(i_Date);

            foreach (string friendName in friendsList)
            {
                listBoxBirthday.Items.Add(friendName);
            }
        }

        private void fetchEventsOfChosenDate(DateTime i_Date)
        {
            List<string> eventsList = r_AppManager.GetEventsOfChosenDate(i_Date);

            foreach (string eventName in eventsList)
            {
                listBoxEvent.Items.Add(eventName);
            }
        }

        private void fetchTasksOfChosenDate(DateTime i_Date)
        {
            List<Task> tasksList = r_AppManager.GetTasksOfChosenDate(i_Date);

            foreach (Task task in tasksList)
            {
                checkedListBoxTask.Items.Add(task);
            }

            m_LastDateTasksDisplayed = i_Date;
        }

        private void deleteCheckedTasks()
        {
            foreach (Task checkedTask in checkedListBoxTask.CheckedItems)
            {
                r_AppManager.RemoveTaskFromCalendar(m_LastDateTasksDisplayed, checkedTask);
                OnTaskRemoved(checkedTask);
            }
        }

        private void buttonAddTask_Click(object sender, EventArgs e)
        {
            bool isValidTask = validTaskInsert();

            if(isValidTask == true)
            {
                AppLogic.Task newTask = new AppLogic.Task() 
                {
                    Description = textBoxAddTask.Text,
                    Priority = (eTaskPriority)Enum.Parse(typeof(eTaskPriority), comboBoxTaskPriority.Text),
                    Type = (eTaskType)Enum.Parse(typeof(eTaskType), comboBoxTaskType.Text),
                    Date = monthCalendar.SelectionStart
                };
                r_AppManager.AddTaskToCalendar(monthCalendar.SelectionStart, newTask);
                checkedListBoxTask.Items.Add(newTask);
                OnTaskAdded(newTask);
                resetAddTaskPanel();
            }
        }

        private bool validTaskInsert()
        {
            bool isValidTask = true;

            foreach (Control control in panelAddTask.Controls)
            {
                if (control.Text.Equals(string.Empty))
                {
                    isValidTask = false;
                    MessageBox.Show("You must fill all the task's fields");
                    break;
                }
            }

            return isValidTask;
        }

        private void resetAddTaskPanel()
        {
            foreach (Control control in panelAddTask.Controls)
            {
                if(!(control is Label))
                {
                    control.ResetText();
                }
            }
        }

        private void clearPreviouslyData()
        {
            listBoxBirthday.Items.Clear();
            listBoxEvent.Items.Clear();
            checkedListBoxTask.Items.Clear();
        }

        protected virtual void OnTaskAdded(Task i_Task)
        {
            if(TaskAdded != null)
            {
                TaskAdded.Invoke(i_Task);
            }
        }

        protected virtual void OnTaskRemoved(Task i_Task)
        {
            if(TaskRemoved != null)
            {
                TaskRemoved.Invoke(i_Task);
            }
        }
    }
}