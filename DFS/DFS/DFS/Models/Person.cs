using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DFS
{
    public class Person : ObservableCollection<PersonalInfo>
    {
        public string Name { get; set; }

    }

    public class PersonalInfo
    {
        public string FirstName { get; set; }
        public string Location { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
