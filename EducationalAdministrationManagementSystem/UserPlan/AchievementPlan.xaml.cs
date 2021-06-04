using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EducationalAdministrationManagementSystem.EncryptionDecryptionFunction;
using GloableVariable;
using MySql.Data.MySqlClient;

namespace EducationalAdministrationManagementSystem.UserPlan
{
    /// <summary>
    /// AchievementPlan.xaml 的交互逻辑
    /// </summary>
    public partial class AchievementPlan : UserControl
    {
        public AchievementPlan()
        {
            InitializeComponent();


            LoadDepartmentData();
            LoadDefaultImage(); //加载默认头像图像
        }

        /// <summary>
        /// 加载学院数据
        /// </summary>
        public void LoadDepartmentData()
        {
            List<string> departList = new List<string>();


            GlobalFunction.FileClass.AnalysisDatabaseConfig(); //解析数据库配置

            if (GlobalVariable.DbConnectInfo != null)
            {
                try
                {
                    DbConnect.MySqlConnection = DbConnect.ConnectionMysql(GlobalVariable.DbConnectInfo);

                    DbConnect.MySqlConnection.Open(); //连接数据库

                }
                catch (Exception)
                {
                    MessageBox.Show("数据库连接失败!请检查数据库配置");
                }

                //第二步,查询部门信息
                string sql = "SELECT * FROM DEPARTMENT_INFO WHERE DELSTATUS != 1";

                MySqlDataReader reader = DbConnect.CarrySqlCmd(sql);



                DepartmentList.Clear();
                int index = 0;
                while (reader.Read())
                {
                    index += 1;
                    string id = reader.GetString("DEPARTMENT_INDEX").ToString();
                    string name = Base64Conver.Base64Str2Text(reader.GetString("DEPARTMENT_NAME").ToString());
                    string note = Base64Conver.Base64Str2Text(reader.GetString("DEPARTMENT_NOTE").ToString());
                    string delStatus = reader.GetString("DELSTATUS").ToString();
                    DepartmentList.Add(new ViewMode.ViewMode.DepartmentInfo(index, id, name, note, delStatus));
                    departList.Add(name);
                }

                DepartmentCombobox.ItemsSource = departList;
                DepartmentCombobox.SelectedIndex = 0;
                DbConnect.MySqlConnection.Close();





            }
            else
            {
                MessageBox.Show("数据库连接信息有误");
            }

        }

        public ObservableCollection<ViewMode.ViewMode.DepartmentInfo> DepartmentList =
            new ObservableCollection<ViewMode.ViewMode.DepartmentInfo>()
            {
                //new DepartmentViewMode.DepartmentInfo("1","TEXT","3","4"),
            };


