using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using System.Reactive;
using System.Runtime.CompilerServices;
using System.Threading;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.Reactive.Threading.Tasks;
namespace async
{
    public class MockerEventArgs : EventArgs
    {
        public string Information { get; set; }

        [RegexStringValidator(@"^\d{5}$")]

        public double IdNumber { get; set; }

        public string Name { get; set; }

        public MockerEventArgs(string information = null, double idnumner = 0, string name = "default")
        {
            this.Information = information;

            this.IdNumber = idnumner;

            this.Name = name;
        }
    }

    public class MockerClass : INotifyPropertyChanged
    {
        /// <summary>
        /// My property
        /// </summary>
        private int myProperty;

        private static readonly ObservableCollection<string> names = new ObservableCollection<string> { "fabbe", "babbe", "sven", "ben", "tjen", "ken", "den"};

        private ListCollectionView mockerList;

        public ListCollectionView MockerList
        {
            get { return this.mockerList; }

            set
            {
                if (this.mockerList == value)
                {
                    return;
                }

                this.mockerList = value;

                this.OnMockerChanged();
            }
        }

        public int MyProperty { get { return this.myProperty; } set { if (value.Equals(this.myProperty)) { return; } this.myProperty = value; this.OnMockerChanged(); } }

        public string Message { get { return this.message; } set { if (value.Equals(this.message)) { return; } this.message = value; this.OnMockerChanged(); } }

        public int Id { get { return this.id; } set { if (value.Equals(this.id)) { return; } this.id = value; this.OnMockerChanged(); } }

        public IObserver<string> ObserverObject()
        {
            var observer = Observer.Create<string>(
                o => { Console.WriteLine("name is:{0}", o); },
                e => Console.WriteLine("{0}", e.Message),
                () => Console.WriteLine("Done")
               );

            return observer;
        }

        IEnumerable<string> alphabet = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i" };

        public event EventHandler<MockerEventArgs> mockerChanged;

        private string message;

        private int id;

        protected void OnMockerChanged([CallerMemberName]string propertyName = null)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(string.Format("{0}:{1}:{2}", this.Message, this.Id, propertyName)));
            }
        }

        IObserver<string> observer { get; set; }

        public  MockerClass()
        {

            this.MockerList = new ListCollectionView(names);

            List<int> integerList = new List<int>();
            try
            {
                //Task.Factory.StartNew(() => Console.WriteLine("hejsan"));
               // var method = Task.Factory.FromAsync<string, int>(Funcis.BeginInvoke, Funcis.EndInvoke, "hejsan", Funcis);
                
                var observable =  method.ToObservable().Subscribe( x => integerList.Add(x), () => System.Diagnostics.Debug.WriteLine("Completed"));
               
            }
            catch (ArgumentNullException e)
            {
                System.Diagnostics.Debug.WriteLine("{0}", e.Message);
                throw;
            }

            var info = Observable.FromEventPattern<MockerEventArgs>(this, "mockerChanged").Subscribe(x => System.Diagnostics.Debug.WriteLine(x.EventArgs.IdNumber));
        }

        public static Func<string, int> Funcis = (word) =>
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine(i);
            }; 
            
            return word.Length;
        };

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
