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
using System.Net;
using System.IO;

namespace HTMLPresser
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 作品一覧を殖やすタグを作る実行可否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutputBookListTag_CanExcute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        
        /// <summary>
        /// 作品一覧を殖やすタグを作る
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutputBookListTag_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            using (StreamReader sr = new StreamReader("Books.txt", Encoding.GetEncoding("utf-8")))
            {
                var text = sr.ReadToEnd();
                text = text.Replace("【_TITLE_】", Textbox_BookName.Text);
                text = text.Replace("【_VALUE_】", Textbox_Value.Text);
                text = text.Replace("【_FILE_】", Textbox_Image.Text);
                text = text.Replace("【_SPEC_】", Textbox_Spec.Text);
                text = text.Replace("【_DATE_】", DatePicker_Date.SelectedDate.Value.ToString("yyyy/MM/dd"));
                text = text.Replace("【_URL_】", Textbox_Link.Text);
                text = text.Replace("【_INFO_】", Textbox_Info.Text);
                Textbox_Output.Text = text;
            }
        }


        /// <summary>
        /// 参照タグをつくる実行可否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refer_CanExcute(object sender, CanExecuteRoutedEventArgs e)
        {
        }

        /// <summary>
        /// 参照タグを作る
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refer_Excuted(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}
