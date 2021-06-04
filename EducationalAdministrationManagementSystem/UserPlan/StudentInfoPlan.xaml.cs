using System;
using System.Collections.Generic;
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

namespace EducationalAdministrationManagementSystem.UserPlan
{
    /// <summary>
    /// StudentInfoPlan.xaml 的交互逻辑
    /// </summary>
    public partial class StudentInfoPlan : UserControl
    {
        public StudentInfoPlan()
        {
            InitializeComponent();
            LoadDataToSexBox();
        }

        /// <summary>
        /// 加载性别数据
        /// </summary>
        public void LoadDataToSexBox()
        {

            List<string> sexList = new List<string>()
            {
                {"男"},
                {"女"},

            };
            GenderCombobox.ItemsSource = sexList;

        }
    }
}
