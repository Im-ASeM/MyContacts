using System.Data;

public class ProcessContact : IContact
{
    Context db = new Context();
    public bool AddContact(NewContact newContact)
    {
        MyContacts added = new MyContacts {Name = newContact.Name , Number = newContact.Number , Email = newContact.Email , CreatTime = DateTime.Now};
        db.Tbl_Contact.Add(added);
        db.SaveChanges();
        return true;
    }

    public bool DelContact(int id)
    {
        MyContacts chose = db.Tbl_Contact.Find(id);
        if(chose == null){
            return false;
        }else{
            db.Tbl_Contact.Remove(chose);
            db.SaveChanges();
            return true;
        }
    }

    public NewContact ShowContact(int id)
    {
        MyContacts chose = db.Tbl_Contact.Find(id);
        if(chose == null){
            return null;
        }else{
            return new NewContact{Name=chose.Name , Number = chose.Number , Email = chose.Email};
        }
    }

    public List<NewContact> ShowContacts()
    {
        List<NewContact> results = new List<NewContact>();
        foreach (MyContacts item in db.Tbl_Contact.ToList())
        {
            NewContact result = new NewContact{ Name=item.Name , Number=item.Number , Email=item.Email};
            results.Add(result);
        }
        return results;
    }

    public bool UpContact(Contact newContact)
    {
        MyContacts contact = db.Tbl_Contact.Find(newContact.Id);
        if(contact == null){
            return false;
        }else{
            contact.Name = newContact.Name;
            contact.Number = newContact.Number;
            contact.Email = newContact.Email;
            db.Tbl_Contact.Update(contact);
            db.SaveChanges();
            return true;
        }
    }
    public List<FullDataContact> ShowDatas(){
        List<FullDataContact> results = new List<FullDataContact>();
        foreach (MyContacts item in db.Tbl_Contact.ToList())
        {
            FullDataContact result = new FullDataContact{
                Id = item.Id,
                Name = item.Name,
                Number = item.Number,
                Email = item.Email,
                CreatTime = item.CreatTime
            };
            results.Add(result);
        }

        return results;
    }
    public List<IdContact> ShowId(){
        List<IdContact> results = new List<IdContact>();
        foreach (MyContacts item in db.Tbl_Contact.ToList()){
            IdContact result = new IdContact{
                Id = item.Id,
                Name = item.Name,
                Number = item.Number
            };
            results.Add(result);
        }
        return results;
    }
}