
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

[Route("[Action]")]
[ApiController]
public class ContactController : Controller
{
    IContact db;
    public ContactController(IContact _db)
    {
        db = _db;
    }

    [HttpGet]
    public IActionResult ShowDatas(){
        return Ok(db.ShowDatas());
    }
    [HttpGet]
    public IActionResult ShowContactsId(){
        return Ok(db.ShowId());
    }
    [HttpGet]
    public IActionResult ShowContacts(){
        return Ok(db.ShowContacts());
    }
    [HttpGet]
    public IActionResult ShowContact(int id){
        NewContact result = db.ShowContact(id);
        return result!=null ? Ok(result) : NotFound();
    }
    [HttpPost]
    public IActionResult AddContact(NewContact index)
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
    public IActionResult UpContact(Contact newContact){
        bool result = db.UpContact(newContact);
        return result ? Ok() : NotFound();
    }

}