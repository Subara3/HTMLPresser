using System;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.IO;
using Microsoft.Win32;
using AngleSharp.Html.Parser;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HTMLPresser
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public string FullName => $"Anders {Name}";

        public MainWindow()
        {
            InitializeComponent();
            BookHTML = Properties.Settings.Default.BooksHTMLPath;
            booklist = new ObservableCollection<Book>();
            if(File.Exists(BookHTML))
            {
                AnalyzeBooksTag();
            }
            if(booklist.Count > 0)
            {
                listbox_books.SelectedIndex = 0;
            }
        }
        
        public static DependencyProperty booklistProperty = DependencyProperty.Register(
            "booklist",
            typeof(ObservableCollection<Book>),
            typeof(MainWindow),
            new PropertyMetadata(null));

        public ObservableCollection<Book> booklist
        {
            get { return (ObservableCollection<Book>)GetValue(booklistProperty); }
            set { SetValue(booklistProperty, value); }
        }


        public static DependencyProperty SelectedBookProperty = DependencyProperty.Register(
            "SelectedBook",
            typeof(Book),
            typeof(MainWindow),
            new PropertyMetadata(new Book()));

        public Book SelectedBook
        {
            get { return (Book)GetValue(SelectedBookProperty); }
            set { SetValue(SelectedBookProperty, value); }
        }


        public static DependencyProperty BookHTMLProperty = DependencyProperty.Register(
            "BookHTML",
            typeof(string),
            typeof(MainWindow),
            new PropertyMetadata(""));

        public String BookHTML
        {
            get { return (string)GetValue(BookHTMLProperty); }
            set { SetValue(BookHTMLProperty, value); }
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
            try
            {
                string tmp = "";
                string button_tmp = "";
                using (StreamReader sr = new StreamReader("Book.txt", Encoding.GetEncoding("utf-8")))
                {
                    tmp = sr.ReadToEnd();
                    sr.Close();
                }


                using (StreamReader sr = new StreamReader("ShopButton.txt", Encoding.GetEncoding("utf-8")))
                {
                    button_tmp = sr.ReadToEnd();
                    sr.Close();
                }

                StringBuilder sb = new StringBuilder();
                int count = 0;
                foreach (var book in booklist)
                {
                    string tag = tmp;
                    string btn = button_tmp;
                    StringBuilder bt = new StringBuilder();

                    tag = tag.Replace("【_TITLE_】", book.BookName);
                    tag = tag.Replace("【_VALUE_】", book.ValueText);
                    tag = tag.Replace("【_FILE_】", book.ImageFileName);
                    tag = tag.Replace("【_SPEC_】", book.Spec);
                    tag = tag.Replace("【_DATE_】", book.PublishdDate.ToString("yyyy/MM/dd"));

                    if (book.IsSoldout == true)
                    {
                        btn = btn.Replace("【_URL_】", "#kikan");
                        btn = btn.Replace("【_Button_Option_】", "class=\"btn red\"");
                        btn = btn.Replace("【_BUTTON_TITLE_】", "完売");
                        bt.Append(btn);
                    }
                    else
                    {
                        foreach (var shopinfo in book.ShopInfoList)
                        {
                            btn = btn.Replace("【_URL_】", shopinfo.ShopURL);
                            btn = btn.Replace("【_Button_Option_】", $"class=\"btn {shopinfo.Button_color.ToString()}\"");
                            btn = btn.Replace("【_BUTTON_TITLE_】", shopinfo.ShopName);

                            bt.Append(btn);
                            btn = button_tmp;
                        }
                    }
                    tag = tag.Replace("【_BUTTON_】", bt.ToString());
                    tag = tag.Replace("【_INFO_】", book.Info);
                    sb.Append(tag);
                    count++;
                }
                string output = sb.ToString();

                string target = "";
                using (StreamReader sr = new StreamReader(BookHTML, Encoding.GetEncoding("utf-8")))
                {
                    target = sr.ReadToEnd();
                    sr.Close();
                }

                var start = target.IndexOf("<!--【販売物一覧開始】-->");
                start = start + "<!--【販売物一覧開始】-->".Length + 1;
                var end = target.IndexOf("<!--【販売物一覧終了】-->");

                string html = target.Remove(start, end - start);
                html = html.Insert(start, output);

                System.IO.StreamWriter sw = new System.IO.StreamWriter(
                BookHTML,
                false,
                System.Text.Encoding.GetEncoding("utf-8"));
                //TextBox1.Textの内容を書き込む
                sw.Write(html);
                //閉じる
                sw.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"エラー：{ex.Message}");
            }
        }


        /// <summary>
        /// 参照タグをつくる実行可否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refer_CanExcute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        /// <summary>
        /// 参照タグを作る
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refer_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            if((string)e.Parameter == "Book")
            {
                // ダイアログのインスタンスを生成
                var dialog = new OpenFileDialog();

                // ファイルの種類を設定
                dialog.Filter = "HTMLファイル (*.html)|*.html";

                // ダイアログを表示する
                if (dialog.ShowDialog() == true)
                {
                    // 選択されたファイル名 (ファイルパス) をメッセージボックスに表示
                    BookHTML = dialog.FileName;
                    AnalyzeBooksTag();
                }
            }
            else
            {
                // ダイアログのインスタンスを生成
                var dialog = new OpenFileDialog();

                // ファイルの種類を設定
                dialog.Filter = "画像ファイル (*.png)|*.png|画像ファイル (*.jpg)|*.jpg";

                // ダイアログを表示する
                if (dialog.ShowDialog() == true)
                {
                    // 選択されたファイル名 (ファイルパス) をメッセージボックスに表示
                    SelectedBook.ImageFileName = dialog.SafeFileName;
                }
            }
        }


        /// <summary>
        /// 登録削除実行可否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteBook_CanExcute(object sender, CanExecuteRoutedEventArgs e)
        {
            if(booklist == null)
            {
                return;
            }
            if(booklist.Count < 0)
            {
                return;
            }
            e.CanExecute = true;
        }

        /// <summary>
        /// 登録削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteBook_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            booklist.RemoveAt(listbox_books.SelectedIndex);
        }


        /// <summary>
        /// 登録実行可否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBook_CanExcute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBook_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            booklist.Add(new Book("新しい作品"));
        }



        private void AnalyzeBooksTag()
        {
            using (StreamReader sr = new StreamReader(BookHTML, Encoding.GetEncoding("utf-8")))
            {
                string targetText = sr.ReadToEnd();


                // HTMLParserのインスタンス生成
                var parser = new HtmlParser();
                // htmlをパースする
                var doc = parser.ParseDocument(targetText);

                // classを指定してElementを取得
                var classpList = doc.GetElementsByClassName("book");
                Book book;
                booklist.Clear();
                foreach (var c in classpList)
                {
                    book = new Book();
                    var title = c.GetElementsByTagName("h4");
                    book.BookName = title[0].TextContent;

                    if(book.BookName.Contains("完売"))
                    {
                        book.IsSoldout = true;
                    }

                    var dd = c.GetElementsByTagName("dd");
                    book.ValueText = dd[0].InnerHtml;
                    book.Spec = dd[1].InnerHtml;
                    // 発売日
                    var datestring = dd[2].TextContent;
                    book.PublishdDate = DateTime.Parse(datestring);

                    var image = c.GetElementsByTagName("img");
                    book.ImageFileName = image[0].GetAttribute("src").Substring(4); // img/分

                    var url = c.GetElementsByTagName("a");
                    //book.ShopURL = url[0].GetAttribute("href"); // img/分
                    book.ShopInfoList.Clear();

                    foreach (var shop in url)
                    {
                        ShopInfo shop_info = new ShopInfo();
                        shop_info.ShopURL = shop.GetAttribute("href");
                        shop_info.ShopName = shop.TextContent;
                        string btn_type = shop.GetAttribute("class");
                        switch(btn_type)
                        {
                            default:
                            case "btn black":
                                shop_info.Button_color = Buttoncolors.black;
                                break;
                            case "btn blue":
                                shop_info.Button_color = Buttoncolors.blue;
                                break;
                            case "btn pink":
                                shop_info.Button_color = Buttoncolors.pink;
                                break;
                            case "btn green":
                                shop_info.Button_color = Buttoncolors.green;
                                break;
                            case "btn white":
                                shop_info.Button_color = Buttoncolors.white;
                                break;
                            case "btn yellow":
                                shop_info.Button_color = Buttoncolors.yellow;
                                break;
                        }

                        book.ShopInfoList.Add(shop_info);

                    }

                    var info = c.GetElementsByTagName("p");
                    book.Info = info[0].InnerHtml;
                    booklist.Add(book);

                }


            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.BooksHTMLPath = BookHTML;
            Properties.Settings.Default.Save();
        }
    }
}
