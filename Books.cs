using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HTMLPresser
{
    public class Books : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Books(string name ="")
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
                    ShopURL = "#kikan";
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
