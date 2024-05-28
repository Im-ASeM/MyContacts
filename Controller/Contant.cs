
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

[Route("[Action]")]
[ApiController]
public class Contact : Controller , IContactController
{
    IContact db;
    public Contact(IContact _db)
    {
        db = _db;
    }
    [HttpGet]
    public IActionResult ShowDatas(){
        return Ok(db.ShowDatas());
    }

    [HttpGet]
    public IActionResult ShowContacts(){
        return Ok(db.ShowContacts());
    }
    [HttpGet]
    public IActionResult ShowContact(int id){
        MMyContacts result = db.ShowContact(id);
        return result!=null ? Ok(result) : NotFound();
    }
    [HttpPost]
    public IActionResult AddContact(MMyContacts index)
    {
        bool result = db.AddContact(index);
        return result ? Ok(): NotFound();
    }
    [HttpDelete]
    public IActionResult DelContact(int id)
    {
        bool result = db.DelContact(id);
        return result ? Ok() : NotFound();

    }
    [HttpPut]
    public IActionResult UpContact(int id , MMyContacts newContact){
        bool result = db.UpContact(id,newContact);
        return result ? Ok() : NotFound();
    }

}