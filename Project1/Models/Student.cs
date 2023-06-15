using System.Text.Json.Serialization;
using CW2.Converters;

namespace CW2.Models
{
    public record Student([property:JsonConverter(typeof(IndexConverter))] string Index, string Name, string Surname, string Email,
        string BirthDate, string MothersName, string FathersName, List<StudiesRecord> Studies)
    {
        //Method that checks if 2 Students are equal,
        //it helps in determining whether Student is unique
        public bool Equal(Student other)
        {
            for(int i = 0; i < other.Studies.Count; i++)
            {
                if (this.Name == other.Name && this.Surname == other.Surname
                        && this.Index == other.Index
                        && this.Studies[0].Name == other.Studies[i].Name
                        && this.Studies[0].Mode == other.Studies[i].Mode)
                {
                    return true;
                }
            }
            return false;
        }
        //Method that helps in determining whether to add new studies (StudiesRecord) to Student
        public bool DifferentStudiesSameStudent(Student other)
        {
            return other.Studies[Studies.Count - 1] != this.Studies[0] && 
                this.Name == other.Name && this.Surname == other.Surname
                && this.Index == other.Index;
        }
    }
    

}