        /// <summary>
        /// 加载系部数据
        /// </summary>
        private void LoadSystemData(string departmentId)
        {

            List<string> systemNameList = new List<string>();

            SystemList.Clear();
            if (GlobalVariable.DbConnectInfo != null)
            {
                try
                {
                    DbConnect.MySqlConnection = DbConnect.ConnectionMysql(GlobalVariable.DbConnectInfo);

                    DbConnect.MySqlConnection.Open(); //连接数据库

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                //第二步,查询系部信息
                string sql =
                    string.Format("SELECT * FROM SYSTEM_INFO WHERE DELSTATUS != 1 AND BELONG_DEPARTMENT = '{0}'",
                        departmentId);

                MySqlDataReader reader = DbConnect.CarrySqlCmd(sql);



                //   SystemList.Clear();
                int index = 0;
                while (reader.Read())
                {
                    index += 1;
                    string id = reader.GetString("SYSTEM_INDEX").ToString();
                    string name = Base64Conver.Base64Str2Text(reader.GetString("SYSTEM_NAME").ToString());
                    string note = Base64Conver.Base64Str2Text(reader.GetString("SYSTEM_NOTE").ToString());
                    string belong = reader.GetString("BELONG_DEPARTMENT");
                    string delStatus = reader.GetString("DELSTATUS").ToString();
                    systemNameList.Add(name);
                    SystemList.Add(new ViewMode.ViewMode.SystemInfoViewMode(index, id, name, note, belong, delStatus));

                }

                SystemCombobox.ItemsSource = systemNameList;
                //SystemCombobox.SelectedIndex = 0;
                DbConnect.MySqlConnection.Close();



            }
            else
            {
                Console.WriteLine("CurriculumPlan.LoadSystemData检查数据库连接信息！");
            }




        }

        public ObservableCollection<ViewMode.ViewMode.SystemInfoViewMode> SystemList =
            new ObservableCollection<ViewMode.ViewMode.SystemInfoViewMode>()
            {
                //new DepartmentViewMode.DepartmentInfo("1","TEXT","3","4"),
            };


        /// <summary>
        /// 加载专业信息
        /// </summary>
        /// <param name="systemId"></param>
        private void LoadMajor(string systemId)
        {
            List<string> majorNameList = new List<string>();

            //清空专业列表
            MajorInfoList.Clear();

            if (GlobalVariable.DbConnectInfo != null)
            {
                try
                {
                    DbConnect.MySqlConnection = DbConnect.ConnectionMysql(GlobalVariable.DbConnectInfo);

                    DbConnect.MySqlConnection.Open(); //连接数据库



                    //第二步,查询系部信息
                    string sql =
                        string.Format("SELECT * FROM MAJOR_INFO WHERE DELSTATUS != 1 AND BELONG_SYSTEM = '{0}'",
                            systemId);

                    MySqlDataReader reader = DbConnect.CarrySqlCmd(sql);



                    //   SystemList.Clear();
                    int index = 0;
                    while (reader.Read())
                    {
                        index += 1;
                        string id = reader.GetString("MAJOR_INDEX").ToString();
                        string name = Base64Conver.Base64Str2Text(reader.GetString("MAJOR_NAME").ToString());
                        string num = reader.GetString("MAJOR_NUMBER").ToString();
                        string note = Base64Conver.Base64Str2Text(reader.GetString("MAJOR_NOTE").ToString());
                        string belong = reader.GetString("BELONG_SYSTEM");
                        string delStatus = reader.GetString("DELSTATUS").ToString();

                        majorNameList.Add(name);
                        MajorInfoList.Add(new ViewMode.ViewMode.MajorInfoViewMode(index, id, name, num, note, belong,
                            delStatus));
                        Console.WriteLine(name);






                    }



                    MajorCombobox.ItemsSource = majorNameList;
                    //SystemCombobox.SelectedIndex = 0;
                    DbConnect.MySqlConnection.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }
            else
            {
                Console.WriteLine("CurriculumPlan.Loadmajor检查数据库连接信息！");
            }







        }



        public ObservableCollection<ViewMode.ViewMode.MajorInfoViewMode> MajorInfoList =
            new ObservableCollection<ViewMode.ViewMode.MajorInfoViewMode>()
            {
                //new DepartmentViewMode.DepartmentInfo("1","TEXT","3","4"),
            };


        /// <summary>
        /// 选择学院
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentCombobox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SystemCombobox.SelectedIndex = -1;

            if (DepartmentList != null)
            {
                //记录当前选择的院部ID
                string departmentInfoId = DepartmentList[DepartmentCombobox.SelectedIndex].Id;


                try
                {
                    //加载系部数据到系部列表
                    LoadSystemData(departmentInfoId);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);

                }


            }
        }

        /// <summary>
        /// 选择系部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemCombobox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MajorCombobox.SelectedIndex = -1;

