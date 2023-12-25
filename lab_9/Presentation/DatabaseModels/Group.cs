namespace DatabaseModels
{
    public class Group
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public DateTime CreationDate { get; set; }
        

        public Group (string name, DateTime time)
        {
            Name = name;
            CreationDate = time;
        }
            
        public Group ()
        {

        }
    }
}