public interface IContact
{
    public bool AddContact(NewContact newContact);
    public bool DelContact(int id);
    public bool UpContact(Contact newContact);
    public List<FullDataContact> ShowDatas();
    public NewContact ShowContact(int id);
    public List<NewContact> ShowContacts();
    public List<IdContact> ShowId();
}