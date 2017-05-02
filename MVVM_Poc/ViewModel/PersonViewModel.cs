using MVVM_Poc.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Poc.Command;
using System.Windows.Input;

namespace MVVM_Poc.ViewModel
{
    // this view Model class helps propogating change notification to UI 
   public class PersonViewModel : INotifyPropertyChanged
    {
       public PersonViewModel()
       {
           Person = new Person();
           Persons = new ObservableCollection<Person>();
       }

        private Person _person; // this is a Object of our model class, doesn't directly interact with View(UI)
        public Person Person
        {
            get { return _person; }
            set { _person = value; OnPropertyChanged("Person"); }
        }

        private ObservableCollection<Person> _persons;
        public ObservableCollection<Person> Persons
        {
            get { return _persons; }
            set { _persons = value; OnPropertyChanged("Persons"); }
        }
      
       private ICommand _submitCommand;
       public ICommand SubmitCommand
       {
            get
           {
                if(_submitCommand ==null)
                {
                    _submitCommand = new RelayCommand(SubmitExecute,CanSubmitExecute,false);
                    return _submitCommand;
                }
                else
                {
                    return _submitCommand;
                }
           }
       }
       private void SubmitExecute(object parameter)
        {
            Persons.Add(Person);
        }
        
       private bool CanSubmitExecute(object parameter)
       {
           if (String.IsNullOrEmpty(Person.Fname) ||String.IsNullOrEmpty(Person.Lname) )
           {
               return false;
           }
           else
           {
               return true;
           }
       }
       
       public event PropertyChangedEventHandler PropertyChanged;

       private void OnPropertyChanged(string propname)
       {
           PropertyChangedEventHandler ph = PropertyChanged;

           if (ph != null)
           {
               ph(this, new PropertyChangedEventArgs(propname));
           }

       }
    }
}
