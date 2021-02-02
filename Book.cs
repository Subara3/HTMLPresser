using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.MobileControls;
using System.Windows;

namespace HTMLPresser
{
    public enum Buttoncolors
    {
        white,
        black,
        yellow,
        pink,
        blue,
        green,
    }

    public class ShopInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string shopName;

        public string ShopName
        {
            get { return shopName; }
            set
            {
                shopName = value;
                OnPropertyChanged();
            }
        }

        private string shopURL;

        public string ShopURL
        {
            get { return shopURL; }
            set
            {
                shopURL = value;
                OnPropertyChanged();
            }
        }

        private Buttoncolors button_color;

        public Buttoncolors Button_color
        {
            get { return button_color; }
            set
            {
                button_color = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }


    public class Book : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Book(string name ="")
        {
            BookName = name;
        }

        private string bookName;
        
        public string BookName
        {
            get { return bookName; }
            set
            {
                bookName = value;
                OnPropertyChanged();
            }
        }

        private string valueText;

        public string ValueText
        {
            get { return valueText; }
            set
            {
                valueText = value;
                OnPropertyChanged();
            }
        }

        private string imageFileName;

        public string ImageFileName
        {
            get { return imageFileName; }
            set
            {
                imageFileName = value;
                OnPropertyChanged();
            }
        }

        private string spec;

        public string Spec
        {
            get { return spec; }
            set
            {
                spec = value;
                OnPropertyChanged();
            }
        }

        private DateTime publishdDate;

        public DateTime PublishdDate
        {
            get { return publishdDate; }
            set
            {
                publishdDate = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ShopInfo> shopInfoList = new ObservableCollection <ShopInfo>();

        public ObservableCollection<ShopInfo> ShopInfoList
        {
            get { return shopInfoList; }
            set
            {
                ShopInfoList = value;
                OnPropertyChanged();
            }
        }

        private string info;

        public string Info
        {
            get { return info; }
            set
            {
                info = value;
                OnPropertyChanged();
            }
        }


        private bool isSoldout;

        public bool IsSoldout
        {
            get { return isSoldout; }
            set
            {
                isSoldout = value;
                if(isSoldout == true)
                {
                    //ShopURL = "#kikan";
                }
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