            if (SystemList != null)
            {

                try
                {
                    //记录当前选择的院部ID
                    string systemInfoId = SystemList[SystemCombobox.SelectedIndex].Id;

                    //加载系部数据到系部列表
                    LoadMajor(systemInfoId);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);

                }

            }

        }

        /// <summary>
        /// 选择专业
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MajorCombobox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string majorId = MajorInfoList[MajorCombobox.SelectedIndex].Num;
                LoadMajorStudent(majorId);

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);

            }
        }

        /// <summary>
        /// 加载该专业学生
        /// </summary>
        /// <param name="majorId">专业ID</param>
        private void LoadMajorStudent(string majorId)
        {
            StudentList.Clear();

            try
            {
                DbConnect.MySqlConnection = DbConnect.ConnectionMysql(GlobalVariable.DbConnectInfo);

                DbConnect.MySqlConnection.Open(); //连接数据库

            }
            catch (Exception)
            {
                MessageBox.Show("数据库连接失败!请检查数据库配置");
            }

            if (majorId != null)
            {
                string sql = string.Format("SELECT * FROM student_info WHERE MAJORNUM  = '{0}'", majorId);

                Console.WriteLine(sql);
                MySqlDataReader reader = DbConnect.CarrySqlCmd(sql);


                int index = 0;
                while (reader.Read())
                {
                    index += 1;
                    string studentName = reader.GetString("STUDENT_NAME");
                    string studentId = reader.GetString("NUMBER"); //学号
                    string studentIndex = reader.GetString("ID"); //学生索引号



                    StudentList.Add(
                        new ViewMode.ViewMode.StudentInfoViewMode(index, studentId, studentName, studentIndex));



                }


                StudentListView.ItemsSource = StudentList;



                DbConnect.MySqlConnection.Close();


            }




        }


        public ObservableCollection<ViewMode.ViewMode.StudentInfoViewMode> StudentList =
            new ObservableCollection<ViewMode.ViewMode.StudentInfoViewMode>()
                { };






        /// <summary>
        /// 选中学生后加载课程和分数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StudentListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectCourseList.Clear();
            string majorId;
            try
            {
                majorId = MajorInfoList[MajorCombobox.SelectedIndex].Num;
            }
            catch (Exception)
            {
                majorId = null;
                Console.WriteLine(e.ToString());

            }

            //加载该学生的课程
            LoadSelectCourse(majorId);

            //加载该学生的头像
            LoadImg(StudentList[StudentListView.SelectedIndex].StudentIndex);

            var student = StudentListView.SelectedItem as ViewMode.ViewMode.StudentInfoViewMode;

            StudentName.Text = student.Name;
            StudentId.Text = student.Id;

        }



        /// <summary>
        ///加载已选课程
        /// </summary>
        private void LoadSelectCourse(string majorId)
        {
            
            //清空专业列表
            SelectCourseList.Clear();

            if (GlobalVariable.DbConnectInfo != null)
            {
                try
                {
                    DbConnect.MySqlConnection = DbConnect.ConnectionMysql(GlobalVariable.DbConnectInfo);

                    DbConnect.MySqlConnection.Open(); //连接数据库

                }
                catch (Exception)
                {
                    MessageBox.Show("数据库连接失败!请检查数据库配置");
                }




                if (majorId != null)
                {
                    string sql = string.Format("SELECT * FROM major_course WHERE MAJOR_ID  = '{0}'", majorId);

                    MySqlDataReader reader = DbConnect.CarrySqlCmd(sql);



                    //   SystemList.Clear();
                    int index = 0;
                    while (reader.Read())
                    {
                        index += 1;
                        string courseIndex = reader.GetString("COURSE_INDEX");
                        majorId = reader.GetString("MAJOR_ID");
                        string courseId = reader.GetString("COURSE_ID");
                        string courseName = reader.GetString("COURSE_NAME");
                        int score = Convert.ToInt32(reader.GetString("SCORE"));

                        //TODO 加载课程对应的成绩，索引COURSE_INDEX+学生ID
                        try
                        {

                            string studentCurriculumIndex =
                                EncryptionDecryptionFunction.MD5EncryptionDecryption.MyTextMD5(StudentId.Text + courseId, 8);



                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            
                        }




                        SelectCourseList.Add(new ViewMode.ViewMode.SelectCurriculumViewMode(index, courseIndex, majorId,
                            courseId, courseName, score));



                    }

                    DbConnect.MySqlConnection.Close();

                    CurriculumScoreListView.ItemsSource = SelectCourseList;

                }
                else
                {
                    Console.WriteLine("CurriculumPlan.LoadSelectCourse检查数据库连接信息！");
                }





            }










        }

        public ObservableCollection<ViewMode.ViewMode.SelectCurriculumViewMode> SelectCourseList =
            new ObservableCollection<ViewMode.ViewMode.SelectCurriculumViewMode>()
                { };





        public BitmapImage DefaultBitmapImage = new BitmapImage();

        private void LoadDefaultImage()
        {
            string path = string.Format(@"/Resources/Contact.png");
            Uri pathUri = new Uri(path, UriKind.RelativeOrAbsolute);
            BitmapImage DefaultBitmap = new BitmapImage();
            DefaultBitmap.BeginInit();
            DefaultBitmap.UriSource = pathUri;
            DefaultBitmap.EndInit();
            DefaultBitmapImage = DefaultBitmap;
        }

        /// <summary>
        /// 加载学生头像
        /// </summary>
        private void LoadImg(string studentId)
        {
            GlobalVariable.ImageBytes = null;
            Image.Source = DefaultBitmapImage;

            Console.WriteLine(studentId);

            if (GlobalVariable.DbConnectInfo != null)
            {
                try
                {
                    DbConnect.MySqlConnection = DbConnect.ConnectionMysql(GlobalVariable.DbConnectInfo);

                    DbConnect.MySqlConnection.Open(); //连接数据库

                }
                catch (Exception)
                {
                    MessageBox.Show("数据库连接失败!请检查数据库配置");
                }

                //第二步,查询系部信息
                string sql = string.Format("SELECT * FROM portrait_img WHERE  STUDENT_ID = '{0}'", studentId);

                // MySqlDataReader reader = DbConnect.CarrySqlCmd(sql);

                try
                {
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, DbConnect.MySqlConnection);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    DbConnect.MySqlConnection.Close();
                    byte[] imgBytes = (Byte[]) dataSet.Tables[0].Rows[0]["IMG"];
                    Image.Source = GlobalFunction.ImageClass.BytesToBitmapImage(imgBytes);
                    GlobalVariable.ImageBytes = imgBytes;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }



            }
            else
            {
                MessageBox.Show("数据库连接信息有误");
            }







        }


        private void CurriculumScoreListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var coures = CurriculumScoreListView.SelectedItem as ViewMode.ViewMode.SelectCurriculumViewMode;

            if (coures != null)
            {

                CurriculumId.Text = coures.CourseId;
                CurriculumName.Text = coures.CourseName;
                CurriculumCreditTextBox.Text = coures.Score.ToString();

                string studentCurriculumIndex =
                    EncryptionDecryptionFunction.MD5EncryptionDecryption.MyTextMD5(StudentId.Text + coures.CourseId, 8);
                LoadStudentScore(studentCurriculumIndex);
            }




        }


        /// <summary>
        /// 加载学生成绩，如果没有成绩则创建成绩记录
        /// </summary>
        /// <param name="studentCurriculumIndex">学生的学号和课程ID组成的索引</param>
        private void LoadStudentScore(string studentCurriculumIndex)
        {
            string sql = string.Format("SELECT * FROM score_info WHERE SCORE_INDEX = '{0}'", studentCurriculumIndex);

            //记录不存在则增加记录并显示
            if (DbConnect.CountDataNumber(sql) < 1)
            {
                sql = string.Format("INSERT INTO score_info  VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')", studentCurriculumIndex,StudentId.Text, CurriculumId.Text,"0","0", "0");

                DbConnect.ModifySql(sql);

                LoadSelectCourse(MajorInfoList[MajorCombobox.SelectedIndex].Num);
            }
            else//记录存在则显示记录
            {

            }



        }
    }
}
