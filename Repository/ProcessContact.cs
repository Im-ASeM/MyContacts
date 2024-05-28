class ProcessContact : IContact
{
    Context db = new Context();
    public bool AddContact(MMyContacts newContact)
    {
        MyContacts added = new MyContacts {Name = newContact.Name , Number = newContact.Number , Email = newContact.Email};
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

    public MMyContacts ShowContact(int id)
    {
        MyContacts chose = db.Tbl_Contact.Find(id);
        if(chose == null){
            return null;
        }else{
            return new MMyContacts{Name=chose.Name , Number = chose.Number , Email = chose.Email};
        }
    }

    public List<MMyContacts> ShowContacts()
    {
        List<MMyContacts> results = new List<MMyContacts>();
        foreach (MyContacts item in db.Tbl_Contact.ToList())
        {
            MMyContacts result = new MMyContacts{ Name=item.Name , Number=item.Number , Email=item.Email};
            results.Add(result);
        }
        return results;
    }

    public bool UpContact(int id, MMyContacts newContact)
    {
        MyContacts contact = db.Tbl_Contact.Find(id);
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
    public List<MyContacts> ShowDatas(){
        return db.Tbl_Contact.ToList();
    }
}