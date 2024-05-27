
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

[Route("[Action]")]
[ApiController]
public class Contact : Controller
{
    Context db = new Context();

    [HttpGet]
    public IActionResult ShowAll(){
        return Ok(db.Tbl_Contact.ToList());
    }
    [HttpPost]
    public IActionResult NewContact(MMyContacts index)
    {
        MyContacts NewCon = new MyContacts();
        NewCon.Name = index.Name;
        NewCon.Number = index.Number;
        NewCon.Email = index.Email;
        db.Tbl_Contact.Add(NewCon);
        db.SaveChanges();
        return Ok();
    }
    [HttpDelete]
    public IActionResult DelContact(int id)
    {
        var deletedItem = db.Tbl_Contact.Find(id);
        if(deletedItem==null){
            return NotFound();
        }
        db.Tbl_Contact.Remove(deletedItem);
        db.SaveChanges();
        return Ok();

    }
    [HttpPut]
    public IActionResult UpdateContact(MyContacts news){

        var p = db.Tbl_Contact.Find(news.Id);
        if(p==null){
            return NotFound();
        }

        p.Name=news.Name;
        p.Number=news.Number;
        p.Email=news.Email;
        db.Tbl_Contact.Update(p);
        db.SaveChanges();
        return Ok();
    }

}