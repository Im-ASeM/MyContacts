public interface IContact
{
    public bool AddContact(MMyContacts newContact);
    public bool DelContact(int id);
    public bool UpContact(int id , MMyContacts newContact);
    public List<MyContacts> ShowDatas(); //show all data with ID
    public MMyContacts ShowContact(int id); // show one contact
    public List<MMyContacts> ShowContacts(); // show all contacts
}